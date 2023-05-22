// See https://aka.ms/new-console-template for more information

const int columnaCodigoProducto = 0;
const int columnaNombreProducto = 1;
const int columnaCodigoCliente = 0;
const int columnaNombreCliente = 1;
const int cantidadLineasFactura = 1000;

string[,] clientes = new string[100,2];
string[,] productos = new string[10,2];
double[,] analisis = new double[100,10];

int[] lineaFacturaNumeroFactura = new int[cantidadLineasFactura];
string[] lineaFacturaCodigoArticulo = new string[cantidadLineasFactura];
string[] lineaFacturaCodigoCliente = new string[cantidadLineasFactura];
int[] lineaFacturaCantidad = new int[cantidadLineasFactura];
double[] lineaFacturaPrecio = new double[cantidadLineasFactura];
double[] lineaFacturaImporte = new double[cantidadLineasFactura];

int filaLineasFactura = 0;

productos[0,columnaCodigoProducto] = "MZ";
productos[0,columnaNombreProducto] = "MANZANA";
productos[1,columnaCodigoProducto] = "NJ";
productos[1,columnaNombreProducto] = "NARANJA";
productos[2,columnaCodigoProducto] = "PAL";
productos[2,columnaNombreProducto] = "PALTA";

clientes[0,columnaCodigoCliente] = "36";
clientes[0,columnaNombreCliente] = "JOSE L. SANCHEZ";
clientes[1,columnaCodigoCliente] = "888";
clientes[1,columnaNombreCliente] = "JUAN TOPO";
clientes[2,columnaCodigoCliente] = "111";
clientes[2,columnaNombreCliente] = "ATILA EL HUNO";

string opcionMenu = "";

