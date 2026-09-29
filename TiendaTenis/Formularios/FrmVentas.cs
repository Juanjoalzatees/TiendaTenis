using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TiendaTenis.Datos;
using TiendaTenis.Modelos;

namespace TiendaTenis.Formularios
{
    // CRUD 3 - Ventas.
    // La interfaz se construye en código (ConstruirInterfaz) para que puedas
    // pegar este único archivo sin depender de un .Designer.cs.
    public class FrmVentas : Form
    {
        private readonly RepositorioVentas _repoVentas = new RepositorioVentas();
        private readonly RepositorioProductos _repoProductos = new RepositorioProductos();
        private readonly RepositorioClientes _repoClientes = new RepositorioClientes();

        // La venta que se está mostrando: una nueva (armándose) o una ya guardada (consulta).
        private Venta _venta = new Venta();
        private bool _esNueva = true;

        private readonly Label lblNumero = new Label();
        private readonly DateTimePicker dtpFecha = new DateTimePicker();
        private readonly ComboBox cboCliente = new ComboBox();
        private readonly ComboBox cboProducto = new ComboBox();
        private readonly NumericUpDown nudCantidad = new NumericUpDown();
        private readonly Button btnAgregar = new Button();
        private readonly Button btnQuitar = new Button();
        private readonly Button btnGuardar = new Button();
        private readonly Button btnNueva = new Button();
        private readonly Button btnActualizar = new Button();
        private readonly Button btnEliminar = new Button();
        private readonly Label lblTotal = new Label();
        private readonly Label lblModo = new Label();
        private readonly DataGridView dgvDetalles = CrearGrilla(new Point(20, 100), new Size(1060, 190));
        private readonly DataGridView dgvVentas = CrearGrilla(new Point(20, 365), new Size(1060, 250));

        public FrmVentas()
        {
            ConstruirInterfaz();
            CargarCombos();
            RefrescarHistorial();
            IniciarNuevaVenta();
        }

        // ---------------------------------------------------------------
        // Interfaz
        // ---------------------------------------------------------------

        private void ConstruirInterfaz()
        {
            Text = "Ventas";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1100, 670);

            // Fila 1: número, fecha y cliente
            Controls.Add(Etiqueta("Venta N°", 20, 20));
            lblNumero.Location = new Point(85, 18);
            lblNumero.AutoSize = true;
            lblNumero.Font = new Font(Font, FontStyle.Bold);
            Controls.Add(lblNumero);

            Controls.Add(Etiqueta("Fecha", 200, 20));
            dtpFecha.Location = new Point(250, 16);
            dtpFecha.Width = 190;
            dtpFecha.Format = DateTimePickerFormat.Custom;
            dtpFecha.CustomFormat = "yyyy-MM-dd HH:mm";
            Controls.Add(dtpFecha);

            Controls.Add(Etiqueta("Cliente", 470, 20));
            cboCliente.Location = new Point(530, 16);
            cboCliente.Width = 320;
            cboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCliente.FormattingEnabled = true;
            cboCliente.Format += (s, e) =>
            {
                if (e.ListItem is Cliente c) e.Value = c.Documento + " - " + c.Nombre;
            };
            Controls.Add(cboCliente);

            // Fila 2: producto, cantidad y botones de detalle
            Controls.Add(Etiqueta("Producto", 20, 64));
            cboProducto.Location = new Point(85, 60);
            cboProducto.Width = 355;
            cboProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            Controls.Add(cboProducto);

            Controls.Add(Etiqueta("Cantidad", 470, 64));
            nudCantidad.Location = new Point(530, 60);
            nudCantidad.Width = 70;
            nudCantidad.Minimum = 1;
            nudCantidad.Maximum = 1000;
            nudCantidad.Value = 1;
            Controls.Add(nudCantidad);

            ConfigurarBoton(btnAgregar, "Agregar detalle", 620, 58, 140);
            btnAgregar.Click += (s, e) => AgregarDetalle();
            ConfigurarBoton(btnQuitar, "Quitar detalle", 770, 58, 140);
            btnQuitar.Click += (s, e) => QuitarDetalle();

            // Grilla de detalles
            AgregarColumna(dgvDetalles, "Código", "CodigoProducto");
            AgregarColumna(dgvDetalles, "Descripción", "Descripcion");
            AgregarColumna(dgvDetalles, "Tipo", "Tipo");
            AgregarColumna(dgvDetalles, "Precio unitario", "PrecioUnitario", "N0");
            AgregarColumna(dgvDetalles, "Cantidad", "Cantidad");
            AgregarColumna(dgvDetalles, "Envío", "CostoEnvio", "N0");
            AgregarColumna(dgvDetalles, "Subtotal", "Subtotal", "N0");
            Controls.Add(dgvDetalles);

