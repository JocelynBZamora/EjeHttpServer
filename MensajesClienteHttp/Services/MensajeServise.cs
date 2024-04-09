using MensajesClienteHttp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MensajesClienteHttp.Services
{
    public class MensajeServise
    {
        public async void EnviarMendaje(ServerModel server,MensajesDTO mensaje) 
        {
            var url = $"http://{server.IPEndPoint?.Address.ToString()}:7002/mensajitos/?texto={mensaje.Texto}&colorletra={mensaje.ColorLetra}&colorgondo={mensaje.ColorFondo}";
            HttpClient client = new();
           var resultado = await client.GetStringAsync(url);

        }
    }
}
