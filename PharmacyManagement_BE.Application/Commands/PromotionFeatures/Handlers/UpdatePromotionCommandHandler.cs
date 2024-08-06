using MediatR;
using PharmacyManagement_BE.Application.Commands.PromotionFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.PromotionFeatures.Handlers
{
    internal class UpdatePromotionCommandHandler : IRequestHandler<UpdatePromotionCommandRequest, ResponseAPI<string>>  
    {
        public Task<ResponseAPI<string>> Handle(UpdatePromotionCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
