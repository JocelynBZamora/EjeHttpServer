using MensajesClienteHttp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MensajesClienteHttp.Services
{
    public class DiscoveryService
    {

        public DiscoveryService()
        {
            SolicitarServidores();
            new Thread(RecibirServidor) { IsBackground = true }.Start();
        }
        void SolicitarServidores () 
        {
            //Preguntar que servidores estan conectados cuando ejecute el cliente
            UdpClient client = new() {EnableBroadcast = true };
            client.Send(new byte[] {1},1,new IPEndPoint(System.Net.IPAddress.Broadcast, 7001));
            client.Close();
        }
        UdpClient cliente = new();
        public EventHandler<ServerModel>? ServidorResivido;
        void RecibirServidor () 
        {
            while (true) 
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any,0);
                byte[] buffer = cliente.Receive(ref endPoint);

                ServerModel server = new()
                {
                    NombreServer = Encoding.UTF8.GetString(buffer),
                    IPEndPoint= endPoint,
                    KeepAlive = DateTime.Now
                };
                Application.Current.Dispatcher.Invoke(new Action(() => {
                    ServidorResivido?.Invoke(this, server);
                }
                ));

            }

        }
    }
}
