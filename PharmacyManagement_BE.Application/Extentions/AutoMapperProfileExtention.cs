using AutoMapper;
using PharmacyManagement_BE.Application.DTOs.Responses;
using PharmacyManagement_BE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Extentions
{
    public class AutoMapperProfileExtention : Profile
    {
        public AutoMapperProfileExtention()
        {
            CreateMap<Product, ViewAllProductQueryResponse>();
        }
    }
}
