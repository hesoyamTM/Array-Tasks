using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Array_Tasks.Utilities
{
    static public class LetterUtility
    {
        public static string GetRandomLetter(Random rand)
        {
            //Случайный символ в байтах и кодируем его в строку
            byte[] symbol = { (byte)rand.Next(65, 91) };
            string result = Encoding.UTF8.GetString(symbol).ToLower();

            //возвращаем
            return result;
        }
    }
}
