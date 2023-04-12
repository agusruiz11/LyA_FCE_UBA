// Type: C#

Console.WriteLine("Bienvenido/a al programa de carga de articulos por nombre y codigo [Ejercicio 12]");

namespace ejercicio12
{
    class Program {
        static void Main(string[] args) {
            const double iva = 0.21;
            const double gastosIndirectos = 100000;

            string codigoArticulo = "";//String no vacio
            string nombreArticulo = "";//String no vacio
            double costoPorKg = 0; // Mayor a 0
            double gramosPorUnidad = 0; // Mayor a 0
            double porcentajeIndirectos = 0; // Entre 0 y 100
            double porcentajeRentabilidad = 0; // Puede ser negativo, rentabilidad 0 (vendo al costo) o mayor a 100 (rentabilidad 200% por ejemplo)

            double precioFinal = 0; //Es calculado por el sistema
            double costoTotal = 0; //Es el costo
            string codigoArticuloMasBarato = "", codigoArticuloMasCaro = "", codigoArticuloMasRentable = "";
            string nombreArticuloMasBarato = "", nombreArticuloMasCaro = "", nombreArticuloMasRentable = "";
            double precioMinimo = 0, precioMaximo = 0, rentabilidadMaxima = 0;
            String continuar = "S";

            do {
                codigoArticulo = "";
                nombreArticulo = "";
                costoPorKg = 0;
                gramosPorUnidad = 0;
                porcentajeIndirectos = 0;
                porcentajeRentabilidad = 0;

                do {
                    Console.WriteLine("Ingrese codigo de articulo");
                    codigoArticulo = Console.ReadLine();
                    if (codigoArticulo == "") {
                        Console.WriteLine("Debe ingresar un codigo");
                    }
                } while (codigoArticulo == "");

                do {
                    Console.WriteLine("Ingrese nombre de articulo");
                    nombreArticulo = Console.ReadLine();
                    if (nombreArticulo == "") {
                        Console.WriteLine("Debe ingresar el nombre del articulo");
                    }
                } while (nombreArticulo == "");
                
                do {
                    Console.WriteLine("Ingrese costo por kg");
                    if (!Double.TryParse(Console.ReadLine(), out costoPorKg)) {
                        Console.WriteLine("Debe ingresar un numero");
                    } else {
                        if (costoPorKg <= 0) {
                            Console.WriteLine("Debe ingresar un numero mayor a 0");
                        }
                    }
                } while (costoPorKg <= 0);

                do {
                    Console.WriteLine("Ingrese gramos por unidad");
                    if (!Double.TryParse(Console.ReadLine(), out gramosPorUnidad)) {
                        Console.WriteLine("Debe ingresar un numero");
                    } else {
                        if (gramosPorUnidad <= 0) {
                            Console.WriteLine("Debe ingresar un numero mayor a 0");
                        }
                    }
                } while (gramosPorUnidad <= 0);
                
                do {
                    Console.WriteLine("Ingrese porcentaje de gastos indirectos");
                    if (!Double.TryParse(Console.ReadLine(), out porcentajeIndirectos)) {
                        Console.WriteLine("Debe ingresar un numero");
                    } else {
                        if (porcentajeIndirectos < 0 || porcentajeIndirectos > 100) {
                            Console.WriteLine("Debe ingresar un numero entre 0 y 100");
                        }
                    }
                } while (porcentajeIndirectos < 0 || porcentajeIndirectos > 100);
                do {
                    Console.WriteLine("Ingrese porcentaje de rentabilidad");
                    if (!Double.TryParse(Console.ReadLine(), out porcentajeRentabilidad)) {
                        Console.WriteLine("Debe ingresar un numero");
                    } else {
                        if (porcentajeRentabilidad < -100 || porcentajeRentabilidad > 100) {
                            Console.WriteLine("Debe ingresar un numero entre -100 y 100");
                        }
                    }
                } while (porcentajeRentabilidad < -100 || porcentajeRentabilidad > 100);

                costoTotal = costoPorKg * gramosPorUnidad / 1000;
                precioFinal = costoTotal + costoTotal * porcentajeIndirectos / 100 + costoTotal * porcentajeRentabilidad / 100 + costoTotal * iva;

                if (precioMinimo == 0 || precioFinal < precioMinimo) {
                    precioMinimo = precioFinal;
                    codigoArticuloMasBarato = codigoArticulo;
                } while (continuar == "???" ); // ojo aca
            } while (continuar == "S");
        }
    }
};