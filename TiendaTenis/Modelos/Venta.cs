using CsvHelper.Configuration.Attributes;

namespace TiendaTenis.Modelos
{
    public class Venta
    {
        public int Numero { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        // Se copian el documento y el nombre del cliente (igual que en SaleDetail):
        // si luego eliminan o editan al cliente, la venta histórica no se rompe.
        public string ClienteDocumento { get; set; } = "";
        public string ClienteNombre { get; set; } = "";

        // Los detalles van en OTRO csv (detalles_venta.csv), por eso no se escriben aquí.
        [Ignore]
        public List<DetalleVenta> Detalles { get; } = new List<DetalleVenta>();

        // El total lo calcula la venta, no la pantalla.
        [Ignore]
        public decimal Total => Detalles.Sum(d => d.Subtotal);

        public void AsignarCliente(Cliente cliente)
        {
            ClienteDocumento = cliente.Documento;
            ClienteNombre = cliente.Nombre;
        }

        public int CantidadEnVenta(string codigoProducto) =>
            Detalles.Where(d => d.CodigoProducto == codigoProducto).Sum(d => d.Cantidad);

        // Regla: si el producto no tiene disponibilidad, el detalle NO se agrega.
        // Si el producto ya estaba en la venta, se suma a su misma línea.
        public bool AgregarDetalle(Producto producto, int cantidad)
        {
            if (cantidad <= 0) return false;

            int totalPedido = CantidadEnVenta(producto.Codigo) + cantidad;
            if (!producto.HayDisponibilidad(totalPedido)) return false;

            var existente = Detalles.FirstOrDefault(d => d.CodigoProducto == producto.Codigo);
            if (existente != null)
                existente.Cantidad += cantidad;
            else
                Detalles.Add(DetalleVenta.DesdeProducto(producto, cantidad));

            return true;
        }

        public void QuitarDetalle(DetalleVenta detalle)
        {
            Detalles.Remove(detalle);
        }

        // Regla: una venta sin detalles no se guarda.
        public bool EsValida() => Detalles.Count > 0;
    }
}
