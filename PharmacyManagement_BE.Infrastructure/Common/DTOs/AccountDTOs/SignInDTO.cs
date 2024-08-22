using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.AccountDTOs
{
    public class SignInDTO
    {
        public string Name { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
        public Guid BrandId { get; set; }
    }
}
