using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TiendaTenis.Modelos;

namespace TiendaTenis.Formularios;
public partial class FrmProductoDigital : Form
{
    public ProductoDigital Resultado { get; private set; }

    public FrmProductoDigital(ProductoDigital existente = null)
    {
        InitializeComponent();

        cboCategoria.Items.AddRange(new object[]
            { "Tarjetas de regalo", "Ebooks", "Guías" });
        cboFormato.Items.AddRange(new object[]
            { "PDF", "EPUB", "Código", "Tarjeta PDF" });

        if (existente != null)
        {
            txtCodigo.Text = existente.Codigo;
            txtCodigo.ReadOnly = true;
            txtNombre.Text = existente.Nombre;
            txtDescripcion.Text = existente.Descripcion;
            nudPrecio.Value = existente.Precio;
            cboCategoria.Text = existente.Categoria;
            nudPeso.Value = (decimal)existente.Peso;
            cboFormato.Text = existente.Formato;
            txtUrl.Text = existente.UrlDescarga;
        }
    }

    private void btnGuardar_Click(object sender, EventArgs e)
    {
        bool hayVacios = string.IsNullOrWhiteSpace(txtCodigo.Text)
                      || string.IsNullOrWhiteSpace(txtNombre.Text)
                      || string.IsNullOrWhiteSpace(txtUrl.Text);
        bool hayPuntoYComa = (txtCodigo.Text + txtNombre.Text +
                              txtDescripcion.Text + txtUrl.Text).Contains(";");

        if (hayVacios || hayPuntoYComa)
        {
            MessageBox.Show("Complete los campos obligatorios y no use el carácter ';'.");
            return;
        }

        Resultado = new ProductoDigital
        {
            Codigo = txtCodigo.Text.Trim(),
            Nombre = txtNombre.Text.Trim(),
            Descripcion = txtDescripcion.Text,
            Precio = nudPrecio.Value,
            Categoria = cboCategoria.Text,
            Peso = (double)nudPeso.Value,
            Formato = cboFormato.Text,
            UrlDescarga = txtUrl.Text.Trim()
        };
        DialogResult = DialogResult.OK;
    }
}
