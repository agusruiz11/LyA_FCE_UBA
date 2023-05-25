using System;
using System.Collections.Generic;
using System.Windows;
using NombreDelEspacioDeNombres;

namespace ConcesionariaAutos
{
    public partial class MainWindow : Window
    {
        private List<Pedido> pedidos;
        private List<Concepto> conceptosDisponibles;

        public Concepto(string codigo, string nombre, string modelo, string patente)
        {
            Codigo = codigo;
            Nombre = nombre;
            Modelo = modelo;
            Patente = patente;
        }

        public MainWindow()
        {
            InitializeComponent();
            pedidos = new List<Pedido>();
            conceptosDisponibles = new List<Concepto>
            {
                new Concepto("C-0000", "De fábrica", 0.00m, "Caja"),
                new Concepto("V-0000", "De fábrica", 0.00m, "Vidrios"),
                new Concepto("L-0000", "De fábrica", 0.00m, "Llantas"),
                new Concepto("P-0000", "De fábrica", 0.00m, "Puertas"),
                new Concepto("C-0001", "Automática de 5 velocidades", 300.00m, "Caja"),
                new Concepto("C-0002", "Automática de 6 velocidades", 400.00m, "Caja"),
                new Concepto("V-0001", "Polarizado delantero", 200.00m, "Vidrios"),
                new Concepto("V-0002", "Polarizado delantero y trasero", 400.00m, "Vidrios"),
                new Concepto("V-0003", "Antivandálico delantero", 400.00m, "Vidrios"),
                new Concepto("V-0004", "Antivandálico delantero y trasero", 600.00m, "Vidrios"),
                new Concepto("L-0001", "Tapacubos plásticos", 200.00m, "Llantas"),
                new Concepto("L-0002", "Llantas de aleación", 300.00m, "Llantas"),
                new Concepto("L-0003", "Llantas de aluminio", 400.00m, "Llantas")
            };
        }

        // Manejar el evento de carga inicial del formulario
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Cargar la lista de conceptos en el ListBox
            conceptosListBox.ItemsSource = conceptosDisponibles;
        }

        // Manejar el evento de clic en el botón Agregar Concepto
        private void agregarButton_Click(object sender, RoutedEventArgs e)
        {
            Concepto conceptoSeleccionado = conceptosListBox.SelectedItem as Concepto;
            if (conceptoSeleccionado != null)
            {
                // Verificar si ya se ha agregado un concepto de esa característica
                string claseConcepto = ObtenerClaseConcepto(conceptoSeleccionado.Codigo);
                if (PedidoActual.Conceptos.ContainsKey(claseConcepto))
                {
                    MessageBox.Show("Ya se ha seleccionado un concepto para esa característica.");
                }
                else
                {
                    PedidoActual.Conceptos.Add(claseConcepto, conceptoSeleccionado);
                    ActualizarListaCaracteristicas();
                }
            }
            else
            {
                MessageBox.Show("Selecciona un concepto para agregar.");
            }
        }

        // Obtener la clase de un concepto a partir de su código
        private string ObtenerClaseConcepto(string codigo)
        {
            if (codigo.StartsWith("C-"))
            {
                return "Caja";
            }
            else if (codigo.StartsWith("V-"))
            {
                return "Vidrios";
            }
            else if (codigo.StartsWith("L-"))
            {
                return "Llantas";
            }
            else if (codigo.StartsWith("P-"))
            {
                return "Puertas";
            }
            else
            {
                return "";
            }
        }

        // Actualizar la lista de características en el ListBox
        private void ActualizarListaCaracteristicas()
        {
            caracteristicasListBox.Items.Clear();
            foreach (string caracteristica in new List<string> { "Caja", "Vidrios", "Llantas", "Puertas" })
            {
                if (PedidoActual.Conceptos.ContainsKey(caracteristica))
                {
                    Concepto concepto = PedidoActual.Conceptos[caracteristica];
                    caracteristicasListBox.Items.Add($"{caracteristica}: {concepto.Codigo} - {concepto.Nombre}");
                }
                else
                {
                    caracteristicasListBox.Items.Add($"{caracteristica}: Vacante");
                }
            }
        }

        // Manejar el evento de clic en el botón Guardar
        private void guardarButton_Click(object sender, RoutedEventArgs e)
        {
            // Validar que se hayan ingresado los datos requeridos
            if (string.IsNullOrEmpty(modeloTextBox.Text) || string.IsNullOrEmpty(patenteTextBox.Text) || string.IsNullOrEmpty(nombreTextBox.Text))
            {
                MessageBox.Show("Completa todos los campos antes de guardar.");
                return;
            }

            // Asignar los datos al pedido actual
            PedidoActual.Modelo = modeloTextBox.Text;
            PedidoActual.Patente = patenteTextBox.Text;
            PedidoActual.NombreCliente = nombreTextBox.Text;

            // Guardar el pedido
            pedidos.Add(PedidoActual);

            // Mostrar un mensaje de éxito
            MessageBox.Show("Pedido guardado correctamente.");

            // Limpiar los campos y preparar para el próximo pedido
            modeloTextBox.Clear();
            patenteTextBox.Clear();
            nombreTextBox.Clear();
            conceptosListBox.SelectedIndex = -1;
            caracteristicasListBox.Items.Clear();
            pedidos.Add(new Pedido());
        }

        // Manejar el evento de clic en el botón Listar Pedidos
        private void listarPedidosButton_Click(object sender, RoutedEventArgs e)
        {
            if (pedidos.Count > 0)
            {
                pedidosListBox.Items.Clear();
                foreach (Pedido pedido in pedidos)
                {
                    decimal importeTotal = CalcularImporteTotal(pedido);
                    pedidosListBox.Items.Add($"{pedido.Modelo} - {pedido.Patente} - {pedido.NombreCliente} - Importe: {importeTotal:C}");
                }
            }
            else
            {
                MessageBox.Show("No hay pedidos para mostrar.");
            }
        }

        // Calcular el importe total de un pedido
        private decimal CalcularImporteTotal(Pedido pedido)
        {
            decimal importeTotal = 0.00m;
            foreach (Concepto concepto in pedido.Conceptos.Values)
            {
                importeTotal += concepto.Precio;
            }
            return importeTotal;
        }
    }
}
