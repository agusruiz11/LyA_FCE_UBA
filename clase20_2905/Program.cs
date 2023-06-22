// See https://aka.ms/new-console-template for more information
AppPortfolio app = new AppPortfolio();
app.ejecutar();

class AppPortfolio {

    private List<String> especies;
    private List<Orden> ordenes;
    private Validador validador;

    public AppPortfolio() {
        especies = new List<String>();
        ordenes = new List<Orden>();
        validador = new Validador();
        especies.Add("MSFT");
        especies.Add("AAPL");
        especies.Add("KO");
        especies.Add("MCD");
    }

    public void ejecutar() {
        string opcion = "";
        do {
            opcion = validador.pedirStringNoVacio("Ingrese opcion");
            if (opcion=="1") {
                mostrarPortfolio();
            } else if (opcion=="2") {
                mostrarPendientes();
            } else if (opcion=="3") {
                ingresarOrden();
            }
        } while (opcion!="4");
    }

    private void ingresarOrden()
    {
        ordenes.Add(validador.ingresarOrden(especies));
    }

    private void mostrarPendientes()
    {
        //Estilo manipulacion de colecciones, sin bucles
        ordenes
            .FindAll(orden => orden.estaPendiente())
            .ForEach(Console.WriteLine);
        //Estilo mas cercano al estructurado
        foreach (Orden orden in ordenes) {
            if (orden.estaPendiente()) {
                Console.WriteLine(orden);
            }
        }

        List<Orden> ordenesFiltradas = new List<Orden>();
        //Esto es el FindAll
        foreach (Orden orden in ordenes) {
            if (orden.estaPendiente()) {
                ordenesFiltradas.Add(orden);
            }
        }
        //Esto es el ForEach
        foreach (Orden orden in ordenesFiltradas) {
            Console.WriteLine(orden);
        }
    }

    private void mostrarPortfolio()
    {
        especies.ForEach(mostrarDetalleDeEspecie);
        String[] espec = new String[10];
//        foreach (String especie in especies) {
//            mostrarDetalleDeEspecie(especie);
//        }
    }

    private void mostrarDetalleDeEspecie(string especie)
    {
        int cantidad = 0;
        double subtotal = 0;
        foreach (Orden orden in ordenes) {
            if (orden.esDeEspecie(especie)) {
                cantidad = cantidad + orden.cantidadOperada() * orden.signo();
                subtotal = subtotal + orden.importeOperado() * orden.signo();
            }
        }
        Console.WriteLine(especie + "\t" + cantidad + "\t" + subtotal);
    }
}

internal class Validador
{


    public Orden ingresarOrden(List<String> especiesDisponibles) {
        Orden retorno = null;
        string tipo = "";
        string especie = "";
        int cantidad = 0;
        double precioLimite = 0;
        do {
            tipo = pedirStringNoVacio("Ingrese C para compra o V para venta");
            if (tipo!="C"&&tipo!="V") {
                Console.WriteLine("Debe ingresar C o V");
            }
        } while (tipo!="C"&&tipo!="V");
        
        do {
            especie = pedirStringNoVacio("Ingrese especie");
            if (!especiesDisponibles.Contains(especie)) {
                Console.WriteLine("Especie invalida");
            }
        } while (!especiesDisponibles.Contains(especie));
        cantidad = pedirInteger("Ingrese cantidad", 1, 9999);
        precioLimite = pedirDouble("Ingrese precio limite", 1, 9999);
        if (tipo=="C") {
            retorno = new OrdenDeCompra(DateTime.Now.ToString(), especie, cantidad, precioLimite);
        } else if (tipo=="V") {
            retorno = new OrdenDeVenta(DateTime.Now.ToString(), especie, cantidad, precioLimite);
        } else {
            throw new InvalidDataException("No se puede instanciar la orden acorde a los dato");
        }
        return(retorno);
    }

    private double pedirDouble(string mensaje, double minimo, double maximo)
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

    internal string pedirStringNoVacio(string mensaje)
    {
            string retorno = "";
        do {
            Console.WriteLine(mensaje);
            retorno = Console.ReadLine();
                if (retorno=="") {
                Console.WriteLine("Debe ingresar un dato");
                }
        } while (retorno == "");
        return(retorno.ToUpper());
    }

    private int pedirInteger(string mensaje, int minimo, int maximo)
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

}

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

    public override string? ToString()
    {
        return fecha + "\t" + especie + "\t" + cantidadLimite + "\t" + precioLimite + "\t" + cantidadPendiente();
    }

    public int cantidadPendiente()
    {
        int pendiente = cantidadLimite;
        pendiente = pendiente - cantidadOperada() - cantidadCancelada();
        return (pendiente);
    }

    public int cantidadOperada()
    {
        int operada = 0;
        foreach (Operacion operacion in operaciones)
        {
            if (!(operacion is Cancelacion)) {
                operada = operada + operacion.getCantidadOperada();
            }
            
        }

        return operada;
    }

    public int cantidadCancelada()
    {
        int operada = 0;
        foreach (Operacion operacion in operaciones)
        {
            if ((operacion is Cancelacion)) {
                operada = operada + operacion.getCantidadOperada();
            }
            
        }

        return operada;
    }

    public double importeOperado() {
        double operada = 0;
        foreach (Operacion operacion in operaciones)
        {
            operada = operada + operacion.getImporteOperado();
        }

        return operada;
    }

    public bool estaPendiente() {
        return(cantidadPendiente()!=0);
    }

    public bool esDeEspecie(string especieAComparar) {
        return(this.especie==especieAComparar);
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
    internal abstract int signo();
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

    internal override int signo()
    {
        return(1);
    }
}

class OrdenDeVenta : Orden
{
    public OrdenDeVenta(string fecha, string especie, int cantidadLimite, double precioLimite) : base(fecha, especie, cantidadLimite, precioLimite)
    {
    }

    protected override List<String> despuesDeValidarClaseBase(Operacion operacion)
    {
        List<String> resultadoEvaluacionBase = new List<string>();
        if (!(operacion is Venta)&&!(operacion is Cancelacion)) {
            resultadoEvaluacionBase.Add("La operacion es de tipo compra y solo se admiten compra o cancelacion");
        }
        if (operacion is Venta venta && venta.getPrecio()<this.getPrecioLimite()) {
            resultadoEvaluacionBase.Add("El precio esta fuera de rangos validos");
        }
        return(resultadoEvaluacionBase);
    }

    internal override int signo()
    {
        return(-1);
    }
}

internal class Venta : Operacion
{
    private double precio;

    public Venta(string fecha, string especie, int cantidadOperada, double precio) : base(fecha, especie, cantidadOperada)
    {
        this.precio = precio;
    }

    public override double getImporteOperado()
    {
        return(this.getCantidadOperada()*precio);
    }

    public double getPrecio() {
        return(precio);
    }
}

internal class Cancelacion : Operacion
{
    public Cancelacion(string fecha, string especie, int cantidadOperada) : base(fecha, especie, cantidadOperada)
    {
    }

    public override double getImporteOperado()
    {
        return(0);
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

    public abstract double getImporteOperado();
}

class Compra : Operacion {

    private double precio;

    public Compra(string fecha, string especie, int cantidadOperada, double precio) : base(fecha, especie, cantidadOperada)
    {
        this.precio = precio;
    }

    public override double getImporteOperado()
    {
        return(precio*getCantidadOperada());
    }

    public double getPrecio() {
        return(precio);
    }
}