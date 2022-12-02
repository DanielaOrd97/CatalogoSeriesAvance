using CatalogoSeries.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;

namespace CatalogoSeries.ViewModels
{
    public class SeriesViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<Series> ListaSeries { get; set; } = new ObservableCollection<Series>();

        public SeriesCRUD? catalogo = new();
        public Series? serie { get; set; }
        public string Error { get; set; } = "";
        public string Vista { get; set; } = "principal";
        public ICommand CancelarCommand { get; set; }
        public ICommand RegresarCommand { get; set; }
        public ICommand VerSerieCommand { get; set; }
        public ICommand VerAgregarCommand { get; set; }
        public ICommand VerEliminarCommand { get; set; }
        public ICommand VerEditarCommand { get; set; }
        public ICommand AgregarSerieCommand { get; set; }
        public ICommand EliminarSerieCommand { get; set; }
        public ICommand EditarSerieCommand { get; set; }
        public ICommand GetSeriesXGeneroCommand { get; set; }
        public ICommand GetSeriesXNumEpisodios { get; set; }


        public int TotalElementos
        {
            get { return ListaSeries.Count(); }
        }

        public SeriesViewModel()
        {
            ActualizarBaseDatos();
            VerAgregarCommand = new RelayCommand(VerAgregar);
            AgregarSerieCommand = new RelayCommand(AgregarSerie);
            EliminarSerieCommand = new RelayCommand(EliminarSerie);
            EditarSerieCommand = new RelayCommand(EditarSerie);
            CancelarCommand = new RelayCommand(Cancelar);
            RegresarCommand = new RelayCommand(Regresar);
            VerSerieCommand = new RelayCommand(VerSerie);
            VerEliminarCommand = new RelayCommand(VerEliminar);
            VerEditarCommand = new RelayCommand(VerEditar);

            GetSeriesXGeneroCommand = new RelayCommand<string>(GetSeriesPorGenero);
        }

       
        private void GetSeriesPorGenero(string s)
        {
            ListaSeries.Clear();

            foreach (var i in catalogo.GetSeriesXGenero(s))
            {
                ListaSeries.Add(i);
            }

            Actualizar();
        }

        private void EditarSerie()
        {

           if(serie != null)
            {
               if(catalogo.Validar(serie,out List<string> Errores))
                {
                    var existe = catalogo.GetSeries(serie);
                    if(existe != null)
                    {
                        existe.Id = serie.Id;
                        existe.Nombre = serie.Nombre;
                        existe.Genero = serie.Genero;
                        existe.Episodios = serie.Episodios;
                        existe.Descripcion = serie.Descripcion;
                        existe.InicioDeEmision = serie.InicioDeEmision;
                        existe.FinDeEmision = serie.FinDeEmision;
                        existe.Imagen = serie.Imagen;

                        catalogo.Update(existe);

                        ActualizarBaseDatos();
                        Vista = "principal";
                        Actualizar();
                    }
                }
                else
                {
                    foreach (var item in Errores)
                    {
                        Error = $"{Error}{item}{Environment.NewLine}";
                        Actualizar();
                    }
                }
                Error = "";
            }
        }


        int indice = 0;
        private void VerEditar()
        {
            if(serie != null && Vista == "principal")
            {
                Vista = "editar";

                Series clon = new()
                {
                    Id = serie.Id,
                    Nombre = serie.Nombre,
                    Genero = serie.Genero,
                    Episodios = serie.Episodios,
                    Descripcion = serie.Descripcion,
                    InicioDeEmision = serie.InicioDeEmision,
                    FinDeEmision = serie.FinDeEmision,
                    Imagen = serie.Imagen,
                };

                indice = ListaSeries.IndexOf(serie);
                serie = clon;

                Actualizar();
            }
        }

        private void EliminarSerie()
        {
            if (serie != null)
            {
                catalogo.Delete(serie);
                ActualizarBaseDatos();
                Vista = "principal";
                Actualizar();
            }
        }

        private void VerEliminar()
        {
            if (serie != null && Vista == "principal")
            {
                Vista = "eliminar";
                Actualizar();
            }
        }

        private void VerSerie()
        {
            if (serie != null && Vista == "principal")
            {
                Vista = "ver";
                Actualizar();
            }
        }

        private void VerAgregar()
        {
            if(Vista == "principal")
            {
                serie = new();
                serie.InicioDeEmision = DateTime.Now;
                serie.Episodios = 0;
                Vista = "agregar";
                Actualizar();
            }
        }

        private void Regresar()
        {
            ActualizarBaseDatos();
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
            if (serie != null)
            {
                if (catalogo.Validar(serie, out List<string> errores))
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
