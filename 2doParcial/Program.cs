AppInmuebles app = new AppInmuebles();
app.ejecutar();

class AppInmuebles{
    List<Inmueble> inmuebles;
    List<Concepto> conceptos;
    Validador validador;
    public AppInmuebles() {
        inmuebles = new List<Inmueble>();
        conceptos = new List<Concepto>();
        validador = new Validador();
    }
    public void ejecutar () {
        string opcion = "";
        do {
            opcion = validador.pedirStringNoVacio("Ingrese opcion de menu:\nA - Nuevo Inmueble\nB - Nuevo Concepto\nC - Mostrar listado para presupuesto"
            + "\nD Salir");
            if (opcion=="A") {
                ingresarInmueble();
            } else if (opcion=="B") {
                ingresarConcepto();
            } else if (opcion=="C") {
                listadoParaPresupuesto();
            }
        } while (opcion!="D");
    }

    private void listadoParaPresupuesto() {
        foreach (Inmueble inmueble in inmuebles)
        {
            Console.WriteLine($"Código: {inmueble.Codigo}");
            Console.WriteLine($"Nombre: {inmueble.Nombre}");
            Console.WriteLine($"Amortización: {(inmueble is InmueblePropio ? ((InmueblePropio)inmueble).Amortizacion.ToString("C") : "-")}");
            Console.WriteLine($"Alquiler: {(inmueble is InmuebleDeTerceros ? ((InmuebleDeTerceros)inmueble).FeeMensualAlquiler.ToString("C") : "-")}");
            Console.WriteLine($"Gastos adicionales: {inmueble.CalcularGastoAdicional().ToString("C")}");
            Console.WriteLine("--------------------------------------------------");
        }
    }

    private void ingresarConcepto() {
        string codigo = validador.pedirStringNoVacio("Ingrese el código del concepto:");
        string nombre = validador.pedirStringNoVacio("Ingrese el nombre del concepto:");
        bool aplicaAPropios = validador.pedirBool("¿Aplica a inmuebles propios? (S/N):");
        bool aplicaATerceros = validador.pedirBool("¿Aplica a inmuebles de terceros? (S/N):");
        int mesesRestantesEjecucion = validador.pedirInteger("Ingrese la cantidad de meses restantes para la ejecución del concepto:");
        double importe = validador.pedirDouble("Ingrese el importe del concepto:");

        Concepto concepto = new Concepto(codigo, nombre, aplicaAPropios, aplicaATerceros, mesesRestantesEjecucion, importe);
        conceptos.Add(concepto);
        Console.WriteLine("Concepto agregado exitosamente.");
    }

    private void ingresarInmueble() {
        string tipoInmueble = validador.pedirStringNoVacio("Ingrese el tipo de inmueble (Propio o De Terceros):");
        // double valorInicial = validador.pedirDouble("Ingrese el valor inicial del inmueble:", 0, double.MaxValue);

        if (tipoInmueble.Equals("Propio", StringComparison.OrdinalIgnoreCase))
        {
            // Ingresar datos específicos del inmueble propio
            // Código, nombre, fecha de alta, valor inicial, meses de vida útil, meses restantes
            string codigo = validador.pedirStringNoVacio("Ingrese el código del inmueble propio:");
            string nombre = validador.pedirStringNoVacio("Ingrese el nombre del inmueble propio:");
            DateTime fechaAlta = validador.pedirFecha("Ingrese la fecha de alta del inmueble propio:");
            double valorInicial = validador.pedirDouble("Ingrese el valor inicial del inmueble propio:");
            int mesesVidaUtil = validador.pedirInteger("Ingrese los meses de vida útil del inmueble propio:");
            int mesesRestantes = validador.pedirInteger("Ingrese los meses restantes del inmueble propio:");

            InmueblePropio inmueblePropio = new InmueblePropio()
            {
                Codigo = codigo,
                Nombre = nombre,
                FechaAlta = fechaAlta,
                ValorInicial = valorInicial,
                MesesVidaUtil = mesesVidaUtil,
                MesesRestantes = mesesRestantes
            };

            if (inmuebles.Any(inmueble => inmueble.Codigo == codigo))
            {
                Console.WriteLine("El inmueble ya existe. Volviendo al menú principal...");
            }
            else
            {
                inmuebles.Add(inmueblePropio);
                Console.WriteLine("Inmueble propio agregado exitosamente.");
            }
        }
        else if (tipoInmueble.Equals("De Terceros", StringComparison.OrdinalIgnoreCase))
        {
            // Ingresar datos específicos del inmueble de terceros
            // Código, nombre, fecha de alta, valor inicial, meses de vida útil, meses restantes, fee mensual de alquiler, meses totales de alquiler
            string codigo = validador.pedirStringNoVacio("Ingrese el código del inmueble de terceros:");
            string nombre = validador.pedirStringNoVacio("Ingrese el nombre del inmueble de terceros:");
            DateTime fechaAlta = validador.pedirFecha("Ingrese la fecha de alta del inmueble de terceros:");
            double valorInicial = validador.pedirDouble("Ingrese el valor inicial del inmueble de terceros:");
            int mesesVidaUtil = validador.pedirInteger("Ingrese los meses de vida útil del inmueble de terceros:");
            int mesesRestantes = validador.pedirInteger("Ingrese los meses restantes del inmueble de terceros:");
            double feeMensualAlquiler = validador.pedirDouble("Ingrese el fee mensual de alquiler del inmueble de terceros:");
            int mesesTotalesAlquiler = validador.pedirInteger("Ingrese los meses totales de alquiler del inmueble de terceros:");

            InmuebleDeTerceros inmuebleDeTerceros = new InmuebleDeTerceros()
            {
                Codigo = codigo,
                Nombre = nombre,
                FechaAlta = fechaAlta,
                ValorInicial = valorInicial,
                MesesVidaUtil = mesesVidaUtil,
                MesesRestantes = mesesRestantes,
                FeeMensualAlquiler = feeMensualAlquiler,
                MesesTotalesAlquiler = mesesTotalesAlquiler
            };

            if (inmuebles.Any(inmueble => inmueble.Codigo == codigo))
            {
                Console.WriteLine("El inmueble ya existe. Volviendo al menú principal...");
            }
            else
            {
                inmuebles.Add(inmuebleDeTerceros);
                Console.WriteLine("Inmueble de terceros agregado exitosamente.");
            }
        }
        else
        {
            Console.WriteLine("Tipo de inmueble inválido. Volviendo al menú principal...");
        }
    }
    
