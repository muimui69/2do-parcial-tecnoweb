using System;

namespace MSVenta.Inventario.Models
{
    public class AjusteInventario
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
    }
}