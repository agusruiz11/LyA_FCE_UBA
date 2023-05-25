const int maximaCantidadDeOrdenes = 100;
const int maximaCantidadDeItems = 500;
const int maximaCantidadDeArticulos = 10;
const int maxCantidadDeItemsPorOrden = 5;


main();
void main()
{
    int filaArticulos = 0;
    int filaOrdenes = 0;

    // Articulos 
    string[] articulosCodigo = { "MZN", "PRA", "NJA" };
    string[] articulosNombre = { "Manzana", "Pera", "Naranja" };

    // Orden de compra
    string[] ordenDeCompraFecha = new string[maximaCantidadDeOrdenes];
    string[] ordenDeCompraProveedor = new string[maximaCantidadDeOrdenes];
    int[] ordenDeCompraNumero = new int[maximaCantidadDeOrdenes];

    // Item
    string[,] itemCodigo = new string[maximaCantidadDeOrdenes, maximaCantidadDeItems];
    int[,] itemCantidad = new int[maximaCantidadDeOrdenes, maximaCantidadDeItems];
    double[,] itemPrecioUnitario = new double[maximaCantidadDeOrdenes, maximaCantidadDeItems];
    double[,] itemSubtotal = new double[maximaCantidadDeOrdenes, maximaCantidadDeItems];

    // Datos Ingresados
    
    string opcion = "";

    do
    {
        int filaItems = 0;
        mostrarMenu();
        opcion = pedirOpcion();
        switch (opcion)
        {
            case "1":

                cargarOrdenDeCompra(filaOrdenes, filaItems, ordenDeCompraFecha, ordenDeCompraProveedor, ordenDeCompraNumero, itemCodigo, itemCantidad, itemPrecioUnitario, itemSubtotal, articulosCodigo);
                filaOrdenes = filaOrdenes + 1;
                break;
            case "2":
                listarProveedores(ordenDeCompraProveedor);
                listarPorProveedor(
                    ordenDeCompraProveedor,
                    ordenDeCompraNumero,
                    itemSubtotal);
                break;
            case "3":
                listarTotalPorArticulo(articulosCodigo, itemCodigo, itemSubtotal, ordenDeCompraNumero);
                break;
        }
    } while (opcion != "4");


}

void cargarOrdenDeCompra(int filaOrdenes, int filaItems, string[] ordenDeCompraFecha, string[] ordenDeCompraProveedor, int[] ordenDeCompraNumero, string[,] itemCodigo, int[,] itemCantidad, double[,] itemPrecioUnitario, double[,] itemSubtotal, string[] articulosCodigo)
{
    if (filaOrdenes == 100)
    {
        Console.WriteLine("Maxima cantidad de ordenes alcanzada");
    }
    else
    {

        string ordenDeCompraFechaIngresado;
        string ordenDeCompraProveedorIngresado;
        int ordenDeCompraNumeroIngresado;
        ordenDeCompraFechaIngresado = DateTime.Now.ToString();
        Console.WriteLine("Ingresar nombre del proveedor");
        ordenDeCompraProveedorIngresado = pedirStringNoVacio("Ingresa un nombre de proveedor valido");
        ordenDeCompraNumeroIngresado = ingresoOrdenDeCompra("El numero de compra Ingresado, ya existe", ordenDeCompraNumero);
        ordenDeCompraFecha[filaOrdenes] = ordenDeCompraFechaIngresado;
        ordenDeCompraProveedor[filaOrdenes] = ordenDeCompraProveedorIngresado;
        ordenDeCompraNumero[filaOrdenes] = ordenDeCompraNumeroIngresado;
        cargarItems(
        filaOrdenes,
        filaItems, articulosCodigo,
        itemCodigo,
        itemCantidad,
        itemPrecioUnitario,
        itemSubtotal);
    }
}



