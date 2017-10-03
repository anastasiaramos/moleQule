using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace moleQule.Library
{
    public class ClassMD5
    {
        // Toma una string y devuelve el hash en MD5
        // como una string hexadecimal de 32 caracteres.
		public static string GetMd5Hash(string input) { return getMd5Hash(input); }
		public static string getMd5Hash(string input)
        {
            // Crea una nueva instancia del objeto MD5CryptoServiceProvider.
            MD5 md5Hasher = MD5.Create();

            // Convierte la string de entrada a un array de byte y computa el hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
			
            // Crea una nueva Stringbuilder para recoger los bytes y crear una string
            StringBuilder sBuilder = new StringBuilder();

            // Recorre cada byte de los datos MD5 
            // y formatea cada uno como una string hexadecimal.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Devuleve la string hexadecimal.
            return sBuilder.ToString();
        }

        // Compara una string en MD5 con otra.
        public static bool verifyMd5Hash(string input, string hash)
        {
            // Toma la entrada.
            string hashOfInput = getMd5Hash(input);

            // Crea una StringComparer para comprara las strings.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}



    



