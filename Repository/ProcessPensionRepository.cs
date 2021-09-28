using Newtonsoft.Json;

using ProcessPensionApi.Model;
using ProcessPensionApi.Repository.IRepository;
using ProcessPensionApi.StaticDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPensionApi.Repository
{
    public class ProcessPensionRepository : IProcessPensionRepository
    {
        private readonly IHttpClientFactory clientFactory; // This IHttpClientFactory we can use due to the services.AddHttpClient()

        public ProcessPensionRepository(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        public async Task<PensionDetail> PensionDetail(PensionerInput pensionerInput, string token="")
        {
            var url = SD.PensionerDetailBaseAPIPath + "api/PensionerDetail/" + pensionerInput.AadhaarNumber;
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var client = clientFactory.CreateClient();

            if (token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            HttpResponseMessage response = await client.SendAsync(request); // Now this cient will pass the request with the content in the particular url mentioned in the request
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync(); // At first the response from the status code in to JSON String
                return JsonConvert.DeserializeObject<PensionDetail>(jsonString); // The Json String is converted or Deserailizes into PensionerDetail
            }

            else
                return null;
        }

        public async Task<ProcessPensionResponse> ProcessedCode(ProcessPensionInput processPensionInput,string token="")
        {
            var url = SD.PensionDisbursementBaseAPIPath + "api/PensionDisbursement/getresponse/"+token;
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(JsonConvert.SerializeObject(processPensionInput), Encoding.UTF8, "application/json");

            var client = clientFactory.CreateClient();

            if (token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync(); // At first the response from the status code in to JSON String
                return JsonConvert.DeserializeObject<ProcessPensionResponse>(jsonString); // The Json String is converted or Deserailizes into PensionerDetail
            }

            else
            {
                var obj = new ProcessPensionResponse()
                {
                    ProcessPensionStatusCode = String.Empty
                };
                return obj;
            }
        }
    }
}
            




    

