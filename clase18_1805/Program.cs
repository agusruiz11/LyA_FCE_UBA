// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

AppCanciones app = new AppCanciones();
app.ejecutar();

class AppCanciones {

    private List<Cancion> disponibles;
    private List<Cancion> reproduccion;
    private Validador validador;

    public AppCanciones()
    {
        disponibles = new List<Cancion>();
        reproduccion = new List<Cancion>();
        validador = new Validador();
        disponibles.Add(new Cancion("November Rain", "Guns & Roses", 1991));
        disponibles.Add(new Cancion("Africa", "Toto", 1980));
        disponibles.Add(new Cancion("Jump", "Van Halen", 1991));
    }

    public void ejecutar() {
        string opcion = "";
        do {
            opcion = validador.pedirStringNoVacio("Ingrese 1 para agregar cancion a la LR, 2 para rep, 3 para salir");
            if (opcion=="1") {
                agregarCancionAListaReproduccion();
            } else if (opcion=="2") {
                reproducirProximaCancion();
            }
        } while (opcion!="3");
    }

    private void reproducirProximaCancion()
    {
        //Si no hay cancion, mensaje de que no hay
        //Muestra proxima cancion
        //Saca la cancion de la lista de repro y la manda a la lista de disponibles
        if (reproduccion.Count==0) {
            Console.WriteLine("No hay nada en la lista de reproduccion");
        } else {
            Cancion aRemover = reproduccion[0];
            reproduccion.RemoveAt(0);
            Console.WriteLine("Reproduciendo " + aRemover);
            disponibles.Add(aRemover);
        }
    }

    private void agregarCancionAListaReproduccion()
    {
        //Mostrar listado de canciones disponibles
        string listadoDisponibles = convertirColeccionAString(disponibles);
        //Pedir cancion al usuario
        Cancion aBuscar = null;
        do {
            string nombre = validador.pedirStringNoVacio("Ingrese nombre de cancion" + listadoDisponibles);
            // aBuscar = validador.pedirCancion("Ingrese una cancion disponible:\n" + listadoDisponibles);
            aBuscar = new Cancion(nombre);
        } while (!disponibles.Contains(aBuscar));
        //Validar cancion existente
        //Si existe, sacarla del listado de canciones disponibles y pasarla al de en reproduccion
        int fila = disponibles.IndexOf(aBuscar);
        aBuscar = disponibles[fila];
        if (disponibles.Remove(aBuscar)) {
            reproduccion.Add(aBuscar);
            Console.WriteLine("Se ha agregado " + aBuscar + " a la lista de reproduccion");
        }
    }

    private string convertirColeccionAString(List<Cancion> canciones)
    {
        string retorno = "";
        foreach (Cancion cancion in canciones) {
            retorno = retorno + cancion.ToString() + "\n";
        }
        return(retorno);
    }
}

internal class Validador
{
    internal Cancion pedirCancion(string mensaje)
    {
        Console.WriteLine(mensaje);
        Cancion retorno = new Cancion(pedirStringNoVacio("Ingrese nombre de cancion"),
                        pedirStringNoVacio("Ingrese artista"),
                        pedirInteger("Ingrese anio", 1990, 2050));
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
        return(retorno);
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

internal class Cancion
{

    private string nombre, artista;
    private int anio;

    public Cancion(string nombre)
    {
        this.nombre = nombre;
    }

    public Cancion(string nombre, string artista, int anio)
    {
        this.nombre = nombre;
        this.artista = artista;
        this.anio = anio;
    }

    public override bool Equals(object? obj)
    {
        return obj is Cancion cancion 
                        && cancion.nombre==this.nombre 
                        && cancion.artista==this.artista
                        && cancion.anio==this.anio;
    }

    public override string? ToString()
    {
        return nombre + " - " + artista + " - (" + anio + ")";
    }
}