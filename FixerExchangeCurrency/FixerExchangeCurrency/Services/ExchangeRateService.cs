using FixerExchangeCurrency.Models;
using FixerExchangeCurrency.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixerExchangeCurrency.Services
{
    public interface IExchangeRateService
    {
        Task DownloadExchangeRateDataAsync();

        Task<List<CurrencyExchangeRate>> GetExchangeRateAsync(Currency targetExchange, DateRange queryTime);
    }

    public class ExchangeRateService : IExchangeRateService
    {
        private ICurrencyExchangeApiService<CurrencyExchageRateApiResponse> _CurrencyExchangeApi = new FixerApiService();

        private ICurrencyExchangeRateRepository _CurrencyExchangeRateRepository = new CurrencyExchangeRateRepository();

        public async Task DownloadExchangeRateDataAsync()
        {
            var euroRateResult = await _CurrencyExchangeApi.GetAsync(Currency.EUR.ToString());
            await InsertDataToDb(euroRateResult);

            var allCurrencies = euroRateResult.Rates.Select(x => x.Key).Take(25).ToList();
            await CreateDummyExchagneRateData(new DateRange { StartTime = new DateTime(2018, 1, 1), EndTime = DateTime.Now.AddDays(-1) }, allCurrencies);
        }

        public async Task<List<CurrencyExchangeRate>> GetExchangeRateAsync(Currency targetExchange, DateRange queryTime)
        {
            if (queryTime.StartTime > queryTime.EndTime)
            {
                queryTime.EndTime = queryTime.StartTime.AddDays(1);
            }
            return await _CurrencyExchangeRateRepository.GetExchangeRateAsync(targetExchange, queryTime);
        }

        private static Dictionary<string, decimal> GenerateDummyRates(List<string> allCurrencies)
        {
            var random = new Random();
            var dummyRates = new Dictionary<string, decimal>();
            foreach (var currency in allCurrencies)
            {
                var value = random.Next(141421, 314160) / 100000m;
                dummyRates.Add(currency, value);
            }
            return dummyRates;
        }

        private async Task CreateDummyExchagneRateData(DateRange dateRange, List<string> allCurrencies)
        {
            var dummyData = new List<CurrencyExchageRateApiResponse>();
            while (dateRange.StartTime < dateRange.EndTime)
            {
                Console.WriteLine(dateRange.StartTime);

                var newEurData = new CurrencyExchageRateApiResponse
                {
                    BaseCurrency = Currency.EUR.ToString(),
                    Rates = GenerateDummyRates(allCurrencies),
                    CreateTime = dateRange.StartTime
                };

                var newUsdData = new CurrencyExchageRateApiResponse
                {
                    BaseCurrency = Currency.USD.ToString(),
                    Rates = GenerateDummyRates(allCurrencies),
                    CreateTime = dateRange.StartTime
                };
                await InsertDataToDb(newEurData);
                await InsertDataToDb(newUsdData);

                dateRange.StartTime = dateRange.StartTime.AddDays(1);
            }
        }

        private async Task InsertDataToDb(CurrencyExchageRateApiResponse source)
        {
            foreach (var item in source.Rates)
            {
                var newData = new CurrencyExchangeRate
                {
                    SourceCurrency = item.Key,
                    TargetCurrency = source.BaseCurrency,
                    ExchangeRate = item.Value,
                    Date = source.CreateTime
                };
                await _CurrencyExchangeRateRepository.InsertDataAsync(newData);
            }
        }
    }
}