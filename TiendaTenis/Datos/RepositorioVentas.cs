using System.Globalization;
using CsvHelper;
using TiendaTenis.Modelos;

namespace TiendaTenis.Datos
{
    // Un CSV es plano, pero una venta tiene detalles. Solución: DOS archivos
    //   ventas.csv          -> una fila por venta (cabecera)
    //   detalles_venta.csv  -> una fila por detalle, con NumeroVenta como llave
    // Al cargar, cada detalle se une a su venta por NumeroVenta.
    public class RepositorioVentas
    {
        private const string RutaVentas = "ventas.csv";
        private const string RutaDetalles = "detalles_venta.csv";
        private List<Venta> _ventas = new List<Venta>();

        public RepositorioVentas()
        {
            Cargar();
        }

        // Más recientes primero.
        public List<Venta> Listar() => _ventas.OrderByDescending(v => v.Numero).ToList();

        public Venta? Buscar(int numero) => _ventas.FirstOrDefault(v => v.Numero == numero);

        public int SiguienteNumero() => _ventas.Count == 0 ? 1 : _ventas.Max(v => v.Numero) + 1;

        public void Agregar(Venta venta)
        {
            if (!venta.EsValida())
                throw new InvalidOperationException("Una venta sin detalles no se puede guardar.");
            if (Buscar(venta.Numero) != null)
                throw new InvalidOperationException("Ya existe una venta con ese número.");

            _ventas.Add(venta);
            Guardar();
        }

        public void Actualizar(Venta venta)
        {
            int indice = _ventas.FindIndex(v => v.Numero == venta.Numero);
            if (indice < 0)
                throw new InvalidOperationException("Venta no encontrada.");

            _ventas[indice] = venta;
            Guardar();
        }

        public void Eliminar(int numero)
        {
            _ventas.RemoveAll(v => v.Numero == numero);
            Guardar();
        }

        private void Guardar()
        {
            var detalles = new List<DetalleVenta>();
            foreach (var venta in _ventas)
            {
                foreach (var detalle in venta.Detalles)
                {
                    detalle.NumeroVenta = venta.Numero;
                    detalles.Add(detalle);
                }
            }

            using (var writer = new StreamWriter(RutaVentas))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(_ventas);
            }

            using (var writer = new StreamWriter(RutaDetalles))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(detalles);
            }
        }

        private void Cargar()
        {
            _ventas = new List<Venta>();

            if (!ArchivoConDatos(RutaVentas)) return;

            using (var reader = new StreamReader(RutaVentas))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                _ventas = csv.GetRecords<Venta>().ToList();
            }

            if (!ArchivoConDatos(RutaDetalles)) return;

            List<DetalleVenta> detalles;
            using (var reader = new StreamReader(RutaDetalles))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                detalles = csv.GetRecords<DetalleVenta>().ToList();
            }

            foreach (var venta in _ventas)
            {
                venta.Detalles.AddRange(detalles.Where(d => d.NumeroVenta == venta.Numero));
            }
        }

        private static bool ArchivoConDatos(string ruta) =>
            File.Exists(ruta) && new FileInfo(ruta).Length > 0;
    }
}
