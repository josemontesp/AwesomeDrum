using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;


namespace T2_Jose_Montes
{
    class Program
    {

        static void Main(string[] args)
        {
            string request = "1";
            while (request != "0")
            {
                
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                Simulador sim = new Simulador();

                //ArchivoWav a = new ArchivoWav();
                //a.WaveHeaderIN(sim.navegadorDeArchivos());

                Console.WriteLine("Bienvenido al Drum Machine del general Zod-To!");
                Console.WriteLine("\n(1) Crear wav a partir de un XML");
                Console.WriteLine("(2) Crear XML usando el editor de partituras");
                Console.WriteLine("\n(ESC) Salir");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.D1)
                {
                    Console.Clear();
                    sim.XMLtoWav(sim.navegadorDeArchivos());
                }
                else if (keyInfo.Key == ConsoleKey.D2)
                {
                    sim.CreadorDePartituras();
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(-1);
                }
            }
            
            

        

        }
        
    }
}
