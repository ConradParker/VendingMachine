using System.Collections.Generic;
using VendingMachine.Model;

namespace VendingMachine.Dto
{
    public class VendingMachineDto
    {
        public IList<CoinDto> MoneyInserted { get; set; }
        public IList<CoinDto> MoneyEjected { get; set; }
        public IList<CoinDto> Wallet { get; set; }
        public IList<Product> Products { get; set; }
        public IList<CoinType> KnownCoins { get; set; }
        public IList<ProductTypeDto> KnownProducts { get; set; }
        public string Error { get; set; }
    }
}
