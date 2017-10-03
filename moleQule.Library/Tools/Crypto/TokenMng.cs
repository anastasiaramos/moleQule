using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace moleQule.Library
{
    public class TokenMng
    {
        public static string GetSalt() { return System.Guid.NewGuid().ToString(); }

        public static string GetToken(string apiKey, string secretKey, string salt)
        {
            string token = ClassMD5.GetMd5Hash(apiKey + salt + secretKey);
            return System.Web.HttpUtility.UrlEncode(token);
        } 
    }
}



    



