using System.Collections.Generic;
using System;

public class AjusteInventarioDTO
{
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public DateTime Fecha { get; set; }
    public string Tipo { get; set; } = null!;
    public string? Descripcion { get; set; }

    public List<DetalleAjusteInventarioDTO> DetallesAjuste { get; set; } = new List<DetalleAjusteInventarioDTO>();
}

public class DetalleAjusteInventarioDTO
{
    public int IdProductoAlmacen { get; set; }
    public int Cantidad { get; set; }

    public string? NombreProducto { get; set; }  // Opcional, si quieres incluir el nombre del producto
    public string? NombreAlmacen { get; set; }   
}
