using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace UserService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        #region ServiceMethod
        public string Connect(string userLogin, string userPassword)
        {
            if (GetHashPassword(userPassword) == GetHashPassword("coucou"))//TODO replace right part by database request
            {
                return GenerateToken(userLogin);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region privateMethod



        #endregion

        #region privateStaticFunction

        static String GetHashPassword(String password)
        {
            Byte[] output;
            using (SHA512 hashingUnit = new SHA512Managed())
            {
                output = hashingUnit.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < output.Length; i++)
            {
                sb.Append(output[i].ToString("X2"));
            }
            return sb.ToString();
        }
        static String GenerateToken(String username)
        {
            Byte[] output;
            using (MD5 hashingUnit = MD5.Create())
            {
                Random rand = new Random();
                output = hashingUnit.ComputeHash(Encoding.UTF8.GetBytes(username + rand.Next(99999).ToString()));
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
