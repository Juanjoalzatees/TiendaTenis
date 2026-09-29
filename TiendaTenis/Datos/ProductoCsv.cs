using TiendaTenis.Modelos;

namespace TiendaTenis.Datos
{
    // Un CSV es plano, pero los productos son de dos tipos con campos distintos.
    // Solución: UNA fila plana con todas las columnas posibles y una columna "Tipo"
    // que dice si la fila es Fisico o Digital. Las columnas que no aplican quedan vacías.
    // CsvHelper lee y escribe esta clase; el resto del sistema sigue usando Producto.
    public class ProductoCsv
    {
        public const string TipoFisico = "Fisico";
        public const string TipoDigital = "Digital";

        // Tipo va primero: es lo que permite saber qué clase reconstruir al leer.
        public string Tipo { get; set; } = "";

        // Común (viene de Producto)
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public decimal Precio { get; set; }
        public string Categoria { get; set; } = "";
        public double Peso { get; set; }

        // Solo físico
        public int? Stock { get; set; }
        public decimal? CostoEnvio { get; set; }

        // Solo digital
        public string? Formato { get; set; }
        public string? UrlDescarga { get; set; }

        // Producto -> fila del CSV
        public static ProductoCsv DesdeProducto(Producto producto)
        {
            var fila = new ProductoCsv
            {
                Codigo = producto.Codigo,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Categoria = producto.Categoria,
                Peso = producto.Peso
            };

            switch (producto)
            {
                case ProductoFisico fisico:
                    fila.Tipo = TipoFisico;
                    fila.Stock = fisico.Stock;
                    fila.CostoEnvio = fisico.CostoEnvio;
                    break;
                case ProductoDigital digital:
                    fila.Tipo = TipoDigital;
                    fila.Formato = digital.Formato;
                    fila.UrlDescarga = digital.UrlDescarga;
                    break;
                default:
                    throw new InvalidOperationException("Tipo de producto no soportado.");
            }

            return fila;
        }

        // Fila del CSV -> Producto (la clase concreta la decide la columna Tipo)
        public Producto AProducto()
        {
            switch (Tipo)
            {
                case TipoFisico:
                    return new ProductoFisico
                    {
                        Codigo = Codigo,
                        Nombre = Nombre,
                        Descripcion = Descripcion,
                        Precio = Precio,
                        Categoria = Categoria,
                        Peso = Peso,
                        Stock = Stock ?? 0,
                        CostoEnvio = CostoEnvio ?? 0m
                    };
                case TipoDigital:
                    return new ProductoDigital
                    {
                        Codigo = Codigo,
                        Nombre = Nombre,
                        Descripcion = Descripcion,
                        Precio = Precio,
                        Categoria = Categoria,
                        Peso = Peso,
                        Formato = Formato ?? "",
                        UrlDescarga = UrlDescarga ?? ""
                    };
                default:
                    throw new InvalidDataException("Tipo de producto desconocido en el CSV: " + Tipo);
            }
        }
    }
}
