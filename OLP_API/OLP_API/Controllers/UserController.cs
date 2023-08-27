using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OLP_API.Data;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
using Microsoft.AspNetCore.JsonPatch;
using SharedModels;

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

        #region HTTP Request

        [HttpPost("LogIn")]
        public IActionResult LogIn(User user)
        {
            try
            {
                if (user == null || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
                {
                    return BadRequest("Email o contraseña introducida inválida");
                }
                user.Password = EncryptPassword(user.Password);
                var userBD = _context.User.FirstOrDefaultAsync(x => x.Email == user.Email &&
                                                                    x.Password == user.Password).Result;
                if (userBD == null) return BadRequest("No se encontró el usuario. Cambie la contraseña o el email.");
                return Ok(userBD);
            }
            catch (Exception ex)
            {
                return ex.InnerException != null ? BadRequest(ex.InnerException.Message) : BadRequest(ex.Message);
            }
        }

        [HttpPost("SignUp")]
        public IActionResult SignUp(User user)
        {
            try
            {
                string err = "";
                if (!ValidateUserFields(user, ref err)) return BadRequest(err);
                user.Password = EncryptPassword(user.Password);
                _context.User.Add(user);
                _context.SaveChanges();
                var userBD = _context.User.FirstOrDefaultAsync(x => x.Email == user.Email &&
                                                                    x.Password == user.Password).Result;
                if (userBD == null) return BadRequest("No se encontró el usuario. Cambie la contraseña o el email.");
                return Ok(userBD);
            }
            catch (Exception ex)
            {
                return ex.InnerException != null ? BadRequest(ex.InnerException.Message) : BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(User user)
        {
            try
            {
                var userDB = _context.User.FirstOrDefaultAsync(x => x.Id == user.Id).Result;
                if (userDB == null) return NotFound("Usuario no encontrado en base de datos");

                userDB.Name = UpdateField(userDB.Name, user.Name);
                userDB.Email = UpdateField(userDB.Email, user.Email);
                userDB.Phone = UpdateField(userDB.Phone, user.Phone);

                _context.User.Update(userDB);
                _context.SaveChanges();
                return Ok(userDB);
            } catch (Exception ex)
            {
                return ex.InnerException != null ? BadRequest(ex.InnerException.Message) : BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdatePassword")]
        public IActionResult UpdatePassword(UserPassword userPassword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userPassword.NewPassword)) return BadRequest("Contraseña introducida inválida");

				var userDB = _context.User.FirstOrDefaultAsync(x => x.Id == userPassword.Id).Result;
                if (userDB == null) return NotFound("Usuario no encontrado en base de datos");

                if (userDB.Password != EncryptPassword(userPassword.CurrentPassword)) return BadRequest("Contraseña actual introducida incorrecta");

                string newPassword = EncryptPassword(userPassword.NewPassword);
				if (userDB.Password != newPassword)
                {
                    userDB.Password = newPassword;
                    _context.User.Update(userDB);
                    _context.SaveChanges();
                }
                return Ok(userDB);
            } catch (Exception ex)
            {
                return ex.InnerException != null ? BadRequest(ex.InnerException.Message) : BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var userBD = _context.User.FirstOrDefaultAsync(x => x.Id == id).Result;
                if (userBD == null) return BadRequest("Error al eliminar el usuario. No se ha encontrado");
                _context.User.Remove(userBD);
                _context.SaveChanges();
                return Ok();
            } catch (Exception ex)
            {
                return ex.InnerException != null ? BadRequest(ex.InnerException.Message) : BadRequest(ex.Message);
            }
        }
        #endregion
        private static bool ValidateUserFields(User user, ref string err)
        {
            #region NAME
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                err = "Nombre introducido inválido";
                return false;
            }
			#endregion
			#region EMAIL
			/* - Starts with one or more letters, digits, periods, underscores, percentage signs, or plus/minus signs
			 * - Followed by an "@" symbol
			 * - Followed by one or more letters, digits, periods, or hyphens
			 * - Followed by a period
			 * - Ends with two or more letters
             */
			string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (string.IsNullOrWhiteSpace(user.Email) || !Regex.IsMatch(user.Email, emailPattern))
            {
                err = "Email introducido inválido";
                return false;
            }
            #endregion
            #region PASSWORD
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                err = "Contraseña introducida inválida";
                return false;
            }
            #endregion
            #region PHONE
            if (string.IsNullOrWhiteSpace(user.Phone) || !int.TryParse(user.Phone, out _))
            {
                err = "Teléfono introducido inválido";
                return false;
            }
            #endregion
            return true;
        }
        private static string UpdateField(string currentValue, string newValue)
        {
            if (!newValue.IsNullOrEmpty() && currentValue != newValue)
            {
                currentValue = newValue;
            }
            return currentValue;
        }
        private static string EncryptPassword(string? password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password), "Contraseña introducida inválida");

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
