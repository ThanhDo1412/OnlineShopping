using System;

namespace OnlineShopping.Database.Entity
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string Branch { get; set; }
    }
}
