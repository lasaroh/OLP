using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OLP_API.Data;
using OLP_API.Models;

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

		[HttpPost("save")]
        public void Save(User user)
        {
			if (string.IsNullOrWhiteSpace(user.Name))
            {
                // return err
            }
			if (string.IsNullOrWhiteSpace(user.Email))
			{
				// return err
			}
			if (string.IsNullOrWhiteSpace(user.Password)) {
				// return err
			}
			if (string.IsNullOrWhiteSpace(user.Phone))
			{
				// return err
			}

			_context.User.Add(user);
			_context.SaveChanges();
		}

		[HttpGet("save")]
		public ActionResult Save()
		{
			return Content("<p>Hola</p>");
		}
	}
}
