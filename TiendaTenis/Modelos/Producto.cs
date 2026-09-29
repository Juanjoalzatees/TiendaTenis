using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TiendaTenis.Modelos;
public abstract class Producto
{
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public string Categoria { get; set; }
    public double Peso { get; set; }

    public abstract string UnidadPeso { get; }
    public abstract string TipoDescripcion { get; }
    public abstract bool HayDisponibilidad(int cantidad);
    public abstract decimal CalcularCostoEntrega();
    public abstract string Entregar();
    public virtual void RegistrarVenta(int cantidad) { }

    // Para la grilla
    public string PesoConUnidad => $"{Peso} {UnidadPeso}";

    public override string ToString() => $"{Codigo} - {Nombre}";
}