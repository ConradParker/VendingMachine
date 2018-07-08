using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Dto
{
    public class ProductTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
