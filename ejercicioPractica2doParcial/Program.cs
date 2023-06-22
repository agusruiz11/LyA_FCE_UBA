using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Mensajeria
{
    class Program
    {
        static void Main (string[] args)
        {
            Pedido miPedido = new Pedido();
            Mensajero miMensajero = new Mensajero();
            miMensajero.NombreCompleto = "Juan Perez";
            // miMensajero.Saludar();

            miPedido.DirecEntrega = "Av. Corrientes 5555 5A" ;
            miPedido.DescArticulo = "Juego de ollas y sartenes" ;
            miMensajero.EntregarPedido(miPedido, "5555-5555");

            // Console.WriteLine(miMensajero.NombreCompleto);
            // Console.WriteLine(miMensajero.GetNombre());
            Console.WriteLine("Pulse una tecla para terminar.");
            Console.ReadKey();
        }
    }
}