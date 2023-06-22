// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

OrdenDeCompra coturel;

coturel.intentarAgregarOperacion(operacion);

abstract class Orden {

    private string fecha;
    private string especie;
    private int cantidadLimite;
    private double precioLimite;

    private List<Operacion> operaciones;

    protected Orden(string fecha, string especie, int cantidadLimite, double precioLimite)
    {
        this.fecha = fecha;
        this.especie = especie;
        this.cantidadLimite = cantidadLimite;
        this.precioLimite = precioLimite;
        this.operaciones = new List<Operacion>();
    }

    protected double getPrecioLimite() {
        return(precioLimite);
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public int cantidadPendiente() {
        int pendiente = cantidadLimite;
        foreach (Operacion operacion in operaciones) {
            pendiente = pendiente - operacion.getCantidadOperada();
        }
        return(pendiente);
    }

    public bool estaPendiente() {
        return(cantidadPendiente()!=0);
    }
    /*
     *
     Metodo que devuelve 0 si no hay problemas y 1 si el error es que la cantidad operada es mayor a la pendiente
     */
    private List<String> puedeAgregarOperacion(Operacion operacion) {
        List<String> retorno = new List<String>();
        if (operacion.getCantidadOperada()>cantidadPendiente()) {
            retorno.Add("La cantidad de la operacion supera a la cantidad limite");
        }
        return(retorno);
    }
    //Template method
    //Metodo plantilla
    //Metodo que tiene operaciones privadas y operaciones abstractas, dejando parte de la implementacion para las clases derivadas, protegiendo una parte
    //Del codigo de la clase base
    public List<String> intentarAgregarOperacion(Operacion operacion) {
        List<String> retorno = puedeAgregarOperacion(operacion);
        retorno.AddRange(despuesDeValidarClaseBase(operacion));
        if (retorno.Count==0) {
            operaciones.Add(operacion);
        }
        return(retorno);
    }

    protected abstract List<String> despuesDeValidarClaseBase(Operacion operacion);
}

class OrdenDeCompra : Orden
{
    public OrdenDeCompra(string fecha, string especie, int cantidadLimite, double precioLimite) : base(fecha, especie, cantidadLimite, precioLimite)
    {
    }

    protected override List<String> despuesDeValidarClaseBase(Operacion operacion)
    {
        List<String> resultadoEvaluacionBase = new List<string>();
        if (!(operacion is Compra)&&!(operacion is Cancelacion)) {
            resultadoEvaluacionBase.Add("La operacion es de tipo venta y solo se admiten compra o cancelacion");
        }
        if (operacion is Compra compra && compra.getPrecio()>this.getPrecioLimite()) {
            resultadoEvaluacionBase.Add("El precio esta fuera de rangos validos");
        }
        return(resultadoEvaluacionBase);
    }

}

internal class Cancelacion : Operacion
{
    public Cancelacion(string fecha, string especie, int cantidadOperada) : base(fecha, especie, cantidadOperada)
    {
    }
}

abstract class Operacion {

    private string fecha;
    private string especie;
    private int cantidadOperada;

    protected Operacion(string fecha, string especie, int cantidadOperada)
    {
        this.fecha = fecha;
        this.especie = especie;
        this.cantidadOperada = cantidadOperada;
    }

    public int getCantidadOperada() {
        return(cantidadOperada);
    }

}

class Compra : Operacion {

    private double precio;

    public Compra(string fecha, string especie, int cantidadOperada, double precio) : base(fecha, especie, cantidadOperada)
    {
        this.precio = precio;
    }

    public double getPrecio() {
        return(precio);
    }
}