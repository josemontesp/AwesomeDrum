using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace T2_Jose_Montes
{
    public class ArchivoWav
    {
        // Esta clase se usó para aprender sobre headers y archivos wav. No se instancia en la tarea final.
        public int fileSize;
        public int bitSize;
        public int numChannels;
        public int sampleRate;
        public int bitsPerSample;
        public int dataSize;
        public int numSeconds;
        public string fileName;

        public void WaveHeaderIN(string spath) // http://www.codeproject.com/Articles/15187/Concatenating-Wave-Files-Using-C
        {
            FileStream fs = new FileStream(spath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            this.fileSize = (int)fs.Length - 8;
            fs.Position = 22;
            this.numChannels = br.ReadInt16();
            fs.Position = 24;
            this.sampleRate = br.ReadInt32();
            fs.Position = 34;
            this.bitsPerSample = br.ReadInt16();
            this.dataSize = (int)fs.Length - 44;
            br.Close ();
            fs.Close();
        }


        private void WaveHeaderOUT(string sPath) // http://www.codeproject.com/Articles/15187/Concatenating-Wave-Files-Using-C
        {
            FileStream fs = new FileStream(sPath, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(new char[4] { 'R', 'I', 'F', 'F' });

            bw.Write(fileSize);

            bw.Write(new char[8] { 'W', 'A', 'V', 'E', 'f', 'm', 't', ' ' });

            bw.Write((int)16);

            bw.Write((short)1);
            bw.Write(numChannels);

            bw.Write(sampleRate);

            bw.Write((int)(sampleRate * ((bitsPerSample * numChannels) / 8)));

            bw.Write((short)((bitsPerSample * numChannels) / 8));

            bw.Write(bitsPerSample);

            bw.Write(new char[4] { 'd', 'a', 't', 'a' });
            bw.Write(dataSize);
            bw.Close();
            fs.Close();
        }


    }
}
//if (!(File.Exists(ruta)))
//{
//    Console.WriteLine("El archivo que se trata de abrir no existe y se ha creado");
//}
//FileStream stream = new FileStream(ruta, FileMode.OpenOrCreate);
//BinaryReader reader = new BinaryReader(stream);

////<header>
//reader.ReadChars(4).CustomToString(); // RIFF
//this.fileSize = (reader.ReadBytes(4).CustomToString()); // File Size
//reader.ReadChars(4).CustomToString(); // FileType header = WAVE
//reader.ReadChars(4).CustomToString(); // "fmt "
//this.bitSize = (reader.ReadBytes(4).CustomToString()); // Length of format data
//reader.ReadBytes(2).CustomToString(); // Type of format, 1 = PCM
//this.numChannels = (reader.ReadBytes(2).CustomToString()); // N of channels
//this.sampleRate = (reader.ReadBytes(4).CustomToString()); // Sample rate
//reader.ReadBytes(4).CustomToString(); // (Sample Rate * BitsPerSample * Channels) / 8. or byterate
//reader.ReadBytes(2).CustomToString(); // (BitsPerSample * Channels) / 8
//this.bitsPerSample = (reader.ReadBytes(2).CustomToString()); // BitsPerSample
//reader.ReadChars(4).CustomToString(); // Data header
//this.dataSize = reader.ReadBytes(4).CustomToString(); // Size of data
////</header>

//stream.Close();
//reader.Close();