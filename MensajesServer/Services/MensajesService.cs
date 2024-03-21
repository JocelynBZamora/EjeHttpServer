using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MensajesServer.Services
{
    public class MensajesService
    {
        HttpListener servidor = new();
        public MensajesService()
        {
            servidor.Prefixes.Add("http://*:7002/mensajitos/");
            servidor.Start();
        }
        void ResivirPeticiones()
        {
            while (true) 
            {
                var context = servidor.GetContext();
                if (context != null) 
                {
                    if (context.Request.QueryString["mensaje"] != null) //si mandaron un mensaje que querystring
                    { }
                }
            }
        }
    }
}
