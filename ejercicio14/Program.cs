namespace ejercicio14
{
    class Program{
        static void Main(string[] args)
        {
            string opcionMenuPrincipal = "";
            string operacion = "";
            int valor = 0;
            do {
                Console.WriteLine("Ingrese opcion:\n1. Definir valor"
                + "\n2. Visualizar valor actual\n3. Realizar una operacion"
                + "\n4. Salir");

                opcionMenuPrincipal = Console.ReadLine();

                switch (opcionMenuPrincipal) {
                    case "1":
                        // lo que hago en opcion 1
                        do {
                            Console.WriteLine("Ingrese un valor numerico mayor a 0");
                            if (!Int32.TryParse(Console.ReadLine(), out valor)) {
                                //Este caso es cuando uso otra cosa que no es numero
                                Console.WriteLine("Debe ingresar un numero");
                            } else {
                                //Este caso es cuando uso un numero
                                if (valor <= 0) {
                                    Console.WriteLine("Debe ingresar un numero mayor a 0");
                                }
                            }
                                
                        } while (valor <= 0);
                        break; // asegura que no se ejecute el resto del codigo
                    case "2":
                        Console.WriteLine("Valor actual: " + valor);
                        // lo que hago en opcion 2
                        break;
                    case "3":
                        do {
                            Console.WriteLine("Ingrese operacion:\nS - Sumar\nR - Restar\nV - Volver al menu principal");
                            operacion = Console.ReadLine().ToUpper();
                            switch (operacion) {
                                case "S":
                                    valor = valor + 1;
                                    break;
                                case "R":
                                    valor = valor - 1;
                                    break;
                                case "V":
                                    break;
                                default:
                                    Console.WriteLine("Debe ingresar una opcion valida (S, R o V)");
                                    break;
                            }
                        } while (operacion !="V");
                        // lo que hago en opcion 3
                        break;
                    case "4":
                        Console.WriteLine("Gracias por usar el programa");
                        break;
                    default:
                    break; 
                }
            } while  (opcionMenuPrincipal != "4");
        }
        
    }
    
} 


