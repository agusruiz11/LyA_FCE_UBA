// See https://aka.ms/new-console-template for more information

const int cantidadClientes = 500;
const int cantidadOrdenesReparacion = 3000;
main();


void main()
{
    int[] numeroDocumentoClientes = new int[cantidadClientes];
    string[] nombreClientes = new string[cantidadClientes];
    string[] telefonoClientes = new string[cantidadClientes];
    int[] numeroPedidosReparacion = new int[cantidadOrdenesReparacion];
    string[] descripcionProblemasReparaciones = new string[cantidadOrdenesReparacion];
    int[] documentoClienteSolicitante = new int[cantidadOrdenesReparacion]; //como no tengo que crear los pedidos de reparacion uso esto de manera auxiliar para saber a quién le corresponde cada pedido
    string[] solucionesProblemasReparaciones = new string[cantidadOrdenesReparacion];
    double[] precioReparaciones = new double[cantidadOrdenesReparacion];
    string[] estadoReparaciones = new string[cantidadOrdenesReparacion];
    string opcionMenu = "";
    string[] opcionesValidasMenu = new string[] { "1", "2", "3", "4", "5" };

    // Esta es la carga inicial para poder trabajar y testear
    numeroPedidosReparacion[0] = 1;
    numeroPedidosReparacion[1] = 2;
    numeroPedidosReparacion[2] = 3;
    numeroPedidosReparacion[3] = 4;
    numeroPedidosReparacion[4] = 5;
    descripcionProblemasReparaciones[0] = "no funca la aguja";
    descripcionProblemasReparaciones[1] = "no tiene pila";
    descripcionProblemasReparaciones[2] = "se rompió el vidrio";
    descripcionProblemasReparaciones[3] = "no cambia la hora";
    descripcionProblemasReparaciones[4] = "sin pila";
    solucionesProblemasReparaciones[0] = "cambiar la aguja";
    solucionesProblemasReparaciones[1] = "cambiar pila";
    solucionesProblemasReparaciones[2] = "cambiar vidrio";
    solucionesProblemasReparaciones[3] = "cambiar pila";
    solucionesProblemasReparaciones[4] = "cambiar pila";
    precioReparaciones[0] = 100.50;
    precioReparaciones[1] = 50;
    precioReparaciones[2] = 300;
    precioReparaciones[3] = 15.50;
    precioReparaciones[4] = 20;
    estadoReparaciones[0] = "A";
    estadoReparaciones[1] = "E";
    estadoReparaciones[2] = "R";
    estadoReparaciones[3] = "A";
    estadoReparaciones[4] = "P";
    documentoClienteSolicitante[0] = 123456;
    documentoClienteSolicitante[1] = 1;
    documentoClienteSolicitante[2] = 2;
    documentoClienteSolicitante[3] = 123456;
    documentoClienteSolicitante[4] = 3;

    //cargaInicial(numeroDocumentoClientes, nombreClientes, telefonoClientes, numeroPedidosReparacion, descripcionProblemasReparaciones, solucionesProblemasReparaciones, precioReparaciones, estadoReparaciones);

    do
    {
        opcionMenu = pedirYValidarOpcion("Ingrese una opción para continuar:\n1. Ingresar nuevo cliente\n2. Ingresar nueva reparación\n3. Comunicar precio presupuestado\n4. Entregar\n5. Salir", opcionesValidasMenu);
        switch (opcionMenu)
        {
            case "1":
                int filaActual = -1;

                filaActual = buscarFilaLibre(numeroDocumentoClientes);

                if (filaActual != -1)
                {
                    int crearDocumentoCliente = -1;
                    bool quieroQueExistaDocumento = false;

                    crearDocumentoCliente = pedirIntYValidarOpcion("Ingrese el número de documento del cliente", 1, 99999999, numeroDocumentoClientes, quieroQueExistaDocumento);

                    if (crearDocumentoCliente != -1)
                    {
                        string nombreCliente = "";
                        string telefonoCliente = "";

                        nombreCliente = pedirYValidarString("Ingrese el nombre del cliente:");
                        telefonoCliente = pedirYValidarString("Ingrese el teléfono del cliente:");


                        numeroDocumentoClientes[filaActual] = crearDocumentoCliente;
                        nombreClientes[filaActual] = nombreCliente;
                        telefonoClientes[filaActual] = telefonoCliente;
                        
                    }
                    else
                    {
                        Console.WriteLine("Ya existe un cliente con ese número de documento");
                    }
                }
                else
                {
                    Console.WriteLine("No puede ingresar más clientes");
                }
                break;
            case "2":
                // no se desarrolla
                break;
            case "3":
                // no se desarrolla
                break;
            case "4":
                int documentoCliente = -1;
                int existeDocumentoCliente = -1;
                bool hayReparacionesAvisadas = false;
                bool quieroQueExistaDocumentoAIngresar = true;
                string listaReparaciones = "Numero de reparación\tNúmero de documento del solicitante\tDescripción del problema\t\tDescripción de la solución\t\tPrecio\tEstado\n";

                do
                {

                    documentoCliente = pedirIntYValidarOpcion("Ingrese el número de documento del cliente:", 1, 99999999, numeroDocumentoClientes, quieroQueExistaDocumentoAIngresar);                    
                    if (documentoCliente == -1)
                    {
                        Console.WriteLine("El número de documento ingresado no pertenece a ningún cliente.");
                    }

                } while (documentoCliente == -1);


                hayReparacionesAvisadas = existenReparacionesAvisadas(documentoCliente, documentoClienteSolicitante, estadoReparaciones);
                if (hayReparacionesAvisadas == true)
                {
                    int numeroReparacion = -1;
                    bool quieroQueExistaNroReparacion = true;
                    listaReparaciones = listaReparaciones + crearListaReparacionesAvisadas(documentoCliente, numeroPedidosReparacion, documentoClienteSolicitante, descripcionProblemasReparaciones, solucionesProblemasReparaciones, precioReparaciones, estadoReparaciones);
                    Console.WriteLine(listaReparaciones);

                    numeroReparacion = pedirIntYValidarOpcion("Ingrese un número de reparación del listado anterior", 1, 40000, numeroPedidosReparacion, quieroQueExistaNroReparacion);

                    if (numeroReparacion != -1)
                    {
                        int filaNumeroReparacion = -1;

                        filaNumeroReparacion = obtenerNumeroIndice(numeroReparacion, numeroPedidosReparacion); 

                        bool existeNumeroReparacionClienteEstado = false;

                        existeNumeroReparacionClienteEstado = correspondeAClienteYEstado(filaNumeroReparacion, documentoClienteSolicitante, documentoCliente, estadoReparaciones);

                        if (existeNumeroReparacionClienteEstado == true)
                        {
                            string registrarEntrega = "";

                            registrarEntrega = registrarEntregaSiONo("Desea registrar la entrega? Ingrese S o N", filaNumeroReparacion, estadoReparaciones, nombreClientes, numeroPedidosReparacion, numeroDocumentoClientes, documentoClienteSolicitante, precioReparaciones);

                            if (registrarEntrega == "S")
                            {
                                estadoReparaciones[filaNumeroReparacion] = "E";
                                Console.WriteLine("La entrega ha sido registrada satisfactoriamente");
                            }
                            else
                            {
                                Console.WriteLine("La entrega no ha sido registrada");
                            }
                        }
                        else
                        {
                            Console.WriteLine("El número de reparación no perrtenece al cliente o no está avisada para entregar");
                        }
                        


                    }
                    else
                    {
                        Console.WriteLine("El número de reparación ingresado no existe");
                    }
                }
                else
                {
                    Console.WriteLine("No existen reparaciones avisadas para entregar para el cliente ingresado.");



                }
                break;
        }
    } while (opcionMenu != "5");
}
/*   int pedirYValidarCliente(int[] numeroDocumentoClientes, string[] nombreClientes, string[] telefonoClientes, int filaActual)
{
int documentoCliente = -1;
int filaDocumento = -1;
string nombreCliente = "";
string telefonoCliente = "";

documentoCliente = pedirIntYValidar("Ingrese el número de documento del cliente:", 1, 100000000, numeroDocumentoClientes);

if(filaDocumento != -1) {
nombreCliente = pedirYValidarString("Ingrese el nombre del cliente:");
telefonoCliente = pedirYValidarString("Ingrese el apellido del cliente:");

numeroDocumentoClientes[filaActual] = documentoCliente;
nombreClientes[filaActual] = nombreCliente;
telefonoCliente[filaActual] = telefonoCliente;
} else
{

}

return documentoCliente;


}
}*/

