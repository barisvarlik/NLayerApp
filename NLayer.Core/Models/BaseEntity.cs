using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; } // Entity Framework will recognize this naming and assign it as primary key
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
