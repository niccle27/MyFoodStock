﻿using System;
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

        public string GenerateTokenHash(string username)
        {
            Random rand = new Random();
            return GenerateMD5Hash(username + rand.Next(99999).ToString());
        }

        #endregion

        #region private region

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