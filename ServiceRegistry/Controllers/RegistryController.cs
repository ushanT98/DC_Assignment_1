using Newtonsoft.Json;
using ServiceJSON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tutorial1Server;

namespace SimpleWebAPI.Controllers
{
    [RoutePrefix("api/Registry")]
    public class RegistryController : ApiController
    {


        public DataServerInterface foob;
        public ServiceValidator validator;


        public List<RegistryOBJ> registerList = new List<RegistryOBJ>();
        [Route("search/{authToken}/{searchText}")]
        [Route("add")]
        [HttpGet]
        public RegistryOBJ Search(int authToken,string searchText)
        {

            validator = new ServiceValidator();

            if (validator.Validate(authToken))
            {
                using (StreamReader r = new StreamReader(@"C:\Users\User\Desktop\Registry.txt"))
                {
                    string json = r.ReadToEnd();
                    List<RegistryOBJ> items = JsonConvert.DeserializeObject<List<RegistryOBJ>>(json);

                   



                    foreach (RegistryOBJ item in items)
                    {
                        if (item.name.ToLower().Contains(searchText.ToLower()))
                        {
                            return item;
                        }
                    }



                    return null;

                }
            }
            else
            {
                RegistryOBJ error = new RegistryOBJ();
                error.status = "Denied";
                error.reason = "Authenticaton Failed";

               

                

                return error;
                
            }

            

  
        }

        [Route("getall/{authToken}")]
        [Route("GetAllServices")]
        [HttpGet]
        public  List<RegistryOBJ> GetAllServices(int authToken)
        {

            validator = new ServiceValidator();

            if (validator.Validate(authToken))
            {
                using (StreamReader r = new StreamReader(@"C:\Users\User\Desktop\Registry.txt"))
                {
                    string json = r.ReadToEnd();
                    List<RegistryOBJ> items = JsonConvert.DeserializeObject<List<RegistryOBJ>>(json);
                    return items;

                }
            }
            else
            {
                List<RegistryOBJ> error = new List<RegistryOBJ>();
                RegistryOBJ rerror = new RegistryOBJ();
                rerror.status = "Denied";
                rerror.reason = "Authenticaton Failed";
                error.Add(rerror);

                return error;

            }


        }








        [HttpPost]
        [Route("register/{authToken}")]
        public RegistryOBJ Post([FromUri()] int authToken,[FromBody] RegistryOBJ obj)
        {

            validator = new ServiceValidator();

            if (validator.Validate(authToken))
            {
                using (StreamReader r = new StreamReader(@"C:\Users\User\Desktop\Registry.txt"))
                {
                    string rjson = r.ReadToEnd();

                    if (rjson.Length == 0)
                    {
                        Console.WriteLine("empty file");
                    }
                    else
                    {
                        registerList = JsonConvert.DeserializeObject<List<RegistryOBJ>>(rjson);
                    }


                }

                if (registerList.Count != 0)
                {

                }


                RegistryOBJ registryOBJ = new RegistryOBJ();

                registryOBJ.name = obj.name;
                registryOBJ.description = obj.description;
                registryOBJ.APIendpoint = obj.APIendpoint;
                registryOBJ.numberofOperands = obj.numberofOperands;
                registryOBJ.operandType = obj.operandType;
                registryOBJ.status = "Accepted";




                registerList.Add(registryOBJ);

                string json = JsonConvert.SerializeObject(registryOBJ, Formatting.Indented);




              



                using (StreamWriter file = File.CreateText(@"C:\Users\User\Desktop\Registry.txt"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, registerList);
                }

                return registryOBJ;
            }
            else
            {
                RegistryOBJ error = new RegistryOBJ();
                error.status = "Denied";
                error.reason = "Authenticaton Failed";





                return error;

            }


        }



        [HttpPost]
        [Route("deleteservice/{authToken}")]
        public RegistryOBJ DeleteService([FromUri()] int authToken, [FromBody] RegistryOBJ obj)
        {
            validator = new ServiceValidator();

            List<RegistryOBJ> items = new List<RegistryOBJ>();
            List<RegistryOBJ> returnList = new List<RegistryOBJ>(); 
            RegistryOBJ deletedItem = new RegistryOBJ();    

            if (validator.Validate(authToken))
            {
                using (StreamReader r = new StreamReader(@"C:\Users\User\Desktop\Registry.txt"))
                {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<RegistryOBJ>>(json);

                     deletedItem = new RegistryOBJ();

                  

                    returnList = new List<RegistryOBJ>();

                    foreach (RegistryOBJ item in items)
                    {
                        if (item.APIendpoint.ToLower() != obj.APIendpoint.ToLower())
                        {
                            returnList.Add(item);
                        }
                        else
                        {
                            deletedItem = item;
                        }

                    }


 

                }

                using (StreamWriter file = File.CreateText(@"C:\Users\User\Desktop\Registry.txt"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, returnList);
                }


                if (deletedItem != null)
                {
                    return deletedItem;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                RegistryOBJ error = new RegistryOBJ();
                error.status = "Denied";
                error.reason = "Authenticaton Failed";
                return error;

            }
        }



        [HttpPost]
        [Route("deleteservice")]
        public RegistryOBJ PreAuthDeleteService( [FromBody] RegistryOBJ obj)
        {
           

            List<RegistryOBJ> items = new List<RegistryOBJ>();
            List<RegistryOBJ> returnList = new List<RegistryOBJ>();
            RegistryOBJ deletedItem = new RegistryOBJ();

     
                using (StreamReader r = new StreamReader(@"C:\Users\User\Desktop\Registry.txt"))
                {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<RegistryOBJ>>(json);

                    deletedItem = new RegistryOBJ();

             

                    returnList = new List<RegistryOBJ>();

                    foreach (RegistryOBJ item in items)
                    {
                        if (item.APIendpoint.ToLower() != obj.APIendpoint.ToLower())
                        {
                            returnList.Add(item);
                        }
                        else
                        {
                            deletedItem = item;
                        }

                    }




                }

                using (StreamWriter file = File.CreateText(@"C:\Users\User\Desktop\Registry.txt"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, returnList);
                }


                if (deletedItem != null)
                {
                    return deletedItem;
                }
                else
                {
                    return null;
                }

       

        }
















        [Route("subtract/{firstNumber}/{secondNumber}")]
        [Route("subtract")]
        [HttpGet]

        public int Subtract(int firstNumber, int secondNumber)
        {
            return firstNumber - secondNumber;
        }

    }
}
