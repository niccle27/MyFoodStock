using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace UserService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {
        #region serviceFunctions
        [OperationContract]
        string Connect(string userLogin, string userPassword);

        [OperationContract]
        string Register(string userLogin, string userPassword);

        [OperationContract]
        bool IsTokenStillValid(string userLogin, string userToken);
        #endregion

    }
}
