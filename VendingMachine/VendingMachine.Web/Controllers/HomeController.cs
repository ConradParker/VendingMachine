using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.Data;
using VendingMachine.Model;
using VendingMachine.Web.Models;

namespace VendingMachine.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Private Variables

        private readonly IMachineRepository _machineRepository;
        const int _machineId = 1;
        
        #endregion Private Variables

        #region Constructor(s)

        public HomeController(IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository;
        }

        #endregion Constructor(s)

        #region Public Methods

        public IActionResult Index()
        {
            var viewModel = _machineRepository.GetMachine(_machineId);
            return View(viewModel);
        }

        public IActionResult ChooseProduct(int productId)
        {            
            var viewModel = _machineRepository.GetMachine(_machineId);
            var selectedProduct = viewModel.KnownProducts.FirstOrDefault(p => p.Id == productId);
            if(selectedProduct.Price > viewModel.MoneyInserted.Sum(i => i.Value))
            {
                viewModel.Error = "Insuffcient credit!";
            }
            else
            {
                _machineRepository.DispenseProduct(_machineId, productId);
                viewModel = _machineRepository.GetMachine(_machineId);
            }
            
            return PartialView("_MachineState", viewModel);
        }

        public IActionResult InsertCoin(int coinTypeId)
        {
            _machineRepository.AcceptCoin(_machineId, coinTypeId);

            var viewModel = _machineRepository.GetMachine(_machineId);
            return PartialView("_MachineState", viewModel);
        }

        public IActionResult EjectCoins()
        {
            var viewModel = _machineRepository.GetMachine(_machineId);
            var moneyEjected = viewModel.MoneyInserted;

            _machineRepository.EjectCoins(_machineId);

            viewModel = _machineRepository.GetMachine(_machineId);
            viewModel.MoneyEjected = moneyEjected;
            return PartialView("_MachineState", viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion Public Methods

        #region Private Methods

        
        #endregion
    }
}
