using Newtonsoft.Json;
using RestSharp;
using ServiceJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial1Server;

namespace ServicePublisherConsole
{
    public class Program
    {

        public static DataServerInterface foob;
        static void Main(string[] args)
        {


            int choiceNum = -1;
            ServiceValidator vaidator = new ServiceValidator();

            foob = vaidator.SetDataServerInstance();







            while (choiceNum != 0)
            {
                bool opfetch = true;



                Console.WriteLine("Enter The Number for the Service You Require\n 1)Register\n 2)Login \n 3)Publish Service \n 4) Unpublish Service ");
                string choice = Console.ReadLine();

                choiceNum = Convert.ToInt32(choice);



                if (opfetch == true)
                {




                    if (choiceNum == 1)
                    {

                        Console.WriteLine("Enter username:");
                        string userName = Console.ReadLine();


                        Console.WriteLine("Enter password:");
                        string password = Console.ReadLine();


                        foob.Register(userName, password);
                        opfetch = true;
                    }
                    if (choiceNum == 2)
                    {
                        Console.WriteLine("Enter username:");
                        string userName = Console.ReadLine();


                        Console.WriteLine("Enter password:");
                        string password = Console.ReadLine();


                        foob.Login(userName, password);
                        opfetch = true;
                    }
                    if (choiceNum == 3)
                    {
                        RegistryOBJ registryOBJ = new RegistryOBJ();
                        RegistryOBJ responseOBJ = new RegistryOBJ();

                        Console.WriteLine("Enter Authentication Token :");
                        string SauthToken = Console.ReadLine();
                        int authToken = Convert.ToInt32(SauthToken);

                        if (foob.Validate(authToken))
                        {
                            Console.WriteLine("Authentication Successfull ");
                        }
                        else
                        {
                            Console.WriteLine("Authentication Failed Please Enter  Valid Token");
                        }


                        Console.WriteLine("Enter name:");
                        string name = Console.ReadLine();

                        Console.WriteLine("Enter description:");
                        string description = Console.ReadLine();

                        Console.WriteLine("Enter APIendpoint:");
                        string APIendpoint = Console.ReadLine();

                        Console.WriteLine("Enter numberofOperand:");
                        string SnumberofOperands = Console.ReadLine();
                        int numberofOperand = Convert.ToInt32(SnumberofOperands);


                        Console.WriteLine("Enter operandType:");
                        string operandType = Console.ReadLine();


                        registryOBJ.name = name;
                        registryOBJ.description = description;
                        registryOBJ.APIendpoint = APIendpoint;
                        registryOBJ.numberofOperands = numberofOperand;
                        registryOBJ.operandType = operandType;

                        string jsonObj = JsonConvert.SerializeObject(registryOBJ);



                        RestClient restClient;
                        RestRequest restRequest;
                        RestResponse restResponse;

                        restClient = new RestClient("http://localhost:62154/");
                        restRequest = new RestRequest("api/Registry/register/{authToken}", Method.Post);
                        restRequest.AddUrlSegment("authToken", authToken);

                        restRequest.AddJsonBody(jsonObj);
                        restResponse = restClient.Execute(restRequest);
                        responseOBJ = JsonConvert.DeserializeObject<RegistryOBJ>(restResponse.Content);

                        Console.WriteLine(responseOBJ);
                        opfetch = true;


                    }
                    if (choiceNum == 4)
                    {
                        RegistryOBJ registryOBJ = new RegistryOBJ();
                        RegistryOBJ responseOBJ = new RegistryOBJ();
                        RestClient restClient;
                        RestRequest restRequest;
                        RestResponse restResponse;



                        Console.WriteLine("Enter Authentication Token :");
                        string SauthToken = Console.ReadLine();
                        int authToken = Convert.ToInt32(SauthToken);


                        restClient = new RestClient("http://localhost:62154/");
                        restRequest = new RestRequest("api/Registry/deleteService/{authToken}", Method.Post);
                        restRequest.AddUrlSegment("authToken", authToken);



                        if (foob.Validate(authToken))
                        {
                            Console.WriteLine("Authentication Successfull ");
                        }
                        else
                        {
                            Console.WriteLine("Authentication Failed Please Enter  Valid Token");
                        }
                        restRequest.AddQueryParameter("authToken", authToken);



                        Console.WriteLine("Enter APIendpoint Of The Service To Be Removed:");
                        string APIendpoint = Console.ReadLine();



                        registryOBJ.APIendpoint = APIendpoint;

                        string jsonObj = JsonConvert.SerializeObject(registryOBJ);







                        restRequest.AddJsonBody(jsonObj);
                        restResponse = restClient.Execute(restRequest);
                        responseOBJ = JsonConvert.DeserializeObject<RegistryOBJ>(restResponse.Content);

                        Console.WriteLine(responseOBJ.name);
                        opfetch = true;
                    }

                }
            }







            Console.ReadLine();





        }




    }
}