void cargarItems(int filaOrdenes,
int filaItems, string[] articulosCodigo,
string[,] itemCodigo,
int[,] itemCantidad,
double[,] itemPrecioUnitario,
double[,] itemSubtotal)
{

    int contador = 0;
    string continuar = "SI";
    Console.WriteLine("Entro con fila " + filaItems);
    while (contador < maxCantidadDeItemsPorOrden && continuar.ToUpper() == "SI")
    {
        cargarItem(
           filaOrdenes,
           filaItems,
           articulosCodigo,
           itemCodigo,
           itemCantidad,
           itemPrecioUnitario,
           itemSubtotal);
        filaItems = filaItems + 1;
        contador = contador + 1;
        Console.WriteLine("Desea agregar otro item? Ingrese SI para agregar otro");
        continuar = pedirStringNoVacio("Ingrese SI para agregar otro item");
        ;
    }


}

void cargarItem(int filaOrdenes,
int filaItems,
string[] articulosCodigo,
string[,] itemCodigo,
int[,] itemCantidad,
double[,] itemPrecioUnitario,
double[,] itemSubtotal)
{
    string itemCodigoIngresado;
    int itemCantidadIngresado;
    double itemPrecioUnitarioIngresado;
    Console.WriteLine("Ingresar el codigo del item");

    do
    {
        itemCodigoIngresado = pedirStringNoVacio("Ingrese un articulo valido");
        if (buscarStringEnArray(itemCodigoIngresado, articulosCodigo) == -1)
        {
            Console.WriteLine("No se encontro al artiuclo");
        }
    } while (buscarStringEnArray(itemCodigoIngresado, articulosCodigo) == -1);
    itemCodigo[filaOrdenes, filaItems] = itemCodigoIngresado;
    Console.WriteLine("Ingresar el la cantidad del item");
    itemCantidadIngresado = pedirInt("Ingrese una cantidad valida", 1, 1000);
    itemCantidad[filaOrdenes, filaItems] = itemCantidadIngresado;
    Console.WriteLine("Ingresar el precio unitario del item");
    itemPrecioUnitarioIngresado = pedirDouble("Ingrese un precio valido", 1, 10000);
    itemPrecioUnitario[filaOrdenes, filaItems] = itemPrecioUnitarioIngresado;
    itemSubtotal[filaOrdenes, filaItems] = itemPrecioUnitarioIngresado * itemCantidadIngresado;

}

int buscarStringEnArray(string item, string[] array)
{
    int retorno = -1;
    for (int i = 0; i <= array.GetUpperBound(0); i++)
    {
        if (array[i] == item)
        {
            retorno = i;
        }
    }
    return retorno;
}


int ingresoOrdenDeCompra(string mensaje, int[] ordenDeCompraNumeros)
{
    int retorno = -1;
    do
    {
        Console.WriteLine("Ingrese numero de compra");
        retorno = pedirInt("Ingrese un numero de compra valido", 1, 99999);
        if (ordenDeCompraYaExiste(retorno, ordenDeCompraNumeros))
        {
            Console.WriteLine(mensaje);
            retorno = -1;
        }
    } while (retorno == -1);
    return retorno;
}


bool ordenDeCompraYaExiste(int ordenDeCompraNumeroIngresado, int[] ordenDeCompraNumeros)
{
    bool retorno = false;
    for (int i = 0; i <= ordenDeCompraNumeros.GetUpperBound(0); i++)
    {
        if (ordenDeCompraNumeros[i] == ordenDeCompraNumeroIngresado)
        {
            retorno = true;
        }
    }
    return retorno;
}


string pedirOpcion()
{
    string retorno = "";
    do
    {
        retorno = pedirStringNoVacio("Ingrese un valor valido");
        if (retorno == "" || retorno != "1" && retorno != "2" && retorno != "3" && retorno != "4")
        {
            Console.WriteLine("Ingresar uno de los siguentes valores: 1, 2, 3 o 4");
        }
    } while (retorno == "" || retorno != "1" && retorno != "2" && retorno != "3" && retorno != "4");

    return retorno;
}

