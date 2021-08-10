using System;
namespace Consola.Dominio.Cobro
{
    public class Cobro : ICloneable, IComparable<Cobro>
    {
        public int Codigo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaCancelacion { get; set; }
        public double Deuda { get; set; }

        public EstadoCobro Estado { get; set; }

        // Legajo cliente
        public int Cliente { get; set; }
        public virtual int FactorInteresPorMora { get => 1; }

        public Cobro()
        {
            Estado = EstadoCobro.Pendiente;
        }

        public virtual double CalcularAdicional()
        {
            DateTime fechaEjecucion = Estado == EstadoCobro.Cancelado ? FechaCancelacion : DateTime.Now;

            int diasRetraso = (int)(fechaEjecucion - FechaVencimiento).TotalDays;
            if (diasRetraso > 0)
            {
                double adicional = Deuda * ((FactorInteresPorMora * diasRetraso) / 100.0);
                return adicional;
            }
            return 0;
        }

        public virtual ImporteSegmentado CalcularImportePorAbonar()
        {
            ImporteSegmentado importe = new ImporteSegmentado(Deuda, CalcularAdicional());
            return importe;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public int CompareTo(Cobro otroCobro)
        {
            double importeTotalPropio = CalcularImportePorAbonar().ImporteTotal;
            double importeTotalOtroCobro = otroCobro.CalcularImportePorAbonar().ImporteTotal;
            if (importeTotalPropio > importeTotalOtroCobro) return -1;
            else if (importeTotalPropio == importeTotalOtroCobro) return 0;
            return 1;
        }
    }
}
