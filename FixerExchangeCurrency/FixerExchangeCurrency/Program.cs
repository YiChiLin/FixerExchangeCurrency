using FixerExchangeCurrency.Models;
using FixerExchangeCurrency.Repositories;
using FixerExchangeCurrency.Services;
using Newtonsoft.Json;
using System;

namespace FixerExchangeCurrency
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var service = new ExchangeRateService(new FixerApiService(), new CurrencyExchangeRateRepository());
            service.DownloadExchangeRateDataAsync().GetAwaiter().GetResult();
            var result = service.GetExchangeRateAsync(Currency.EUR, new DateRange { StartTime = new DateTime(2018, 7, 11) }).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(result));
            Console.ReadLine();
        }
    }
}