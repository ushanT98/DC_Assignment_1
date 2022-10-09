using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Tutorial1Server
{

    [ServiceContract]
    public  interface DataServerInterface
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        int Login(string userName,string Password);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void Register(string regUsername, string regPassword);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool  Validate(int token);


    }




}
