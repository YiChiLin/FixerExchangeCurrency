using FixerExchangeCurrency.Models;
using FixerExchangeCurrency.Services;
using Newtonsoft.Json;
using System;

namespace FixerExchangeCurrency
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var service = new ExchangeRateService();
            //service.DownloadExchangeRateDataAsync().GetAwaiter().GetResult();

            try
            {
                var result = service.GetExchangeRateAsync(Currency.EUR, new DateRange { StartTime = new DateTime(2018, 7, 11) }).GetAwaiter().GetResult();
                Console.WriteLine(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }
    }
}