do {
    string[] opcionesMenu = new string[] {"1", "2", "3", "4"};
    opcionMenu = pedirYValidarOpcion("Ingrese 1 para ingresar venta, 2 para ver por cliente, 3 para analisis, 4 para finalizar.", opcionesMenu);
    if (opcionMenu=="1") {
        string codigoClienteAIngresar = "";
        string codigoProductoAIngresar = "";
        int numeroFacturaIngresar = 0;
        int cantidadIngresar = 0;
        double precioIngresar = 0;
        bool existeLineaDeFactura = false;
        string listadoClientes = "Codigo\tNombre\n";
        string listadoProductos = "Codigo\tNombre\n";
        listadoClientes = listadoClientes + armarListadoDeMatriz(columnaCodigoCliente, columnaNombreCliente, clientes);
        listadoProductos = listadoProductos + armarListadoDeMatriz(columnaCodigoProducto, columnaNombreProducto, productos);
        codigoClienteAIngresar = pedirYValidarOpcion("Ingrese un cliente del siguiente listado:\n" + listadoClientes, obtenerOpcionesAPartirDeMatriz(columnaCodigoCliente, clientes));
        codigoProductoAIngresar = pedirYValidarOpcion("Ingrese un producto del siguiente listado:\n" + listadoProductos, obtenerOpcionesAPartirDeMatriz(columnaCodigoProducto, productos));
        numeroFacturaIngresar = pedirInteger("Ingrese numero de factura", 1, 1000000);
        for (int filaLineaFactura = 0; filaLineaFactura <= lineaFacturaNumeroFactura.GetUpperBound(0); filaLineaFactura++) {
            if (lineaFacturaNumeroFactura[filaLineaFactura]==numeroFacturaIngresar&&lineaFacturaCodigoArticulo[filaLineaFactura]==codigoProductoAIngresar) {
                existeLineaDeFactura = true;
            }
        }
        if (existeLineaDeFactura) {
            Console.WriteLine("La linea ya existe");
        } else {
            cantidadIngresar = pedirInteger("Ingrese cantidad entre 1 y 1000", 1, 1000);
            precioIngresar = pedirDouble("Ingrese precio entre 1 y 10000", 1, 10000);
            lineaFacturaCantidad[filaLineasFactura] = cantidadIngresar;
            lineaFacturaCodigoArticulo[filaLineasFactura] = codigoProductoAIngresar;
            lineaFacturaCodigoCliente[filaLineasFactura] = codigoClienteAIngresar;
            lineaFacturaImporte[filaLineasFactura] = cantidadIngresar * precioIngresar;
            lineaFacturaNumeroFactura[filaLineasFactura] = numeroFacturaIngresar;
            lineaFacturaPrecio[filaLineasFactura] = precioIngresar;
            filaLineasFactura = filaLineasFactura + 1;
        }
        
    } else if (opcionMenu=="2")
    {
        string listadoClientes = "Codigo\tNombre\n";
        string clienteABuscar = "";
        listadoClientes = listadoClientes + armarListadoDeMatriz(columnaCodigoCliente, columnaNombreCliente, clientes);
        string listadoItemsDelCliente = "";
        string[] codigosDeCliente = obtenerOpcionesAPartirDeMatriz(columnaCodigoCliente, clientes);
        clienteABuscar = pedirYValidarOpcion("Ingrese un cliente del siguiente listado:\n" + listadoClientes, codigosDeCliente);
        

        for (int fila = 0; fila <= lineaFacturaCodigoCliente.GetUpperBound(0); fila++)
        {
            if (clienteABuscar == lineaFacturaCodigoCliente[fila])
            {
                string nombreProducto = "";
                for (int filaProducto = 0; filaProducto <= productos.GetUpperBound(0); filaProducto++)
                {
                    if (productos[filaProducto, columnaCodigoProducto] == lineaFacturaCodigoArticulo[fila])
                    {
                        nombreProducto = productos[filaProducto, columnaNombreProducto];
                    }
                }
                listadoItemsDelCliente = listadoItemsDelCliente +
                    lineaFacturaNumeroFactura[fila] + "\t"
                    + lineaFacturaCodigoArticulo[fila] + "\t"
                    + nombreProducto + "\t"
                    + lineaFacturaCantidad[fila] + "\t"
                    + lineaFacturaPrecio[fila] + "\t"
                    + lineaFacturaImporte[fila] + "\n";
            }
            else if (lineaFacturaNumeroFactura[fila] == 0){
                Console.WriteLine("No hay ventas para este cliente");
                break;
            }
        }
        Console.WriteLine(listadoItemsDelCliente);

    }
    else if (opcionMenu=="3") {
        string opcion = "";
        string[] opciones = new string[] {"A", "B", "C"};
        opcion = pedirYValidarOpcion("Ingrese A para graficar por cantidad, B por importe, C por promedio", opciones);
        if (opcion=="A") {
            analisis = calcularMatrizPorCantidad(lineaFacturaCodigoArticulo, lineaFacturaCodigoCliente, lineaFacturaCantidad, clientes, productos);
        } else if (opcion=="B") {
            //analisis = calcularMatrizPorImporte(lineaFacturaCodigoArticulo, lineaFacturaCodigoCliente, lineaFacturaImporte, clientes, productos);
        } else if (opcion=="C") {
            //analisis = calcularMatrizPorPrecioPromedio(lineaFacturaCodigoArticulo, lineaFacturaCodigoCliente, lineaFacturaImporte, clientes, productos);
        }
        string matrizParaImprimir = armarMatrizParaImprimir(analisis, clientes, productos);
        Console.WriteLine(matrizParaImprimir);

    }
} while (opcionMenu!="4");

string armarMatrizParaImprimir(double[,] analisis, string[,] clientes, string[,] productos)
{
    string retorno = "";
    for (int filaCliente = 0; filaCliente <= clientes.GetUpperBound(0); filaCliente ++) {
        if (clientes[filaCliente, 0]!=null) {
            for (int filaProducto = 0; filaProducto <= productos.GetUpperBound(0); filaProducto++) {
                string fila = "";
                if (productos[filaProducto, 0]!=null) {
                    fila = fila + analisis[filaCliente, filaProducto] + "\t";
                }
                retorno = retorno + fila + "\n";
            }
        }
        
    }
    return (retorno);
}

