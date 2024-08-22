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
                if (request.ProductIngredients != null && request.ProductIngredients.Count > 0)
                {
                    foreach (var item in request.ProductIngredients)
                    {
                        var ingredient = await _entities.IngredientService.GetById(item);

                        if (ingredient == null)
                            return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, $"Thành phần có mã {item} không tồn tại");
                    }
                }

                // Kiểm tra hỗ trợ sản phẩm tồn tại
                if (request.ProductSupports != null && request.ProductSupports.Count > 0)
                {
                    foreach (var item in request.ProductSupports)
                    {
                        var support = await _entities.SupportService.GetById(item);

                        if (support == null)
                            return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, $"Hỗ trợ có mã {item} không tồn tại");
                    }
                }

                // Kiểm tra loại bệnh tồn tại
                if (request.ProductDiseases != null && request.ProductDiseases.Count > 0)
                {
                    foreach (var item in request.ProductDiseases)
                    {
                        var disease = await _entities.DiseaseService.GetById(item);

                        if (disease == null)
                            return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, $"Loại bệnh có mã {item} không tồn tại");
                    }
                }

                // Sửa sản phẩm
                var product = await _entities.ProductService.GetById(request.Id);
                _mapper.Map(request, product);
                var updateProductResult = _entities.ProductService.Update(product);

                if (!updateProductResult)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, $"Lỗi trong quá trình cập nhật sản phẩm.");

                // Xử lý thành phần
                {
                    var productIngredientOld = (await _entities.ProductIngredientService.GetProductIngredientByProductId(product.Id))
                        .Select(item => item.IngredientId)
                        .ToList();

                    // Tìm các phần tử cần thêm vào 
                    var itemsToAdd = request.ProductIngredients.Except(productIngredientOld).ToList();

                    // Tìm các phần tử cần xóa khỏi 
                    var itemsToRemove = productIngredientOld.Except(request.ProductIngredients).ToList();

                    // Thêm thành phần sản phẩm
                    if (itemsToAdd != null && itemsToAdd.Count > 0)
                    {
                        var productIngredients = itemsToAdd.Select(item => new ProductIngredient
                        {
                            ProductId = product.Id,
                            IngredientId = item,
                            UnitId = Guid.Parse("4881154e-4715-485a-8c5c-c088295369c3"),
                            Content = 20
                        }).ToList();

                        var createProductIngredientsResult = await _entities.ProductIngredientService.CreateRange(productIngredients);

                        if (!createProductIngredientsResult)
                            return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, $"Lỗi trong quá trình thêm thành phần sản phẩm.");
                    }

                    // Xóa thành phần sản phẩm
                    foreach (var id in itemsToRemove)
                    {
                        var item = await _entities.ProductIngredientService.GetById(id);
                        _entities.ProductIngredientService.Delete(item);
                    }
                }

                // Xử lý hỗ trợ
                {
                    var productSupportOlds = (await _entities.ProductSupportService.GetProductSupportsByProductId(product.Id))
                        .Select(item => item.SupportId)
                        .ToList();

                    // Tìm các phần tử cần thêm vào 
                    var itemsToAdd = request.ProductSupports.Except(productSupportOlds).ToList();

                    // Tìm các phần tử cần xóa khỏi 
                    var itemsToRemove = productSupportOlds.Except(request.ProductSupports).ToList();

                    // Thêm
                    if (itemsToAdd != null && itemsToAdd.Count > 0)
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

                    // Xóa 
                    foreach (var id in itemsToRemove)
                    {
                        var item = await _entities.ProductSupportService.GetById(id);
                        _entities.ProductSupportService.Delete(item);
                    }
                }

                // Xử lý loại bệnh
                {
                    var productDiseaseOlds = (await _entities.ProductDiseaseService.GetProductDiseasesByProductId(product.Id))
                        .Select(item => item.DiseaseId)
                        .ToList();

                    // Tìm các phần tử cần thêm vào 
                    var itemsToAdd = request.ProductDiseases.Except(productDiseaseOlds).ToList();

                    // Tìm các phần tử cần xóa khỏi 
                    var itemsToRemove = productDiseaseOlds.Except(request.ProductDiseases).ToList();

                    // Thêm
                    if (itemsToAdd != null && itemsToAdd.Count > 0)
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

                    // Xóa 
                    foreach (var id in itemsToRemove)
                    {
                        var item = await _entities.ProductDiseaseService.GetById(id);
                        _entities.ProductDiseaseService.Delete(item);
                    }
                }

                {
                    // Lấy danh sách hình ảnh
                    var imageExists = await _entities.ProductImageService.GetProductImagesByProductId(product.Id);

                    // Thêm hình ảnh sản phẩm
                    if (request.Images.Count > 0)
                    {
                        foreach (var item in request.Images)
                        {
                            var productImage = new ProductImage
                            {
                                ProductId = product.Id,
                                Image = item
                            };

                            _entities.ProductImageService.Create(productImage);
                        }
                    }

                    var productImagesOlds = (await _entities.ProductImageService.GetProductImagesByProductId(product.Id))
                        .Select(item => item.Image)
                        .ToList();

                    // Tìm các phần tử cần thêm vào 
                    var itemsToAdd = request.Images.Except(productImagesOlds).ToList();

                    // Tìm các phần tử cần xóa khỏi 
                    var itemsToRemove = productImagesOlds.Except(request.Images).ToList();

                    // Thêm
                    if (itemsToAdd != null && itemsToAdd.Count > 0)
                    {
                        foreach (var item in itemsToAdd)
                        {
                            var productImage = new ProductImage
                            {
                                ProductId = product.Id,
                                Image = item
                            };

                            _entities.ProductImageService.Create(productImage);
                        }
                    }

                    // Xóa 
                    foreach (var image in itemsToRemove)
                    {
                        var item = await _entities.ProductImageService.GetProductImagesByImage(image);
                        _entities.ProductImageService.Delete(item);
                    }
                }                
                
                // SaveChange
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, $"Cập nhật sản phẩm có mã {request.CodeMedicine} thành công.", product.Id.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
