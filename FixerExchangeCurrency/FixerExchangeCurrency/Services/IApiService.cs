using System.Threading.Tasks;

namespace FixerExchangeCurrency.Services
{
    public interface IApiService<TOut>
    {
        Task<TOut> GetAsync();
    }
}