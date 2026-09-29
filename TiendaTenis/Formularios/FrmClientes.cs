using System;
using System.Windows.Forms;
using TiendaTenis.Datos;
using TiendaTenis.Modelos;

namespace TiendaTenis.Formularios
{
    public partial class FrmClientes : Form
    {
        private RepositorioClientes _repositorio;
        private string _documentoSeleccionado;

        public FrmClientes()
        {
            InitializeComponent();
            _repositorio = new RepositorioClientes();
            ActualizarGrid();
        }

        private void ActualizarGrid()
        {
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = _repositorio.ObtenerClientes();
        }

        private void LimpiarCampos()
        {
            txtDocumento.Clear();
            txtNombre.Clear();
            txtCorreo.Clear();
            txtTelefono.Clear();
            _documentoSeleccionado = null;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDocumento.Text) || string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El documento y el nombre son obligatorios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Cliente nuevoCliente = new Cliente
                {
                    Documento = txtDocumento.Text,
                    Nombre = txtNombre.Text,
                    Correo = txtCorreo.Text,
                    Telefono = txtTelefono.Text
                };

                _repositorio.AgregarCliente(nuevoCliente);
                ActualizarGrid();
                LimpiarCampos();
                MessageBox.Show("Cliente agregado correctamente al CSV.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_documentoSeleccionado))
                {
                    MessageBox.Show("Seleccione un cliente de la tabla para actualizar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Cliente clienteActualizado = new Cliente
                {
                    Documento = txtDocumento.Text,
                    Nombre = txtNombre.Text,
                    Correo = txtCorreo.Text,
                    Telefono = txtTelefono.Text
                };

                _repositorio.ActualizarCliente(clienteActualizado, _documentoSeleccionado);
                ActualizarGrid();
                LimpiarCampos();
                MessageBox.Show("Cliente actualizado correctamente en el CSV.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_documentoSeleccionado))
                {
                    MessageBox.Show("Seleccione un cliente de la tabla para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirmacion = MessageBox.Show($"¿Desea eliminar al cliente {_documentoSeleccionado}?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    _repositorio.EliminarCliente(_documentoSeleccionado);
                    ActualizarGrid();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvClientes.Rows[e.RowIndex];

                _documentoSeleccionado = fila.Cells["Documento"].Value?.ToString();

                txtDocumento.Text = fila.Cells["Documento"].Value?.ToString();
                txtNombre.Text = fila.Cells["Nombre"].Value?.ToString();
                txtCorreo.Text = fila.Cells["Correo"].Value?.ToString();
                txtTelefono.Text = fila.Cells["Telefono"].Value?.ToString();
            }
        }
    }
}