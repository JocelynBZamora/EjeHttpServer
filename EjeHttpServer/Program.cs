using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

HttpListener server = new();
var ip = Dns.GetHostAddresses(Dns.GetHostName()).Where(x=> x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(x => x.ToString()).FirstOrDefault();
server.Prefixes.Add($"http://{ip}:10000/");
server.Start();


while (true) 
{
    HttpListenerContext context = server.GetContext();
    var nombre = context.Request.QueryString["nombre"];
    string respuesta;
    if (nombre != null)
    {
        Console.WriteLine(nombre + "ha echo una petición");
        respuesta = $"<html><body><h1>Saludos {nombre}</h1></body></html>";
    }
    else 
    {
        respuesta=$"<html><body><h1>Respuesta desde el servidor</h1></body></html>";
    }
    byte[] buffer = Encoding.UTF8.GetBytes(respuesta);
    context.Response.ContentLength64 = buffer.Length;
    var ns = context.Response.OutputStream;
    ns.Write(buffer, 0, buffer.Length);
    context.Response.StatusCode = 200;//ok
    context.Response.Close();
}