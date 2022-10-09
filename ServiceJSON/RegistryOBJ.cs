using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceJSON
{
    public  class RegistryOBJ
    {
        public string name { get; set; }
        public string description { get; set; }
        public string APIendpoint { get; set; }
        public int numberofOperands { get; set; }
        public string operandType { get; set; }

        public string status { get; set; }

        public string reason { get; set; }



    }
}
