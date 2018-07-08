using System.Collections.Generic;
using VendingMachine.Dto;
using VendingMachine.Model;

namespace VendingMachine.Data
{
    public interface IMachineRepository : IRepository
    {
        bool AcceptCoin(int machineId, int coinTypeId);
        VendingMachineDto GetMachineDto(int id);
        bool EjectCoins(int machineId);
        bool DispenseProduct(int machineId, int productTypeId);
    }
}
