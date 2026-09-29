using System.Drawing;
using System.Windows.Forms;

namespace TiendaTenis.Formularios
{
    // Paleta y estilo compartidos por toda la aplicación (tema "cancha de tenis").
    // Para cambiar los colores de todo el sistema basta con editar las constantes de abajo.
    // No toca la lógica de ningún formulario: solo colores y apariencia de los controles.
    public static class Tema
    {
        public static readonly Color Primario = ColorTranslator.FromHtml("#14532D");      // verde oscuro
        public static readonly Color PrimarioHover = ColorTranslator.FromHtml("#166534"); // verde al pasar el mouse
        public static readonly Color Secundario = ColorTranslator.FromHtml("#65A30D");    // verde lima
        public static readonly Color Acento = ColorTranslator.FromHtml("#A3E635");        // amarillo pelota de tenis
        public static readonly Color Fondo = ColorTranslator.FromHtml("#F1F5EE");         // fondo de formularios
        public static readonly Color Texto = ColorTranslator.FromHtml("#1F2937");         // texto principal
        public static readonly Color FilaAlterna = ColorTranslator.FromHtml("#E6EFE1");   // filas alternas de las grillas
        public static readonly Color Deshabilitado = ColorTranslator.FromHtml("#D1D5DB"); // botones deshabilitados

        // Aplica el tema a un formulario y a todos sus controles.
        public static void Aplicar(Form formulario)
        {
            formulario.BackColor = Fondo;
            formulario.ForeColor = Texto;
            AplicarAControles(formulario.Controls);
        }

        private static void AplicarAControles(Control.ControlCollection controles)
        {
            foreach (Control control in controles)
            {
                if (control is Button boton)
                    EstilizarBoton(boton);
                else if (control is DataGridView grilla)
                    EstilizarGrilla(grilla);

                if (control.HasChildren)
                    AplicarAControles(control.Controls);
            }
        }

        private static void EstilizarBoton(Button boton)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.FlatAppearance.MouseOverBackColor = PrimarioHover;
            boton.UseVisualStyleBackColor = false;
            boton.ForeColor = Color.White;
            boton.Cursor = Cursors.Hand;

            // Los botones se habilitan/deshabilitan según el modo (ej. FrmVentas): el color debe seguirlos.
            boton.EnabledChanged += (s, e) => ColorearBoton(boton);
            ColorearBoton(boton);
        }

        private static void ColorearBoton(Button boton)
        {
            boton.BackColor = boton.Enabled ? Primario : Deshabilitado;
        }

        private static void EstilizarGrilla(DataGridView grilla)
        {
            grilla.BorderStyle = BorderStyle.None;
            grilla.BackgroundColor = Color.White;
            grilla.GridColor = ColorTranslator.FromHtml("#D5E0CE");
            grilla.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Encabezado verde con letra blanca
            grilla.EnableHeadersVisualStyles = false;
            grilla.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grilla.ColumnHeadersDefaultCellStyle.BackColor = Primario;
            grilla.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grilla.ColumnHeadersDefaultCellStyle.SelectionBackColor = Primario;
            grilla.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            grilla.ColumnHeadersDefaultCellStyle.Font = new Font(grilla.Font, FontStyle.Bold);

            // Filas alternas y selección amarilla
            grilla.AlternatingRowsDefaultCellStyle.BackColor = FilaAlterna;
            grilla.DefaultCellStyle.SelectionBackColor = Acento;
            grilla.DefaultCellStyle.SelectionForeColor = Texto;
        }
    }
}
