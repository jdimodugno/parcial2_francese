using System;
using Consola.Datos;
using Consola.Dominio;

namespace Consola.Negocio
{
    public class ClienteNegocio
    {
        public void CrearCliente(
            int legajo,
            string nombre
        )
        {
            Repositorio repo = Repositorio.ObtenerInstacia();
            if (ExisteCliente(legajo)) throw new Exception("El cliente que desea dar de alta ya existe");
            Cliente nuevoCliente = new Cliente(legajo) { Nombre = nombre };
            repo.CrearCliente(nuevoCliente);
        }

        public void ActualizarCliente(Cliente clienteActualizado)
        {
            Repositorio repo = Repositorio.ObtenerInstacia();
            repo.ActualizarCliente(clienteActualizado);
        }

        public void EliminarCliente(int legajo)
        {
            Repositorio repo = Repositorio.ObtenerInstacia();
            if (repo.ClientePoseeCobros(legajo)) throw new Exception("No puede eliminarse un cliente que posee cobros");
            repo.EliminarCliente(legajo);
        }

        private bool ExisteCliente(int legajo)
        {
            Repositorio repo = Repositorio.ObtenerInstacia();
            return repo.ExisteCliente(legajo);
        }
    }
}
