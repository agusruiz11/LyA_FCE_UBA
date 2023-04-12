using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLA20190314
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Pide el nombre 
             * y lo muestra
             */
            string Mensaje;
            const int NotaMax = 10;
            double Nota1;
            double Nota2;
            double Promedio;

            //Pedir el nombre
            //Console.WriteLine("Ingrese su nombre");
            Console.WriteLine("Ingrese su nombre de pila");
            Mensaje = Console.ReadLine();
            Console.WriteLine("Hola " + Mensaje);
            Console.WriteLine("La nota máxima es " + NotaMax);

            Console.WriteLine("Ingrese su primer nota:");

            if ( !double.TryParse(Console.ReadLine(), out Nota1))
            {
                Nota1 = 0;
            }

            Console.WriteLine("Ingrese su segunda nota");

            if (!double.TryParse(Console.ReadLine(), out Nota2))
            {
                Nota2 = 0;
            }

            Promedio = (Nota1 + Nota2) / 2;
            Console.WriteLine("El promedio es: " + Promedio);
            if (Promedio >= 7)
            {
                Console.WriteLine("Promociona");
            } else
            {
                if (Promedio >= 4)
                {
                    Console.WriteLine("Regulariza");
                } else
                {
                    Console.WriteLine("Insuficiente");
                }
            }
            Console.ReadKey();








        }
    }
}
