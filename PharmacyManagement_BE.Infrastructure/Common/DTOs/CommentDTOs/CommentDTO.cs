using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Domain.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.CommentDTOs
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } 
        public Guid? StaffId { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; } 
        public int Rating { get; set; }
        public string CommentText { get; set; }
        public Guid? ReplayCommentId { get; set; }
        public DateTime CommentDate { get; set; }
        public CommentType CommentType { get; set; }
    }
}
