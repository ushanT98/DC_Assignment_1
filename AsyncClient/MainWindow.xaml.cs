using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ServiceModel;
using Newtonsoft;



using System.Threading;
using Tutorial1Server;
using RestSharp;
using ServiceJSON;
using Newtonsoft.Json;

namespace AsyncClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate string  Search(string value);
        public DataServerInterface foob;
       // private string searchInput;
        private string username, password;
        private string rusername, rpassword;

        int userAuthToken;

        int input1, input2, input3;
        dynamic row;




        public MainWindow()
        {
            InitializeComponent();

       
            //Abstraction of RPC Implementation 

            ChannelFactory<DataServerInterface> foobFactory;



            NetTcpBinding tcp = new NetTcpBinding();

            string URL = "net.tcp://localhost:8100/DataService";



            try
            {
                //Establishing connection and getting the client count

                foobFactory = new ChannelFactory<DataServerInterface>(tcp, URL);

                foob = foobFactory.CreateChannel();

               
            }
            catch (InvalidOperationException e)
            {

                Console.WriteLine($"The Connectiion Failed: '{e}'");
            }

      



            Input1Box.Visibility = Visibility.Hidden;
            Input2Box.Visibility = Visibility.Hidden;
            Input3Box.Visibility = Visibility.Hidden;



        }

   

        private async void Go_Click(object sender, RoutedEventArgs e)
        {


            username = UsernameBox.Text;
            password = PasswordBox.Text;
                
        
            Task<string> task = new Task<string>(Login);

           
            task.Start();
  

            var result = await task;




        }



        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

            rusername = UsernameRegBox.Text;
            rpassword = PasswordRegBox.Text;

            Task  task = new Task(Register);

            task.Start();

            await task;

        }




        private void Register()
        { 
            foob.Register(rusername,rpassword);
        }





    

        private string Login()
        {

          
            userAuthToken =  foob.Login(username,password);

            List<RegistryOBJ> registryOBJs = new List<RegistryOBJ>();
            UpdateProgressBar(30);

            RestClient restClient = new RestClient("http://localhost:62154/");
            RestRequest restRequest = new RestRequest("api/registry/getall/{authToken}", Method.Get);
            restRequest.AddUrlSegment("authToken", userAuthToken);

            RestResponse restResponse = restClient.Execute(restRequest);

            registryOBJs = JsonConvert.DeserializeObject<List<RegistryOBJ>>(restResponse.Content);
            UpdateProgressBar(80);
            if (registryOBJs != null)
            {
                foreach (var item in registryOBJs)
                {
                    DataGridXAML.Dispatcher.Invoke(new Action(() => DataGridXAML.Items.Add(item)));

                    //DataGridXAML.Items.Add(item);
                }

            }
            else {
                Console.WriteLine("Registry object not initialized, pleasew enter services before loging in");
            }




            UpdateProgressBar(100);
            return "Login Check Compleated";

        }



        private void ValidateTokenButton_Click(object sender, RoutedEventArgs e)
        {
            foob.Validate(Int32.Parse(TokenInputBox.Text));
        }




        private void SelectedItemBox_Click(object sender, RoutedEventArgs e)
        {
            row = DataGridXAML.SelectedItem;
            string name;
            name = row.name;
            Console.WriteLine(row.name);

            if(row.numberofOperands == 2)
            {
                Input1Box.Visibility = Visibility.Visible;
                Input2Box.Visibility = Visibility.Visible;
                Input3Box.Visibility = Visibility.Hidden;
            }
            if(row.numberofOperands == 3)
            {
                Input1Box.Visibility = Visibility.Visible;
                Input2Box.Visibility = Visibility.Visible;
                Input3Box.Visibility = Visibility.Visible;
            }
           

        }


        private async void TestAPIButton_Click(object sender, RoutedEventArgs e)
        {


            row = DataGridXAML.SelectedItem;

            input1 = Int32.Parse(Input1Box.Text);
            input2 = Int32.Parse(Input2Box.Text);
            input3 = 0;

            if (row.numberofOperands != 2)
            {
                input3 = Int32.Parse(Input3Box.Text);
            }

            Task task = new Task(TestAPI);

            task.Start();

            await task;




        }



        private void TestAPI()
        {
            RestRequest restRequest;

            











            string hostNum = row.APIendpoint;
            string host = hostNum.Substring(0, 23);
            Console.WriteLine(host);



            string action = hostNum.Substring(23, hostNum.Length - 23);




            ServiceProviderOBJ responseOBJ = new ServiceProviderOBJ();
            RestClient restClient = new RestClient(host);

            if (row.numberofOperands == 2)
            {
                restRequest = new RestRequest("api/services/{action}/{authToken}/{input1}/{input2}", Method.Get);
                restRequest.AddUrlSegment("authToken", userAuthToken);
                restRequest.AddUrlSegment("action", action);
                restRequest.AddUrlSegment("input1", input1);
                restRequest.AddUrlSegment("input2", input2);
            }
            else
            {
                restRequest = new RestRequest("api/services/{action}/{authToken}/{input1}/{input2}/{input3}", Method.Get);
                restRequest.AddUrlSegment("authToken", userAuthToken);
                restRequest.AddUrlSegment("action", action);
                restRequest.AddUrlSegment("input1", input1);
                restRequest.AddUrlSegment("input2", input2);
                restRequest.AddUrlSegment("input3", input3);
            }


            RestResponse restResponse = restClient.Execute(restRequest);


            responseOBJ = JsonConvert.DeserializeObject<ServiceProviderOBJ>(restResponse.Content);

          
            
            AnswerBlock.Dispatcher.Invoke(new Action(() => AnswerBlock.Text = responseOBJ.answer.ToString()));

        }










        private bool   HasSpecialChars(string yourString)
        {
            return yourString.Any(ch => !Char.IsLetterOrDigit(ch));
        }

        private void UpdateProgressBar(int value)
        {
            ProgressBar.Dispatcher.Invoke(new Action(() => ProgressBar.Value = value));
        }

        private void DisableGUI()
        {
          
        }

        private void EnableGUI()
        {
         
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

  

        public static string Test(int compare)
        {
            if (compare == 0)
                return "equal to";
            else if (compare < 0)
                return "less than";
            else
                return "greater than";
        }






    }
}
