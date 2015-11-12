using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace T2_Jose_Montes
{
    public class Compas
    {
        public int loop;
        public int tempo;
        public int length;
        public float duracion; // en milisegundos
        public List<Nota> notas = new List<Nota>();

        public Compas(string loop, string tempo, string length)
        {
            this.loop = Int32.Parse(loop);
            this.tempo = Int32.Parse(tempo);
            this.length = Int32.Parse(length);
            this.duracion = this.length * 60000 * this.loop / this.tempo;
        }

        public void agregarNota(Nota n)
        {
            this.notas.Add(n);
        }
    }
}
