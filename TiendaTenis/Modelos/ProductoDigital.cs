namespace TiendaTenis.Modelos;
public class ProductoDigital : Producto
{
    public string Formato { get; set; }
    public string UrlDescarga { get; set; }

    public override string UnidadPeso => "MB";
    public override string TipoDescripcion => "Digital";
    public override bool HayDisponibilidad(int cantidad) => true;
    public override decimal CalcularCostoEntrega() => 0m;
    public override string Entregar() => $"{Nombre}: descarga en {UrlDescarga}";

    public override string ALineaCsv() =>
        ParteComun("D") + ";" + Formato + ";" + UrlDescarga;
}