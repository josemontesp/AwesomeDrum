using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace T2_Jose_Montes
{
    public class Cancion
    {
        public List<Compas> compases = new List<Compas>();
        public float duracion;

        public Cancion(string ruta)
        {
            XmlReader reader = XmlReader.Create(ruta);
            List<Nota> notas = new List<Nota>();
            while (reader.Read())
            {
                if (reader.HasAttributes && reader.Name == "bar")
                {
                    Compas comp = new Compas((reader.GetAttribute("loop")), (reader.GetAttribute("tempo")), (reader.GetAttribute("length")));
                    compases.Add(comp);
                }
                if (reader.HasAttributes && reader.Name == "note")
                {
                    Nota n = new Nota(reader.GetAttribute("i"), reader.GetAttribute("pos"), reader.GetAttribute("num"), reader.GetAttribute("type"));
                    compases.Last().agregarNota(n);
                }
            }
            foreach (Compas c in compases)
            {
                duracion += c.duracion;
            }
        }
        public Cancion()
        {

        }

       
    }
}
