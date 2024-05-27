using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PharmacyManagement_BE.Application.Commands.DiseaseFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.RoleFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.StaffFeatures.Requests;
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

            CreateMap<CreateUserCommandRequest, Customer>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.UserName));
            CreateMap<Customer, SignUpCommandResponse>();

            CreateMap<Customer, SignInResponse>();

            CreateMap<IdentityRole<Guid>, RoleResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NormalizedName));

            CreateMap<CreateRoleCommandRequest, IdentityRole<Guid>>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(opt => opt.Name.ToUpper()));

            CreateMap<CreateStaffCommandRequest, Staff>();

            CreateMap<UpdateStaffCommandRequest, Staff>();

            CreateMap<Staff, Staff>();

            CreateMap<CreateDiseaseCommandRequest, Disease>();
        }
    }
}
