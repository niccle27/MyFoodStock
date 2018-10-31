using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.AuthenticationServiceReference;

namespace Client.ViewModel
{
    public class ConnectionWindowViewModel:ViewModelBase
    {
        UserServiceClient userServiceClient = new UserServiceClient();
    }
}
