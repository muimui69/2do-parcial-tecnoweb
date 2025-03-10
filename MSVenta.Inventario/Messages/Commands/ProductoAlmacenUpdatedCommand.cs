﻿using Aforo255.Cross.Event.Src.Commands;

namespace MSVenta.Inventario.Messages.Commands
{
    public class ProductoAlmacenUpdatedCommand : Command
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public int AlmacenId { get; set; }
        public int Stock { get; set; }

        public ProductoAlmacenUpdatedCommand(int id, int productoId, int almacenId,int stock)
        {
            Id = id;
            ProductoId = productoId;
            AlmacenId = almacenId;
            Stock = stock;
        }
    }
}
