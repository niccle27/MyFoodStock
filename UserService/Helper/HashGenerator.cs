using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace UserService.Helper
{
    public class HashGenerator
    {
        #region public method
        /// <summary>
        /// hash a word into a SHA512
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public string GenerateSHA512Hash(String word)
        {
            Byte[] output;
            using (SHA512 hashingUnit = new SHA512Managed())
            {
                output = hashingUnit.ComputeHash(Encoding.UTF8.GetBytes(word));
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < output.Length; i++)
            {
                sb.Append(output[i].ToString("X2"));
            }
            return sb.ToString();
        }
        /// <summary>
        /// return a token in MD5
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string GenerateTokenHash(string username)
        {
            Random rand = new Random();
            return GenerateMD5Hash(username + rand.Next(99999).ToString());
        }

        #endregion

        #region private region
        /// <summary>
        /// generate a token in md5
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private string GenerateMD5Hash(string word)
        {
            Byte[] output;
            using (MD5 hashingUnit = MD5.Create())
            {
                output = hashingUnit.ComputeHash(Encoding.UTF8.GetBytes(word));
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < output.Length; i++)
            {
                sb.Append(output[i].ToString("X2"));
            }
            return sb.ToString();
        }
        

        #endregion

    }
}