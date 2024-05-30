using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class Cart : BaseEntity<Guid>
    {
        [Required]
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Required]
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; } = null!;

        [Required]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
