using Microsoft.AspNetCore.Mvc;
using OLP_API.Data;
using SharedModels;

namespace OLP_API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CategoryController : ControllerBase
	{
		private readonly OLP_DbContext _context;

		public CategoryController(OLP_DbContext context)
		{
			_context = context;
		}


		#region HTTP Request
		[HttpGet]
		public IActionResult Categories()
		{
			try
			{
				List<Category> Categories = _context.Category.ToList();
				return Ok(Categories);
			}
			catch (Exception ex)
			{
				return ex.InnerException != null ? BadRequest(ex.InnerException.Message) : BadRequest(ex.Message);
			}
		}
		#endregion
	}
}
