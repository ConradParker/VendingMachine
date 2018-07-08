using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Dto
{
    public class CoinDto
    {
        public int Id { get; set; }
        public string CoinTypeName { get; set; }
        public decimal Value { get; set; }
    }
}
