using Azure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NuGet.Protocol;
using OLP_WEB.Common;
using OLP_WEB.Models;
using SharedModels;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace OLP_WEB.Controllers
{
	public class APIController : Controller
	{
		private readonly HttpClient client_api;
		public APIController()
		{
			client_api = new()
			{
				BaseAddress = new Uri("https://localhost:7210/")
			};
		}

		[HttpPost("LogIn")]
		public async Task<IActionResult> LogIn(User user)
		{
			using HttpResponseMessage result = await client_api.PostAsync("api/User/LogIn", JsonContent.Create(user));
			try
			{
				result.EnsureSuccessStatusCode();
				User responseUser = await result.Content.ReadAsAsync<User>();
				UserController.SaveSesionCookie(responseUser, Response);
				return RedirectToAction(Routes.UserActionProfile, Routes.UserControllerName);
			}
			catch (Exception ex)
			{
				string err_message = await result.Content.ReadAsStringAsync();
				TempData[Constants.TempData_msgErr] = err_message.IsNullOrEmpty() ? ex.Message : err_message;
				return RedirectToAction(Routes.UserActionLogIn, Routes.UserControllerName);
			}
		}

		[HttpPost("SignUp")]
		public async Task<IActionResult> SignUp(UserSignUp userSignUser)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(userSignUser.Password) ||
					string.IsNullOrWhiteSpace(userSignUser.Password2) ||
					userSignUser.Password != userSignUser.Password2)
				{
					throw new Exception("Contraseñas introducidas inválidas. Escríbelas de nuevo");
				}

				User user = new()
				{
					Name = userSignUser.Name,
					Email = userSignUser.Email,
					Password = userSignUser.Password,
					Phone = userSignUser.Phone
				};

				using HttpResponseMessage result = await client_api.PostAsync("api/User/SignUp", JsonContent.Create(user));
				try
				{
					result.EnsureSuccessStatusCode();
					user = await result.Content.ReadAsAsync<User>();
					UserController.SaveSesionCookie(user, Response);
					return RedirectToAction(Routes.UserActionProfile, Routes.UserControllerName);
				}
				catch (Exception ex)
				{
					string err_message = await result.Content.ReadAsStringAsync();
					TempData[Constants.TempData_msgErr] = err_message.IsNullOrEmpty() ? ex.Message : err_message;
					return RedirectToAction(Routes.UserActionSignUp, Routes.UserControllerName);
				}
			}
			catch (Exception ex)
			{
				TempData[Constants.TempData_msgErr] = ex.Message;
				return RedirectToAction(Routes.UserActionSignUp, Routes.UserControllerName);
			}
		}

		[HttpPost("UpdateUser")]
		public async Task<IActionResult> UpdateUser(User user)
		{
			try
			{
				string UserSessionCookie = Request.Cookies[Constants.NameUserSessionCookie] ?? throw new Exception("No se pudo leer la información del usuario");
				User UserCookie = JsonConvert.DeserializeObject<User>(UserSessionCookie) ?? throw new Exception("No se pudo leer la información del usuario");

				if (UserCookie.Name != user.Name) UserCookie.Name = user.Name;
				if (UserCookie.Email != user.Email) UserCookie.Email = user.Email;
				if (UserCookie.Phone != user.Phone) UserCookie.Phone = user.Phone;

				using HttpResponseMessage result = await client_api.PutAsync("api/User/Update", JsonContent.Create(UserCookie));
				try
				{
					result.EnsureSuccessStatusCode();
					User responseUser = await result.Content.ReadAsAsync<User>();
					UserController.SaveSesionCookie(responseUser, Response);
					return RedirectToAction(Routes.UserActionProfile, Routes.UserControllerName);
				}
				catch (HttpRequestException ex)
				{
					string err_message = await result.Content.ReadAsStringAsync();
					TempData[Constants.TempData_msgErr] = err_message.IsNullOrEmpty() ? ex.Message : err_message;
					return RedirectToAction(Routes.UserActionSignUp, Routes.UserControllerName);
				}
			}
			catch (Exception ex)
			{
				TempData[Constants.TempData_msgErr] = ex.Message;
				return RedirectToAction(Routes.UserActionSignUp, Routes.UserControllerName);
			}
		}

		[HttpPost("UpdatePassword")]
		public async Task<IActionResult> UpdatePassword(UserPassword userPassword)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(userPassword.CurrentPassword) ||
					string.IsNullOrWhiteSpace(userPassword.NewPassword) ||
					string.IsNullOrWhiteSpace(userPassword.NewPassword2) ||
					userPassword.NewPassword != userPassword.NewPassword2)
				{
					throw new Exception("Contraseñas introducidas inválidas. Escríbelas de nuevo");
				}

				string UserSessionCookie = Request.Cookies[Constants.NameUserSessionCookie] ?? throw new Exception("No se pudo leer la información del usuario");
				User UserCookie = JsonConvert.DeserializeObject<User>(UserSessionCookie) ?? throw new Exception("No se pudo leer la información del usuario");
				userPassword.Id = UserCookie.Id;

				using HttpResponseMessage result = await client_api.PutAsync("api/User/UpdatePassword", JsonContent.Create(userPassword));
				try
				{
					result.EnsureSuccessStatusCode();
					User responseUser = await result.Content.ReadAsAsync<User>();
					UserController.SaveSesionCookie(responseUser, Response);
					return RedirectToAction(Routes.UserActionProfile, Routes.UserControllerName);
				}
				catch (Exception ex)
				{
					string err_message = await result.Content.ReadAsStringAsync();
					TempData[Constants.TempData_msgErr] = err_message.IsNullOrEmpty() ? ex.Message : err_message;
					return RedirectToAction(Routes.UserActionSignUp, Routes.UserControllerName);
				}
			}
			catch (Exception ex)
			{
				TempData[Constants.TempData_msgErr] = ex.Message;
				return RedirectToAction(Routes.UserActionSignUp, Routes.UserControllerName);
			}
		}

		[HttpPost("DeleteUser")]
		public async Task<IActionResult> DeleteUser()
		{
			try
			{
				string UserSessionCookie = Request.Cookies[Constants.NameUserSessionCookie] ?? throw new Exception("No se pudo leer la información del usuario");
				User User = JsonConvert.DeserializeObject<User>(UserSessionCookie) ?? throw new Exception("No se pudo leer la información del usuario");
				using HttpResponseMessage result = await client_api.DeleteAsync("api/User/DeleteUser/" + User.Id.ToString());
				try
				{
					result.EnsureSuccessStatusCode();
					UserController.CloseSessionCookie(Response);
					return RedirectToAction(Routes.UserActionSignUp, Routes.UserControllerName);
				}
				catch (Exception ex)
				{
					string err_message = await result.Content.ReadAsStringAsync();
					TempData[Constants.TempData_msgErr] = err_message.IsNullOrEmpty() ? BadRequest(ex.Message) : BadRequest(err_message);
					return RedirectToAction(Routes.UserActionProfile, Routes.UserControllerName);
				}
			}
			catch (Exception ex)
			{
				TempData[Constants.TempData_msgErr] = ex.Message;
				return RedirectToAction(Routes.UserActionLogIn, Routes.UserControllerName);
			}
		}
	}
}
