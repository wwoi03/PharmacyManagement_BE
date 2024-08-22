using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using PharmacyManagement_BE.Infrastructure.Respositories.Implementations;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Extentions
{
    public static class RepositoryExtension
    {
        public static void AddRepositoryExtension(this IServiceCollection services)
        {
            services.AddTransient<IPMEntities, PMEntities>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IBranchService, BranchService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IDiseaseService, DiseaseService>();
            services.AddTransient<IDiseaseSymptomService, DiseaseSymptomService>();
            services.AddTransient<IIngredientService, IngredientService>();
            services.AddTransient<IOrderDetailsService, OrderDetailsService>();
            services.AddTransient<IOrderDetailsService, OrderDetailsService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPaymentMethodService, PaymentMethodService>();
            services.AddTransient<IProductDiseaseService, ProductDiseaseService>();
            services.AddTransient<IProductImageService, ProductImageService>();
            services.AddTransient<IProductIngredientService, ProductIngredientService>();
            services.AddTransient<IProductSupportService, ProductSupportService>();
            services.AddTransient<IShipmentDetailsUnitService, ShipmentDetailsUnitService>();
            services.AddTransient<IPromotionHistoryService, PromotionHistoryService>();
            services.AddTransient<IPromotionProductService, PromotionProductService>();
            services.AddTransient<IPromotionProgramService, PromotionProgramService>();
            services.AddTransient<IPromotionService, PromotionService>();
            services.AddTransient<IReceiverInformationService, ReceiverInformationService>();
            services.AddTransient<IShipmentDetailsService, ShipmentDetailsService>();
            services.AddTransient<IShipmentService, ShipmentService>();
            services.AddTransient<IStaffService, StaffService>();
            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<ISupportService, SupportService>();
            services.AddTransient<ISymptomService, SymptomService>();
            services.AddTransient<IUnitService, UnitService>();
            services.AddTransient<IVoucherHistoryService, VoucherHistoryService>();
            services.AddTransient<IVoucherService, VoucherService>();
            services.AddTransient<IVnPayService, VnPayService>();
            services.AddTransient<IPredictionService, PredictionService>();
        }
    }
}
