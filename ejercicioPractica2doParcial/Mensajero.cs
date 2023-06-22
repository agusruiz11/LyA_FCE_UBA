using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Mensajeria
{
    class Mensajero
    {
        private string nombreCompleto;

        public string NombreCompleto
        {
            set
            {
                nombreCompleto = value.ToUpper();
            }

            get
            {
                return nombreCompleto;
            }
        }

        public string GetNombre()
        {
            return nombreCompleto;
        }
        public void Saludar()
        {
            Console.WriteLine("Hola, soy un mensajero. Me llamo " + nombreCompleto + ".");
        }
        public void EntregarPedido(Pedido unPedido, string contacto)
        {
            Saludar();
            Console.WriteLine("Voy a entregar este pedido:\n"
                + unPedido.Detallar() + ".");
            Console.WriteLine( "Si surge un inconveniente llamaré al: " + contacto);
        }
    }
}