string registrarEntregaSiONo(string mensaje, int filaReparacion, string[] estadoReparacion, string[] nombreClientes, int[] numeroPedidosReparacion, int[] numeroDocumentoClientes, int[] documentoClienteSolicitante, double[] precioReparaciones)
{
    string opcionRegistro = "";
    string[] opcionesValidasRegistro = new string[] { "S", "N" };
    string resumenEntrega = "Nombre cliente\tNumero reparacion\tPrecio\n";


    resumenEntrega = resumenEntrega + crearResumenEntrega(filaReparacion, nombreClientes, numeroPedidosReparacion, documentoClienteSolicitante, numeroDocumentoClientes, precioReparaciones);
    Console.WriteLine(resumenEntrega);
    opcionRegistro = pedirYValidarOpcion(mensaje, opcionesValidasRegistro);

    return opcionRegistro;
}

string crearResumenEntrega(int filaReparacion, string[] nombreClientes, int[] numeroPedidosReparacion, int[] documentoClienteSolicitante, int[] numeroDocumentoClientes, double[] precioReparaciones)
{
    string nombreCliente = "";
    string valorDeRetorno = "";

    nombreCliente = obtenerNombreCliente(filaReparacion, documentoClienteSolicitante, numeroDocumentoClientes, nombreClientes);
    valorDeRetorno = valorDeRetorno + nombreCliente + "\t" + numeroPedidosReparacion[filaReparacion] + "\t" + precioReparaciones[filaReparacion] + "\n";

    return valorDeRetorno;
}

