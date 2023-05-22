// See https://aka.ms/new-console-template for more information


Console.WriteLine("Hello, World!");
main();
void main() {
    List<Comprobante> comprobantes = new List<Comprobante>();
    comprobantes.Add(new Factura(DateTime.Now.ToString(), 100, "A", 1, 1, "CCC"));
    comprobantes.Add(new NotaDeCredito(DateTime.Now.ToString(), 90, "A", 1, 1, "CCC"));
    comprobantes.Add(new Recibo(DateTime.Now.ToString(), 10, "0001/001", 1));

    double saldoFinal = 0;
    Console.WriteLine(saldoFinal);
    foreach (Comprobante item in comprobantes) {
        Console.WriteLine(item);
        saldoFinal = saldoFinal + item.getImporte();
    }
    Console.WriteLine(saldoFinal);
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

    List<MediosDeCobro> mediosDeCobro;

    protected MovimientoDeFondos(string fecha, string signo, double importe, string talonario, int numero) : base(fecha, signo, importe)
    {
        this.talonario = talonario;
        this.numero = numero;
        mediosDeCobro = new List<MediosDeCobro>();
    }

    public void agregarMedioDeCobro(MediosDeCobro aAgregar) {
        mediosDeCobro.Add(aAgregar);
    }

    public override string ToString()
    {
        return base.ToString() + "\t" + talonario + "\t" + numero;
    }
}

abstract class MediosDeCobro {

    private double importe;

    public MediosDeCobro(double importe) {
        this.importe = importe;
    }
}

class Recibo : MovimientoDeFondos
{
    public Recibo(string fecha, double importe, string talonario, int numero) : base(fecha, "H", importe, talonario, numero)
    {
    }

    public override string ToString()
    {
        return "Recibo\t" + base.ToString();
    }
}