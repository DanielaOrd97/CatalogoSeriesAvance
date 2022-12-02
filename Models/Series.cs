using System;
using System.Collections.Generic;

namespace CatalogoSeries.Models
{
    public partial class Series
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public int Episodios { get; set; }
        public string Descripcion { get; set; } = null!;
        public DateTime InicioDeEmision { get; set; } 
        public DateTime? FinDeEmision { get; set; }
        public string Imagen { get; set; } = null!;
    }
}
