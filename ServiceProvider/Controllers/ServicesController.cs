using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;
using ServiceJSON;
using Tutorial1Server;

namespace ServiceProvider.Controllers
{
    [RoutePrefix("api/services")]
    public class ServicesController : ApiController
    {

        public DataServerInterface foob;
        public bool isDataServerInstanciated = false;


        [Route("ADDTwoNumbers/{authToken}/{firstNumber}/{secondNumber}")]
        [HttpGet]
        public ServiceProviderOBJ ADDTwoNumbers(int authToken,int firstNumber, int secondNumber)
        {

            ServiceProviderOBJ providerJson = new ServiceProviderOBJ();

            SetDataServerInstance();


            if (foob.Validate(authToken))
            {
                providerJson.answer = firstNumber + secondNumber ;
                providerJson.Status = "Accepted";
                providerJson.Reason = "Authentication Successfull";
            }
            else
            {
                providerJson.Status = "Denied";
                providerJson.Reason = "Authentication Error";

            }



            return providerJson;
        }

        [Route("ADDThreeNumbers/{authToken}/{firstNumber}/{secondNumber}/{thirdNumber}")]      
        [HttpGet]
        public ServiceProviderOBJ ADDThreeNumbers(int authToken, int firstNumber,int secondNumber,int thirdNumber)
        {

            ServiceProviderOBJ providerJson = new ServiceProviderOBJ();

            SetDataServerInstance();


            if (foob.Validate(authToken))
            {
                providerJson.answer = firstNumber + secondNumber+ thirdNumber;
                providerJson.Status = "Accepted";
                providerJson.Reason = "Authentication Successfull";
            }
            else
            {
                providerJson.Status = "Denied";
                providerJson.Reason = "Authentication Error";

            }

            return providerJson;

        }


        [Route("MulTwoNumbers/{authToken}/{firstNumber}/{secondNumber}")]
        [HttpGet]
        public ServiceProviderOBJ MulTwoNumbers(int authToken,int firstNumber, int secondNumber)
        {

            ServiceProviderOBJ providerJson = new ServiceProviderOBJ();

     

            SetDataServerInstance();

            if (foob.Validate(authToken))
            {
                providerJson.answer = firstNumber * secondNumber;
                providerJson.Status = "Accepted";
                providerJson.Reason = "Authentication Successfull";
            }
            else
            {
                providerJson.Status = "Denied";
                providerJson.Reason = "Authentication Error";

            }

            return providerJson;
        }


        [Route("MulThreeNumbers/{authToken}/{firstNumber}/{secondNumber}/{thirdNumber}")]
        [HttpGet]
        public ServiceProviderOBJ MulThreeNumbers( int authToken ,int firstNumber, int secondNumber,int thirdNumber)
        {

            ServiceProviderOBJ providerJson = new ServiceProviderOBJ();

            SetDataServerInstance();


            if (foob.Validate(authToken))
            {
                providerJson.answer = firstNumber * secondNumber * thirdNumber;
                providerJson.Status = "Accepted";
                providerJson.Reason = "Authentication Successfull";
            }
            else
            {
                providerJson.Status = "Denied";
                providerJson.Reason = "Authentication Error";

            }

            return providerJson;
        }



 



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


        private void SetDataServerInstance()
        {

            if (!isDataServerInstanciated)
            {
                ChannelFactory<DataServerInterface> foobFactory;



                NetTcpBinding tcp = new NetTcpBinding();

                string URL = "net.tcp://localhost:8100/DataService";




                //Establishing connection and getting the client count

                foobFactory = new ChannelFactory<DataServerInterface>(tcp, URL);

                foob = foobFactory.CreateChannel();
            }
            else
            {
                return;
            }
    
        }


    }
}
