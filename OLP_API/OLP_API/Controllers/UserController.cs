using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OLP_API.Data;
using OLP_API.Models;
using System.Security.Cryptography;
using System.Text;

namespace OLP_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
	public class UserController : ControllerBase
    {
		private readonly OLP_DbContext _context;

		public UserController(OLP_DbContext context)
		{
			_context = context;
		}

		[HttpPost("LogIn")]
		public ActionResult LogIn(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
            {
                return Content("Email o Password inválido");
            } else
            {
                user.Password = EncryptPassword(user.Password);
                var userBD = _context.User.FirstOrDefaultAsync(x => x.Email == user.Email).Result;
                if (userBD != null && userBD.Password == user.Password)
                {
                    // Usuario autenticado correctamente
                    // Devolver la respuesta correspondiente
                    return Content("Usuario encontrado");
                }
                else
                {
                    return Content("Usuario no encontrado");
                }
            }            
		}

        [HttpPost("SignUp")]
        public ActionResult SignUp(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name) ||
                string.IsNullOrWhiteSpace(user.Email) ||
                string.IsNullOrWhiteSpace(user.Password) ||
                string.IsNullOrWhiteSpace(user.Phone))
            {
                return Content("Campos inválidos");
            }

            user.Password = EncryptPassword(user.Password);
            _context.User.Add(user);
            _context.SaveChanges();
            return Content("Usuario " + user.Name + " guardado con éxito");
        }

        public static string EncryptPassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = SHA256.HashData(passwordBytes);
            StringBuilder sb = new();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
