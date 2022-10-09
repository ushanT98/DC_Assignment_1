using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServiceJSON;

namespace ServiceProvider.Controllers
{
    [RoutePrefix("api/calculator")]
    public class CalculatorController : ApiController
    {
        [Route("add/{firstNumber}/{secondNumber}")]
        [Route("add")]
        [HttpGet]
        public ServiceProviderOBJ Add(int firstNumber, int secondNumber)
        {

            ServiceProviderOBJ providerJson = new ServiceProviderOBJ();

            providerJson.answer = firstNumber+ secondNumber;
            return providerJson;
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
