using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace ConsoleApp20
{
    internal class Program
    {
        //Definicion de constantes
        const int cantidadOrdenes = 100;
        const int cantidadItems = 5;
        const int cantidadArticulos = 10;

        static void Main(string[] args)
        {
            /*Una empresa maneja órdenes de compra. Cada orden de compra tiene una cabecera:
                    - Fecha
                    - Nombre de proveedor
                    - Número
            
                - Y hasta 5 ítems de detalle con:
                    - Nombre de artículo
                    - Cantidad
                    - Precio unitario
                    - Subtotal
                Hay un arreglo de 10 nombres de artículos disponibles.

                Manejamos hasta 100 órdenes de compra.Debe armar un programa que permita:
                    1.Dar de alta una orden de compra. No admite repetición de números
                    2.Listar un total de importe en orden de compra por proveedor
                    3.Listar un total de importe en orden de compra por artículo
                    4.Salir*/
            {
                //Declaracion de arreglo de articulos
                string[] articuloCodigo = new string[cantidadArticulos];
                string[] articuloNombre = new string[cantidadArticulos];
                //Declaracion de arreglo de Ordenes de Compra 
                string[] cabeceraFecha = new string[cantidadOrdenes];
                int[] cabeceraNumero = new int[cantidadOrdenes];
                string[] cabeceraProveedor = new string[cantidadOrdenes];
                //Declaracion de arreglo de Items de OC
                string[,] itemCodigo = new string[cantidadOrdenes, cantidadItems];
                int[,] itemCantidad = new int[cantidadOrdenes, cantidadItems];
                double[,] itemPrecio = new double[cantidadOrdenes, cantidadItems];
                double[,] itemSubtotal = new double[cantidadOrdenes, cantidadItems];
                string menu = "1.Dar de alta una orden de compra. No admite repetición de números\r\n2.Listar un total de importe en orden de compra por proveedor\r\n3.Listar un total de importe en orden de compra por artículo\r\n4.Salir";
                string opcionElegida = "";
                //Inicio , llama a  carga inicial de artiulos
                cargaInicial(articuloCodigo,articuloNombre);

                do
                {
                    opcionElegida = pedirStringNoVacio("Ingrese alguna de las siguientes opciones:\n" + menu);
                    if (opcionElegida == "1")/*1.Dar de alta una orden de compra. */
                    {
                        ingresarOrdenDeCompra(articuloCodigo, articuloNombre, cabeceraFecha, cabeceraNumero,cabeceraProveedor, itemCodigo, itemCantidad, itemPrecio, itemSubtotal);
                    }
                    else if (opcionElegida == "2")/* 2.Listar un total de importe en orden de compra por proveedor*/
                    {
                        //Pedir un Proveedor
                        string Proveedor = "";
                        Proveedor = pedirStringNoVacio("Ingrese un Proveedor:");
                        Console.Clear();
                        Console.WriteLine(ListarTotalesPorProveedor(Proveedor, cabeceraNumero, cabeceraProveedor, itemSubtotal));
                        Console.ReadKey();
                        Console.Clear();
                    }   
                    else if (opcionElegida == "3") /*3.Listar un total de importe en orden de compra por artículo*/
                    {
                        //Pedir Articulo                       
                        string codigoArticuloSeleccionado = "";
                        int filaArticulo = 0;
                        do
                        {
                            codigoArticuloSeleccionado = pedirStringNoVacio("Ingrese el Articulo a Totalizar\nCodigo\tNombre\n" + armarListadoArticulos(articuloCodigo, articuloNombre));
                            filaArticulo = buscarEnString(articuloCodigo, codigoArticuloSeleccionado);
                            if (filaArticulo == -1)
                            {
                                Console.WriteLine("El codigo no existe");
                            }
                        } while (filaArticulo == -1);
                        Console.Clear();
                        Console.WriteLine(ListarTotalesPorArticulo(codigoArticuloSeleccionado, cabeceraNumero, itemCodigo,itemSubtotal));
                        Console.ReadKey();
                        Console.Clear();


                    }
                } while (opcionElegida != "4");

            }
            /*Procedimiento carga de OC*/
            void ingresarOrdenDeCompra( string[] articuloCodigo, 
                                        string[] articuloNombre, 
                                        string[] cabeceraFecha, 
                                        int[] cabeceraNumero, 
                                        string[] cabeceraProveedor, 
                                        string[,] itemCodigo, 
                                        int[,] itemCantidad, 
                                        double[,] itemPrecio, 
                                        double[,] itemSubtotal)
            {
                //Validacion de arreglo no lleno y obtencion posiciob de la ultima fila vacia
                int filaVacia = buscarEnString(cabeceraFecha, null);
                //Declaracion de variales de input de datos
                int filaOrdenDeCompra = 0;
                int numeroIngresar = -1;
                string fechaIngresar = DateTime.Now.ToString();
                string proveedorIngresar = "";
                if (filaVacia == -1)
                {
                    Console.WriteLine("No hay lugar para mas ordenes de compra");
                }
                else
                {
                    //el arreglo no esta lleno
                    do
                    {
                        //Ingreso de numero de OC valido
                        numeroIngresar = pedirInteger("Ingrese numero de OC", 1, 999999);
                        //Validacion  de OC no existente
                        filaOrdenDeCompra = buscarInteger(cabeceraNumero, numeroIngresar);
                        if (filaOrdenDeCompra != -1)
                        {
                            Console.WriteLine("La orden de compra ya existe");
                        }
                    } while (filaOrdenDeCompra != -1);

                    proveedorIngresar = pedirStringNoVacio("Ingrese proveedor");
                    //mapeo de inputs al arreglo de OC (cabecera)
                    cabeceraFecha[filaVacia] = fechaIngresar;
                    cabeceraProveedor[filaVacia] = proveedorIngresar;
                    cabeceraNumero[filaVacia] = numeroIngresar;
                    // ingreso de datos al arreglo de Items de la OC
                    ingresarItemsDeOrden(filaVacia, articuloCodigo, articuloNombre, itemCodigo, itemPrecio, itemCantidad, itemSubtotal);

                }
            }

            void ingresarItemsDeOrden(int filaVacia, string[] articuloCodigo, string[] articuloNombre, string[,] itemCodigo, double[,] itemPrecio, int[,] itemCantidad, double[,] itemSubtotal)
            {
                string continuarCarga = "";
                int contador = 0;

                do
                {
                    cargarItemDeOrden(filaVacia, contador, articuloCodigo, articuloNombre, itemCodigo, itemPrecio, itemCantidad, itemSubtotal);
                    continuarCarga = pedirSiONo("Desea continuar?");
                    contador = contador + 1;
                } while (contador <= itemCodigo.GetUpperBound(1) && continuarCarga == "S");
            }

            string pedirSiONo(string v)
            {
                string SioNo = "";
                do
                {
                    //Console.WriteLine(v);
                    SioNo = pedirStringNoVacio(v);
                    if (SioNo != "S" && SioNo != "N") {
                        Console.WriteLine("Debe ingresar 'S' o 'N'");
                    }
                } while (SioNo != "S" && SioNo != "N");
                return (SioNo);
            }

            void cargarItemDeOrden(int filaOrdenDeCompra, int filaItem, string[] articuloCodigo, string[] articuloNombre, string[,] itemCodigo, double[,] itemPrecio, int[,] itemCantidad, double[,] itemSubtotal)
            {
                //Declaracion de variables locales para la carga de los items
                string codigoIngresar = "";
                int cantidadIngresar = 0;
                double precioIngresar = 0;
                int filaArticulo = 0;



                do
                {
                    codigoIngresar = pedirStringNoVacio("Ingrese codigo de articulo\nCodigo\tNombre\n" + armarListadoArticulos(articuloCodigo, articuloNombre));
                    filaArticulo = buscarEnString(articuloCodigo, codigoIngresar);
                    if (filaArticulo == -1)
                    {
                        Console.WriteLine("El codigo no existe");
                    }
                } while (filaArticulo == -1);
                
                cantidadIngresar = pedirInteger("Ingrese cantidad", 1, 10000);               
                precioIngresar = pedirDouble("Ingrese precio", 1, 10000);
                // mapeo variables a los arreglos de itmes
                itemCodigo[filaOrdenDeCompra, filaItem] = codigoIngresar;
                itemPrecio[filaOrdenDeCompra, filaItem] = precioIngresar;
                itemCantidad[filaOrdenDeCompra, filaItem] = cantidadIngresar;
                itemSubtotal[filaOrdenDeCompra, filaItem] = precioIngresar * cantidadIngresar;

            }

            double pedirDouble(string v1, int v2, int v3)
            {
                double valor = 0;
                do
                {
                    Console.WriteLine(v1);
                    if (!Double.TryParse(Console.ReadLine(), out valor))
                    {
                        Console.WriteLine("Ingrese un valor numerico");
                    }
                    else if (valor < v2 || valor > v3)
                    {
                        Console.WriteLine("Ingrese un valor entre: " + v1 + " y " + v3);
                    }
                } while (valor == 0);
                return (valor);
            }

            string armarListadoArticulos(string[] articuloCodigo, string[] articuloNombre)
            {
                string retorno = "";
                for (int fila = 0; fila <= articuloCodigo.GetUpperBound(0); fila++) 
                {
                    retorno = retorno + articuloCodigo[fila] + "\t" + articuloNombre[fila] + "\n";
                }
                return (retorno);
            }
            /*Verifica que no exista la OC previamente*/
            int buscarInteger(int[] cabeceraNumero, int numeroIngresar)
            {
                int posicion = -1;
                for (int fila = 0; (fila <= cabeceraNumero.GetUpperBound(0)) && posicion == -1; fila++)
                {
                    if (cabeceraNumero[fila] == numeroIngresar)
                    {
                        posicion = fila;
                    }
                }
                return (posicion);
            }

            int pedirInteger(string v1, int v2, int v3)
            {
                int valor = 0;
                do 
                {
                    Console.WriteLine(v1);
                    if (!Int32.TryParse(Console.ReadLine(), out valor))
                    {
                        Console.WriteLine("Ingrese un valor numerico");
                    }
                    else if (valor < v2 || valor > v3)
                    {
                        Console.WriteLine("Ingrese un valor entre: " + v1 + " y " + v3);
                    }                   
                } while(valor == 0);
                return (valor);
            }

            /*
               Busca un string. Devuelve -1 si no encuentra o sino la fila donde se encuentra
            */
            int buscarEnString(string[] Arreglo, string value)
            {
                int retorno = -1;
                for (int fila = 0; fila<=Arreglo.GetUpperBound(0) && retorno == -1;fila++) 
                {
                    if (Arreglo[fila] == value) {
                        retorno = fila;
                    }
                }
                return (retorno);
            }

            string pedirStringNoVacio(string valor)
            {
                string retorno = "";
                do
                {
                    Console.WriteLine(valor);
                    retorno = Console.ReadLine().ToUpper();
                    if (retorno == "")
                    {
                        Console.WriteLine("Ingrese un valor no vacio");                    
                    }
                } while (retorno == "");
                return(retorno);
            }
            
            void cargaInicial(string[] articuloCodigo, string[] articuloNombre)
            {
                articuloCodigo[0] = "A";
                articuloNombre[0] = "Articulo#1";
                articuloCodigo[1] = "B";
                articuloNombre[1] = "Articulo#2";
                articuloCodigo[2] = "C";
                articuloNombre[2] = "Articulo#3";
                articuloCodigo[3] = "D";
                articuloNombre[3] = "Articulo#4";
                articuloCodigo[4] = "E";
                articuloNombre[4] = "Articulo#5";
                articuloCodigo[5] = "F";
                articuloNombre[5] = "Articulo#6";
                articuloCodigo[6] = "G";
                articuloNombre[6] = "Articulo#7";
                articuloCodigo[7] = "H";
                articuloNombre[7] = "Articulo#8";
                articuloCodigo[8] = "I";
                articuloNombre[8] = "Articulo#9";
                articuloCodigo[9] = "J";
                articuloNombre[9] = "Articulo#10";

            }
            String ListarTotalesPorProveedor(string proveedor, int[] cabeceraNumero, string[] cabeceraProveedor, double[,] itemSubtotal)
            {
                // asigno variable listado
                string Listado = "Proveedor: " + proveedor + "\n";
                //recorrer las OC
                for (int fila = 0; fila <= cabeceraNumero.GetUpperBound(0); fila++)
                {
                    if (cabeceraProveedor[fila] == proveedor)
                    {
                        //si coincide el proveedor, entonces  listado = Listado + OC
                        Listado = Listado + "Nro. Orden de Compra: \t" + cabeceraNumero[fila] + "\t";
                        //recorro la matriz de subtotales y acumulo (suma) en variable el valor
                        double totalporOrden = 0;
                        for (int columna = 0; columna <= itemSubtotal.GetUpperBound(1); columna++)
                        {
                            totalporOrden = totalporOrden + itemSubtotal[fila, columna];
                        }
                        Listado = Listado + "Total: " + totalporOrden + "\n";
                    }
                }
                return (Listado);
            }
            string ListarTotalesPorArticulo(string codigoArticuloSeleccionado, int[] cabeceraNumero, string[,] itemCodigo, double[,] itemSubtotal)
            {
                string ListadoTotalesPorArticulo ="";
                
                ListadoTotalesPorArticulo = ListadoTotalesPorArticulo + "Codigo de Articulo:\t" + codigoArticuloSeleccionado + "\n";
                // recorrer las filas de la matriz de items de codigos

                for (int fila = 0; fila <= itemCodigo.GetUpperBound(0) && itemCodigo[fila,0]!=null; fila++) 
                {
                    ListadoTotalesPorArticulo = ListadoTotalesPorArticulo + "Nro OC:\t" + cabeceraNumero[fila] + "\t";
                    //por cada fila recorrer las columnas en busqueda del codigo de articulo seleccionado
                    double TotalPorArticulo = 0;
                    for (int columna = 0; columna <= itemCodigo.GetUpperBound(1); columna++) 
                    {
                        // compara el codigo de articulo seleecionado con el del arreglo de codigos de los items
                        if (codigoArticuloSeleccionado == itemCodigo[fila, columna])
                        {
                            // si lo encontró, toma la fila y la columna y acumula el subtotal    
                            TotalPorArticulo = TotalPorArticulo + itemSubtotal[fila, columna];
                        }
                    }
                    // imprime el total correspondiente a la OC en el listado
                    ListadoTotalesPorArticulo = ListadoTotalesPorArticulo + TotalPorArticulo + "\n";
                }
                return (ListadoTotalesPorArticulo);
            }
        }


    }
}