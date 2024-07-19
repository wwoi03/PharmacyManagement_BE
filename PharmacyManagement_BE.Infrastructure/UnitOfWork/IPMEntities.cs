using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.UnitOfWork
{
    public interface IPMEntities
    {
        IAccountService AccountService { get; set; }
        IBranchService BranchService { get; set; }
        ICartService CartService { get; set; }
        ICategoryService CategoryService { get; set; }
        ICommentService CommentService { get; set; }
        ICustomerService CustomerService { get; set; }
        IDiseaseService DiseaseService { get; set; }
        IDiseaseSymptomService DiseaseSymptomService { get; set; }
        IIngredientService IngredientService { get; set; }
        IOrderDetailsService OrderDetailsService { get; set; }
        IOrderService OrderService { get; set; }
        IPaymentMethodService PaymentMethodService { get; set; }
        IProductDiseaseService ProductDiseaseService { get; set; }
        IProductImageService ProductImageService { get; set; }
        IProductSupportService ProductSupportService { get; set; }
        IProductIngredientService ProductIngredientService { get; set; }
        IShipmentDetailsUnitService ShipmentDetailsUnitService { get; set; }
        IPromotionHistoryService PromotionHistoryService { get; set; }
        IPromotionProductService PromotionProductService { get; set; }
        IPromotionProgramService PromotionProgramService { get; set; }
        IPromotionService PromotionService { get; set; }
        IReceiverInformationService ReceiverInformationService { get; set; }
        IShipmentDetailsService ShipmentDetailsService { get; set; }
        IShipmentService ShipmentService { get; set; }
        IStaffService StaffService { get; set; }
        ISupplierService SupplierService { get; set; }
        ISupportService SupportService { get; set; }
        ISymptomService SymptomService { get; set; }
        IUnitService UnitService { get; set; }
        IVoucherHistoryService VoucherHistoryService { get; set; }
        IVoucherService VoucherService { get; set; }
        IProductService ProductService { get; set; }
        ITokenService TokenService { get; set; }
        int SaveChange();
    }
}
