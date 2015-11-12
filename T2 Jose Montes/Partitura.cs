using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace T2_Jose_Montes
{
    public class Partitura
    {
        public int[] TL;
        public int[] TH;
        public int[] TF;
        public int[] S;
        public int[] R;
        public int[] K;
        public int[] HHO;
        public int[] HHC;
        public int[] CL;
        public int[] CH;
        public int[] C;
        public int compases;
        public int tiempos;
        public int tempo;
        public Partitura(int compases, int tiempos, int tempo)
        {
            this.TL = new Int32[compases * tiempos * 16];
            this.TL = new Int32[compases * tiempos * 16];
            this.TH = new Int32[compases * tiempos * 16];
            this.TF = new Int32[compases * tiempos * 16];
            this.S = new Int32[compases * tiempos * 16];
            this.R = new Int32[compases * tiempos * 16];
            this.K = new Int32[compases * tiempos * 16];
            this.HHO = new Int32[compases * tiempos * 16];
            this.HHC = new Int32[compases * tiempos * 16];
            this.CL = new Int32[compases * tiempos * 16];
            this.CH = new Int32[compases * tiempos * 16];
            this.C = new Int32[compases * tiempos * 16];
            this.compases = compases;
            this.tiempos = tiempos;
            this.tempo = tempo;
        }

        public void mostrarPartitura()
        {
            Console.Clear();
            Console.SetWindowSize(150, 24);
            
            if (compases * tiempos * 16 > 139)
            {
                Console.BufferWidth = compases * tiempos * 16 + 10;
            }
            string regla1 = "         |";
            string espacio = "";
            for (int i=0; i<tiempos; i++)
            {
                espacio += "        ";
            }
            for (int i = 1; i < compases; i++)
            {
                if (i < 10)
                {
                    regla1 += espacio.Slice(0, -1) + i.ToString() + espacio.Slice(0, -1) + "|";
                }
                else if (i < 100)
                {
                    regla1 += espacio.Slice(0, -2) + i.ToString() + espacio.Slice(0, -1) + "|";
                }
                else
                {
                    regla1 += espacio.Slice(0, -3) + i.ToString() + espacio.Slice(0, -1) + "|";
                }
            }
            Console.WriteLine(regla1);
            string regla = " \t ";
            for (int j = 0; j < compases;  j++)
            {
                for (int i = 1; i < tiempos + 1; i++)
                {
                    regla += i + "               ";
                }
            }
            Console.WriteLine(regla);
            Console.Write(" TL\t|");
            foreach (int i in TL)
            {
                if (i == 0)
                {
                    Console.Write("·");
                }
                else
                {
                    Console.Write(i);
                }
            }
            Console.Write("\n TH\t|");
            foreach (int i in TH)
            {
                if (i == 0)
                {
                    Console.Write("·");
                }
                else
                {
                    Console.Write(i);
                }
            }
            Console.Write("\n TF\t|");
            foreach (int i in TF)
            {
                if (i == 0)
                {
                    Console.Write("·");
                }
                else
                {
                    Console.Write(i);
                }
            }
            Console.Write("\n S\t|");
            foreach (int i in S)
            {
                if (i == 0)
                {
                    Console.Write("·");
                }
                else
                {
                    Console.Write(i);
                }
            }
            Console.Write("\n R\t|");
            foreach (int i in R)
            {
                if (i == 0)
                {
                    Console.Write("·");
                }
                else
                {
                    Console.Write(i);
                }
            }
            Console.Write("\n K\t|");
            foreach (int i in K)
            {
                if (i == 0)
                {
                    Console.Write("·");
                }
                else
                {
                    Console.Write(i);
                }
            }
            Console.Write("\n HHO\t|");
            foreach (int i in HHO)
            {
                if (i == 0)
                {
                    Console.Write("·");
                }
                else
                {
                    Console.Write(i);
                }
            }
            Console.Write("\n HHC\t|");
            foreach (int i in HHC)
            {
                if (i == 0)
                {
                    Console.Write("·");
                }
                else
                {
                    Console.Write(i);
                }
            }
            Console.Write("\n CL\t|");
            foreach (int i in CL)
            {
                if (i == 0)
                {
                    Console.Write("·");
                }
                else
                {
                    Console.Write(i);
                }
            }
            Console.Write("\n CH\t|");
            foreach (int i in CH)
            {
                if (i == 0)
                {
                    Console.Write("·");
                }
                else
                {
                    Console.Write(i);
                }
            }
            Console.Write("\n C\t|");
            foreach (int i in C)
            {
                if (i == 0)
                {
                    Console.Write("·");
                }
                else
                {
                    Console.Write(i);
                }
            }
            Console.WriteLine("\n\n Mueve el cursor con las flechas");
            Console.WriteLine(" Usa Shift + flecha para moverte más rápido");
            Console.WriteLine(" Crea una nota con las teclas (1) (2) (3) según el sample");
            Console.WriteLine(" Borra una nota con un punto (.)");
            Console.WriteLine(" (P) Play!");
            Console.WriteLine(" (T) Para cambiar el tempo");
            Console.WriteLine("\n Para terminar el proyecto presione ESC");

        }

        public string entregarElemento(int cursorposx, int cursorposy)
        {
            cursorposy -= 2;
            cursorposx -= 9;
            if (cursorposy == 0)
            {
                if (TL[cursorposx] == 0)
                {
                    return "·";
                }
                return TL[cursorposx].ToString();
            }
            else if (cursorposy == 1)
            {
                if (TH[cursorposx] == 0)
                {
                    return "·";
                }
                return TH[cursorposx].ToString();
            }
            else if (cursorposy == 2)
            {
                if (TF[cursorposx] == 0)
                {
                    return "·";
                }
                return TF[cursorposx].ToString();
            }
            else if (cursorposy == 3)
            {
                if (S[cursorposx] == 0)
                {
                    return "·";
                }
                return S[cursorposx].ToString();
            }
            else if (cursorposy == 4)
            {
                if (R[cursorposx] == 0)
                {
                    return "·";
                }
                return R[cursorposx].ToString();
            }
            else if (cursorposy == 5)
            {
                if (K[cursorposx] == 0)
                {
                    return "·";
                }
                return K[cursorposx].ToString();
            }
            else if (cursorposy == 6)
            {
                if (HHO[cursorposx] == 0)
                {
                    return "·";
                }
                return HHO[cursorposx].ToString();
            }
            else if (cursorposy == 7)
            {
                if (HHC[cursorposx] == 0)
                {
                    return "·";
                }
                return HHC[cursorposx].ToString();
            }
            else if (cursorposy == 8)
            {
                if (CL[cursorposx] == 0)
                {
                    return "·";
                }
                return CL[cursorposx].ToString();
            }
            else if (cursorposy == 9)
            {
                if (CH[cursorposx] == 0)
                {
                    return "·";
                }
                return CH[cursorposx].ToString();
            }
            else if (cursorposy == 10)
            {
                if (C[cursorposx] == 0)
                {
                    return "·";
                }
                return C[cursorposx].ToString();
            }
            else return "cursor out of range";
        }

        public void agregarElemento(int cursorposx, int cursorposy, int elemento)

        {
            cursorposy -= 2;
            cursorposx -= 9;
            if (cursorposy == 0)
            {
                TL[cursorposx] = elemento;
            }
            else if (cursorposy == 1)
            {
                TH[cursorposx] = elemento;
            }
            else if (cursorposy == 2)
            {
                TF[cursorposx] = elemento;
            }
            else if (cursorposy == 3)
            {
                S[cursorposx] = elemento;
            }
            else if (cursorposy == 4)
            {
                R[cursorposx] = elemento;
            }
            else if (cursorposy == 5)
            {
                K[cursorposx] = elemento;
            }
            else if (cursorposy == 6)
            {
                HHO[cursorposx] = elemento;
            }
            else if (cursorposy == 7)
            {
                HHC[cursorposx] = elemento;
            }
            else if (cursorposy == 8)
            {
                CL[cursorposx] = elemento;
            }
            else if (cursorposy == 9)
            {
                CH[cursorposx] = elemento;
            }
            else if (cursorposy == 10)
            {
                C[cursorposx] = elemento;
            }
            else Console.WriteLine("cursor out of range");
        }

        public void eliminarElemento(int cursorposx, int cursorposy)
        {
            cursorposy -= 2;
            cursorposx -= 9;
            if (cursorposy == 0)
            {
                TL[cursorposx] = 0;
            }
            else if (cursorposy == 1)
            {
                TH[cursorposx] = 0;
            }
            else if (cursorposy == 2)
            {
                TF[cursorposx] = 0;
            }
            else if (cursorposy == 3)
            {
                S[cursorposx] = 0;
            }
            else if (cursorposy == 4)
            {
                R[cursorposx] = 0;
            }
            else if (cursorposy == 5)
            {
                K[cursorposx] = 0;
            }
            else if (cursorposy == 6)
            {
                HHO[cursorposx] = 0;
            }
            else if (cursorposy == 7)
            {
                HHC[cursorposx] = 0;
            }
            else if (cursorposy == 8)
            {
                CL[cursorposx] = 0;
            }
            else if (cursorposy == 9)
            {
                CH[cursorposx] = 0;
            }
            else if (cursorposy == 10)
            {
                C[cursorposx] = 0;
            }
            else Console.WriteLine("cursor out of range");
        }

        public void crearXML(string ruta, bool silencio = false)
        {
            Cancion song = new Cancion();
            for (int i = 0; i < compases; i++)
            {
                Compas c = new Compas("1", this.tempo.ToString(), this.tiempos.ToString());
                for (int j = 0; j < 16*this.tiempos; j++)
                {
                    if (TL[i * 16 * tiempos + j] != 0)
                    {
                        Nota n = new Nota("100", (j / 16.0).ToString(), TL[i * 16 * tiempos + j].ToString(), "tom_low");
                        c.agregarNota(n);
                    }
                    if (TH[i * 16 * tiempos + j] != 0)
                    {
                        Nota n = new Nota("100", (j / 16.0).ToString(), TH[i * 16 * tiempos + j].ToString(), "tom_high");
                        c.agregarNota(n);
                    }
                    if (TF[i * 16 * tiempos + j] != 0)
                    {
                        Nota n = new Nota("100", (j / 16.0).ToString(), TF[i * 16 * tiempos + j].ToString(), "tom_floor");
                        c.agregarNota(n);
                    }
                    if (S[i * 16 * tiempos + j] != 0)
                    {
                        Nota n = new Nota("100", (j / 16.0).ToString(), S[i * 16 * tiempos + j].ToString(), "snare");
                        c.agregarNota(n);
                    }
                    if (R[i * 16 * tiempos + j] != 0)
                    {
                        Nota n = new Nota("100", (j / 16.0).ToString(), R[i * 16 * tiempos + j].ToString(), "ride");
                        c.agregarNota(n);
                    }
                    if (K[i * 16 * tiempos + j] != 0)
                    {
                        Nota n = new Nota("100", (j / 16.0).ToString(), K[i * 16 * tiempos + j].ToString(), "kick");
                        c.agregarNota(n);
                    }
                    if (HHO[i * 16 * tiempos + j] != 0)
                    {
                        Nota n = new Nota("100", (j / 16.0).ToString(), HHO[i * 16 * tiempos + j].ToString(), "hihat_open");
                        c.agregarNota(n);
                    }
                    if (HHC[i * 16 * tiempos + j] != 0)
                    {
                        Nota n = new Nota("100", (j / 16.0).ToString(), HHC[i * 16 * tiempos + j].ToString(), "hihat_closed");
                        c.agregarNota(n);
                    }
                    if (CL[i * 16 * tiempos + j] != 0)
                    {
                        Nota n = new Nota("100", (j / 16.0).ToString(), CL[i * 16 * tiempos + j].ToString(), "crash_low");
                        c.agregarNota(n);
                    }
                    if (CH[i * 16 * tiempos + j] != 0)
                    {
                        Nota n = new Nota("100", (j / 16.0).ToString(), CH[i * 16 * tiempos + j].ToString(), "crash_high");
                        c.agregarNota(n);
                    }
                    if (C[i * 16 * tiempos + j] != 0)
                    {
                        Nota n = new Nota("100", (j / 16.0).ToString(), C[i * 16 * tiempos + j].ToString(), "cowbell");
                        c.agregarNota(n);
                    }
                }
                song.compases.Add(c);
            }


            using (XmlWriter writer = XmlWriter.Create(ruta))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("barList");

                foreach (Compas c in song.compases)
                {
                    writer.WriteStartElement("bar");
                    writer.WriteAttributeString("loop", c.loop.ToString());
                    writer.WriteAttributeString("tempo", c.tempo.ToString());
                    writer.WriteAttributeString("length", c.length.ToString());
                        writer.WriteStartElement("notelist");
                        foreach (Nota n in c.notas)
                        {
                            writer.WriteStartElement("note");
                            writer.WriteAttributeString("i", n.i.ToString());
                            writer.WriteAttributeString("pos", n.pos.ToString());
                            writer.WriteAttributeString("num", n.num.ToString());
                            writer.WriteAttributeString("type", n.typeCompleto);
                            writer.WriteEndElement();
                        }

                        writer.WriteEndElement();


                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
                if (!silencio)
                {
                    Console.WriteLine("XML creado correctamente!");
                    Console.WriteLine(ruta);
                    System.Threading.Thread.Sleep(1500);
                }
                

            }
        }

    }

}