double[,] calcularMatrizPorCantidad(string[] lineaFacturaCodigoArticulo, string[] lineaFacturaCodigoCliente, int[] lineaFacturaCantidad, string[,] clientes, string[,] productos)
{
    double[,] retorno = new double[clientes.GetUpperBound(0) + 1, productos.GetUpperBound(0) + 1];
    for (int filaCliente = 0; filaCliente <= clientes.GetUpperBound(0); filaCliente ++) {
        for (int filaProducto = 0; filaProducto <= productos.GetUpperBound(0); filaProducto++) {
            int cantidad = 0;
            for (int filaLineasFactura = 0; filaLineasFactura <= lineaFacturaCodigoArticulo.GetUpperBound(0); filaLineasFactura++) {
                if (lineaFacturaCodigoArticulo[filaLineasFactura]==productos[filaProducto, 0]&&lineaFacturaCodigoCliente[filaLineasFactura]==clientes[filaCliente, 0]) {
                    cantidad = cantidad + lineaFacturaCantidad[filaLineasFactura];
                }
            }
            retorno[filaCliente, filaProducto] = cantidad;
        }
    }
    return(retorno);
}

string pedirYValidarOpcion(string mensaje, string[] opciones) {
    string valorDeRetorno = "";
    bool valorEnArreglo = false;
    do {
        Console.WriteLine(mensaje);
        valorDeRetorno = Console.ReadLine().ToUpper();
        for (int fila=0;fila<=opciones.GetUpperBound(0)&&valorEnArreglo==false;fila++) {
            if (opciones[fila]==valorDeRetorno) {
                valorEnArreglo = true;
            }
        }
        if (!valorEnArreglo) {
            Console.WriteLine("Valor incorrecto");
        }
    } while (!valorEnArreglo);
    
    return(valorDeRetorno);
}

int pedirInteger(string mensaje, int minimo, int maximo) {
    int valorDeRetorno = 0;
    do {
        Console.WriteLine(mensaje);
        if (!Int32.TryParse(Console.ReadLine(), out valorDeRetorno)) {
            Console.WriteLine("Debe ingresar un valor numerico");
            valorDeRetorno = minimo - 1;
        } else if (valorDeRetorno < minimo || valorDeRetorno > maximo) {
            Console.WriteLine("Debe ingresar un valor entre " + minimo + " y " + maximo);
        }
    } while (valorDeRetorno < minimo || valorDeRetorno > maximo);
    return(valorDeRetorno);
}

double pedirDouble(string mensaje, double minimo, double maximo) {
    double valorDeRetorno = 0;
    do {
        Console.WriteLine(mensaje);
        if (!double.TryParse(Console.ReadLine(), out valorDeRetorno)) {
            Console.WriteLine("Debe ingresar un valor numerico");
            valorDeRetorno = minimo - 1;
        } else if (valorDeRetorno < minimo || valorDeRetorno > maximo) {
            Console.WriteLine("Debe ingresar un valor entre " + minimo + " y " + maximo);
        }
    } while (valorDeRetorno < minimo || valorDeRetorno > maximo);
    return(valorDeRetorno);
}
//Algoritmo de reduccion
string armarListadoDeMatriz(int columnaCodigo, int columnaNombre, string[,] elementos)
{
    string valorDeRetorno = "";
    for (int fila = 0; fila <= elementos.GetUpperBound(0); fila++)
    {
        if (elementos[fila, columnaCodigo] != null)
        {
            valorDeRetorno = valorDeRetorno + elementos[fila, columnaCodigo] + "\t" + elementos[fila, columnaNombre] + "\n";
        }
    }

    return valorDeRetorno;
}
//Algoritmo de mapeo: convierte N elementos codigo - nombre en N elementos codigo (de [,] a [])
string[] obtenerOpcionesAPartirDeMatriz(int columnaAExtraer, string[,] elementos) {
    string[] valorDeRetorno = new string[elementos.GetUpperBound(0) + 1];
    for (int fila = 0; fila <= elementos.GetUpperBound(0); fila++) {
        valorDeRetorno[fila] = elementos[fila, columnaAExtraer];
    }
    return(valorDeRetorno);
}