using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.DTOs.Responses
{
    public class StaffResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public Guid BranchId { get; set; }
        public List<string> Roles { get; set; }
    }
}
