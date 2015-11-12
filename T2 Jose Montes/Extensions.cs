using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2_Jose_Montes
{
    public static class Extensions
    {
        
        public static string Slice(this string source, int start, int end) //http://www.dotnetperls.com/string-slice
        {
            /// <summary>
            /// Get the string slice between the two indexes.
            /// Inclusive for start index, exclusive for end index.
            /// </summary>
            if (end < 0) // Keep this for negative end support
            {
                end = source.Length + end;
            }
            int len = end - start;               // Calculate length
            return source.Substring(start, len); // Return Substring of length
        }

        public static string CustomToString(this char[] source)
        {
            string acumulador = "";
            foreach (object i in source)
            {
                acumulador += i;
            }
            return acumulador;
        }

        public static string CustomToString(this string[] source)
        {
            string acumulador = "";
            foreach (object i in source)
            {
                acumulador += i;
            }
            return acumulador;
        }

        public static string CustomToString(this int[] source)
        {
            string acumulador = "";
            foreach (object i in source)
            {
                acumulador += i;
            }
            return acumulador;
        }

        public static int CustomToString(this byte[] source)
        {
            if (source.Length == 2)
            {
                return BitConverter.ToInt16(source, 0);
            }
            return BitConverter.ToInt32(source, 0);
        }


    }
}
