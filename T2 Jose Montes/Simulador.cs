using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Media; // Para poder escuchar los wav mientras se edita una partitura
using System.Windows.Forms;
using System.Threading;


namespace T2_Jose_Montes
{
    public class Simulador
    {
        public string navegadorDeArchivos()
        {
            
            bool continuar = true;
            string directorio = ".\\..\\..\\..\\";
            string[] archivos = System.IO.Directory.GetFiles(directorio);
            directorio = System.IO.Directory.GetParent(archivos[0]).ToString();
            while (continuar)
            {
                
                string[] carpetas = System.IO.Directory.GetDirectories(directorio);
                archivos = System.IO.Directory.GetFiles(directorio);
                Console.WriteLine("Directorio Actual:");

                
                Console.WriteLine(directorio);
                Console.WriteLine("Contiene archivos:\n");
                Console.WriteLine("\t..\\");
                foreach (string a in carpetas)
                {
                    string[] nombre = a.Split('\\');
                    Console.WriteLine("\t" + nombre.Last());
                }
                foreach (string a in archivos)
                {
                    string[] nombre = a.Split('\\');
                    Console.WriteLine("\t" + nombre.Last());
                }
                Console.WriteLine("\n Escriba el nombre del archivo con extension o carpeta para navegar.\n Tambien puede ingresar la ruta exacta del archivo.\n Si la ruta exacta la tiene en el portapapeles presione ENTER");
                
                
                
                string request = Console.ReadLine();
                if (request == "")
                {
                    Console.Clear();
                    string idat = null; //http://stackoverflow.com/questions/518701/clipboard-gettext-returns-null-empty-string
                    Exception threadEx = null;
                    Thread staThread = new Thread(
                        delegate()
                        {
                            try
                            {
                                idat = Clipboard.GetText();
                            }

                            catch (Exception ex)
                            {
                                threadEx = ex;
                            }
                        });
                    staThread.SetApartmentState(ApartmentState.STA);
                    staThread.Start();
                    staThread.Join();
                    if (idat.Length > 100)
                    {
                        Console.WriteLine(" Tu portapapeles contiene:\n\n \"" + idat.Slice(0, 50) + "(...)" + idat.Slice(idat.Length - 50, -1) +"\"");
                    }
                    else
                    {
                        Console.WriteLine(" Tu portapapeles contiene:\n\n \"" + idat+ "\"");
                    }
                    Console.WriteLine("\n Quiere usarlo como ruta? (1) SI, (0) NO");
                    var usarlo = Console.ReadKey(true);
                    if (usarlo.Key == ConsoleKey.D1)
                    {
                        request = idat;
                    }

                    //C:\Users\Administrador\Documents\Visual Studio 2013\Projects\T2 Jose Montes\T2 Jose Montes\bin\Debug\tom.xml
                    //C:\\Users\\Administrador\\Documents\\Visual Studio 2013\\Projects\\T2 Jose Montes\\T2 Jose Montes\\bin\\Debug\\tom.xml
                    
                }
                if (request == ".." || request == "..\\") // Request directorio padre
                {
                    directorio += "\\..";
                    Console.Clear();
                }
                else if (request.Contains('.')) // Request de archivo
                {
                    if (File.Exists(directorio + "\\" + request))
                    {
                        return directorio + "\\" + request;
                    }
                    else if (File.Exists(request))
                    {
                        return request;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("El archivo no existe!\n");
                    }
                    
                }
                else // Request carpeta
                {
                    if (Directory.Exists(directorio + "\\" + request))
                    {
                        directorio += "\\" + request;
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("La carpeta no existe!\n");
                        
                    }
                }
            }
            return "hola";

        } //Abre la interfaz para navegar archivos y retorna la ruta del archivo seleccionado.

        public void XMLtoWav(string ruta, bool silencio = false)
        {
            Cancion song = new Cancion(ruta);
            int y = (Int32)Math.Round(song.duracion);
            int o = (Int32)Math.Ceiling( y * (44100 / 1000.0) + 6 * 44100.0);
            Canales main = new Canales(o);
            int Samplepos = 0;
            //Se agregan todas las notas al main
            foreach (Compas c in song.compases)
            {
                for (int i = 0; i < c.loop; i++)
                {
                    foreach (Nota n in c.notas)
                    {
                        var npos = Math.Round(((n.pos * 60 * (44100.0 / c.tempo)) + Samplepos));
                        main = incluirNota(main, n, (int)npos);
                    }
                    Samplepos += (int)Math.Round(c.duracion * (44100 / 1000.0));
                }
            }
            //Se normalizan los canales
            main.normalizar(silencio);
            
            List<short> leftSamples = new List<short>();
            List<short> rightSamples = new List<short>();
            for (int i = 0; i < main.left.Count; i++)
            {

                leftSamples.Add(Convert.ToInt16(main.left[i]));
                rightSamples.Add(Convert.ToInt16(main.right[i]));
            }
            SaveToWave(leftSamples, rightSamples, ruta.Slice(0,-3) + "wav", silencio);
        }

