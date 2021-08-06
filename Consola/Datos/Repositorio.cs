using System;
using System.Collections.Generic;
using System.Linq;
using Consola.Dominio;
using Consola.Dominio.Cobro;

namespace Consola.Datos
{
    public class Repositorio
    {
        static Repositorio Instancia;

        List<Cliente> clientes = new List<Cliente>();
        List<Cobro> cobros = new List<Cobro>();

        public static Repositorio ObtenerInstacia()
        {
            if (Instancia == null) Instancia = new Repositorio();
            return Instancia;
        }

        // Cliente
        public Cliente ObtenerCliente(int legajo)
        {
            return (Cliente)clientes.Find(c => c.Legajo == legajo).Clone();
        }

        public void CrearCliente(Cliente nuevoCliente)
        {
            clientes.Add(nuevoCliente);
        }

        public void ActualizarCliente(Cliente clienteActualizado)
        {
            int indiceCliente = clientes.FindIndex(c => c.Legajo == clienteActualizado.Legajo);
            clientes[indiceCliente] = clienteActualizado;
        }

        public void EliminarCliente(int legajo)
        {
            int indiceCliente = clientes.FindIndex(c => c.Legajo == legajo);
            clientes.RemoveAt(indiceCliente);
        }

        public bool ExisteCliente(int legajo)
        {
            return clientes.Exists(c => c.Legajo == legajo);
        }

        public bool ClientePoseeCobros(int legajo)
        {
            return cobros.Exists(c => c.Cliente == legajo);
        }

        // Cobros
        public Cobro ObtenerCobro(int codigo)
        {
            return (Cobro)cobros.Find(c => c.Codigo == codigo).Clone();
        }

        public bool ExisteCobro(int codigo)
        {
            return cobros.Exists(c => c.Codigo == codigo);
        }

        public void CrearCobro(Cobro nuevoCobro)
        {
            cobros.Add(nuevoCobro);
            System.Console.WriteLine();
        }

        public void CancelarCobro(int codigoCobro)
        {
            int indiceCobro = cobros.FindIndex(c => c.Codigo == codigoCobro);
            cobros[indiceCobro].Estado = EstadoCobro.Cancelado;
        }

        public int ObtenerCantidadCobrosPendientesPorCliente(int legajo)
        {
            return cobros.Where(c => c.Cliente == legajo && c.Estado == EstadoCobro.Pendiente).Count();
        }

        //  LINQ -- Usamos Where
        public List<Cobro> ObtenerListadoGrilla3(int legajo)
        {
            return cobros.Where(c => c.Estado == EstadoCobro.Cancelado && c.Cliente == legajo).ToList();
        }

        //  LINQ -- Usamos Where
        public List<Cobro> ObtenerListadoGrilla4(int legajo, CriteriosOrdenamiento criterio)
        {

            List<Cobro> cancelados = cobros.Where(c => c.Estado == EstadoCobro.Cancelado && c.Cliente == legajo).ToList();

            cancelados.Sort();
            // Ordeno siempre, si lo quiero de menor a mayor, lo invierto
            if (criterio == CriteriosOrdenamiento.MenorAMayor) cancelados.Reverse();

            return cancelados;
        }
    }
}