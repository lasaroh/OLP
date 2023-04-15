// CERTIFICATE
builder.Services.AddControllersWithViews();

var certificate = new X509Certificate2("D:\\Proyectos\\OLP\\OLP_WEB\\OLP_WEB\\Data\\cert.pfx", "gM3}eE");

builder.WebHost.ConfigureKestrel(options =>
{
	options.Listen(IPAddress.Any, 7029, listenOptions =>
	{
		listenOptions.UseHttps(certificate);
	});
});


// CORS
app.UseCors(builder =>
{
	builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});


// Operador de coalescencia nula `??`
builder.WebHost.UseUrls(Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? "https://localhost:443");




















using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddAuthorization();
// Operador de coalescencia nula `??`
builder.WebHost.UseUrls(Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? "https://localhost:443");

// Configure services
builder.Services.AddControllersWithViews()
	.AddMvcOptions(options => options.EnableEndpointRouting = false);
builder.Services.AddRazorPages();
builder.Services.AddSingleton<ITempDataDictionaryFactory, TempDataDictionaryFactory>();

var app = builder.Build();


// Map request to API controllers to a different port
app.Use(async (context, next) =>
{
	if (context.Request.Path.StartsWithSegments("/api"))
	{
		context.Request.Host = new HostString("localhost", 7210);
		var targetUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
		var request = new HttpRequestMessage(HttpMethod.Post, targetUrl);

		// Copy headers initial request to new request
		foreach (var header in context.Request.Headers)
		{
			request.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
		}

		// Send request to api and get response
		using var client = new HttpClient();
		var response = await client.SendAsync(request);
		context.Response.StatusCode = (int)response.StatusCode;

		// Copy headers response headers to original response
		foreach (var header in response.Headers)
		{
			context.Response.Headers[header.Key] = header.Value.ToArray();
		}

		context.Response.ContentType = response.Content.Headers.ContentType?.ToString();
		using var stream = await response.Content.ReadAsStreamAsync();
		await stream.CopyToAsync(context.Response.Body);
	}
	else
	{
		await next.Invoke(context);
	}
});

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();





{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*",
    "CertConfig": {
        "CertPath": "D:\\Proyectos\\OLP\\OLP_WEB\\OLP_WEB\\Data\\cert.pfx",
        "CertPassword": "gM3}eE"
    },
    "https_port": 443,
    "ASPNETCORE_URLS": "https://localhost:443"
}




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();







// Map request to API controllers to a different port
app.Use(async (context, next) =>
{
	if (context.Request.Path.StartsWithSegments("/api"))
	{
		context.Request.Host = new HostString("localhost", 7210);
		var targetUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
		var request = new HttpRequestMessage(HttpMethod.Post, targetUrl);

		// Copy headers initial request to new request
		foreach (var header in context.Request.Headers)
		{
			request.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
		}

		// Send request to api and get response
		using var client = new HttpClient();
		var response = await client.SendAsync(request);
		context.Response.StatusCode = (int)response.StatusCode;

		// Copy headers response headers to original response
		foreach (var header in response.Headers)
		{
			context.Response.Headers[header.Key] = header.Value.ToArray();
		}

		context.Response.ContentType = response.Content.Headers.ContentType?.ToString();
		using var stream = await response.Content.ReadAsStreamAsync();
		await stream.CopyToAsync(context.Response.Body);
	}
	else
	{
		await next.Invoke(context);
	}
});


security.enterprise_roots.enabled




//var certificate = new X509Certificate2(@"D:\Proyectos\OLP\OLP_WEB\OLP_WEB\Data\cert.pfx", "gM3}eE");

//builder.WebHost.ConfigureKestrel(options =>
//{
//	options.Listen(IPAddress.Any, 7210, listenOptions =>
//	{
//		listenOptions.UseHttps(certificate);
//	});
//});
