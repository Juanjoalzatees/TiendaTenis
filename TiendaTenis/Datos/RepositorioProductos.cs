using System.Globalization;
using TiendaTenis.Modelos;

namespace TiendaTenis.Datos;
public class RepositorioProductos
{
    private const string Ruta = "productos.csv";
    private List<Producto> _items = new List<Producto>();

    public RepositorioProductos()
    {
        Cargar();
    }

    public List<Producto> Listar() => _items.ToList();
    public Producto Buscar(string codigo) => _items.FirstOrDefault(p => p.Codigo == codigo);

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

    private void Guardar() =>
        File.WriteAllLines(Ruta, _items.Select(p => p.ALineaCsv()));

    private void Cargar() =>
        _items = File.Exists(Ruta)
            ? File.ReadAllLines(Ruta).Select(DesdeLinea).ToList()
            : new List<Producto>();

   
    private static Producto DesdeLinea(string linea)
    {
        string[] c = linea.Split(';');

        return c[0] == "F"
            ? new ProductoFisico
            {
                Codigo = c[1],
                Nombre = c[2],
                Descripcion = c[3],
                Precio = decimal.Parse(c[4]),
                Categoria = c[5],
                Peso = double.Parse(c[6]),
                Stock = int.Parse(c[7]),
                CostoEnvio = decimal.Parse(c[8])
            }
            : new ProductoDigital
            {
                Codigo = c[1],
                Nombre = c[2],
                Descripcion = c[3],
                Precio = decimal.Parse(c[4]),
                Categoria = c[5],
                Peso = double.Parse(c[6]),
                Formato = c[7],
                UrlDescarga = c[8]
            };
    }
}

