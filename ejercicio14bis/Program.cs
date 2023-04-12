/*

Un algoritmo REQUIERE X, ASEGURA Y y HACE Z

1. Pedir y validar dato numerico

*/

// Queremos un programa para cargar facturas.
Console.WriteLine("Bienvenido/a al programa de carga de facturas");
// De cada factura voy a pedir numero de factura, importe.
int numeroFactura, numeroFacturaMenorImporte, numeroFacturaMayorImporte, cantidadFacturas = 0;
double importeFactura, minimoImporteFactura = 1000000, maximoImporteFactura = 0, importeTotal = 0, ticketPromedio = 0;
string continuar = "";
do {
    // Voy a pedir tantas facturas como el usuario quiera.
    // Y cuando el usuario, ante la pregunta "Desea cargar mas facturas?" diga que NO, voy a mostrar en pantalla:
    numeroFactura = 0;
    importeFactura = 0;
    // Pedir numero de factura
    while (numeroFactura <= 0) {
        Console.WriteLine("Ingrese numero de factura");
        bool pudeConvertir = Int32.TryParse(Console.ReadLine(), out numeroFactura);
        if (!pudeConvertir) {
            //LO que hacemos si la condicion es verdadera
            Console.WriteLine("Debe ingresar un numero entero valido");
            numeroFactura = 0;
        } else {
            //Lo que hacemos si la condicion es falsa
            if (numeroFactura <= 0) {
                Console.WriteLine("Debe ingresar un numero mayor a 0");
            }
        }
    }
    // Si el numero ingresado es menor o igual a cero informar error y volver a pedirlo

    // Pedir el importe
    do {
        Console.WriteLine("Ingrese el importe de factura");
        bool pudeConvertir = Double.TryParse(Console.ReadLine(), out importeFactura);
        if (!pudeConvertir) {
            Console.WriteLine("Debe ingresar un numero decimal valido");
        } else if (importeFactura <= 0) {
            Console.WriteLine("Debe ingresar un importe mayor a 0");
        }
    } while (importeFactura <= 0);
    // Si el importe ingresado es menor o igual a cero informar error y volver a pedirlo

    //Si el importe es mayor al maximo, actualizar numero e importe maximo
    if (importeFactura>maximoImporteFactura) {
        maximoImporteFactura = importeFactura;
        numeroFacturaMayorImporte = numeroFactura;
    }

    //Si el importe es menor al minimo, actualizar numero e importe minimo
    if (importeFactura<minimoImporteFactura) {
        minimoImporteFactura = importeFactura;
        numeroFacturaMenorImporte = numeroFactura;
    }

    //Actualizar total, cantidad y promedio
    importeTotal = importeTotal + importeFactura;
    cantidadFacturas = cantidadFacturas + 1;
    ticketPromedio = importeTotal / cantidadFacturas;

    //Pedir si desea continuar
    do {
        Console.WriteLine("Ingrese SI para cargar mas facturas y NO para no continuar la carga");
        continuar = Console.ReadLine().ToUpper();
        if (continuar!="SI"&&continuar!="NO") {
            Console.WriteLine("Debe ingresar SI o NO.");
        }
    } while (continuar!="SI"&&continuar!="NO");
    //Si el usuario informar algo distinto a SI o NO, informar error y volver a pedir

} while (continuar=="SI");
// Importe y numero de la factura de mayor importe
// Importe y numero de la factura de menor importe
// Cantidad de facturas
// Importe total facturado
// Ticket promedio

/*

    Estructuras de repeticion: while - do while - falta ver el for
    Estructuras condicionales: if - else - else if - falta ver el switch
    Planteo conceptual de algoritmos
    El algoritmo de solicitud y validacion de datos

*/