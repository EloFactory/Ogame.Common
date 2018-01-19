using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ogame.Common
{
    public static class StringUtils
    {
        public static string GetStringBetween(string Source, string Start, string End)
        {
            // You should check for errors in real-world code, omitted for brevity

            if (Source != string.Empty)
            {
                int startIndex = Source.IndexOf(Start) + Start.Length;
                int endIndex = Source.IndexOf(End, startIndex);
                if (startIndex < endIndex)
                    return Source.Substring(startIndex, endIndex - startIndex);
            }
            return string.Empty;
        }

        public static string GetStringBefore(string Source, string Origine, int CaractCount)
        {
            int startPos = Source.LastIndexOf(Origine) - CaractCount;
            if (startPos >= 0)
                return Source.Substring(startPos, CaractCount);
            return string.Empty;
        }

        public static string GetStringAfter(string Source, string Origine, int CaractCount)
        {
            int startPos = Source.LastIndexOf(Origine) + Origine.Length;
            if (startPos >= 0)
                return Source.Substring(startPos, CaractCount);
            return string.Empty;
        }

        public static string GetStringAfter(string Source, string Origine)
        {
            int startPos = Source.LastIndexOf(Origine) + Origine.Length;
            if (startPos >= 0)
                return Source.Substring(startPos, Source.Length - startPos);
            return string.Empty;
        }
    }
}
