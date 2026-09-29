using CsvHelper.Configuration.Attributes;

namespace TiendaTenis.Modelos
{
    // Patrón SaleDetail: el detalle COPIA lo que se vendió (nombre, precio, envío)
    // tal como estaba ese día. Nunca guarda una referencia al Producto, así que
    // si el producto cambia de precio o se elimina, la venta histórica no cambia.
    public class DetalleVenta
    {
        public int NumeroVenta { get; set; }
        public string CodigoProducto { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string Tipo { get; set; } = "";
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal CostoEnvio { get; set; }

        // Calculado: no se escribe en el CSV.
        [Ignore]
        public decimal Subtotal => PrecioUnitario * Cantidad + CostoEnvio;

        // Crea el detalle copiando los datos del producto en este momento.
        // Usa el polimorfismo de Producto (CalcularCostoEntrega, TipoDescripcion):
        // no pregunta si es físico o digital.
        public static DetalleVenta DesdeProducto(Producto producto, int cantidad)
        {
            return new DetalleVenta
            {
                CodigoProducto = producto.Codigo,
                Descripcion = string.IsNullOrWhiteSpace(producto.Descripcion)
                    ? producto.Nombre
                    : producto.Nombre + " - " + producto.Descripcion,
                Tipo = producto.TipoDescripcion,
                PrecioUnitario = producto.Precio,
                Cantidad = cantidad,
                CostoEnvio = producto.CalcularCostoEntrega()
            };
        }
    }
}
