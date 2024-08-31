using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendSimilarProduct.Models
{
    public class ProductInteractive
    {
        public long CustomerId { get; set; }
        public long ProductId { get; set; }
        public float Label { get; set; } // 1.0 nếu sản phẩm đã mua, 0.5 nếu chỉ thêm vào giỏ hàng
    }

    public class ProductInteractivePrediction
    {
        public float Label;
        public float Score { get; set; } 
    }
}
