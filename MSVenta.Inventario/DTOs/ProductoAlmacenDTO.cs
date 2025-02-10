using System.Collections.Generic;

public class ProductoAlmacenDTO
{
    public int Id { get; set; }
    public string ProductoNombre { get; set; }
    public string AlmacenNombre { get; set; }
    public int Stock { get; set; }
    public List<DetalleAjusteProductoAlmacenDTO> DetallesAjuste { get; set; }
}

public class DetalleAjusteProductoAlmacenDTO
{
    public int IdDetalleAjuste { get; set; }
    public int Cantidad { get; set; }

}