﻿using CatalogoSeries.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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


        public IEnumerable<Series> GetSeriesXGenero(string palabra)
        {

            palabra = string.Concat(Regex.Replace(palabra, @"(?i)[\p{L}-[ña-z]]+", m => m.Value.Normalize(NormalizationForm.FormD))
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));
          
            return contenedor.Series.Where(x => x.Genero.Contains(palabra.ToUpper()));
        }

        public void Create(Series s)
        {
            s.Genero = string.Concat(Regex.Replace(s.Genero, @"(?i)[\p{L}-[ña-z]]+", m => m.Value.Normalize(NormalizationForm.FormD))
              .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));

            contenedor.Series.Add(s);
            contenedor.SaveChanges();
        }

        public bool Validar(Series s, out List<string> Errores)
        {
            Errores = new();

            if (s != null)
            {
                if (string.IsNullOrWhiteSpace(s.Nombre))
                {
                    Errores.Add("Escriba el título de la serie.");
                }
                if (string.IsNullOrWhiteSpace(s.Genero))
                {
                    Errores.Add("Escriba el género de la serie.");
                }
                if (s.Episodios == 0 )
                {
                    Errores.Add("Indique el total de episodios.");
                }
                if (string.IsNullOrWhiteSpace(s.Descripcion))
                {
                    Errores.Add("Coloque la sinopsis.");
                }

                if (s.FinDeEmision <= s.InicioDeEmision)
                {
                    Errores.Add("Escriba el fin de eimisión correctamente.");
                }
                if (string.IsNullOrWhiteSpace(s.Imagen))
                {
                    Errores.Add("Escriba la URL correspondiente.");
                }
                if (s.Imagen != null && !Uri.TryCreate(s.Imagen, UriKind.Absolute, out var uri))
                {
                    Errores.Add("Escribe una URL válida.");
                }

            }

            return Errores.Count == 0;
        }

        public Series GetSeries(Series s)
        {
            Series? existe = contenedor.Series.Find(s.Id);

            if(existe != null)
            {
                return existe;
            }
            else
            {
                return null;
            }
        }



        public void Update(Series s)
        {
            s.Genero = string.Concat(Regex.Replace(s.Genero, @"(?i)[\p{L}-[ña-z]]+", m => m.Value.Normalize(NormalizationForm.FormD))
              .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));

            contenedor.Series.Update(s);
            contenedor.SaveChanges();
        }

        public void Delete(Series s)
        {
            contenedor.Series.Remove(s);
            contenedor.SaveChanges();
        }


    }
}
