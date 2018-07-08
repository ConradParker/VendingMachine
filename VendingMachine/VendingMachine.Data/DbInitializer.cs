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

            // Add 10 cents
            var tenCents = new Coin { CoinType = new CoinType { Name = "10 cent", Value = 0.10M } };
            100.Times(() => wallet.Add(tenCents));

            // Add 20 cents
            var twentyCents = new Coin { CoinType = new CoinType { Name = "20 cent", Value = 0.20M } };
            100.Times(() => wallet.Add(twentyCents));

            // Add 50 cents
            var fiftyCents = new Coin { CoinType = new CoinType { Name = "50 cent", Value = 0.50M } };
            100.Times(() => wallet.Add(fiftyCents));

            // Add 1 euro
            var oneEuro = new Coin { CoinType = new CoinType { Name = "1 euro", Value = 1 } };
            100.Times(() => wallet.Add(oneEuro));

            return wallet;
        }

        private static IList<Product> InitializeProducts()
        {
            var products = new List<Product>();

            // Add Tea
            var tea = new Product { ProductType = new ProductType { Name = "Tea", Price = 1.30M } };
            10.Times(() => products.Add(tea));

            // Add Expresso
            var expresso = new Product { ProductType = new ProductType { Name = "Expresso", Price = 1.80M } };
            20.Times(() => products.Add(expresso));

            // Add Juice
            var juice = new Product { ProductType = new ProductType { Name = "Juice", Price = 1.80M } };
            20.Times(() => products.Add(juice));
            
            // Add Soup
            var soup = new Product { ProductType = new ProductType { Name = "Chicken Soup", Price = 1.80M } };
            15.Times(() => products.Add(soup));

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