        public Canales incluirNota(Canales main, Nota nota, int pos)
        {
            string ruta = nota.ruta;

            List<short> lDataList = new List<short>();
            List<short> rDataList = new List<short>();

            using (FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read))
            using (BinaryReader br = new BinaryReader(fs))
            {
                br.ReadBytes(4);br.ReadUInt32();br.ReadBytes(4);br.ReadBytes(4); br.ReadUInt32();br.ReadUInt16(); br.ReadUInt16(); br.ReadUInt32(); br.ReadUInt32();
                var blockSize = br.ReadUInt16();
                br.ReadUInt16(); br.ReadBytes(4);
                var dataSize = br.ReadUInt32();

                for (int i = 0; i < dataSize / blockSize; i++)
                {
                    lDataList.Add((short)br.ReadUInt16());
                    rDataList.Add((short)br.ReadUInt16());
                }
                br.Close();
                fs.Close();
            }
            Canales channels = new Canales(lDataList, rDataList);

            for (int i = 0; i < channels.left.Count(); i++)
            {
                main.left[pos + i] += channels.left[i];
            }
            for (int i = 0; i < channels.right.Count(); i++)
            {
                main.right[pos + i] += channels.right[i];
            }
            
            return main;
        }

        public void SaveToWave(List<short> leftSamples, List<short> rightSamples, string filename, bool silencio = false) //https://gist.github.com/steveniles/3027614
        {
            const short NumberOfChannels = 2; // stereo audio
            const short BytesPerSample = 2;   // 16bit samples
            const int SamplingRate = 44100;   // 44.1 kHz
            var shortChannel = (leftSamples.Count < rightSamples.Count) ? leftSamples : rightSamples;
            int totalBytes = checked(44 + (shortChannel.Count * BytesPerSample * NumberOfChannels)); // size of headers + data
            var output = new byte[totalBytes];
            Buffer.BlockCopy(GetLEBytes(0x46464952), 0, output, 0, 4);  // "RIFF"
            Buffer.BlockCopy(GetLEBytes(totalBytes - 8), 0, output, 4, 4);  // RIFF chunk size
            Buffer.BlockCopy(GetLEBytes(0x45564157), 0, output, 8, 4);  // "WAVE"
            Buffer.BlockCopy(GetLEBytes(0x20746D66), 0, output, 12, 4); // "fmt "
            Buffer.BlockCopy(GetLEBytes(16), 0, output, 16, 4); // fmt chunk size
            Buffer.BlockCopy(GetLEBytes((short)1), 0, output, 20, 2);   // compression code (1 - PCM/Uncompressed)
            Buffer.BlockCopy(GetLEBytes((short)NumberOfChannels), 0, output, 22, 2);   // number of channels
            Buffer.BlockCopy(GetLEBytes(SamplingRate), 0, output, 24, 4);   // sampling rate
            Buffer.BlockCopy(GetLEBytes(SamplingRate * BytesPerSample * NumberOfChannels), 0, output, 28, 4);   // bytes/second
            Buffer.BlockCopy(GetLEBytes((short)(BytesPerSample * NumberOfChannels)), 0, output, 32, 2);   // block size
            Buffer.BlockCopy(GetLEBytes((short)(BytesPerSample * 8)), 0, output, 34, 2);  // bits per sample
            Buffer.BlockCopy(GetLEBytes(0x61746164), 0, output, 36, 4); // "data"
            Buffer.BlockCopy(GetLEBytes(totalBytes - 44), 0, output, 40, 4);    // data chunk size
            for (var i = 0; i < shortChannel.Count; i++)
            {
                Buffer.BlockCopy(GetLEBytes(leftSamples[i]), 0, output, (BytesPerSample * i * NumberOfChannels) + 44, BytesPerSample);
                Buffer.BlockCopy(GetLEBytes(rightSamples[i]), 0, output, (BytesPerSample * i * NumberOfChannels) + 44 + BytesPerSample, BytesPerSample);
            }

            File.WriteAllBytes(filename, output);
            if (!silencio)
            {
                Console.WriteLine("Se ha creado con exito el archivo!");
                System.Threading.Thread.Sleep(1500);
            }
            
        }

