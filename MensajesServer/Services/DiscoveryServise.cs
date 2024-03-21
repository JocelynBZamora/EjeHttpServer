using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MensajesServer.Services
{
    public class DiscoveryServise
    {
        UdpClient server = new()
        {
            EnableBroadcast = true,
        };
        byte[] buffer;
        IPEndPoint destino = new IPEndPoint(IPAddress.Broadcast, 7000);
        public DiscoveryServise()
        {
            var usuario = Environment.UserName;
            buffer = Encoding.UTF8.GetBytes(usuario);
        }
        public void Saludar() 
        {
            server.Send(buffer,buffer.Length,destino);
        }
        private void ResivirMensaje() 
        {
            Saludar();
        }
    }
}
