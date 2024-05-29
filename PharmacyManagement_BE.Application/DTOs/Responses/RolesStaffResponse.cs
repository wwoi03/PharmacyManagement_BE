﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.DTOs.Responses
{
    public class RolesStaffResponse
    {
        public Guid StaffId { get; set; }
        public string FullName { get; set; }
        public List<string> Roles { get; set; }
    }
}