    // Clase abstracta para representar un inmueble
    public abstract class Inmueble
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int MesesRestantes { get; set; }

        // Método abstracto para calcular el gasto adicional del inmueble
        public abstract double CalcularGastoAdicional();
    }

    // Clase concreta para representar un inmueble propio
    public class InmueblePropio : Inmueble
    {
        public DateTime FechaAlta { get; set; }
        public double ValorInicial { get; set; }
        public int MesesVidaUtil { get; set; }
        public string NombreLegalPropietario { get; set; }

        public double Amortizacion => ValorInicial / MesesVidaUtil;

        public override double CalcularGastoAdicional()
        {
            double totalGastosAdicionales = 0;
            // Aquí puedes implementar la lógica para calcular los gastos adicionales de los inmuebles propios
            return totalGastosAdicionales;
        }
    }

    // Clase concreta para representar un inmueble de terceros
    public class InmuebleDeTerceros : Inmueble
    {
        public double FeeMensualAlquiler { get; set; }
        public int MesesTotalesAlquiler { get; set; }
        public DateTime FechaAlta { get; internal set; }
        public double ValorInicial { get; internal set; }
        public int MesesVidaUtil { get; internal set; }

        public override double CalcularGastoAdicional()
        {
            double totalGastosAdicionales = 0;
            // Aquí puedes implementar la lógica para calcular los gastos adicionales de los inmuebles de terceros
            return totalGastosAdicionales;
        }
    }

    // Clase para representar un concepto
    public class Concepto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool AplicaAPropios { get; set; }
        public bool AplicaATerceros { get; set; }
        public int MesesRestantesEjecucion { get; set; }
        public double Importe { get; set; }

        public Concepto(string codigo, string nombre, bool aplicaAPropios, bool aplicaATerceros, int mesesRestantesEjecucion, double importe)
        {
            Codigo = codigo;
            Nombre = nombre;
            AplicaAPropios = aplicaAPropios;
            AplicaATerceros = aplicaATerceros;
            MesesRestantesEjecucion = mesesRestantesEjecucion;
            Importe = importe;
        }

        public override string ToString()
        {
            return $"Código: {Codigo}\n" +
                $"Nombre: {Nombre}\n" +
                $"Aplica a propios: {AplicaAPropios}\n" +
                $"Aplica a terceros: {AplicaATerceros}\n" +
                $"Meses restantes para ejecución: {MesesRestantesEjecucion}\n" +
                $"Importe: {Importe}\n";
        }
    }


    class Validador
    {
        internal double pedirDouble(string mensaje, double minimo, double maximo)
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

        internal string pedirStringNoVacio(string mensaje) {
            string valor = "";
            do {
                Console.WriteLine(mensaje);
                valor = Console.ReadLine();
                if (valor=="") {
                    Console.WriteLine("Debe ingresar un dato");
                }
            } while (valor=="");
            return valor;
        }

        internal int pedirInteger (string mensaje, int minimo, int maximo) {
            int valor = minimo -1;
            do {
                Console.WriteLine(mensaje);
                if (!Int32.TryParse(Console.ReadLine(), out valor)) {
                    Console.WriteLine("Debe ingresar un numero");
                } else {
                    if (valor < minimo && valor > maximo) {
                    Console.WriteLine("Fuera de rango");
            }
        }
            } while (valor < minimo && valor > maximo);
            return valor;
        }

        internal DateTime pedirFecha(string mensaje) {
            DateTime valor = DateTime.Now;
            do {
                Console.WriteLine(mensaje);
                string ingreso = Console.ReadLine();
                try {
                    valor = Convert.ToDateTime(ingreso);
                } catch (Exception e) {
                    Console.WriteLine("Error: " + e.Message);
                }
            } while (valor==DateTime.Now);
            return valor;
        }

        internal bool pedirBool (string mensaje) {
            bool valor = false;
            do {
                Console.WriteLine(mensaje);
                string ingreso = Console.ReadLine();
                try {
                    valor = Convert.ToBoolean(ingreso);
                } catch (Exception e) {
                    Console.WriteLine("Error: " + e.Message);
                }
            } while (valor==false);
            return valor;
        }

        public Concepto pedirConcepto (string codigo)
        {
            Concepto retorno;
            retorno = new Concepto(codigo);
            return retorno;
        }

        public bool existeGasto(Concepto conceptoABuscar, List<Concepto> concepto)
        {
            bool retorno ;
            retorno = concepto.Contains(conceptoABuscar);
            return retorno;
        }
    }
}