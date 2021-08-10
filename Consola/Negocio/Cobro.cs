using System;
using System.Collections.Generic;
using Consola.Datos;
using Consola.Dominio.Cobro;

namespace Consola.Negocio
{
    public class CobroNegocio
    {
        public void CrearCobro(
            int codigo,
            DateTime fechaVencimiento,
            double deuda,
            int cliente,
            TipoCobro tipoCobro
        )
        {
            Repositorio repo = Repositorio.ObtenerInstacia();

            if (ExisteCobro(codigo)) throw new Exception("El cobro que desea dar de alta ya existe");
            if (ClienteYaPoseeMaximoCobrosPendientes(cliente)) throw new Exception("El cliente ya posee dos cobros pendientes");

            if (tipoCobro == TipoCobro.Normal)
            {
                Cobro cobro = new Cobro()
                {
                    Codigo = codigo,
                    Deuda = deuda,
                    Cliente = cliente,
                    FechaVencimiento = fechaVencimiento
                };
                repo.CrearCobro(cobro);
            }
            else if (tipoCobro == TipoCobro.Especial)
            {
                CobroEspecial cobro = new CobroEspecial()
                {
                    Codigo = codigo,
                    Deuda = deuda,
                    Cliente = cliente,
                    FechaVencimiento = fechaVencimiento
                };
                repo.CrearCobro(cobro);
            }
        }

        public Cobro ObtenerCobro(int codigo)
        {
            Repositorio repo = Repositorio.ObtenerInstacia();
            return repo.ObtenerCobro(codigo);
        }

        public void CancelarCobro(int codigoCobro, double importe)
        {
            Repositorio repo = Repositorio.ObtenerInstacia();
            Cobro cobro = repo.ObtenerCobro(codigoCobro);
            if (cobro.Estado == EstadoCobro.Cancelado) throw new Exception("El cobro ya se encuentra cancelado");
            if (importe < cobro.CalcularImportePorAbonar().ImporteTotal) throw new Exception("El importe es insuficiente");
            repo.CancelarCobro(codigoCobro);
        }

        public List<Cobro> ObtenerGrilla3(int legajo)
        {
            Repositorio repo = Repositorio.ObtenerInstacia();
            return repo.ObtenerListadoGrilla3(legajo);
        }

        public List<Cobro> ObtenerGrilla4(int legajo, CriteriosOrdenamiento criterio)
        {
            Repositorio repo = Repositorio.ObtenerInstacia();
            return repo.ObtenerListadoGrilla4(legajo, criterio);
        }

        public List<object> ObtenerGrilla5()
        {
            Repositorio repo = Repositorio.ObtenerInstacia();
            return repo.ObtenerListadoGrilla5();
        }

        private bool ClienteYaPoseeMaximoCobrosPendientes(int legajo) {
            Repositorio repo = Repositorio.ObtenerInstacia();
            return repo.ObtenerCantidadCobrosPendientesPorCliente(legajo) == 2;
        }

        private bool ExisteCobro(int codigo)
        {
            Repositorio repo = Repositorio.ObtenerInstacia();
            return repo.ExisteCobro(codigo);
        }

    }
}
