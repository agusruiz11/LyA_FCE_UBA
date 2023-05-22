const int cantidad = 100;
const string EstPend = "P";
const string EstVisi = "V";
const long NumMin = 1;
const long NumMax = 999999;

main();

void main() {

     

    long[] VisitaNumero = new long[cantidad];
    string[] VisitaDomicilio = new string[cantidad];
    string[] VisitaNombreCliente = new string[cantidad];
    string[] VisitaSintoma = new string[cantidad];
    string[] VisitaCausaSolucion = new string[cantidad];
    string[] VisitaEstado = new string[cantidad];


    string opcion = "";
    //Llamo al metodo cargaInicialDeDatos
    cargaInicialDeDatos(VisitaNumero, VisitaDomicilio, VisitaNombreCliente, VisitaSintoma, VisitaCausaSolucion, VisitaEstado);



    do {
        //2.    Allí la aplicación armará una lista con las visitas pendientes (como se ve en la próxima imagen). 
        //Si no hubiera ninguna mostrará el mensaje “No hay visitas pendientes”. 
        //Si las hubiera mostrará la lista mientras solicita el número de visita a registrar:
        string listaVisitasPendientes = listarVisitasPendientes(VisitaNumero, VisitaDomicilio, VisitaNombreCliente, VisitaSintoma, VisitaEstado);
        long visitaARegistrar;
        if (listaVisitasPendientes=="") {
            Console.WriteLine("No hay visitas pendientes");
        } else {
            visitaARegistrar = pedirLong("Ingrese numero de visita:\nNumero\tDomicilio\tCliente\tProblema\n" + listaVisitasPendientes, 1, 999999);
            //3.	Una vez que el técnico ingresa el número de visita deseada, la aplicación buscará la visita por su número.
            int indice = buscarLong(VisitaNumero, visitaARegistrar);
            //4.	Si no existen visitas con ese número, la aplicación informará la situación al usuario y salteará los pasos siguientes hasta el número 9, donde se consulta al usuario si quiere continuar en la aplicación.
            if (indice==-1) {
                Console.WriteLine("No existen visitas con el numero ingresado");
            } else {
                //5.	Si existe una visita con ese número, pero la misma no está pendiente, la aplicación informará la situación al usuario y salteará los pasos siguientes hasta el número 9, donde se consulta al usuario si quiere continuar en la aplicación.
                if (VisitaEstado[indice]!=EstPend) {
                    Console.WriteLine("La visita no esta pendiente");
                } else {
                    //6.	Satisfechas estas validaciones, la aplicación solicitará confirmación para proceder o no a completar la visita seleccionada (“S” o “N”):
                    string completarVisita = pedirSoN("Desea completar la visita? (S/N)");
                    if (completarVisita=="S") {
                        //7.	Si responde afirmativamente, la aplicación solicitará la causa y solución, registrará este dato en la visita junto con el cambio de estado, y mostrará el mensaje “Completó satisfactoriamente la visita” y salteará los pasos siguientes hasta el número 9, donde se consulta al usuario si quiere continuar en la aplicación.
                        VisitaEstado[indice] = EstVisi;
                        VisitaCausaSolucion[indice] = pedirStringNoVacio("Ingrese la causa y la solucion");
                    } else {
                        //8.	Si responde negativamente, la aplicación mostrará el mensaje “Eligió NO completar esta visita”, y continuará con el paso siguiente.
                        Console.WriteLine("Eligió NO completar esta visita");
                    }
                }
            }
        }
        opcion = pedirSoN("Desea continuar? (S/N)");
    } while (opcion=="S");
}

//Declaracion del metodo cargaInicialDeDatos
void cargaInicialDeDatos(long[] visitaNumero, string[] visitaDomicilio, string[] visitaNombreCliente, string[] visitaSintoma, string[] visitaCausaSolucion, string[] visitaEstado)
{
    visitaNumero[0] = 1010;
    visitaDomicilio[0] = "Peron 3322";
    visitaNombreCliente[0] = "JJ LOPEZ";
    visitaSintoma[0] = "No enfria";
    visitaCausaSolucion[0] = "";
    visitaEstado[0] = EstPend;
    visitaNumero[1] = 1011;
    visitaDomicilio[1] = "Calle falsa 123";
    visitaNombreCliente[1] = "NO TIENE";
    visitaSintoma[1] = "No se si se apaga la luz al cerrar la puerta";
    visitaCausaSolucion[1] = "";
    visitaEstado[1] = EstPend;
}
//Requiere los datos de las visitas y asegura un string con las visitas pendientes. Si no hay ninguna devuelve cadena vacia
string listarVisitasPendientes(long[] visitaNumero, string[] visitaDomicilio, string[] visitaNombreCliente, string[] visitaSintoma, string[] visitaEstado)
{
    string retorno = "";
    for (int fila=0;fila<=visitaNumero.GetUpperBound(0);fila++) {
        if (visitaEstado[fila]==EstPend) {
            retorno = retorno + visitaNumero[fila] + "\t" + visitaDomicilio[fila] + "\t" + visitaNombreCliente[fila]
                + "\t" + visitaSintoma[fila] + "\n";
        }
    }
    return(retorno);
}

long pedirLong(string mensaje, int minimo, int maximo)
{
    long retorno = 0;
    do {
        Console.WriteLine(mensaje);
        if (!long.TryParse(Console.ReadLine(), out retorno)) {
            Console.WriteLine("Debe ingresar un numero entero");
            retorno = minimo - 1;
        } else {
            if (retorno < minimo || retorno > maximo) {
                Console.WriteLine("El numero debe estar entre " + minimo + " y " + maximo);
            }
        }
    } while (retorno < minimo || retorno > maximo);
    return(retorno);
}
//Requiere: un arreglo de numeros long y un long a buscar. Asegura la fila donde se encuentra el long a buscar
//o -1 si dicho long no se encuentra en el arreglo
int buscarLong(long[] visitaNumero, long visitaARegistrar)
{
    int retorno = -1;
    for (int fila=0;fila<=visitaNumero.GetUpperBound(0)&&retorno==-1;fila++) {
        if (visitaNumero[fila]==visitaARegistrar) {
            retorno = fila;
        }
    }
    return(retorno);
}

string pedirStringNoVacio(string mensaje)
{
    string retorno = "";
    do {
        Console.WriteLine(mensaje);
        retorno = Console.ReadLine().ToUpper();
        if (retorno == "") {
            Console.WriteLine("Debe ingresar un valor");
        }
    } while (retorno == "");
    return(retorno);
}

string pedirSoN(string mensaje)
{
    string retorno = "";
    do {
        retorno = pedirStringNoVacio(mensaje);
        if (retorno!="S"&&retorno!="N") {
            Console.WriteLine("Debe ingresar un valor que sea S o N");
        }
    } while (retorno!="S"&&retorno!="N");
    return(retorno);
}