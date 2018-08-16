using Dapper;
using System;

namespace FixerExchangeCurrency.Models
{
    [Table("currencyexchangerate")]
    public class CurrencyExchangeRate
    {
        public DateTime Date { get; set; }

        public decimal ExchangeRate { get; set; }

        [Key]
        public int Id { get; set; }

        public string SourceCurrency { get; set; }
        public string TargetCurrency { get; set; }
    }
}