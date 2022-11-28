using CatalogoSeries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoSeries.ViewModels
{
    public class SeriesCRUD
    {

        CatalogoseriesContext contenedor = new();

        public IEnumerable<Series> GetSeries()
        {
            return contenedor.Series.OrderBy(x => x.Nombre);
        }

        public void Create(Series s)
        {
            contenedor.Series.Add(s);
            contenedor.SaveChanges();
        }

        public bool Validar (Series s, out List<string> Errores)
        {
            Errores = new();

            if(s != null)
            {
                if (string.IsNullOrWhiteSpace(s.Nombre))
                {
                    Errores.Add("Escriba el título de la serie.");
                }
                if (string.IsNullOrWhiteSpace(s.Genero))
                {
                    Errores.Add("Escriba el género de la serie.");
                }
                if(s.Episodios == 0)
                {
                    Errores.Add("Indique el total de episodios.");
                }
                if (string.IsNullOrWhiteSpace(s.Descripcion))
                {
                    Errores.Add("Coloque la sinopsis.");
                }
                
                if(s.FinDeEmision <= s.InicioDeEmision)
                {
                    Errores.Add("Escriba el fin de eimisión correctamente.");
                }
                if (string.IsNullOrWhiteSpace(s.Imagen))
                {
                    Errores.Add("Escriba la URL correspondiente.");
                }
                if(s.Imagen!= null && !Uri.TryCreate(s.Imagen,UriKind.Absolute, out var uri))
                {
                    Errores.Add("Escribe una URL válida.");
                }

            }

            return Errores.Count == 0;
        }

        public void Delete (Series s)
        {
            contenedor.Series.Remove(s);
            contenedor.SaveChanges();
        }


    }
}
