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

        public Machine GetMachine(int id)
        {
            return _dbContext.Set<Machine>()
                .Where(m => m.Id == id)
                .Include(m => m.MoneyInserted)
                .Include(m => m.Wallet)
                .FirstOrDefault();
        }

        public IEnumerable<ProductTypeDto> GetProductTypeDtos()
        {
            return _dbContext.Set<ProductType>()
                .ProjectTo<ProductTypeDto>();
        }

        public VendingMachineDto GetMachineDto(int id)
        {
            var dto = _dbContext.Set<Machine>()
                .Where(m => m.Id == id)
                .ProjectTo<VendingMachineDto>()
                .FirstOrDefault();

            dto.KnownCoins = GetAll<CoinType>().ToList();
            dto.KnownProducts = GetProductTypeDtos().ToList();

            return dto;
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

        public bool DispenseProduct(int machineId, int productTypeId)
        {
            var machine = GetMachine(machineId);
            var machineDto = GetMachineDto(machineId);
            var product = GetProductByType(productTypeId);
            var amountToPay = product.ProductType.Price;

            Delete<Product>(product.Id);

            machineDto.MoneyInserted
                .OrderByDescending(c => c.Value)
                .ToList()
                .ForEach(m => {                    
                    if (amountToPay > 0)
                    {
                        var coin = GetById<Coin>(m.Id);
                        machine.Wallet.Add(coin);
                        machine.MoneyInserted.Remove(coin);
                        amountToPay -= m.Value;
                    }
                 });

            _dbContext.SaveChanges();
            return true;
        }


        private Product GetProductByType(int productTypeId)
        {
            return _dbContext.Set<Product>()
                .Where(m => m.ProductType.Id == productTypeId)
                .Include(p=>p.ProductType)
                .FirstOrDefault();
        }

    }
}
