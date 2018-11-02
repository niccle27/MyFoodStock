using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MySql.Data.MySqlClient;
using UserService.Database;
using UserService.Helper;
using UserService.Modele;

namespace UserService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        private MySqlConnection _connection = DbConnection.CreateConnection();
        UserDAO _userDao=new UserDAO(DbConnection.CreateConnection());
        static HashGenerator _hashGenerator = new HashGenerator();
        static Validator _validator = new Validator();

        #region ServiceMethod
        /// <summary>
        /// try to connect with a login and password. This verify whether they match
        /// or not and in case of match, it generates and return the token
        /// </summary>
        /// <param name="userLogin">user in database</param>
        /// <param name="userPassword">password that match the user</param>
        /// <returns>token in case of success, otherwise null</returns>
        public string Connect(string userLogin, string userPassword)
        {
            User user = new User(userLogin, userPassword);
            string generatedToken = null;
            
            if (_validator.Authenticate(_connection,user, _hashGenerator ))
            {
                generatedToken = _validator.ValidUniqueToken(_connection, user, _hashGenerator);
                _userDao.UpdateToken(user,generatedToken);
            }
            return generatedToken;
        }
        /// <summary>
        /// Create a user entry in the database. This function check whether the
        /// login is already taken or not. Return token in case of success
        /// </summary>
        /// <param name="userLogin">user in database</param>
        /// <param name="userPassword">password that match the user</param>
        /// <returns>token in case of success, otherwise null</returns>
        public string Register(string userLogin, string userPassword)
        {
            string generatedToken = null;
            User user = new User(userLogin,userPassword);
            if (!_validator.CheckIfUsernameIsAlreadyTaken(_connection,user))
            {
                generatedToken = _validator.ValidUniqueToken(_connection, user, _hashGenerator);
                _userDao.Create(user,_hashGenerator,generatedToken);
            }
            return generatedToken;
        }
        /// <summary>
        /// verify whether the token match the login and is still valid
        /// </summary>
        /// <param name="userLogin">login in database</param>
        /// <param name="userToken">token in database</param>
        /// <returns>true in case of success , false otherwise</returns>
        public bool IsTokenStillValid(string userLogin, string userToken)//todo complete token validation
        {
            User user = new User()
            {
                Login = userLogin,
                Token = userToken
            };
            return _validator.IsTokenStillValid(_connection, user);
        }

        #endregion

    }
}
