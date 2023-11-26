// CERTIFICATE
var certificate = new x509certificate2(@"d:\proyectos\olp\olp_web\olp_web\data\cert.pfx", "gm3}ee");

builder.webhost.configurekestrel(options =>
{
	options.listen(ipaddress.any, 7210, listenoptions =>
	{
		listenoptions.usehttps(certificate);
	});
});

// LOGGER
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
_logger.LogTrace("Start login successful for user " + responseUser.Name);

// IMPROVEMENTS
+ añadir validación del lado cliente antes de hacer peticiones a la API


//TODO PROYECTO


//TODO DOCUMENTACIÓN
+ Diagrama de casos de uso
+ Introducción
+ Portada
+ ¿Un curso puede ser utilizado por muchos usuarios no puede ser viceversa también?
