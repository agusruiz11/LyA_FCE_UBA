const int cantidadOrdenes = 100;
const int cantidadItems = 5;
const int cantidadArticulos = 0;

main();

void main() {

    string[] articuloCodigo = new string[cantidadArticulos];
    string[] articuloNombre = new string[cantidadArticulos];

    string[] cabeceraFecha = new string[cantidadOrdenes];
    int[] cabeceraNumero = new int[cantidadOrdenes];
    string[] cabeceraProveedor = new string[cantidadOrdenes];

    string[,] itemCodigo = new string[cantidadOrdenes, cantidadItems];
    int[,] itemCantidad = new int[cantidadOrdenes, cantidadItems];
    double[,] itemPrecio = new double[cantidadOrdenes, cantidadItems];
    double[,] itemSubtotal = new double[cantidadOrdenes, cantidadItems];

    string opcionElegida = "";

    cargaInicial();

    do {
        opcionElegida = pedirStringNoVacio("ACA VA EL MENU");
        if (opcionElegida=="1") {
            ingresarOrdenDeCompra(articuloCodigo, articuloNombre, cabeceraFecha, cabeceraNumero,
                        cabeceraProveedor, itemCodigo, itemCantidad, itemPrecio, itemSubtotal);
        } else if (opcionElegida=="2") {

        } else if (opcionElegida=="3") {

        }
    } while (opcionElegida!="4");

}

void ingresarOrdenDeCompra(string[] articuloCodigo, string[] articuloNombre, string[] cabeceraFecha, int[] cabeceraNumero, string[] cabeceraProveedor, string[,] itemCodigo, int[,] itemCantidad, double[,] itemPrecio, double[,] itemSubtotal)
{
    int filaVacia = buscarEnString(cabeceraFecha, null);
    int filaOrdenDeCompra = 0;
    int numeroIngresar = -1;
    string fechaIngresar = DateTime.Now.ToString();
    string proveedorIngresar = "";
    if (filaVacia==-1) {
        Console.WriteLine("No hay lugar para mas ordenes de compra");
    } else {
        do {
            numeroIngresar = pedirInteger("Ingrese numero de OC", 1, 999999);
            filaOrdenDeCompra = buscarInteger(cabeceraNumero, numeroIngresar);
            if (filaOrdenDeCompra!=-1) {
                Console.WriteLine("La orden de compra ya existe");
            }
        } while (filaOrdenDeCompra!=-1);

        proveedorIngresar = pedirStringNoVacio("Ingrese proveedor");

        cabeceraFecha[filaVacia] = fechaIngresar;
        cabeceraProveedor[filaVacia] = proveedorIngresar;
        cabeceraNumero[filaVacia] = numeroIngresar;


        ingresarItemsDeOrden(filaVacia, articuloCodigo, articuloNombre, itemCodigo, itemPrecio, itemCantidad, itemSubtotal);
        

    }
}

void ingresarItemsDeOrden(int filaVacia, string[] articuloCodigo, string[] articuloNombre, string[,] itemCodigo, double[,] itemPrecio, int[,] itemCantidad, double[,] itemSubtotal)
{
    string continuarCarga = "";
    int contador = 0;

    do {
        cargarItemDeOrden(filaVacia, contador, articuloCodigo, articuloNombre, itemCodigo, itemPrecio, itemCantidad, itemSubtotal);
        continuarCarga = pedirSiONo("Desea continuar?");
        contador = contador + 1;
    } while (contador <= itemCodigo.GetUpperBound(1)&&continuarCarga=="S");
}

string pedirSiONo(string v)
{
    throw new NotImplementedException();
}

void cargarItemDeOrden(int filaOrdenDeCompra, int filaItem, string[] articuloCodigo, string[] articuloNombre, string[,] itemCodigo, double[,] itemPrecio, int[,] itemCantidad, double[,] itemSubtotal)
{
    string codigoIngresar = "";
    int cantidadIngresar = 0;
    double precioIngresar = 0;
    int filaArticulo = 0;

    do {
        codigoIngresar = pedirStringNoVacio("Ingrese codigo de articulo\n" + armarListadoArticulos(articuloCodigo, articuloNombre));
        filaArticulo = buscarEnString(articuloCodigo, codigoIngresar);
        if (filaArticulo==-1) {
            Console.WriteLine("El codigo no existe");
        }
    } while (filaArticulo==-1);
    cantidadIngresar = pedirInteger("Ingrese cantidad", 1, 10000);
    precioIngresar = pedirDouble("Ingrese precio", 1, 10000);

    itemCodigo[filaOrdenDeCompra, filaItem] = codigoIngresar;
    itemPrecio[filaOrdenDeCompra, filaItem] = precioIngresar;
    itemCantidad[filaOrdenDeCompra, filaItem] = cantidadIngresar;
    itemSubtotal[filaOrdenDeCompra, filaItem] = precioIngresar * cantidadIngresar;

}

double pedirDouble(string v1, int v2, int v3)
{
    throw new NotImplementedException();
}

string armarListadoArticulos(string[] articuloCodigo, string[] articuloNombre)
{
    throw new NotImplementedException();
}

int buscarInteger(int[] cabeceraNumero, int numeroIngresar)
{
    throw new NotImplementedException();
}

int pedirInteger(string v1, int v2, int v3)
{
    throw new NotImplementedException();
}

/*
   Busca un string. Devuelve -1 si no encuentra o sino la fila donde se encuentra
*/
int buscarEnString(string[] cabeceraFecha, object value)
{
    throw new NotImplementedException();
}

string pedirStringNoVacio(string v)
{
    throw new NotImplementedException();
}

void cargaInicial()
{
    throw new NotImplementedException();
}