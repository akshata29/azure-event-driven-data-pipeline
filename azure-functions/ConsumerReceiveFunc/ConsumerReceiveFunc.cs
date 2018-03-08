using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace ConsumerReceiveFunc
{
    public static class ConsumerReceiveFunc
    {
        [FunctionName(nameof(ConsumerReceiveFunc))]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post")]HttpRequestMessage req, 
            TraceWriter log)
        {
            var product = await req.Content.ReadAsStringAsync();
            bool isOk = new Random().Next(0, 10) < 6;

            return isOk
                ? req.CreateResponse(HttpStatusCode.OK, "Received Product: " + product)
                : req.CreateResponse(HttpStatusCode.InternalServerError, "Random Error: " + product);
        }
    }
}
