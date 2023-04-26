using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; } // Entity Framework will recognize this naming and assign it as foreign key
        public Category Category { get; set; } // Navigation Property
        public ProductFeature ProductFeature { get; set; }// Navigation Property
    }
}
