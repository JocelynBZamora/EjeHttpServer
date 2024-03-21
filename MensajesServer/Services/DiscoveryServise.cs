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
            Saludar();//Cuando saludamos en la red
            new Thread(ResivirSaludo) { IsBackground = true }.Start();//esperar aque nos saluden
            new Thread(StillAlive) { IsBackground = true }.Start();//para informar que seguimos vivos 
        }
        public void Saludar() 
        {
            server.Send(buffer,buffer.Length,destino);
        }

        private void StillAlive() 
        {
            while (true) 
            {
                Thread.Sleep(TimeSpan.FromSeconds(30));
                Saludar();
            }
        }

        private void ResivirSaludo() //Respondo el saludo cuando me saludan
        {
            UdpClient udp2 = new UdpClient(7001);
            while (true) 
            {
                IPEndPoint remorto = new(IPAddress.Any, 0);//ve quien lo envio IP
                byte[] buffer = udp2.Receive(ref remorto); 

                server.Send(buffer,buffer.Length,remorto);
            }
        }
    }
}
