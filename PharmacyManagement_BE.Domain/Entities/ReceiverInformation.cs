using PharmacyManagement_BE.Domain.Entities.Bases;
using PharmacyManagement_BE.Domain.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class ReceiverInformation : BaseEntity<Guid>
    {
        [Required]
        [StringLength(100)]
        public string ReceiverName { get; set; }

        [Required]
        [StringLength(50)]
        public string RecipientPhone { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Satus { get; set; }

        [Required]
        [StringLength(50)]
        public string ProvinceOrCity { get; set; }

        [Required]
        [StringLength(50)]
        public string District { get; set; }

        [Required]
        [StringLength(50)]
        public string Ward { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public AddressType AddressType { get; set; }

        [Required]
        [StringLength(50)]
        public string AddressDetails { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
    }
}
