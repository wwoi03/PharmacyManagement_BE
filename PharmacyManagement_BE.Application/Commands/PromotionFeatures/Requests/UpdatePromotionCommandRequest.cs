﻿using MediatR;
using PharmacyManagement_BE.Domain.Types;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.PromotionDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using PharmacyManagement_BE.Infrastructure.Customs.SupportFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.PromotionFeatures.Requests
{
    public class UpdatePromotionCommandRequest : IRequest<ResponseAPI<string>>
    {
        //Giá trị Promotion
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public PromotionType DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public string CodePromotion { get; set; }

        //Chọn product được giảm giá

        public ValidationNotify<string> IsValid()
        {
            // Clean and validate inputs
            Name = CheckInput.CheckInputName(Name);
            CodePromotion = CheckInput.CheckInputCode(CodePromotion);

            // Validate Name field
            if (string.IsNullOrWhiteSpace(Name))
                return new ValidationNotifyError<string>("Vui lòng nhập tên khuyến mãi.", "name");

            // Validate CodePromotion field
            if (string.IsNullOrWhiteSpace(CodePromotion))
                return new ValidationNotifyError<string>("Vui lòng nhập mã khuyến mãi.", "codePromotion");

            // Check if CodePromotion is alphanumeric
            if (!CheckInput.IsAlphaNumeric(CodePromotion))
                return new ValidationNotifyError<string>("Mã khuyến mãi không hợp lệ, vui lòng kiểm tra lại", "codePromotion");

            // Validate Date fields
            if (StartDate == default)
                return new ValidationNotifyError<string>("Vui lòng nhập ngày bắt đầu.", "startDate");

            if (EndDate == default)
                return new ValidationNotifyError<string>("Vui lòng nhập ngày kết thúc.", "endDate");

            if (StartDate >= EndDate)
                return new ValidationNotifyError<string>("Ngày bắt đầu phải trước ngày kết thúc.", "startDate");

            // Validate DiscountValue field
            if (DiscountValue <= 0)
                return new ValidationNotifyError<string>("Giá trị khuyến mãi phải lớn hơn 0.", "discountValue");

            // Return success if all validations pass
            return new ValidationNotifySuccess<string>();
        }
    }
}