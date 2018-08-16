using FixerExchangeCurrency.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixerExchangeCurrency.Repositories
{
    public interface ICurrencyExchangeRateRepository
    {
        Task<List<CurrencyExchangeRate>> GetExchangeRateAsync(Currency targetCurrency, DateRange queryTime);

        Task InsertDataAsync(CurrencyExchangeRate data);
    }

    public class CurrencyExchangeRateRepository : BaseRepository, ICurrencyExchangeRateRepository
    {
        public async Task<List<CurrencyExchangeRate>> GetExchangeRateAsync(Currency targetCurrency, DateRange queryTime)
        {
            var param = new { targetCurrency = targetCurrency.ToString(), startTime = queryTime.StartTime, endTime = queryTime.EndTime };
            return (await GetListAsync<CurrencyExchangeRate>("WHERE TargetCurrency = @targetCurrency AND `Date` BETWEEN @startTime AND @endTime", param)).ToList();
        }

        public async Task InsertDataAsync(CurrencyExchangeRate data)
        {
            await InsertAsync(data);
        }
    }
}