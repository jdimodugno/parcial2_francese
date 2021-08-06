using System;

namespace Consola.Dominio
{
    public class Cliente: ICloneable
    {
        public int Legajo { get; }
        public string Nombre { get; set; }

        public Cliente(int _legajo)
        {
            Legajo = _legajo;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