            // Total y acciones de la venta
            lblTotal.Location = new Point(20, 300);
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font(Font.FontFamily, 13f, FontStyle.Bold);
            Controls.Add(lblTotal);

            lblModo.Location = new Point(280, 305);
            lblModo.AutoSize = true;
            Controls.Add(lblModo);

            ConfigurarBoton(btnGuardar, "Guardar venta", 800, 296, 150);
            btnGuardar.Click += (s, e) => GuardarVenta();
            ConfigurarBoton(btnNueva, "Nueva venta", 960, 296, 120);
            btnNueva.Click += (s, e) => IniciarNuevaVenta();

            // Historial
            Controls.Add(Etiqueta("Historial de ventas (clic en una fila para consultarla)", 20, 342));
            AgregarColumna(dgvVentas, "N°", "Numero");
            AgregarColumna(dgvVentas, "Fecha", "Fecha", "yyyy-MM-dd HH:mm");
            AgregarColumna(dgvVentas, "Documento", "ClienteDocumento");
            AgregarColumna(dgvVentas, "Cliente", "ClienteNombre");
            AgregarColumna(dgvVentas, "Total", "Total", "N0");
            dgvVentas.CellClick += DgvVentas_CellClick;
            Controls.Add(dgvVentas);

            ConfigurarBoton(btnActualizar, "Actualizar cliente/fecha", 20, 625, 210);
            btnActualizar.Click += (s, e) => ActualizarVenta();
            ConfigurarBoton(btnEliminar, "Eliminar venta", 240, 625, 140);
            btnEliminar.Click += (s, e) => EliminarVenta();
        }

