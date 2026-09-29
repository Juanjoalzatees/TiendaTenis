using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TiendaTenis.Datos;
using TiendaTenis.Modelos;

namespace TiendaTenis.Formularios
{
    public partial class FrmCatalogo : Form
    {
        private readonly RepositorioProductos _repo = new RepositorioProductos();

        public FrmCatalogo()
        {
            InitializeComponent();
            Refrescar();
        }

        private void Refrescar()
        {
            dgv.DataSource = null;
            dgv.DataSource = _repo.Listar();
        }

        private Producto Seleccionado()
        {
            return dgv.CurrentRow?.DataBoundItem as Producto;
        }

        private void btnNuevoFisico_Click(object sender, EventArgs e)
        {
            var frm = new FrmProductoFisico();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _repo.Agregar(frm.Resultado);
                    Refrescar();
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnNuevoDigital_Click(object sender, EventArgs e)
        {
            var frm = new FrmProductoDigital();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _repo.Agregar(frm.Resultado);
                    Refrescar();
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var p = Seleccionado();
            if (p is ProductoFisico f)
            {
                var frm = new FrmProductoFisico(f);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _repo.Actualizar(frm.Resultado);
                    Refrescar();
                }
            }
            else if (p is ProductoDigital d)
            {
                var frm = new FrmProductoDigital(d);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _repo.Actualizar(frm.Resultado);
                    Refrescar();
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var p = Seleccionado();
            if (p == null) return;

            var respuesta = MessageBox.Show("¿Eliminar " + p.Nombre + "?",
                "Confirmar", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                _repo.Eliminar(p.Codigo);
                Refrescar();
            }
        }
    }
}
