using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.Model
{
    public class Machine
    {
        #region Properties

        public int Id { get; set; }
        public IList<Coin> MoneyInserted { get; set; }
        public IList<Coin> Wallet { get; set; }
        public IList<Product> Products { get; set; }

        #endregion Properties

        #region Constructors
        public Machine()
        {
            MoneyInserted = new List<Coin>();
        }
        #endregion Constructors
        #region Public Methods

        public decimal CalculateCredit()
        {
            var credit = MoneyInserted.ToList().Sum(c => c.CoinType.Value);
            return credit;
        }

        #endregion Public Methods
    }
}
