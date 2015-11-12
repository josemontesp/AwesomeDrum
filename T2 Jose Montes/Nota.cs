using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2_Jose_Montes
{
    public class Nota
    {
        public int i;
        public float pos;
        public int num;
        public string type = "";
        public string ruta;
        public string typeCompleto;

        public Nota(string i, string pos, string num, string type)
        {
            this.i = Int32.Parse(i);
            this.pos = float.Parse(pos);
            this.num = Int32.Parse(num);
            this.typeCompleto = type;
            foreach (string j in type.Split('_'))
            {
                this.type += j[0];
            }
            if (this.typeCompleto == "hihat_closed")
            {
                this.type = "hhc";
            }
            else if (this.typeCompleto == "hihat_open")
            {
                this.type = "hho";
            }
            this.ruta = ".\\..\\..\\..\\samples\\" + type + "\\" + this.type + num + ".wav";
            
        }
    }
}
