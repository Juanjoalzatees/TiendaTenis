using System.Globalization;

namespace TiendaTenis.Modelos
{
    public class ProductoFisico : Producto
    {
        public int Stock { get; set; }
        public decimal CostoEnvio { get; set; }

        public override string UnidadPeso => "kg";
        public override string TipoDescripcion => "Físico";
        public override bool HayDisponibilidad(int cantidad) => Stock >= cantidad;
        public override decimal CalcularCostoEntrega() => CostoEnvio;
        public override string Entregar() => $"{Nombre}: despachado por envío.";
        public override void RegistrarVenta(int cantidad) => Stock -= cantidad;

        public override string ALineaCsv() =>
        ParteComun("F") + ";" + Stock + ";" + CostoEnvio;
    }
}