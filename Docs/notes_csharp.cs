// CERTIFICATE
var certificate = new x509certificate2(@"d:\proyectos\olp\olp_web\olp_web\data\cert.pfx", "gm3}ee");

builder.webhost.configurekestrel(options =>
{
	options.listen(ipaddress.any, 7210, listenoptions =>
	{
		listenoptions.usehttps(certificate);
	});
});
