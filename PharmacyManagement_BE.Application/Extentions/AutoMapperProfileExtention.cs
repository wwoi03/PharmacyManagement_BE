using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PharmacyManagement_BE.Application.Commands.CategoryFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.DiseaseFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.RoleFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.ShipmentDetailsFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.ShipmentFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.StaffFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.SupportFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.SymptomFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.UserFeatures.Requests;
using PharmacyManagement_BE.Application.DTOs.Requests;
using PharmacyManagement_BE.Application.DTOs.Responses;
using PharmacyManagement_BE.Application.DTOs.Responses.DiseaseResponses;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.SupportDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.SymptomDTOs;
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
            #region Product
            CreateMap<Product, AllProductQueryResponse>();
            #endregion Product 

            #region Customer
            CreateMap<CreateUserCommandRequest, Customer>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.UserName));
            CreateMap<Customer, SignUpCommandResponse>();
            CreateMap<Customer, SignInResponse>();
            #endregion Customer

            #region Role
            CreateMap<IdentityRole<Guid>, RoleResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NormalizedName));
            CreateMap<CreateRoleCommandRequest, IdentityRole<Guid>>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(opt => opt.Name.ToUpper()));
            #endregion Role

            #region Staff
            CreateMap<CreateStaffCommandRequest, Staff>();
            CreateMap<UpdateStaffCommandRequest, Staff>();
            CreateMap<Staff, Staff>();
            CreateMap<Staff, StaffResponse>();
            #endregion Staff

            #region Category
            CreateMap<Category, CategoryResponse>();
            #endregion Category

            #region Shipment
            CreateMap<ShipmentResponse, ShipmentDTO>().ReverseMap();
            CreateMap<Shipment, UpdateShipmentCommandRequest>().ReverseMap();
            CreateMap<Shipment, CreateShipmentCommandRequest>().ReverseMap();
            #endregion Shipment

            #region ShipmentDetails
            CreateMap<ShipmentDetails, UpdateShipmentDetailsCommandRequest>().ReverseMap();
            CreateMap<ShipmentDetails, ShipmentDetailsRequest>().ReverseMap();
            #endregion ShipmentDetails

            #region Disease 
            CreateMap<CreateDiseaseCommandRequest, Disease>().ReverseMap();
            CreateMap<Disease, DiseaseDTO>().ReverseMap();
            #endregion Disease

            #region Symptom
            CreateMap<CreateSymptomCommandRequest, Symptom>().ReverseMap();
            CreateMap<Symptom, SymptomDTO>().ReverseMap();
            #endregion Symptom

            #region Support
            CreateMap<CreateSupportCommandRequest, Support>().ReverseMap();
            CreateMap<Support, SupportDTO>().ReverseMap();
            #endregion Support

            #region Category
            CreateMap<Category, CreateCategoryCommandRequest>().ReverseMap();
            #endregion Category
        }
    }
}
