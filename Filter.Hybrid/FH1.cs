using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms.Text;

namespace Filter.Hybrid
{
    public class FH1
    {
        List<Product> products = new List<Product>();
        List<Data> dataList = new List<Data>();
        List<Data> testDataList = new List<Data>();

        public void Run()
        {
            // Tạo MLContext
            var mlContext = new MLContext();
            /*--------------------------------------LỌC CỘNG TÁC--------------------------------------*/
            // Tạo dữ liệu giả, Tạo dữ liệu giả cho sản phẩm
            RandomData();

            IDataView trainingDataView = mlContext.Data.LoadFromEnumerable(dataList);
            IDataView testDataView = mlContext.Data.LoadFromEnumerable(testDataList);

            // Chuyển đổi cột CustomerId và ProductId sang KeyType
            var dataProcessingPipeline = mlContext.Transforms.Conversion
                .MapValueToKey("CustomerIdEncoded", nameof(Data.CustomerId))
                .Append(mlContext.Transforms.Conversion.MapValueToKey("ProductIdEncoded", nameof(Data.ProductId)));

            // Cấu hình thuật toán lọc cộng tác
            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "CustomerIdEncoded",
                MatrixRowIndexColumnName = "ProductIdEncoded",
                LabelColumnName = "Label",
                NumberOfIterations = 20,
                ApproximationRank = 100
            };

            var trainerEstimator = dataProcessingPipeline.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

            // Huấn luyện mô hình lọc cộng tác
            Console.WriteLine("=============== Training the model ===============");
            ITransformer collaborativeFilteringModel = trainerEstimator.Fit(trainingDataView);

            // Đánh giá độ chính xác
            Console.WriteLine("=============== Evaluating the model ===============");
            var prediction1 = collaborativeFilteringModel.Transform(testDataView);
            var metrics = mlContext.Regression.Evaluate(prediction1, labelColumnName: "Label", scoreColumnName: "Score");
            Console.WriteLine("Root Mean Squared Error : " + metrics.RootMeanSquaredError.ToString());
            Console.WriteLine("RSquared: " + metrics.RSquared.ToString());

            // Dự đoán với mô hình Lọc cộng tác
            var predictionEngine = mlContext.Model.CreatePredictionEngine<Data, ProductRecommendation>(collaborativeFilteringModel);
            var prediction2 = predictionEngine.Predict(new Data
            {
                CustomerId = "KH001",
                ProductId = "P010"
            });
            Console.WriteLine($"Khả năng khách hàng mua sản phẩm: {prediction2.Score}");
        }

        public void RandomData()
        {
            Random rnd = new Random();

            // Sample product names and types in Vietnamese
            string[] productNames = { "Thuốc ho", "Vitamin C", "Kem chống nắng", "Sữa rửa mặt", "Thuốc cảm", "Kem dưỡng da", "Thuốc bổ não", "Siro ho", "Thuốc đau đầu", "Kem trị mụn" };
            string[] productTypes = { "Dược phẩm", "Mỹ phẩm", "Thực phẩm chức năng" };
            string[] productSupports = { "Tăng cường sức khỏe", "Chống viêm", "Hỗ trợ tiêu hóa", "Chăm sóc da", "Giảm đau" };
            string[] productDiseases = { "Ho", "Cảm cúm", "Đau đầu", "Dị ứng", "Mụn trứng cá" };
            string[] seasons = { "Xuân", "Hạ", "Thu", "Đông" };

            // Generate sample products
            for (int i = 0; i < 100; i++)
            {
                products.Add(new Product
                {
                    ProductId = "P" + (i + 1).ToString("D3"),
                    ProductName = productNames[i % productNames.Length],
                    ProductType = productTypes[rnd.Next(productTypes.Length)],
                    ProductSupport = productSupports[rnd.Next(productSupports.Length)],
                    ProductDisease = productDiseases[rnd.Next(productDiseases.Length)]
                });
            }

            // Generate 100 sample training ata
            for (int i = 0; i < 100000; i++)
            {
                dataList.Add(new Data
                {
                    CustomerId = "KH" + rnd.Next(1, 101).ToString("D3"),
                    ProductId = products[rnd.Next(products.Count)].ProductId,
                    Label = rnd.NextDouble() > 0.5 ? 1.0f : 0.5f,
                    Season = seasons[rnd.Next(seasons.Length)]
                });
            }

            // Generate 20 sample test data
            for (int i = 0; i < 20; i++)
            {
                testDataList.Add(new Data
                {
                    CustomerId = "KH" + rnd.Next(1, 4).ToString("D3"),
                    ProductId = products[rnd.Next(products.Count)].ProductId,
                    Label = rnd.NextDouble() > 0.5 ? 1.0f : 0.5f,
                    Season = seasons[rnd.Next(seasons.Length)]
                });
            }

            // Output sample product
            foreach (var data in products)
            {
                Console.WriteLine($"ProductId: {data.ProductId}, ProductName: {data.ProductName}, ProductType: {data.ProductType}, ProductSupport: {data.ProductSupport}, ProductDisease: {data.ProductDisease}");
            }

            // Output sample data
            foreach (var data in dataList)
            {
                Console.WriteLine($"CustomerId: {data.CustomerId}, ProductId: {data.ProductId}, Label: {data.Label}, Season: {data.Season}");
            }

            // Output sample data
            Console.WriteLine("-----------------------------TestData-----------------------------");
            foreach (var data in testDataList)
            {
                Console.WriteLine($"CustomerId: {data.CustomerId}, ProductId: {data.ProductId}, Label: {data.Label}, Season: {data.Season}");
            }
        }
    }

    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string ProductSupport { get; set; }
        public string ProductDisease { get; set; }
    }

    public class Data
    {
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public float Label { get; set; } // 1.0 nếu sản phẩm đã mua, 0.5 nếu chỉ thêm vào giỏ hàng
        public string Season { get; set; }
    }

    public class ProductRecommendation
    {
        public float Score { get; set; } // Điểm dự đoán của mô hình hybrid
    }
}


/*--------------------------------------LỌC THEO NỘI DUNG--------------------------------------*//*
            var productData = mlContext.Data.LoadFromEnumerable(products);

            // Xử lý văn bản để lấy đặc trưng sản phẩm
            var contentPipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(Product.ProductType))
                .Append(mlContext.Transforms.Conversion.MapValueToKey(nameof(Product.ProductId)));

            // Áp dụng pipeline xử lý dữ liệu sản phẩm
            var processedProductData = contentPipeline.Fit(productData).Transform(productData);

            *//*--------------------------------------LỌC KẾT HỢP--------------------------------------*//*
            // Kết hợp các đặc trưng từ mô hình lọc cộng tác và lọc theo nội dung
            var hybridPipeline = mlContext.Transforms.Concatenate("CombinedFeatures", "Features")
                .Append(mlContext.Transforms.CopyColumns("Label", nameof(Data.Label))) // Đảm bảo cột Label được sao chép vào pipeline
                .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "Label", featureColumnName: "CombinedFeatures"));

            // Huấn luyện mô hình hybrid
            var hybridModel = hybridPipeline.Fit(processedTransactionData);

            // Dự đoán với mô hình hybrid
            var predictionEngine = mlContext.Model.CreatePredictionEngine<Data, ProductRecommendation>(hybridModel);*/