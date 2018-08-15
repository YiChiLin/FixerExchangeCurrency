using FixerExchangeCurrency.Services;

namespace FixerExchangeCurrency
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var service = new FixerApiService();
            var result = service.GetAsync().GetAwaiter().GetResult();
        }
    }
}