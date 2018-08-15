using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FixerExchangeCurrency.Services
{
    public class CurrencyExchageRateApiResponse
    {
        [JsonProperty("base")]
        public string BaseCurrency { get; set; }

        [JsonProperty("date")]
        public DateTime CreateTime { get; set; }

        [JsonProperty("rates")]
        public Dictionary<string, decimal> Rates { get; set; }
    }

    public class FixerApiService : IApiService<CurrencyExchageRateApiResponse>
    {
        public async Task<CurrencyExchageRateApiResponse> GetAsync()
        {
            using (var client = new WebClient())
            {
                var uri = new Uri("http://data.fixer.io/api/latest?access_key=7e36d952945656dca220f2d2210c9dc3");
                var response = await client.DownloadDataTaskAsync(uri);
                var result = Encoding.UTF8.GetString(response);
                return JsonConvert.DeserializeObject<CurrencyExchageRateApiResponse>(result);
            }
        }
    }
}