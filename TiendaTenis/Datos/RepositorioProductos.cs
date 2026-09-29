using System.Globalization;
using CsvHelper;
using TiendaTenis.Modelos;

namespace TiendaTenis.Datos;

public class RepositorioProductos
{
    private const string Ruta = "productos.csv";
    private const string RutaFormatoAntiguo = "productos_formato_antiguo.csv";
    private List<Producto> _items = new List<Producto>();

    public RepositorioProductos()
    {
        Cargar();
    }

    public List<Producto> Listar() => _items.ToList();
    public Producto? Buscar(string codigo) => _items.FirstOrDefault(p => p.Codigo == codigo);

    public void Agregar(Producto p)
    {
        if (Buscar(p.Codigo) != null)
            throw new InvalidOperationException("El código ya existe.");
        _items.Add(p);
        Guardar();
    }

    public void Actualizar(Producto p)
    {
        _items.RemoveAll(x => x.Codigo == p.Codigo);
        _items.Add(p);
        Guardar();
    }

    public void Eliminar(string codigo)
    {
        _items.RemoveAll(x => x.Codigo == codigo);
        Guardar();
    }

    // Escribe con CsvHelper: Producto -> ProductoCsv (fila plana) -> archivo.
    private void Guardar()
    {
        var filas = _items.Select(ProductoCsv.DesdeProducto).ToList();

        using (var writer = new StreamWriter(Ruta))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(filas);
        }
    }

    // Lee con CsvHelper: archivo -> ProductoCsv (fila plana) -> Producto (Fisico o Digital).
    private void Cargar()
    {
        _items = new List<Producto>();

        if (!File.Exists(Ruta) || new FileInfo(Ruta).Length == 0) return;

        // Si el archivo es del formato viejo (líneas con ';' hechas a mano), no se pierde:
        // se guarda como copia y el catálogo arranca vacío con el formato nuevo.
        if (!TieneFormatoNuevo())
        {
            File.Move(Ruta, RutaFormatoAntiguo, true);
            return;
        }

        using (var reader = new StreamReader(Ruta))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            _items = csv.GetRecords<ProductoCsv>()
                        .Select(fila => fila.AProducto())
                        .ToList();
        }
    }

    private static bool TieneFormatoNuevo()
    {
        string? encabezado = File.ReadLines(Ruta).FirstOrDefault();
        return encabezado != null && encabezado.StartsWith("Tipo,");
    }
}
