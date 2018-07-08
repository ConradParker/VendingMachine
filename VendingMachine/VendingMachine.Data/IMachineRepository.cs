using VendingMachine.Dto;
using VendingMachine.Model;

namespace VendingMachine.Data
{
    public interface IMachineRepository : IRepository
    {
        bool AddToWallet(int machineId, int coinTypeId);
        bool AcceptCoin(int machineId, int coinTypeId);
        VendingMachineDto GetMachine(int id);
        bool EjectCoins(int machineId);
        bool DispenseProduct(int machineId, int productId);
    }
}
