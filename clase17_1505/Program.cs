// See https://aka.ms/new-console-template for more information


Console.WriteLine("Hello, World!");
main();
void main() {
    List<Comprobante> comprobantes = new List<Comprobante>();
    comprobantes.Add(new Factura(DateTime.Now.ToString(), 100, "A", 1, 1, "CCC"));
    comprobantes.Add(new NotaDeCredito(DateTime.Now.ToString(), 90, "A", 1, 1, "CCC"));
    comprobantes.Add(new Recibo(DateTime.Now.ToString(), 10, "0001/001", 1));

    string continuarCarga = "";

    do {
        Comprobante aIngresar = null;
        do {
            aIngresar = pedirComprobante();
            if (comprobantes.Contains(aIngresar)) {
                Console.WriteLine("El comprobante ya existe");
            }
        } while (aIngresar==null&&comprobantes.Contains(aIngresar));
        comprobantes.Add(aIngresar);
        continuarCarga = pedirStringNoVacio("Ingrese S o N");
    } while (continuarCarga=="S");

    double saldoFinal = 0;
    Console.WriteLine(saldoFinal);
    foreach (Comprobante item in comprobantes) {
        Console.WriteLine(item);
        saldoFinal = saldoFinal + item.getImporte();
    }
    Console.WriteLine(saldoFinal);
}

Comprobante? pedirComprobante()
{
    Comprobante retorno = null;
    string opcion = "";
    do {
        opcion = pedirStringNoVacio("Ingrese A si desea cargar un comprobante emitido y B si desea cargar un mov. de fondos");
        if (opcion!="A"&&opcion!="B") {
            Console.WriteLine("Ingrese A o B");
        }
    } while (opcion!="A"&&opcion!="B");
    if (opcion=="A") {
        retorno = pedirComprobanteEmitido();
    } else if (opcion=="B") {
        retorno = pedirMovimientoDeFondos();
    }
    return(retorno);
}

MovimientoDeFondos? pedirMovimientoDeFondos()
{
    string fecha; double importe; string talonario; int numero;
    string tipo = "";
    MovimientoDeFondos retorno = null;
    fecha = DateTime.Now.ToString();
    importe = pedirDouble("Ingrese importe", 1, 999999);
    numero = pedirInteger("Ingrese numero", 1, 999999);
    talonario = pedirStringNoVacio("Ingrese talonario");
    do {
        tipo = pedirStringNoVacio("Ingrese RC para recibo y OPC para reembolso");
    } while (tipo!="RC"&&tipo!="OPC");
    if (tipo=="RC") {
        retorno = new Recibo(fecha, importe, talonario, numero);
    } else if (tipo=="OPC") {
        retorno = new Reembolso(fecha, importe, talonario, numero);
    }
    return(retorno);

}

ComprobanteEmitido? pedirComprobanteEmitido()
{
    string fecha; double importe; string letra; int puntoDeVenta; int numero; string cae;
    ComprobanteEmitido retorno = null;
    string tipo = "";
    fecha = DateTime.Now.ToString();
    puntoDeVenta = pedirInteger("Ingrese punto de venta", 1, 10);
    numero = pedirInteger("Ingrese numero de comprobante", 1, 999999);
    letra = pedirStringNoVacio("Ingrese letra");
    cae = pedirStringNoVacio("Ingrese CAE");
    importe = pedirDouble("Ingrese importe", 1, 999999);
    do {
        tipo = pedirStringNoVacio("Ingrese FC para factura, ND para nota de debito, NC para nota de credito");
    } while (tipo!="FC"&&tipo!="ND"&&tipo!="NC"&&tipo!="AJ");
    if (tipo=="FC") {
        retorno = new Factura(fecha, importe, letra, puntoDeVenta, numero, cae);
    } else if (tipo=="ND") {
        retorno = new NotaDeDebito(fecha, importe, letra, puntoDeVenta, numero, cae);
    } else if (tipo=="NC") {
        retorno = new NotaDeCredito(fecha, importe, letra, puntoDeVenta, numero, cae);
    } else if (tipo=="AJ") {
        try {
            retorno = new Ajuste(fecha, importe, "CAPRICORNIO", letra, puntoDeVenta, numero, cae);
        } catch (ArgumentException e) {
            Console.WriteLine(e.Message);
            Console.WriteLine("No se podra agregar el ajuste porque el programador fue reemplazado por un gemelo malvado");
        } finally {
            retorno = new Ajuste(fecha, importe, "D", letra, puntoDeVenta, numero, cae);
        }
        
    }
    return(retorno);
}

double pedirDouble(string mensaje, int minimo, int maximo)
{
    double retorno = minimo -1;
    do {
        Console.WriteLine(mensaje);
        if (!Double.TryParse(Console.ReadLine(), out retorno)) {
            Console.WriteLine("Debe ingresar un numero");
        } else {
            if (retorno < minimo && retorno > maximo) {
                Console.WriteLine("Fuera de rango");
            }
        }
    } while (retorno < minimo && retorno > maximo);
    return(retorno);
}

int pedirInteger(string mensaje, int minimo, int maximo)
{
    int retorno = minimo -1;
    do {
        Console.WriteLine(mensaje);
        if (!Int32.TryParse(Console.ReadLine(), out retorno)) {
            Console.WriteLine("Debe ingresar un numero");
        } else {
            if (retorno < minimo && retorno > maximo) {
                Console.WriteLine("Fuera de rango");
            }
        }
    } while (retorno < minimo && retorno > maximo);
    return(retorno);
}

