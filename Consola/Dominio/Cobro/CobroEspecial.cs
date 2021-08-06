using System;
namespace Consola.Dominio.Cobro
{
    public class CobroEspecial : Cobro
    {
        private int MontoAdicional = 1000;
        public override int FactorInteresPorMora { get => 2; }

        public override double CalcularAdicional()
        {
            return base.CalcularAdicional() + MontoAdicional;
        }
    }
}
