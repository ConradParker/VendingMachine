using Microsoft.EntityFrameworkCore;
using System.Linq;
using VendingMachine.Dto;
using VendingMachine.Model;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;

namespace VendingMachine.Data
{
    public class MachineRepository : Repository, IMachineRepository
    {
        public MachineRepository(VendingMachineContext dbContext) : base(dbContext) { }

        public VendingMachineDto GetMachine(int id)
        {
            return _dbContext.Set<Machine>()
                .Where(m => m.Id == id)
                .ProjectTo<VendingMachineDto>()
                .FirstOrDefault();
        }

        public bool AddToWallet(int machineId, int coinTypeId)
        {
            var machine = GetById<Machine>(machineId);
            var coinType = GetById<CoinType>(coinTypeId);
            machine.Wallet.Add(new Coin { CoinType = coinType });
            return _dbContext.SaveChanges() == 1;
        }

        public bool AcceptCoin(int machineId, int coinTypeId)
        {
            var machine = GetById<Machine>(machineId);
            var coinType = GetById<CoinType>(coinTypeId);
            machine.MoneyInserted.Add(new Coin { CoinType = coinType });
            return _dbContext.SaveChanges() == 1;
        }

        public bool EjectCoins(int machineId)
        {
            var machine = GetMachine(machineId);
            machine.MoneyInserted.ToList().ForEach(c => Delete<Coin>(c.Id));
            return true;
        }

        public bool DispenseProduct(int machineId, int productId)
        {
            var product = GetById<Product>(productId);
            Delete<Product>(productId);
            return true;
        }
    }
}
