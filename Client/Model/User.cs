using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    public class User
    {
        private int id;
        private string login;
        private string password;
        private string token;

        public int Id
        {
            get => id;
            set => id = value;
        }

        public string Login
        {
            get => login;
            set => login = value;
        }

        public string Password
        {
            get => password;
            set => password = value;
        }

        public string Token
        {
            get => token;
            set => token = value;
        }
    }
}