string obtenerNombreCliente(int filaReparacion, int[] documentoClienteSolicitante, int[] numeroDocumentoClientes, string[] nombreClientes)
{
    string valorDeRetorno = "";

    for (int i = 0; i <= numeroDocumentoClientes.GetUpperBound(0); i++)
    {
        if (numeroDocumentoClientes[i] == documentoClienteSolicitante[filaReparacion])
        {
            valorDeRetorno = nombreClientes[i];
        }
    }
    return valorDeRetorno;
}

bool existenReparacionesAvisadas(int documentoCliente, int[] documentoClienteSolicitante, string[] estadoReparaciones)
{
    bool existenReparacionesAvisadas = false;
    int i = 0;

    while (i <= estadoReparaciones.GetUpperBound(0) && existenReparacionesAvisadas == false)
    {
        if (documentoCliente == documentoClienteSolicitante[i] && estadoReparaciones[i] == "A")
        {
            existenReparacionesAvisadas = true;
        }
        else
        {
            i++;
        }
    }

    return existenReparacionesAvisadas;
}

string crearListaReparacionesAvisadas(int documentoCliente, int[] numeroPedidosReparacion, int[] documentoClienteSolicitante, string[] descripcionProblemasReparaciones, string[] solucionesProblemasReparaciones, double[] precioReparaciones, string[] estadoReparaciones)
{
    string valorDeRetorno = "";
    for (int i = 0; i <= numeroPedidosReparacion.GetUpperBound(0); i++)
    {
        if (documentoCliente == documentoClienteSolicitante[i] && estadoReparaciones[i] == "A")
        {
            valorDeRetorno = valorDeRetorno + numeroPedidosReparacion[i] + "\t" + documentoClienteSolicitante[i] + "\t" + descripcionProblemasReparaciones[i] + "\t\t" + solucionesProblemasReparaciones[i] + "\t\t" + precioReparaciones[i] + "\t" + estadoReparaciones[i] + "\n";

        }
    }
    return valorDeRetorno;
}

string pedirYValidarString(string mensaje)
{
    string valorDeRetorno = "";
    do
    {
        Console.WriteLine(mensaje);
        valorDeRetorno = Console.ReadLine().ToUpper();
        if (valorDeRetorno == null || valorDeRetorno == "")
        {
            Console.WriteLine("Debe ingresar un texto no vacío");
        }
    } while (valorDeRetorno == null || valorDeRetorno == "");
    return valorDeRetorno;
}

int buscarFilaLibre(int[] numeroDocumentoClientes)
{
    int filaActual = -1;

    for (int i = 0; i <= numeroDocumentoClientes.GetUpperBound(0) && filaActual == -1; i++)
    {
        if (numeroDocumentoClientes[i] == null || numeroDocumentoClientes[i] == 0)
        {
            filaActual = i;
        }
    }
    return filaActual;
}

int pedirIntYValidar(string mensaje, int minimo, int maximo)
{
    int valorDeRetorno = -1;

    Console.WriteLine(mensaje);
    if (!Int32.TryParse(Console.ReadLine(), out valorDeRetorno))
    {
        Console.WriteLine("Debe ingresar un valor numérico");
        valorDeRetorno = -1;
    }
    else if (valorDeRetorno < minimo || valorDeRetorno > maximo)
    {
        Console.WriteLine("Debe ingresar un valor mayor a " + minimo + " y menor que " + maximo);
        valorDeRetorno = -1;
    }

    return valorDeRetorno;
}