        private static DataGridView CrearGrilla(Point ubicacion, Size tamano)
        {
            return new DataGridView
            {
                Location = ubicacion,
                Size = tamano,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                MultiSelect = false,
                RowHeadersVisible = false,
                AutoGenerateColumns = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
        }

        private static void AgregarColumna(DataGridView dgv, string encabezado, string propiedad, string? formato = null)
        {
            var columna = new DataGridViewTextBoxColumn
            {
                HeaderText = encabezado,
                DataPropertyName = propiedad,
                ReadOnly = true
            };
            if (formato != null) columna.DefaultCellStyle.Format = formato;
            dgv.Columns.Add(columna);
        }

        private static Label Etiqueta(string texto, int x, int y)
        {
            return new Label { Text = texto, Location = new Point(x, y), AutoSize = true };
        }

        private void ConfigurarBoton(Button boton, string texto, int x, int y, int ancho)
        {
            boton.Text = texto;
            boton.Location = new Point(x, y);
            boton.Size = new Size(ancho, 30);
            boton.UseVisualStyleBackColor = true;
            Controls.Add(boton);
        }

        // ---------------------------------------------------------------
        // Carga y refresco
        // ---------------------------------------------------------------

        private void CargarCombos()
        {
            cboCliente.DataSource = _repoClientes.ObtenerClientes().ToList();
            cboCliente.SelectedIndex = -1;

            cboProducto.DataSource = _repoProductos.Listar();
            cboProducto.SelectedIndex = -1;
        }

        private void RefrescarHistorial()
        {
            dgvVentas.DataSource = null;
            dgvVentas.DataSource = _repoVentas.Listar();
            dgvVentas.ClearSelection();
        }

        private void RefrescarDetalles()
        {
            dgvDetalles.DataSource = null;
            dgvDetalles.DataSource = _venta.Detalles.ToList();
            lblTotal.Text = "Total: $ " + _venta.Total.ToString("N0");
        }

        // Pinta en pantalla la venta actual (_venta).
        private void MostrarVenta()
        {
            lblNumero.Text = _venta.Numero.ToString();
            dtpFecha.Value = _venta.Fecha;
            SeleccionarCliente(_venta.ClienteDocumento);
            RefrescarDetalles();

            btnAgregar.Enabled = _esNueva;
            btnQuitar.Enabled = _esNueva;
            btnGuardar.Enabled = _esNueva;
            cboProducto.Enabled = _esNueva;
            nudCantidad.Enabled = _esNueva;
            btnActualizar.Enabled = !_esNueva;
            btnEliminar.Enabled = !_esNueva;

            lblModo.Text = _esNueva
                ? "Armando una venta nueva"
                : "Venta guardada: los detalles no se modifican, solo cliente y fecha.";
        }

        private void SeleccionarCliente(string documento)
        {
            cboCliente.SelectedIndex = -1;
            if (string.IsNullOrEmpty(documento)) return;

            for (int i = 0; i < cboCliente.Items.Count; i++)
            {
                if (cboCliente.Items[i] is Cliente c && c.Documento == documento)
                {
                    cboCliente.SelectedIndex = i;
                    return;
                }
            }
        }

        private void IniciarNuevaVenta()
        {
            _venta = new Venta
            {
                Numero = _repoVentas.SiguienteNumero(),
                Fecha = DateTime.Now
            };
            _esNueva = true;

            cboProducto.SelectedIndex = -1;
            nudCantidad.Value = 1;
            dgvVentas.ClearSelection();
            MostrarVenta();
        }

        // ---------------------------------------------------------------
        // Detalles de la venta que se está armando
        // ---------------------------------------------------------------

        private void AgregarDetalle()
        {
            var producto = cboProducto.SelectedItem as Producto;
            if (producto == null)
            {
                MessageBox.Show("Seleccione un producto.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int cantidad = (int)nudCantidad.Value;

            // La venta decide si hay disponibilidad; el formulario no pregunta el tipo de producto.
            if (!_venta.AgregarDetalle(producto, cantidad))
            {
                MessageBox.Show("No hay disponibilidad suficiente de \"" + producto.Nombre +
                    "\" para esa cantidad. El detalle no se agregó.", "Sin stock",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            RefrescarDetalles();
        }

        private void QuitarDetalle()
        {
            var detalle = dgvDetalles.CurrentRow?.DataBoundItem as DetalleVenta;
            if (detalle == null)
            {
                MessageBox.Show("Seleccione el detalle que desea quitar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _venta.QuitarDetalle(detalle);
            RefrescarDetalles();
        }

        // ---------------------------------------------------------------
        // CRUD
        // ---------------------------------------------------------------

        // CREATE
        private void GuardarVenta()
        {
            var cliente = cboCliente.SelectedItem as Cliente;
            if (cliente == null)
            {
                MessageBox.Show("Seleccione un cliente.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_venta.EsValida())
            {
                MessageBox.Show("Una venta sin detalles no se puede guardar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Antes de guardar se revalida que cada producto siga existiendo y con disponibilidad.
            foreach (var detalle in _venta.Detalles)
            {
                var producto = _repoProductos.Buscar(detalle.CodigoProducto);
                if (producto == null || !producto.HayDisponibilidad(detalle.Cantidad))
                {
                    MessageBox.Show("El producto \"" + detalle.Descripcion +
                        "\" ya no está disponible en esa cantidad.", "Sin stock",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                _venta.Fecha = dtpFecha.Value;
                _venta.AsignarCliente(cliente);
                _repoVentas.Agregar(_venta);

                // Se descuenta el stock: en el digital RegistrarVenta no hace nada.
                foreach (var detalle in _venta.Detalles)
                {
                    var producto = _repoProductos.Buscar(detalle.CodigoProducto);
                    if (producto != null)
                    {
                        producto.RegistrarVenta(detalle.Cantidad);
                        _repoProductos.Actualizar(producto);
                    }
                }

                MessageBox.Show("Venta " + _venta.Numero + " guardada. Total: $ " +
                    _venta.Total.ToString("N0"), "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefrescarHistorial();
                IniciarNuevaVenta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // READ: al hacer clic en el historial se muestra la venta guardada.
        private void DgvVentas_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvVentas.Rows[e.RowIndex].DataBoundItem is Venta venta)
            {
                _venta = venta;
                _esNueva = false;
                MostrarVenta();
            }
        }

        // UPDATE: solo cabecera (cliente y fecha). Los detalles son historia y no se tocan.
        private void ActualizarVenta()
        {
            if (_esNueva)
            {
                MessageBox.Show("Seleccione una venta del historial.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var cliente = cboCliente.SelectedItem as Cliente;
                if (cliente != null) _venta.AsignarCliente(cliente);
                _venta.Fecha = dtpFecha.Value;

                _repoVentas.Actualizar(_venta);
                RefrescarHistorial();
                MessageBox.Show("Venta actualizada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                IniciarNuevaVenta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // DELETE: anula la venta y devuelve el stock de los productos físicos.
        private void EliminarVenta()
        {
            if (_esNueva)
            {
                MessageBox.Show("Seleccione una venta del historial.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var respuesta = MessageBox.Show("¿Eliminar la venta " + _venta.Numero +
                "? Se devolverá el stock de los productos físicos.", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;

            try
            {
                foreach (var detalle in _venta.Detalles)
                {
                    var producto = _repoProductos.Buscar(detalle.CodigoProducto);
                    if (producto != null)
                    {
                        // Cantidad negativa = devolver: en el físico suma al stock, en el digital no hace nada.
                        producto.RegistrarVenta(-detalle.Cantidad);
                        _repoProductos.Actualizar(producto);
                    }
                }

                _repoVentas.Eliminar(_venta.Numero);
                RefrescarHistorial();
                IniciarNuevaVenta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
