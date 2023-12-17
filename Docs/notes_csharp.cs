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



Credenciales OLP_DB
user:	sa
password:	fP5_bR7>eI


launchSettings.json – Permite modificar puertos

// NOTES
TODO: para tareas pendientes
HACK: para soluciones temporal o inseguras
UNDONE: para tareas que se han cancelado o que no se realizarán
BUG: para errores o problemas conocidos


Utilizar NCHAR(utiliza 2 bytes) en lugar VARCHAR(utiliza 1 byte)  para poder almacenar multiples idiomas



COMMIT
+ ahora el navbar es responsivo
+ ahora el video es responsivo
+ en el listado de cursos se añade un div extra a la última fila para mantener la estructura
+ Cuando se actualizan los campos de un usuario existente también se validan