void mostrarMenu()
{
    Console.WriteLine("1. Dar alta orden de compra");
    Console.WriteLine("2. Listar por proveedor");
    Console.WriteLine("3. Listar por articulo");
    Console.WriteLine("4. Salir");

}


string pedirStringNoVacio(string mensaje)
{
    string retorno = "";
    do
    {
        retorno = Console.ReadLine();
    } while (retorno == "");
    return retorno;
}

int pedirInt(string mensaje, int min, int max)
{
    int retorno = -1;

    do
    {

        if (!Int32.TryParse(Console.ReadLine(), out retorno))
        {
            Console.WriteLine(mensaje);
            retorno = -1;
        }
        else if (retorno < min || retorno > max)
        {
            Console.WriteLine("El numero debe ser mayor a " + min + " y menor a " + max);
            retorno = -1;
        }
    } while (retorno == -1);

    return retorno;
}
double pedirDouble(string mensaje, int min, int max)
{
    double retorno = 0;

    do
    {
        if (!double.TryParse(Console.ReadLine(), out retorno))
        {
            Console.WriteLine(mensaje);
            retorno = 0;
        }
        else if (retorno < min || retorno > max)
        {
            Console.WriteLine("Debe ser entre " + min + "y " + max);
        }
    } while (retorno == 0);
    return retorno;
}

void listarPorProveedor(
string[] ordenDeCompraProveedor,
int[] ordenDeCompraNumero,
double[,] itemSubtotal
)
{
    string nombreProveedorIngresado;
    double totalVentas = 0;
    Console.WriteLine("Ingrese nombre del proveedor");
    nombreProveedorIngresado = pedirStringNoVacio("Ingrese un nombre de proveedor valido");

    for (int i = 0; i <= ordenDeCompraProveedor.GetUpperBound(0); i++)
    {
        if (ordenDeCompraProveedor[i] == nombreProveedorIngresado)
        {
            totalVentas = totalVentas + totalItemPorProveedor(i, itemSubtotal);
        }
    }

    if (totalVentas == 0)
    {
        Console.WriteLine("EL proveedor no tiene ventas");
    }
    else
    {
        Console.WriteLine("El proveedor tiene : " + totalVentas + " ventas");
    }


}

double totalItemPorProveedor(int filaOrdenSeleccionada, double[,] itemSubtotal)
{
    double retorno = 0;
    for (int i = 0; i < maxCantidadDeItemsPorOrden; i++)
    {
        Console.WriteLine(itemSubtotal[filaOrdenSeleccionada, i]);
        if (itemSubtotal[filaOrdenSeleccionada, i] != 0)
        {
            retorno = retorno + itemSubtotal[filaOrdenSeleccionada, i];
        }
    }
    return retorno;

}


void listarProveedores(string[] listaProveedores)
{
    string mensaje = "";
    for (int i = 0; i <= listaProveedores.GetUpperBound(0); i++)
    {
        if (listaProveedores[i] != null && listaProveedores[i] != "")
        {
            mensaje = mensaje + listaProveedores[i] + "\n";
        }
    }
    Console.WriteLine(mensaje);
}


void listarTotalPorArticulo(
string[] articulosCodigo,
string[,] itemCodigo,
double[,] itemSubtotal,
int[] ordenDeCompraNumero)
{
    double total = 0;
    Console.WriteLine("Ingrese un codigo de articulo");
    string articuloIngresado = pedirStringNoVacio("Ingrese un articulo valido");
    if (buscarStringEnArray(articuloIngresado, articulosCodigo) != -1)
    {
        for (int filaOrden = 0; filaOrden <= itemCodigo.GetUpperBound(0); filaOrden++)
        {
            for (int filaItem = 0; filaItem < maxCantidadDeItemsPorOrden; filaItem++)
            {
                if (itemCodigo[filaOrden, filaItem] == articuloIngresado)
                {
                    total = total + itemSubtotal[filaOrden, filaItem];
                }

            }

        }
    }
    Console.WriteLine("El total por el articulo: " + " es de : " + total);
}