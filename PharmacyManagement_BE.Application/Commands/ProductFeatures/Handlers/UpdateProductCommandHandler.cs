using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.ProductFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.ProductFeatures.Handlers
{
    internal class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var productExists = await _entities.ProductService.GetProductByCodeMedicineOrName(request.CodeMedicine, request.Name);

                // Kiểm tra sản phẩm tồn tại
                if (productExists != null)
                {
                    // Kiểm tra CodeMedicine tồn tại
                    if (productExists.CodeMedicine.Equals(request.CodeMedicine))
                        return new ResponseErrorAPI<string>(StatusCodes.Status409Conflict, $"Mã sản phẩm {request.CodeMedicine} đã tồn tại.");

                    // Kiểm tra tên sản phẩm tồn tại
                    if (productExists.Name.Equals(request.Name))
                        return new ResponseErrorAPI<string>(StatusCodes.Status409Conflict, $"Tên sản phẩm đã tồn tại.");
                }

                // Kiểm tra loại sản phẩm tồn tại
                var category = await _entities.CategoryService.GetById(request.CategoryId);

                if (category == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, $"Loại sản phẩm không tồn tại");

                // Kiểm tra thành phần sản phẩm tồn tại
                foreach (var item in request.ProductIngredients)
                {
                    var ingredient = await _entities.IngredientService.GetById(item);

                    if (ingredient == null)
                        return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, $"Thành phần có mã {item} không tồn tại");
                }

                // Kiểm tra hỗ trợ sản phẩm tồn tại
                foreach (var item in request.ProductSupports)
                {
                    var support = await _entities.SupportService.GetById(item);

                    if (support == null)
                        return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, $"Hỗ trợ có mã {item} không tồn tại");
                }

                // Kiểm tra loại bệnh tồn tại
                foreach (var item in request.ProductDiseases)
                {
                    var disease = await _entities.DiseaseService.GetById(item);

                    if (disease == null)
                        return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, $"Loại bệnh có mã {item} không tồn tại");
                }

                // Thêm sản phẩm
                var product = _mapper.Map<Product>(request);
                product.Id = Guid.NewGuid();
                product.CreatedTime = DateTime.Now;
                var result = _entities.ProductService.Create(product);

                if (!result)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, $"Lỗi trong quá trình thêm sản phẩm.");

                // Thêm thành phần sản phẩm
                if (request.ProductIngredients != null && request.ProductIngredients.Count > 0)
                {
                    var productIngredients = request.ProductIngredients.Select(item => new ProductIngredient
                    {
                        ProductId = product.Id,
                        IngredientId = item
                    }).ToList();

                    var createProductIngredientsResult = await _entities.ProductIngredientService.CreateRange(productIngredients);

                    if (!createProductIngredientsResult)
                        return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, $"Lỗi trong quá trình thêm thành phần sản phẩm.");
                }

                // Thêm hỗ trợ sản phẩm
                if (request.ProductSupports != null && request.ProductSupports.Count > 0)
                {
                    var productSupports = request.ProductSupports.Select(item => new ProductSupport
                    {
                        ProductId = product.Id,
                        SupportId = item
                    }).ToList();

                    var createProductSupportsResult = await _entities.ProductSupportService.CreateRange(productSupports);

                    if (!createProductSupportsResult)
                        return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, $"Lỗi trong quá trình thêm hỗ trợ sản phẩm.");
                }

                // Thêm loại bệnh
                if (request.ProductDiseases != null && request.ProductDiseases.Count > 0)
                {
                    var productDiseases = request.ProductDiseases.Select(item => new ProductDisease
                    {
                        ProductId = product.Id,
                        DiseaseId = item
                    }).ToList();

                    var createProductDiseasesResult = await _entities.ProductDiseaseService.CreateRange(productDiseases);

                    if (!createProductDiseasesResult)
                        return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, $"Lỗi trong quá trình thêm loại bệnh sản phẩm.");
                }

                // Thêm hình ảnh sản phẩm

                // SaveChange
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, $"Thêm sản phẩm có mã {request.CodeMedicine} thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