        public byte[] GetLEBytes(short value)
        {
            if (BitConverter.IsLittleEndian)
            {
                return BitConverter.GetBytes(value);
            }
            else
            {
                return BitConverter.GetBytes((short)((value & 0xFF) << 8 | (value & 0xFF00) >> 8));
            }
        }

        public byte[] GetLEBytes(int value)
        {
            if (BitConverter.IsLittleEndian)
            {
                return BitConverter.GetBytes(value);
            }
            else
            {
                return BitConverter.GetBytes((int)((value & 0xFF) << 24) | (int)((value & 0xFF00) << 8)
                    | (int)((value & 0xFF0000) >> 8) | (int)((value & 0xFF000000) >> 24));
            }
        }

        public void CreadorDePartituras()
        {
            
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("Ingresa el tempo de tu cancion");
            int tempo = Int32.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Ingresa la cantidad de compases");
            int compases = Int32.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Ingresa el largo en tiempos de cada compás");
            int largo = Int32.Parse(Console.ReadLine());

            Partitura p = new Partitura(compases, largo, tempo);
            p.mostrarPartitura();
            int posx = 9;
            int posy = 2;
            Console.SetCursorPosition(posx, posy);
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            while (keyInfo.Key != ConsoleKey.Escape)
            {
                
                if (keyInfo.Key == ConsoleKey.UpArrow && posy>2)
                {
                    posy -= 1;
                    Console.SetCursorPosition(posx, posy);
                } 
                else if (keyInfo.Key == ConsoleKey.DownArrow && posy < 12) 
                {
                    posy += 1;
                    Console.SetCursorPosition(posx, posy);
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow && posx > 9)
                {
                    if ((keyInfo.Modifiers == ConsoleModifiers.Shift) && posx > 16)
                    {
                        posx -= 8;
                    }
                    else
                    {
                        posx -= 1;
                    }
                    Console.SetCursorPosition(posx, posy);
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow && posx < compases*largo*16 + 8)
                {
                    if ((keyInfo.Modifiers == ConsoleModifiers.Shift) && posx < compases * largo * 16)
                    {
                        posx += 8;
                    }
                    else
                    {
                        posx += 1;
                    }
                    Console.SetCursorPosition(posx, posy);
                }
                else if (keyInfo.Key == ConsoleKey.D1)
                {
                    Console.SetCursorPosition(posx, posy);
                    Console.Write("1");
                    p.agregarElemento(posx, posy, 1);
                }
                else if (keyInfo.Key == ConsoleKey.D2)
                {
                    Console.SetCursorPosition(posx, posy);
                    Console.Write("2");
                    p.agregarElemento(posx, posy, 2);
                }
                else if (keyInfo.Key == ConsoleKey.D3)
                {
                    Console.SetCursorPosition(posx, posy);
                    Console.Write("3");
                    p.agregarElemento(posx, posy, 3);
                }
                else if (keyInfo.KeyChar.ToString() == ".")
                {
                    Console.SetCursorPosition(posx, posy);
                    Console.Write("·");
                    p.eliminarElemento(posx, posy);
                }
                else if (keyInfo.KeyChar.ToString() == "p")
                {
                    p.crearXML(".\\temporal.xml", true);
                    XMLtoWav(".\\temporal.xml", true);
                    SoundPlayer my_wave_file = new SoundPlayer(".\\temporal.wav");// http://stackoverflow.com/questions/14491431/playing-wav-file-with-c-sharp
                    my_wave_file.PlaySync();
                    File.Delete(".\\temporal.xml");
                    File.Delete(".\\temporal.wav");
                }
                else if (keyInfo.KeyChar.ToString() == "t")
                {
                    Console.Clear();
                    Console.WriteLine("Ingrese el nuevo tempo para su cancion");
                    p.tempo = Int32.Parse(Console.ReadLine());
                    p.mostrarPartitura();
                }
                Console.SetCursorPosition(posx, posy);
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Console.WriteLine("Está seguro que quiere salir? (ESC) SI, (0) NO");
                    keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key != ConsoleKey.Escape)
                    {
                        Console.Clear();
                        p.mostrarPartitura();
                    }
                }
                
            }
            Console.Clear();
            Console.WriteLine("Ingrese el nombre de su creación sin extencion");
            string nombre = Console.ReadLine();


            p.crearXML(".\\..\\..\\..\\creaciones\\"+ nombre + ".xml");

            Console.WriteLine("Quieres crear un archivo WAV con tu creación? (1) SI (0) NO");
            var req = Console.ReadKey();
            if (req.Key == ConsoleKey.D1)
            {
                this.XMLtoWav(".\\..\\..\\..\\creaciones\\" + nombre + ".xml");
            }
            

        }

        
    }
}
