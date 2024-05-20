using AutoMapper;
using PharmacyManagement_BE.Application.Commands.UserFeatures.Requests;
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
            CreateMap<Product, AllProductQueryResponse>();
            CreateMap<Customer, RegisterCommandResponse>()
                .ForMember(destination => destination.FullName, options => options.MapFrom(source => source.UserName));
            CreateMap<CreateUserCommandRequest, Customer>()
                .ForMember(destination => destination.PasswordHash, options => options.MapFrom(source => source.Password));
        }
    }
}
