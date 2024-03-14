using ServidorPostit.Models;
using ServidorPostit.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorPostit.ViewModels
{

    public class NotasViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Notas> Notas { get; set; }
        NotasServer server = new NotasServer();
        public NotasViewModel()
        {
            server.NotaResivida += Server_NotaResivida;
            server.Iniciar();
        }
        Random r = new Random();
        private void Server_NotaResivida(object? seander, Notas e) 
        {
            e.Angulo = r.Next(-5, 6);
            Notas.Add(e);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
