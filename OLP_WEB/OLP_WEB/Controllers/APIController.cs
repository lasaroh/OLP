using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OLP_WEB.Models;
using System.Net;
using System.Text;

namespace OLP_WEB.Controllers
{
    public class APIController : Controller
    {
        //todo ver que hacer con los logs si registrarlos en BD
        private readonly ILogger<APIController> _logger;
        private readonly HttpClient client_api;

        public APIController(ILogger<APIController> logger)
        {
            _logger = logger;
            client_api = new()
            {
                BaseAddress = new Uri("https://localhost:7210/")
            };
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client_api.PostAsync("api/User/LogIn", content);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return Ok(responseContent);
                case HttpStatusCode.NotFound:
                    return NotFound("Usuario no encontrado");
                default:
                    return BadRequest("Error: " + response.StatusCode.ToString() + ". Al buscar el usuario");
            }
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client_api.PostAsync("api/User/SignUp", content);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return Ok(responseContent);
                    //var resultado = JsonConvert.DeserializeObject<Resultado>(responseContent);
                    /**
                     * public class Resultado
                     * {
                     * public bool Exito { get; set; }
                     * public string Mensaje { get; set; }
                     * }
                     */
                case HttpStatusCode.NotFound:
                    return NotFound("Usuario no encontrado");
                default:
                    return BadRequest("Error: " + response.StatusCode.ToString() + ". Al buscar el usuario");
            }
        }
    }
}
