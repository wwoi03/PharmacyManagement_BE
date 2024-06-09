using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.UnitOfWork
{
    public class PMEntities : IPMEntities
    {
        private PharmacyManagementContext _context;
        public IProductService ProductService { get; set; }
        public IBranchService BranchService { get; set; }
        public ICartService CartService { get; set; }
        public ICategoryService CategoryService { get; set; }
        public ICommentService CommentService { get; set; }
        public ICustomerService CustomerService { get; set; }
        public IDiseaseService DiseaseService { get; set; }
        public IDiseaseSymptomService DiseaseSymptomService { get; set; }
        public IIngredientService IngredientService { get; set; }
        public IOrderDetailsService OrderDetailsService { get; set; }
        public IOrderService OrderService { get; set; }
        public IPaymentMethodService PaymentMethodService { get; set; }
        public IProductDiseaseService ProductDiseaseService { get; set; }
        public IProductImageService ProductImageService { get; set; }
        public IProductSupportService ProductSupportService { get; set; }
        public IProductIngredientService ProductIngredientService { get; set; }
        public IShipmentDetailsUnitService ProductUnitService { get; set; }
        public IPromotionHistoryService PromotionHistoryService { get; set; }
        public IPromotionProductService PromotionProductService { get; set; }
        public IPromotionProgramService PromotionProgramService { get; set; }
        public IPromotionService PromotionService { get; set; }
        public IReceiverInformationService ReceiverInformationService { get; set; }
        public IShipmentDetailsService ShipmentDetailsService { get; set; }
        public IShipmentService ShipmentService { get; set; }
        public IStaffService StaffService { get; set; }
        public ISupplierService SupplierService { get; set; }
        public ISupportService SupportService { get; set; }
        public ISymptomService SymptomService { get; set; }
        public IUnitService UnitService { get; set; }
        public IVoucherHistoryService VoucherHistoryService { get; set; }
        public IVoucherService VoucherService { get; set; }
        public ITokenService TokenService { get; set; }
        public IAccountService AccountService { get; set; }

        public PMEntities(
            PharmacyManagementContext context,
            IProductService productService,
            IBranchService branchService,
            ICartService cartService,
            ICategoryService categoryService,
            ICommentService commentService,
            ICustomerService customerService,
            IDiseaseService diseaseService,
            IDiseaseSymptomService diseaseSymptomService,
            IIngredientService ingredientService,
            IOrderDetailsService orderDetailsService,
            IOrderService orderService,
            IPaymentMethodService paymentMethodService,
            IProductDiseaseService productDiseaseService,
            IProductImageService productImageService,
            IProductIngredientService productIngredientService,
            IProductSupportService productSupportService,
            IShipmentDetailsUnitService productUnitService,
            IPromotionHistoryService promotionHistoryService,
            IPromotionProductService promotionProductService,
            IPromotionProgramService promotionProgramService,
            IPromotionService promotionService,
            IReceiverInformationService receiverInformationService,
            IShipmentDetailsService shipmentDetailsService,
            IShipmentService shipmentService,
            IStaffService staffService,
            ISupplierService supplierService,
            ISupportService supportService,
            ISymptomService symptomService,
            IUnitService unitService,
            IVoucherHistoryService voucherHistoryService,
            IVoucherService voucherService, 
            ITokenService tokenService,
            IAccountService accountService)
        {
            this._context = context;
            this.ProductService = productService;
            this.BranchService = branchService;
            this.CartService = cartService;
            this.CategoryService = categoryService;
            this.CommentService = commentService;
            this.CustomerService = customerService;
            this.DiseaseService = diseaseService;
            this.PaymentMethodService = paymentMethodService;
            this.ProductDiseaseService = productDiseaseService;
            this.ProductImageService = productImageService;
            this.DiseaseSymptomService = diseaseSymptomService;
            this.IngredientService = ingredientService;
            this.OrderDetailsService = orderDetailsService;
            this.OrderService = orderService;
            this.PromotionHistoryService = promotionHistoryService;
            this.ProductIngredientService = productIngredientService;
            this.ProductSupportService = productSupportService;
            this.ProductUnitService = productUnitService;
            this.PromotionProductService = promotionProductService;
            this.PromotionProgramService = promotionProgramService;
            this.PromotionService = promotionService;
            this.ReceiverInformationService = receiverInformationService;
            this.ShipmentDetailsService = shipmentDetailsService;
            this.ShipmentService = shipmentService;
            this.UnitService = unitService;
            this.StaffService = staffService;
            this.SupplierService = supplierService;
            this.SupportService = supportService;
            this.SymptomService = symptomService;
            this.VoucherHistoryService = voucherHistoryService;
            this.VoucherService = voucherService;
            this.TokenService = tokenService;
            this.AccountService = accountService;
        }

        public int SaveChange()
        {
            return _context.SaveChanges();
        }
    }
}
