using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class Comment : BaseEntity<Guid>
    {
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public Guid StaffId { get; set; }
        public Staff Staff { get; set; } = null!;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Rating { get; set; }
        public string CommentText { get; set; }
        public Guid ReplayCommentId { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentType { get; set; }
    }
}
