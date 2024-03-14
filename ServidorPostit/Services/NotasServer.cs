using ServidorPostit.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace ServidorPostit.Services
{
    public class NotasServer
    {
        HttpListener server = new();
        public NotasServer()
        {
            server.Prefixes.Add("http://*:12345/notas/");
        }
        public void Iniciar()
        {
            if (!server.IsListening)
            {
                server.Start();
                new Thread(Escuchar)
                {
                    IsBackground = true
                }.Start();

            }
        }
        public event EventHandler<Notas>? NotaResivida;
        void Escuchar()
        {
            while (true)
            {
                var context = server.GetContext();
                var pagina = File.ReadAllText("Assents/index.html");
                var bufferPagina = Encoding.UTF8.GetBytes(pagina);
                if (context.Request.Url != null)
                {

                    if (context.Request.Url.LocalPath == "/notas/")
                    {
                        context.Response.ContentLength64 = bufferPagina.Length;
                        context.Response.OutputStream.Write(bufferPagina, 0, bufferPagina.Length);
                        
                    }
                    else if (context.Request.HttpMethod == "POST" && context.Request.Url.LocalPath == "/notas/crear") //manda los datos al formulario
                    {
                        byte[] buffer = new byte[context.Request.ContentLength64];
                        context.Request.InputStream.Read(buffer, 0, buffer.Length);
                        string datos = Encoding.UTF8.GetString(buffer);
                        context.Response.StatusCode = 200;
                        var diccionario = HttpUtility.ParseQueryString(datos);

                        Notas nota = new Notas()
                        {
                            Titulo = diccionario["titulo"] ?? "",
                            Contenido = diccionario["contenido"] ?? "",
                            X = double.Parse(diccionario["x"] ?? ""),
                            Y = double.Parse(diccionario["y"] ?? ""),
                            Remitente = Dns.GetHostEntry(IPAddress.Parse(context.Request.UserHostAddress)).HostName
                        };
                        Application.Current.Dispatcher.Invoke(() => 
                        {
                            NotaResivida?.Invoke(this,nota);

                        });
                        context.Response.StatusCode = 200;
                        context.Response.Close();
                    }

                    else
                    {
                        context.Response.StatusCode = 404;
                        context.Response.Close();
                    }
                }
            }
        }

        public void Detener()
        {
            server.Stop();
        }
    }
}
