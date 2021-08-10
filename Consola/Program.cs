using System;
using Consola.Dominio;
using Consola.Dominio.Cobro;
using Consola.Negocio;

namespace Consola
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            ClienteNegocio clienteNegocio = new ClienteNegocio();
            CobroNegocio cobroNegocio = new CobroNegocio();

            try
            {

                int legajoJuan = 1;
                int legajoBergo = 2;
                int codigoCobroNormal = 1;
                int codigoCobroEspecial = 2;
                int codigoCobroBloqueado= 3;

                clienteNegocio.CrearCliente(legajoJuan, "Juan");
                clienteNegocio.CrearCliente(legajoBergo, "Bergo");

                cobroNegocio.CrearCobro(
                    codigoCobroNormal,
                    DateTime.Now.AddDays(-10),
                    5000,
                    legajoJuan,
                    TipoCobro.Normal
                );

                cobroNegocio.CrearCobro(
                    codigoCobroEspecial,
                    DateTime.Now.AddDays(-5),
                    1000,
                    legajoJuan,
                    TipoCobro.Especial
                );

                // Boton - CalcularDeuda
                // Obtener el cobro
                Cobro cn = cobroNegocio.ObtenerCobro(codigoCobroNormal);
                ImporteSegmentado importeCN = cn.CalcularImportePorAbonar();
                Console.WriteLine($"El importe básico es: ${importeCN.ImporteBase}. \nEl importe adicional es: ${importeCN.ImporteAdicional}. \nEl importe total es: ${importeCN.ImporteTotal}.");

                // Boton MessageBox - CalcularDeuda
                cobroNegocio.CancelarCobro(codigoCobroNormal, 5500);

                cobroNegocio.CancelarCobro(codigoCobroEspecial, 2100);

                cobroNegocio.CrearCobro(
                    codigoCobroBloqueado,
                    DateTime.Now.AddDays(-10),
                    2000,
                    legajoJuan,
                    TipoCobro.Normal
                );

                cobroNegocio.CrearCobro(
                    4,
                    DateTime.Now.AddDays(-10),
                    7000,
                    legajoBergo,
                    TipoCobro.Normal
                );

                cobroNegocio.CancelarCobro(codigoCobroBloqueado, 2200);
                cobroNegocio.CancelarCobro(4, 7700);

                cobroNegocio.ObtenerGrilla4(legajoJuan, CriteriosOrdenamiento.MenorAMayor);

                foreach (var item in cobroNegocio.ObtenerGrilla5())
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine("Corrí feliz");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Cobro normalVencido = new Cobro() {
            //    Codigo = 1,
            //    Cliente = cliente.Legajo,
            //    Deuda = 1000,
            //    FechaVencimiento = DateTime.Now.AddDays(-7)
            //};

            //Cobro normalPorVencer = new Cobro()
            //{
            //    Codigo = 2,
            //    Cliente = cliente.Legajo,
            //    Deuda = 1000,
            //    FechaVencimiento = DateTime.Now.AddDays(1)
            //};

            //CobroEspecial especialVencido = new CobroEspecial()
            //{
            //    Codigo = 3,
            //    Cliente = cliente.Legajo,
            //    Deuda = 1000,
            //    FechaVencimiento = DateTime.Now.AddDays(-7)
            //};

            //CobroEspecial especialPorVencer = new CobroEspecial()
            //{
            //    Codigo = 4,
            //    Cliente = cliente.Legajo,
            //    Deuda = 1000,
            //    FechaVencimiento = DateTime.Now.AddDays(1)
            //};


            //ImporteSegmentado importeNormalVencido = normalVencido.CalcularImportePorAbonar();
            //ImporteSegmentado importeNormalPorVencer = normalPorVencer.CalcularImportePorAbonar();
            //ImporteSegmentado importeEspecialVencido = especialVencido.CalcularImportePorAbonar();
            //ImporteSegmentado importeEspecialPorVencer = especialPorVencer.CalcularImportePorAbonar();



        }
    }
}
