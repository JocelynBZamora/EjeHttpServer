using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MensajesClienteHttp.Models
{
    public class ServerModel
    {
        public IPEndPoint? IPEndPoint { get; set; }
        public DateTime KeepAlive { get; set; }
        public string NombreServer { get; set; } = "";
    }
}
