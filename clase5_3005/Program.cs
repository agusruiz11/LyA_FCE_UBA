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

productos[0,columnaCodigoProducto] = "MZ";
productos[0,columnaNombreProducto] = "MANZANA";
productos[1,columnaCodigoProducto] = "NJ";
productos[1,columnaNombreProducto] = "NARANJA";

clientes[0,columnaCodigoCliente] = "36";
clientes[0,columnaNombreCliente] = "JOSE L. SANCHEZ";
clientes[1,columnaCodigoCliente] = "888";
clientes[1,columnaNombreCliente] = "JUAN TOPO";
clientes[2,columnaCodigoCliente] = "111";
clientes[2,columnaNombreCliente] = "ATILA EL HUNO";

string opcionMenu = "";

do {
    Console.WriteLine("Ingrese 1 para ingresar venta, 2 para ver por cliente, 3 para analisis");
    opcionMenu = Console.ReadLine();
    if (opcionMenu=="1") {
        string codigoClienteAIngresar = "";
        string codigoProductoAIngresar = "";
        int numeroFacturaIngresar = 0;
        int cantidadIngresar = 0;
        double precioIngresar = 0;
        bool existeCliente = false;
        string listadoClientes = "Codigo\tNombre\n";
        string listadoProductos = "Codigo\tNombre\n";
        for (int fila = 0; fila<=clientes.GetUpperBound(0);fila++) {
            if (clientes[fila, columnaCodigoCliente]!=null) {
                listadoClientes = listadoClientes + clientes[fila, columnaCodigoCliente] + "\t" + clientes[fila, columnaNombreCliente] + "\n";
            }
        }
        for (int fila = 0; fila<=productos.GetUpperBound(0);fila++) {
            if (productos[fila, columnaCodigoProducto]!=null) {
                listadoProductos = listadoProductos + productos[fila, columnaCodigoCliente] + "\t" + clientes[fila, columnaNombreProducto] + "\n";
            }
        }
        do {
            do {
                Console.WriteLine("Ingrese un cliente del siguiente listado:\n");
                Console.WriteLine(listadoClientes);
                codigoClienteAIngresar = Console.ReadLine();
                if (codigoClienteAIngresar==null) {
                    Console.WriteLine("Debe ingresar algun dato");
                }
            } while (codigoClienteAIngresar==null);
            
            for (int fila = 0;fila<=clientes.GetUpperBound(0)&&!existeCliente;fila++) {
                if (clientes[fila, columnaCodigoCliente]==codigoClienteAIngresar) {
                    existeCliente=true;
                }
            }
        } while (!existeCliente);
    } else if (opcionMenu=="2") {
        string listadoClientes = "Codigo\tNombre\n";
        bool existeCliente = false;
        string clienteABuscar = "";
        string listadoItemsDelCliente = "Numero factura\tCodigo producto\tNombreProducto\tCantidad\tPrecio\tImporte\n";
        for (int fila = 0; fila<=clientes.GetUpperBound(0);fila++) {
            if (clientes[fila, columnaCodigoCliente]!=null) {
                listadoClientes = listadoClientes + clientes[fila, columnaCodigoCliente] + "\t" + clientes[fila, columnaNombreCliente] + "\n";
            }
        }
        do {
            do {
                Console.WriteLine("Ingrese un cliente del siguiente listado:\n");
                Console.WriteLine(listadoClientes);
                clienteABuscar = Console.ReadLine();
                if (clienteABuscar==null) {
                    Console.WriteLine("Debe ingresar algun dato");
                }
            } while (clienteABuscar==null);
            
            for (int fila = 0;fila<=clientes.GetUpperBound(0)&&!existeCliente;fila++) {
                if (clientes[fila, columnaCodigoCliente]==clienteABuscar) {
                    existeCliente=true;
                }
            }
        } while (!existeCliente);
        
        for (int fila = 0;fila<=lineaFacturaCodigoCliente.GetUpperBound(0);fila++) {
            if (clienteABuscar==lineaFacturaCodigoCliente[fila]) {
                string nombreProducto = "";
                for (int filaProducto = 0;filaProducto<=productos.GetUpperBound(0);filaProducto++) {
                   if (productos[filaProducto, columnaCodigoProducto]==lineaFacturaCodigoArticulo[fila]) {
                        nombreProducto = productos[filaProducto, columnaNombreProducto];
                    }
                }
                listadoItemsDelCliente = listadoItemsDelCliente +
                    lineaFacturaNumeroFactura[fila] + "\t"
                    + lineaFacturaCodigoArticulo[fila] + "\t"
                    + nombreProducto + "\t"
                    + lineaFacturaCantidad + "\t"
                    + lineaFacturaPrecio + "\t"
                    + lineaFacturaImporte + "\n";
            }
        }
        Console.WriteLine(listadoItemsDelCliente);

    } else if (opcionMenu=="3") {
        

    }
} while (opcionMenu!="4");