using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.AuthenticationServiceReference;
using Client.Helper;

namespace Client.ViewModel
{
    public class RegisterWindowViewModel:ViewModelBase
    {
        private string _login;
        private string _password;
        private string _confirmedPassword;
        private bool _passwordsDontMatch = false;
        private bool _loginAlreadyTaken = false;

        
        private UserServiceClient userServiceClient;

        public UserServiceClient UserServiceClient
        {
            get => userServiceClient;
            set => userServiceClient = value;
        }


        public bool LoginAlreadyTaken
        {
            get => _loginAlreadyTaken;
            set
            {
                _loginAlreadyTaken = value;
                OnPropertyChanged(nameof(LoginAlreadyTaken));
            }
        }

        public bool PasswordsDontMatch
        {
            get => _passwordsDontMatch;
            set
            {
                _passwordsDontMatch = value;
                OnPropertyChanged(nameof(PasswordsDontMatch));
            }
        }

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ConfirmedPassword
        {
            get => _confirmedPassword;
            set
            {
                _confirmedPassword = value;
                OnPropertyChanged(nameof(ConfirmedPassword));
            }
        }

        private RelayCommand registerCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand RegisterCommand
        {
            get
            {
                return registerCommand
                    ?? (registerCommand = new RelayCommand(
                           (o) =>
                           {
                               PasswordsDontMatch = false;
                               LoginAlreadyTaken = false;
                               if (_password == _confirmedPassword)
                               {
                                   string token = userServiceClient.Register(_login, _password);
                                   if (token != null)
                                   {
                                       new MainWindow().Show();
                                       CloseWindow();
                                   }
                                   else
                                   {
                                       LoginAlreadyTaken = true;
                                   }

                               }
                               else
                               {
                                   PasswordsDontMatch = true;
                               }
                           },
                    (o) => !(string.IsNullOrWhiteSpace(Login) ||
                             string.IsNullOrWhiteSpace(Password) ||
                             string.IsNullOrWhiteSpace(ConfirmedPassword))));
            }
        }
    }
}
