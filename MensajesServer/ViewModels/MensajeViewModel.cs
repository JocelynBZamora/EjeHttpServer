using CommunityToolkit.Mvvm.ComponentModel;
using MensajesServer.Models;
using MensajesServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajesServer.ViewModels
{
    public partial class MensajeViewModel : ObservableObject
    {
        [ObservableProperty]
        public Mensaje mensaje = new();

        MensajesService service = new();
        DiscoveryServise discoveryServise = new();
        public MensajeViewModel()
        {
            service.MensajeResivido += Server_MensajeResivido;
        }

        private void Server_MensajeResivido(object? sender, Mensaje e)
        {

            Mensaje = e;

            
        }
    }
}
