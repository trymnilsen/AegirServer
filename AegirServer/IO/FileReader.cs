using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.IO
{
    public class FileReader
    {
        public static string ReadAllWithoutControlCharacters(string path)
        {
            return RemoveControlCharacters(File.ReadAllText(path));
        }
        public static string RemoveControlCharacters(string inString)
        {
            if (inString == null) return null;
            StringBuilder newString = new StringBuilder();
            char ch;
            for (int i = 0; i < inString.Length; i++)
            {
                ch = inString[i];
                if (!char.IsControl(ch))
                {
                    newString.Append(ch);
                }
            }
            return newString.ToString();
        }
    }
}