int pedirIntYObtenerIndice(string mensaje, int minimo, int maximo, int[] numeroDocumentoClientes)
{
    int valorDeRetorno = -1;
    int numeroIngresar = -1;
    bool existeInt = false;
    Console.WriteLine(mensaje);
    if (!Int32.TryParse(Console.ReadLine(), out numeroIngresar))
    {
        Console.WriteLine("Debe ingresar un valor numérico");
    }
    else if (numeroIngresar < minimo || numeroIngresar > maximo)
    {
        Console.WriteLine("Debe ingresar un valor mayor a " + minimo + " y menor que " + maximo);
    }
    else
    {
        valorDeRetorno = obtenerNumeroIndice(numeroIngresar, numeroDocumentoClientes);
    }
    return valorDeRetorno;
}
int pedirIntYValidarOpcion(string mensaje, int minimo, int maximo, int[] numeroDocumentoClientes, bool quieroQueExista)
{
    int valorDeRetorno = -1;
    bool existeInt = true;
        Console.WriteLine(mensaje);
        if (!Int32.TryParse(Console.ReadLine(), out valorDeRetorno))
        {
            Console.WriteLine("Debe ingresar un valor numérico");
            valorDeRetorno = -1;
        }
        else if (valorDeRetorno < minimo || valorDeRetorno > maximo)
        {
            Console.WriteLine("Debe ingresar un valor mayor a " + minimo + " y menor que " + maximo);
            valorDeRetorno = -1;
        }
        else
        {
            existeInt = existeIntEnArray(valorDeRetorno, numeroDocumentoClientes);
            if (existeInt != quieroQueExista)
            {                
                valorDeRetorno = -1;
            }
        }
    return valorDeRetorno;
}

bool correspondeAClienteYEstado(int valorABuscar, int[] numeroDocumentoClientes, int documentoCliente, string[] estadoReparaciones)
{
    bool valorDeRetorno = false;

    if (numeroDocumentoClientes[valorABuscar] == documentoCliente && estadoReparaciones[valorABuscar] == "A")
    {
        valorDeRetorno = true;
    }
    return valorDeRetorno;
}

int obtenerNumeroIndice(int valorABuscar, int[] arregloDondeBuscar)
{
    bool existeValor = false;
    int i = 0;
    int valorDeRetorno = -1;
    while (existeValor == false && i <= arregloDondeBuscar.GetUpperBound(0))
    {
        if (arregloDondeBuscar[i] == valorABuscar)
        {
            existeValor = true;
            valorDeRetorno = i;
        }
        else
        {
            i++;
        }
    }
    return valorDeRetorno;
}

/*int pedirIntYValidarQueExistaSinRepetir(string mensaje, int minimo, int maximo, int[] numeroDocumentoClientes)
{
int valorDeRetorno = -1;
bool existeInt = true;

Console.WriteLine(mensaje);
if (!Int32.TryParse(Console.ReadLine(), out valorDeRetorno))
{
Console.WriteLine("Debe ingresar un valor numérico");
}
else if (valorDeRetorno < minimo || valorDeRetorno > maximo)
{
Console.WriteLine("Debe ingresar un valor mayor a " + minimo + " y menor que " + maximo);
}
else
{
existeInt = existeIntEnArray(valorDeRetorno, numeroDocumentoClientes);
if (existeInt == false)
{
Console.WriteLine("El número ingresado no existe en el sistema");
}
}

return valorDeRetorno;
}*/

/*void cargaInicial(int[] numeroDocumentoClientes, string[] nombreClientes, string[] telefonoClientes, int[] numeroPedidosReparacion, string[] descripcionProblemasReparaciones, string[] solucionesProblemasReparaciones, double[] precioReparaciones, string[] estadoReparaciones)
{
throw new NotImplementedException();
};
*/
string pedirYValidarOpcion(string mensaje, string[] opcionesValidas)
{
    string valorDeRetorno = "";
    bool existeOpcion = false;
    do
    {
        Console.WriteLine(mensaje);
        valorDeRetorno = Console.ReadLine().ToUpper();
        for (int i = 0; i <= opcionesValidas.GetUpperBound(0) && existeOpcion == false; i++)
        {
            if (valorDeRetorno == opcionesValidas[i])
            {
                existeOpcion = true;
            }
        }
        if (!existeOpcion)
        {
            Console.WriteLine("Debe ingresar un valor válido");
        }
    } while (existeOpcion == false);
    return valorDeRetorno;
}

bool existeIntEnArray(int valorABuscar, int[] arrayDondeBuscar)
{
    bool existeOpcion = false;

    for (int i = 0; i <= arrayDondeBuscar.GetUpperBound(0) && existeOpcion == false; i++)
    {
        if (valorABuscar == arrayDondeBuscar[i])
        {
            existeOpcion = true;
        }
    }
    return existeOpcion;
}


/*bool existeStringEnArray(string valorABuscar, string[] arrayDondeBuscar, bool existeOpcion)
{
for (int i = 0; i <= opcionesValidas.GetUpperBound(0) && existeOpcion == false; i++)
{
if (valorDeRetorno == opcionesValidas[i])
{
existeOpcion = true;
}
else
{
Console.WriteLine("La opción ingresada no existe");
}
}
}
*/