// Hello World! program
using System;

namespace HelloWorld
{
    class Hello {
        static void Main(string[] args)
        {
            int numeroDeFactura = 0, numeroFacturaMenorImporte = 0, numeroFacturaMayorImporte = 0, cantidadFacturas = 0;
            double importeFactura = 0, minimoImporteFactura = 0, maximoImporteFactura = 0, importeTotal = 0, ticketPromedio = 0;
            string continuar = "SI";

            do {
                numeroDeFactura = 0;
                importeFactura = 0;
            
                do {
                    Console.WriteLine("Ingrese el numero de factura");
                    bool datoCorrecto = Int32.TryParse(Console.ReadLine(), out numeroDeFactura);
                    if (!datoCorrecto) {
                        Console.WriteLine("Numero de factura invalido");
                    } else {
                        if (numeroDeFactura <= 0) {
                            Console.WriteLine("El numero de factura debe ser diferente a 0");
                        }
                    }
                } while (numeroDeFactura <= 0);

                do {
                    Console.WriteLine("Ingrese el importe de la factura");
                    bool datoCorrecto = Double.TryParse(Console.ReadLine(), out importeFactura);
                    
                    if (!datoCorrecto) {
                        Console.WriteLine("Ingrese un importe valido");
                    } else if (importeFactura <= 0) {
                        Console.WriteLine("Ingrese un importe mayor a 0");
                    }
                } while (importeFactura <= 0);


                cantidadFacturas = cantidadFacturas + 1;

                importeTotal = importeTotal + importeFactura;

                ticketPromedio = importeTotal / cantidadFacturas;

                if (importeFactura > maximoImporteFactura)
                {
                    maximoImporteFactura = importeFactura;
                    numeroFacturaMayorImporte = numeroDeFactura;
                }
                if (minimoImporteFactura == 0 || importeFactura < minimoImporteFactura)
                {
                    minimoImporteFactura = importeFactura;
                    numeroFacturaMenorImporte = numeroDeFactura;
                }

                do {
                    Console.WriteLine("Desea cargar otra factura?");
                    continuar = Console.ReadLine();
                    if (continuar != "NO" && continuar != "SI")
                    {
                        Console.WriteLine("Ingrese 'SI' o 'NO'");
                    }
                } while (continuar != "NO" && continuar != "SI");

                if (continuar == "NO") {
                    Console.WriteLine("Total: " + importeTotal + " Importe promedio: " + ticketPromedio + " cantidad de facturas: " + cantidadFacturas + " factura maxima: " + maximoImporteFactura + " numero de facutura de mayor importe: " + numeroFacturaMayorImporte + " importe de factura mas pequeña: " + minimoImporteFactura + " numero factura mas pequeña: " + numeroFacturaMenorImporte);

                }
            } while (continuar == "SI");

        }
    }
}