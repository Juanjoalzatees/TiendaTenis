using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TiendaTenis.Modelos;

namespace TiendaTenis.Formularios
{
    public partial class FrmProductoFisico : Form
    {
        public ProductoFisico Resultado { get; private set; }

        public FrmProductoFisico(ProductoFisico existente = null)
        {
            InitializeComponent();
            Tema.Aplicar(this);
            cboCategoria.Items.AddRange(new object[]
                { "Jordan", "Nike", "Adidas", "Puma", "New Balance" });

            if (existente != null)
            {
                txtCodigo.Text = existente.Codigo;
                txtCodigo.ReadOnly = true;
                txtNombre.Text = existente.Nombre;
                txtDescripcion.Text = existente.Descripcion;
                nudPrecio.Value = existente.Precio;
                cboCategoria.Text = existente.Categoria;
                nudPeso.Value = (decimal)existente.Peso;
                nudStock.Value = existente.Stock;
                nudCostoEnvio.Value = existente.CostoEnvio;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool hayVacios = string.IsNullOrWhiteSpace(txtCodigo.Text)
                     || string.IsNullOrWhiteSpace(txtNombre.Text);
            bool hayPuntoYComa = (txtCodigo.Text + txtNombre.Text +
                                  txtDescripcion.Text).Contains(";");

            if (hayVacios || hayPuntoYComa)
            {
                MessageBox.Show("Complete los campos obligatorios y no use el carácter ';'.");
                return;
            }

            Resultado = new ProductoFisico
            {
                Codigo = txtCodigo.Text.Trim(),
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text,
                Precio = nudPrecio.Value,
                Categoria = cboCategoria.Text,
                Peso = (double)nudPeso.Value,
                Stock = (int)nudStock.Value,
                CostoEnvio = nudCostoEnvio.Value

            };
            DialogResult = DialogResult.OK;
        }
    }
}
