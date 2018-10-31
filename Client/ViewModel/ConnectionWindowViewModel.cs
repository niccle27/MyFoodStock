using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.AuthenticationServiceReference;
using Client.Helper;

namespace Client.ViewModel
{
    public class ConnectionWindowViewModel:ViewModelBase
    {

        UserServiceClient userServiceClient = new UserServiceClient();
        private string login;
        private string password;
        private bool connectionFailed = false;

        public bool ConnectionFailed
        {
            get => connectionFailed;
            set
            {
                connectionFailed = value;
                OnPropertyChanged(nameof(ConnectionFailed));
            }
        }

        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private RelayCommand connectionCommand;
        /// <summary>
        /// try to connect using the UserService
        /// </summary>
        public RelayCommand ConnectionCommand
        {
            get
            {
                return connectionCommand
                    ?? (connectionCommand = new RelayCommand(
                    (o) =>
                    {
                        string result = userServiceClient.Connect(Login,Password);
                        if (result != null)
                        {
                            ConnectionFailed = false;
                            new MainWindow().Show();
                        }
                        else
                        {
                            ConnectionFailed = true;
                        }
                    },
                    (o) => { return !(String.IsNullOrWhiteSpace(Login) || String.IsNullOrWhiteSpace(Password)); }));
            }
        }
        private RelayCommand resetCommand;

        /// <summary>
        /// Gets the ResetCommand.
        /// </summary>
        public RelayCommand ResetCommand
        {
            get
            {
                return resetCommand
                    ?? (resetCommand = new RelayCommand(
                           (o) =>
                           {
                               Login = string.Empty;
                               Password = string.Empty;
                               ConnectionFailed = false;
                           },
                    (o) => true));
            }
        }
    }
}
