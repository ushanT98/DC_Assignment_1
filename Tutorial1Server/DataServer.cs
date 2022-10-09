using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.IO;

namespace Tutorial1Server
{

    [ServiceBehavior (ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext= false)]

    internal class DataServer: DataServerInterface
    {


        public DataServer()
        {

        }
        public int GetNumEntries()
        {
            return 1;
        }


        public int Login(string userName, string password) {

            string username = userName;
            string pass = password;
            bool AuthStatus = false;
         

            string[] content = System.IO.File.ReadAllLines(@"C:\Users\User\Desktop\Userlog.txt");
            Console.WriteLine(content.Length);

   

            for (int i = 1; i < content.Length -1 ; i++)
            {
                if (content[i] == username && content[i+1] == password)
                {
                    Console.WriteLine("User Authenticated");
                    AuthStatus = true;

                    Random rnd = new Random();
                    int num = rnd.Next();

                    StreamWriter tokenwriter = new StreamWriter(@"C:\Users\User\Desktop\Token.txt", true);
                    tokenwriter.WriteLine(num);
                    tokenwriter.Close(); 
                    return num;


                }

            }

            if(AuthStatus == false)
            {
                Console.WriteLine("User Not found:Bad Auth ");
                
            }
            return 0;


        }



        public void Register(string regUsername, string regPassword)
        {
        
            StreamWriter writer = new StreamWriter(@"C:\Users\User\Desktop\Userlog.txt", true);
            writer.WriteLine(regUsername);
            writer.WriteLine(regPassword);

            writer.Close();

            Console.WriteLine("User Successfully Registered");

        }




        public bool Validate(int token)
        {
            string Stoken = token.ToString();    
            string[] content = System.IO.File.ReadAllLines(@"C:\Users\User\Desktop\Token.txt");


            for (int i = 1; i < content.Length; i++)
            {
              if(content[i] == Stoken)
                {
                    Console.WriteLine("Token Validated");
                    return true;
                }

            }
            Console.WriteLine("Validation Failed");
            return false;


        }








    }
}
