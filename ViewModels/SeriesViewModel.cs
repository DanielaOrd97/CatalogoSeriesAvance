using CatalogoSeries.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;

namespace CatalogoSeries.ViewModels
{
    public class SeriesViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<Series> ListaSeries { get; set; } = new ObservableCollection<Series>();

        public SeriesCRUD? catalogo = new();
        public Series serie { get; set; }
        public string Error { get; set; } = "";
        public string Vista { get; set; } = "principal";
        public ICommand CancelarCommand { get; set; }
        public ICommand RegresarCommand { get; set; }
        public ICommand VerSerieCommand { get; set; }
        public ICommand VerAgregarCommand { get; set; }
        public ICommand VerEliminarCommand { get; set; }
        public ICommand AgregarSerieCommand { get; set; }
        public ICommand EliminarSerieCommand { get; set; }

        public SeriesViewModel()
        {
            ActualizarBaseDatos();
            VerAgregarCommand = new RelayCommand(VerAgregar);
            AgregarSerieCommand = new RelayCommand(AgregarSerie);
            EliminarSerieCommand = new RelayCommand(EliminarSerie);
            CancelarCommand = new RelayCommand(Cancelar);
            RegresarCommand = new RelayCommand(Regresar);
            VerSerieCommand = new RelayCommand(VerSerie);
            VerEliminarCommand = new RelayCommand(VerEliminar);
        }

        private void EliminarSerie()
        {
           if(serie != null)
            {
                catalogo.Delete(serie);
                ActualizarBaseDatos();
                Vista = "principal";
                Actualizar();
            }
        }

        private void VerEliminar()
        {
            if(serie != null)
            {
                Vista = "eliminar";
                Actualizar();
            }
        }

        private void VerSerie()
        {
            if(serie != null)
            {
                Vista = "ver";
                Actualizar();
            }
        }

        private void VerAgregar()
        {
            serie = new();
            serie.InicioDeEmision = DateTime.Now;
            serie.Episodios = 0;
            Vista = "agregar";
            Actualizar();
        }

        private void Regresar()
        {
            Vista = "principal";
            Actualizar();
          //  serie = null;
        }

        private void Cancelar()
        {
            serie = null;
            Vista = "principal";
            Actualizar();
        }

        private void AgregarSerie()
        {
            if(serie != null)
            {
                if(catalogo.Validar(serie, out List<string> errores))
                {
                    catalogo.Create(serie);
                    ActualizarBaseDatos();
                    Vista = "principal";
                    Actualizar();
                }
                else
                {
                    foreach (var item in errores)
                    {
                        Error = $"{Error}{item}{Environment.NewLine}";
                    }
                    Actualizar();
                }


                Error = "";
            }
        }

        public void ActualizarBaseDatos()
        {
            ListaSeries.Clear();

            foreach (var s in catalogo.GetSeries())
            {
                ListaSeries.Add(s);
            }

            Actualizar();
        }



        void Actualizar(string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
