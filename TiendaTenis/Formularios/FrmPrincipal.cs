using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TiendaTenis.Formularios
{
    // Menú para llegar a los tres CRUDs. Cada formulario se abre como diálogo
    // (ShowDialog): así solo hay uno abierto a la vez y cada repositorio vuelve
    // a leer los CSV actualizados cada vez que se abre.
    // Además aplica el Tema (colores) a cada formulario justo antes de mostrarlo.
    public class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            Text = "Tienda Tenis";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            ClientSize = new Size(420, 430);
            ResizeRedraw = true;

            Controls.Add(CrearTitulo("TIENDA TENIS", 24F, 50));
            Controls.Add(CrearTitulo("Sistema de gestión", 11F, 105));

            var btnProductos = CrearBoton("Catálogo de productos", 170);
            btnProductos.Click += (s, e) => Abrir(new FrmCatalogo());

            var btnClientes = CrearBoton("Clientes", 245);
            btnClientes.Click += (s, e) => Abrir(new FrmClientes());

            var btnVentas = CrearBoton("Ventas", 320);
            btnVentas.Click += (s, e) => Abrir(new FrmVentas());
        }

        // Fondo con degradado vertical (verde oscuro arriba, verde lima abajo).
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (ClientRectangle.Width == 0 || ClientRectangle.Height == 0) return;

            using (var pincel = new LinearGradientBrush(
                ClientRectangle, Tema.Primario, Tema.Secundario, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(pincel, ClientRectangle);
            }
        }

        private Label CrearTitulo(string texto, float tamano, int y)
        {
            return new Label
            {
                Text = texto,
                Font = new Font("Segoe UI", tamano, tamano >= 15F ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false,
                Bounds = new Rectangle(0, y, ClientSize.Width, 50)
            };
        }

        private Button CrearBoton(string texto, int y)
        {
            var boton = new Button
            {
                Text = texto,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Size = new Size(280, 54),
                Location = new Point((ClientSize.Width - 280) / 2, y),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Tema.Primario,
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            boton.FlatAppearance.BorderSize = 0;
            boton.FlatAppearance.MouseOverBackColor = Tema.Acento;
            Controls.Add(boton);
            return boton;
        }

        private void Abrir(Form formulario)
        {
            using (formulario)
            {
                Tema.Aplicar(formulario);
                formulario.ShowDialog(this);
            }
        }
    }
}
