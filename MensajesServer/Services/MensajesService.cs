using MensajesServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MensajesServer.Services
{
    public class MensajesService
    {
        HttpListener servidor = new();
        public MensajesService()
        {
            servidor.Prefixes.Add("http://*:7002/mensajitos/");
            servidor.Start();
            new Thread(ResivirPeticiones) { IsBackground = true }.Start();
        }
        public EventHandler<Mensaje> MensajeResivido;
        void ResivirPeticiones()
        {
            while (true) 
            {
                var context = servidor.GetContext();
                if (context != null) 
                {
                    if (context.Request.QueryString["Texto"] != null) //si mandaron un mensaje que querystring
                    {
                        Mensaje mensaje = new() 
                        {
                            Texto = context.Request.QueryString["letra"] ??"",
                            ColorFondo = context.Request.QueryString["colorfondo"] ?? "#fff",
                            ColorLetra = context.Request.QueryString["colorletra"] ?? "#000"
                        };
                        Application.Current.Dispatcher.Invoke(() => 
                        {
                            MensajeResivido?.Invoke(this,mensaje);
                        });

                        context.Response.StatusCode = 200;
                        context.Response.Close();

                    }
                }
            }
        }
    }
}
