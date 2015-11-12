using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2_Jose_Montes
{
    public class Canales
    {
        public List<int> left = new List<int>();
        public List<int> right = new List<int>();

        public Canales(List<short> left, List<short> right)
        {
            foreach (short i in left)
            {
                this.left.Add(Convert.ToInt32(i));
            }
            foreach (short i in right)
            {
                this.right.Add(Convert.ToInt32(i));
            }
        }

        public Canales(List<int> left, List<int> right)
        {
            this.left = left;
            this.right = right;
        }

        public Canales(int largo) // inicializa 2 canales del largo especificado
        {
            for (int i = 0; i < largo; i++)
            {
                this.left.Add(0);
                this.right.Add(0);
            }
        }

        public void normalizar(bool silencio = false)
        {
            int max = right.Max();
            if (left.Max() > right.Max())
            {
                max = left.Max();
            }
            int min = right.Min();
            if (left.Min() < right.Min())
            {
                min = left.Min();
            }
            if (Math.Abs(min) > max)
            {
                max = Math.Abs(min);
            }
            for (int i = 0; i < left.Count; i++)
            {
                right[i] = right[i] * 30700 / max;
                left[i] = left[i] * 30700 / max;
            }
            //Eliminar bytes 0 del final
            bool continuar = true;
            while (continuar)
            {
                if (this.left.Last() == 0 && this.right.Last() == 0)
                {
                    left.RemoveAt(left.Count - 1);
                    right.RemoveAt(right.Count - 1);
                }
                else
                {
                    continuar = false;
                }
            }
            if (!silencio)
            {
                Console.WriteLine("Canales normalizados");
            }
            
        }

    }

}
