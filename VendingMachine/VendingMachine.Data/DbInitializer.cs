using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Model;

namespace VendingMachine.Data
{
    public static class DbInitializer
    {
        #region Public Static Methods

        public static void Initialize(VendingMachineContext context)
        {
            context.Database.EnsureCreated();

            // Look for any existing data.
            if (context.Machines.Any())
            {
                return;   // DB has been seeded
            }
            
            // Add Machine
            var machine = new Machine
            {
                Wallet = InitializeWallet(),
                Products = InitializeProducts()
            };
            context.Machines.Add(machine);

            context.SaveChanges();
        }
        
        #endregion Public Static Methods
        
        #region Private Static Methods

        private static IList<Coin> InitializeWallet()
        {
            var wallet = new List<Coin>();
            
            var tenCents = new CoinType { Name = "10 cent", Value = 0.10M };
            var twentyCents = new CoinType { Name = "20 cent", Value = 0.20M };
            var fiftyCents = new CoinType { Name = "50 cent", Value = 0.50M };
            var oneEuro = new CoinType { Name = "1 euro", Value = 1 };

            100.Times(() => wallet.Add(new Coin { CoinType = tenCents }));
            100.Times(() => wallet.Add(new Coin { CoinType = twentyCents }));
            100.Times(() => wallet.Add(new Coin { CoinType = fiftyCents }));
            100.Times(() => wallet.Add(new Coin { CoinType = oneEuro }));

            return wallet;
        }

        private static IList<Product> InitializeProducts()
        {
            var products = new List<Product>();
            var tea = new ProductType { Name = "Tea", Price = 1.30M };
            var expresso = new ProductType { Name = "Expresso", Price = 1.80M };
            var juice = new ProductType { Name = "Juice", Price = 1.80M };
            var soup = new ProductType { Name = "Chicken Soup", Price = 1.80M };
            
            10.Times(() => products.Add(new Product { ProductType = tea }));
            20.Times(() => products.Add(new Product { ProductType = expresso }));
            20.Times(() => products.Add(new Product { ProductType = juice }));
            15.Times(() => products.Add(new Product { ProductType = soup }));

            return products;
        }

        private static void Times(this int count, Action action)
        {
            for (int i = 0; i < count; i++)
            {
                action?.Invoke();
            }
        }

        #endregion Private Static Methods

    }
}
