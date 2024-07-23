using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PharmacyManagement_BE.Application.Commands.CategoryFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.DiseaseFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.ProductFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.IngredientFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.RoleFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.ShipmentDetailsFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.ShipmentFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.StaffFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.SupportFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.SymptomFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.UserFeatures.Requests;
using PharmacyManagement_BE.Application.DTOs.Requests;
using PharmacyManagement_BE.Application.DTOs.Responses;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.DiseaseDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.IngredientDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.SupportDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.SymptomDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.CommentDTOs;
using PharmacyManagement_BE.Application.Commands.CommentFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.OrderDTOs;
using PharmacyManagement_BE.Application.Commands.ProductDiseaseFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductDiseaseDTOs;
using PharmacyManagement_BE.Application.Commands.DiseaseSymptomFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductSupportDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.DiseaseSymptomDTOs;
using PharmacyManagement_BE.Application.Commands.ProductSupportFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductDTOs;

namespace PharmacyManagement_BE.Application.Extentions
{
    public class AutoMapperProfileExtention : Profile
    {
        public AutoMapperProfileExtention()
        {
            #region Product
            CreateMap<Product, AllProductQueryResponse>();
            CreateMap<Product, CreateProductCommandRequest>().ReverseMap();
            CreateMap<Product, UpdateProductCommandRequest>().ReverseMap();
            CreateMap<Product, DetailsProductDTO>().ReverseMap();
            CreateMap<Product, ProductOrderDTO>().ReverseMap();
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
            CreateMap<ShipmentDetails, CreateShipmentDetailsCommandRequest>().ReverseMap();
            CreateMap<ShipmentDetails, ShipmentDetailsRequest>().ReverseMap();
            CreateMap<ShipmentDetails, ShipmentDetailsOrderDTO>().ReverseMap();
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
            CreateMap<Category, UpdateCategoryCommandRequest>().ReverseMap();
            #endregion Category

            #region Ingredient
            CreateMap<CreateIngredientCommandRequest, Ingredient>().ReverseMap();
            CreateMap<Ingredient, IngredientDTO>().ReverseMap();
            #endregion Ingredient

            #region Comment
            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<Comment, CreateReplyCommentCommandRequest>().ReverseMap();
            #endregion Comment

            #region Order
            CreateMap<Order, OrderDTO>().ReverseMap();
            #endregion Order

            #region OrderDetails
            CreateMap<OrderDetails, OrderDetailsDTO>().ReverseMap();
            #endregion OrderDetails

            #region ProductDisease
            CreateMap<CreateProductDiseaseCommandRequest, ProductDisease>().ReverseMap();
            CreateMap<ProductDiseaseDTO, ProductDisease>().ReverseMap();
            #endregion ProductDisease

            #region DiseaseSymptom
            CreateMap<CreateDiseaseSymptomCommandRequest, DiseaseSymptom>().ReverseMap();
            CreateMap<DiseaseSymptomDTO, DiseaseSymptom>().ReverseMap();
            #endregion DiseaseSymptom

            #region ProductSupport
            CreateMap<CreateProductSupportCommandRequest, ProductSupport>().ReverseMap();
            CreateMap<ProductSupportDTO, ProductSupport>().ReverseMap();
            #endregion ProductSupport

        }
    }
}
