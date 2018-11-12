using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.AuthenticationServiceReference;
using Client.Helper;
using Client.View;
using UserService.Modele;

namespace Client.ViewModel
{
    public class ConnectionWindowViewModel:ViewModelBase
    {

        #region Private Fields

        UserServiceClient userServiceClient = new UserServiceClient();

        private string _login;
        private string _password;
        private bool _connectionFailed = false;

        #endregion

        #region Properties
        public bool ConnectionFailed
        {
            get => _connectionFailed;
            set
            {
                _connectionFailed = value;
                OnPropertyChanged(nameof(ConnectionFailed));
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

        #endregion

        #region RelayCommands

        private RelayCommand _connectCommand;
        private RelayCommand _resetCommand;
        private RelayCommand _openRegisterWindow;

        public RelayCommand ConnectionCommand
        {
            get
            {
                return _connectCommand
                       ?? (_connectCommand = new RelayCommand(
                           (o) =>
                           {
                               string token = userServiceClient.Connect(Login,Password);
                               if (token != null)
                               {
                                   ConnectionFailed = false;
                                   User user = new User()
                                   {
                                       Login = Login,
                                       Password = Password,
                                       Token = token
                                   };
                                   new MainWindow(user).Show();
                                   this.CloseWindow();
                               }
                               else
                               {
                                   ConnectionFailed = true;
                               }
                           },
                           (o) => { return !(String.IsNullOrWhiteSpace(Login) || String.IsNullOrWhiteSpace(Password)); }));
            }
        }
        public RelayCommand ResetCommand
        {
            get
            {
                return _resetCommand
                       ?? (_resetCommand = new RelayCommand(
                           (o) =>
                           {
                               Login = string.Empty;
                               Password = string.Empty;
                               ConnectionFailed = false;
                           },
                           (o) => true));
            }
        }
        public RelayCommand OpenRegisterWindowCommand
        {
            get
            {
                return _openRegisterWindow
                       ?? (_openRegisterWindow = new RelayCommand(
                           (o) =>
                           {
                               var win = new RegisterWindow(new RegisterWindowViewModel()
                               {
                                   UserServiceClient = userServiceClient
                               });
                               win.Show();
                               CloseWindow();
                           },
                           (o) => true));
            }
        }

        #endregion
    }
}
