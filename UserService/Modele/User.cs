using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserService.Modele
{
    public class User
    {
        public User()
        {
            
        }

        public User(string login, string password)
        {
            _login = login;
            _password = password;
        }
        private int _id;
        private string _login;
        private string _password;
        private string _token;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Login
        {
            get => _login;
            set => _login = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public string Token
        {
            get => _token;
            set => _token = value;
        }
    }
}