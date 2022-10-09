using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Tutorial1Server;

namespace ServiceJSON
{
    public  class ServiceValidator
    {



        public DataServerInterface foob;
        public bool isDataServerInstanciated = false;

        public bool Validate(int token)
        {
            string Stoken = token.ToString();
            string[] content = System.IO.File.ReadAllLines(@"C:\Users\User\Desktop\Token.txt");


            for (int i = 1; i < content.Length; i++)
            {
                if (content[i] == Stoken)
                {
                    Console.WriteLine("Token Validated");
                    return true;
                }

            }
            Console.WriteLine("Validation Failed");
            return false;


        }


        public DataServerInterface SetDataServerInstance()
        {

            if (!isDataServerInstanciated)
            {
                ChannelFactory<DataServerInterface> foobFactory;



                NetTcpBinding tcp = new NetTcpBinding();

                string URL = "net.tcp://localhost:8100/DataService";




                //Establishing connection and getting the client count

                foobFactory = new ChannelFactory<DataServerInterface>(tcp, URL);

                foob = foobFactory.CreateChannel();
                return foob;
            }
            else
            {
                return null;
            }

        }




    }
}
