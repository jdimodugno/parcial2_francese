using System;
namespace Consola.Dominio.Cobro
{
    public struct ImporteSegmentado
    {
        public double ImporteBase { get; set; }
        public double ImporteAdicional { get; set; }
        public double ImporteTotal { get; set; }

        public ImporteSegmentado(double _importeBase, double _importeAdicional) {
            ImporteBase = _importeBase;
            ImporteAdicional = _importeAdicional;
            ImporteTotal = ImporteBase + ImporteAdicional;
        }
    }
}
