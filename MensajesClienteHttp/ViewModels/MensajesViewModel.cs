using CommunityToolkit.Mvvm.ComponentModel;
using MensajesClienteHttp.Models;
using MensajesClienteHttp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajesClienteHttp.ViewModels
{
    public partial class MensajesViewModel:ObservableObject
    {
        MensajeServise mensajeServise = new();
        DiscoveryService discoveryService = new();
        public ObservableCollection<ServerModel> servers { get; set; } = new();
        public MensajesViewModel()
        {
            discoveryService.ServidorResivido += DiscoveryServise_ServidorResivido; 
            
        }

        private void DiscoveryServise_ServidorResivido(object? sender, ServerModel e)
        {
            servers.Add(e);
        }
    }
}
