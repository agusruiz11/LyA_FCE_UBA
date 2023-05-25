using System;
using System.Collections.Generic;

namespace NombreDelEspacioDeNombres
{
    public class Pedido
    {
        public string? Modelo { get; set; }
        public string? Patente { get; set; }
        public string? NombreCliente { get; set; }
        public Dictionary<string, Concepto> Conceptos { get; set; }

        public Pedido()
        {
            Conceptos = new Dictionary<string, Concepto>();
        }
    }

    public class Concepto
    {
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public decimal? Precio { get; set; }
    }
}