string pedirStringNoVacio(string mensaje)
{
    string retorno = "";
    do {
        Console.WriteLine(mensaje);
        retorno = Console.ReadLine();
        if (retorno=="") {
            Console.WriteLine("Debe ingresar un dato");
        }
    } while (retorno == "");
    return(retorno);
}

class Factura : ComprobanteEmitido
{
    public Factura(string fecha, double importe, string letra, int puntoDeVenta, int numero, string cae) 
    : base(fecha, importe, "D", letra, puntoDeVenta, numero, cae)
    {
    }

    public override string ToString()
    {
        return "Factura\t\t" + base.ToString();
    }
}

class NotaDeCredito : ComprobanteEmitido {

    public NotaDeCredito(string fecha, double importe, string letra, int puntoDeVenta, int numero, string cae) 
    : base(fecha, importe, "H", letra, puntoDeVenta, numero, cae)
    {
    }

    public override string ToString()
    {
        return "Nota de credito\t" + base.ToString();
    }

}

class Ajuste : ComprobanteEmitido {

    public Ajuste(string fecha, double importe, string signo, string letra, int puntoDeVenta, int numero, string cae) 
    : base(fecha, importe, signo, letra, puntoDeVenta, numero, cae)
    {
    }

    public override string ToString()
    {
        return "Nota de credito\t" + base.ToString();
    }

}

class NotaDeDebito : ComprobanteEmitido {

    public NotaDeDebito(string fecha, double importe, string letra, int puntoDeVenta, int numero, string cae) 
    : base(fecha, importe, "H", letra, puntoDeVenta, numero, cae)
    {
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj) && obj is NotaDeDebito;
    }

    public override string ToString()
    {
        return "Nota de debito\t" + base.ToString();
    }

}

abstract class Comprobante {

    private string fecha;
    private string signo;
    private double importe;

    protected Comprobante(string fecha, string signo, double importe)
    {
        if (signo!="D"&&signo!="H") {
            throw new ArgumentException("El signo solo puede D o H");
        }
        
        this.fecha = fecha;
        this.signo = signo;
        this.importe = importe;
    }

    public double getImporte() {
        return(signo == "D" ? importe : importe * -1);
    }

    public override string ToString()
    {
        return fecha + "\t" + signo + "\t" + importe;
    }
}


abstract class ComprobanteEmitido : Comprobante {

    private string letra;
    private int puntoDeVenta;
    private int numero;
    private string cae;

    protected ComprobanteEmitido(string fecha, double importe, string signo, string letra, int puntoDeVenta, int numero, string cae)
     : base(fecha, signo, importe)
    {
        this.letra = letra;
        this.puntoDeVenta = puntoDeVenta;
        this.numero = numero;
        this.cae = cae;
    }

    public override bool Equals(object? obj)
    {
        return obj is ComprobanteEmitido emitido &&
               letra == emitido.letra &&
               puntoDeVenta == emitido.puntoDeVenta &&
               numero == emitido.numero;
    }

    public override string ToString()
    {
        return base.ToString() + "\t" + letra + "-" + puntoDeVenta + "-" + numero;
    }

    
}

abstract class MovimientoDeFondos : Comprobante {

    string talonario;
    int numero;

    List<MedioDeCobro> mediosDeCobro;

    protected MovimientoDeFondos(string fecha, string signo, double importe, string talonario, int numero) : base(fecha, signo, importe)
    {
        this.talonario = talonario;
        this.numero = numero;
        mediosDeCobro = new List<MedioDeCobro>();
    }

    public void agregarMedioDeCobro(MedioDeCobro aAgregar) {
        mediosDeCobro.Add(aAgregar);
    }

    public override string ToString()
    {
        return base.ToString() + "\t" + talonario + "\t" + numero;
    }

    public double getTotalDeMediosDeCobro() {
        double retorno = 0;
        foreach (MedioDeCobro medio in mediosDeCobro) {
            retorno = retorno + medio.getImporte();
        }
        return(retorno);
    }
}

class Recibo : MovimientoDeFondos
{
    public Recibo(string fecha, double importe, string talonario, int numero) : base(fecha, "H", importe, talonario, numero)
    {
    }

    public override string ToString()
    {
        return "Recibo\t\t" + base.ToString();
    }
}

class Reembolso : MovimientoDeFondos
{
    public Reembolso(string fecha, double importe, string talonario, int numero) : base(fecha, "D", importe, talonario, numero)
    {
    }

    public override string ToString()
    {
        return "Reembolso\t\t" + base.ToString();
    }
}

interface MedioDeCobro {

    double getImporte();

}

class Efectivo : MedioDeCobro
{

    private double importe;

    public Efectivo(double importe)
    {
        this.importe = importe;
    }

    public double getImporte()
    {
        return(importe);
    }
}

class Cheque : MedioDeCobro {

    private double importe;
    private string bancoEmisor;
    private string fechaDeCobro;

    private string nombreEmisor;

    public Cheque(double importe, string bancoEmisor, string fechaDeCobro, string nombreEmisor)
    {
        this.importe = importe;
        this.bancoEmisor = bancoEmisor;
        this.fechaDeCobro = fechaDeCobro;
        this.nombreEmisor = nombreEmisor;
    }

    public double getImporte()
    {
        return(importe);
    }
}