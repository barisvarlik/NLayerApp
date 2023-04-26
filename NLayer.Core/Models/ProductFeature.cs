using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class ProductFeature
    {
        public int Id { get; set; }
        public string? Color { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int ProductId { get; set; } // Entity Framework will recognize this naming and assign it as foreign key
        public Product Product { get; set; } // Navigation Property
    }
}
