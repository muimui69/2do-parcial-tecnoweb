﻿using Aforo255.Cross.Event.Src.Commands;

namespace MSVenta.Inventario.Messages.Commands
{
    public class CategoriaUpdatedCommand : Command
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public CategoriaUpdatedCommand(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}
