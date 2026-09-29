using CsvHelper;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using TiendaTenis.Modelos;

namespace TiendaTenis.Datos
{
    public class RepositorioClientes
    {
        private string rutaArchivo = "clientes.csv";
        private List<Cliente> clientes;

        public RepositorioClientes()
        {
            clientes = new List<Cliente>();
            CargarClientes();
        }

        public List<Cliente> ObtenerClientes()
        {
            return clientes;
        }

        private void CargarClientes()
        {
            if (File.Exists(rutaArchivo))
            {
                using (var reader = new StreamReader(rutaArchivo))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    clientes = csv.GetRecords<Cliente>().ToList();
                }
            }
        }

        private void GuardarClientes()
        {
            using (var writer = new StreamWriter(rutaArchivo))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(clientes);
            }
        }

        public void AgregarCliente(Cliente cliente)
        {
            if (clientes.Any(c => c.Documento == cliente.Documento))
            {
                throw new Exception("Ya existe un cliente con este documento.");
            }

            clientes.Add(cliente);
            GuardarClientes();
        }

        public void ActualizarCliente(Cliente clienteActualizado, string documentoOriginal)
        {
            if (documentoOriginal != clienteActualizado.Documento &&
                clientes.Any(c => c.Documento == clienteActualizado.Documento))
            {
                throw new Exception("El nuevo documento ya está asignado a otro cliente.");
            }

            var cliente = clientes.FirstOrDefault(c => c.Documento == documentoOriginal);
            if (cliente != null)
            {
                cliente.Documento = clienteActualizado.Documento;
                cliente.Nombre = clienteActualizado.Nombre;
                cliente.Correo = clienteActualizado.Correo;
                cliente.Telefono = clienteActualizado.Telefono;
                GuardarClientes();
            }
            else
            {
                throw new Exception("Cliente no encontrado.");
            }
        }

        public void EliminarCliente(string documento)
        {
            var cliente = clientes.FirstOrDefault(c => c.Documento == documento);
            if (cliente != null)
            {
                clientes.Remove(cliente);
                GuardarClientes();
            }
            else
            {
                throw new Exception("Cliente no encontrado.");
            }
        }
    }
}