using System.Threading.Tasks;

namespace FixerExchangeCurrency.Services
{
    public interface ICurrencyExchangeApiService<TOut>
    {
        Task<TOut> GetAsync(string baseCurrency);
    }
}