using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.ML;
using Microsoft.ML.Trainers;
using RecommendSimilarProduct.Models;

namespace RecommendSimilarProduct
{
    public static class ProductPrediction
    {
        // Chuẩn bị dự liệu
        public static (IDataView training, IDataView test) LoadData(MLContext mlContext, List<ProductInteractive> trainingData, List<ProductInteractive> testData)
        {
            IDataView trainingDataView = mlContext.Data.LoadFromEnumerable<ProductInteractive>(trainingData);
            IDataView testDataView = mlContext.Data.LoadFromEnumerable<ProductInteractive>(testData);

            return (trainingDataView, testDataView);
        }

        // Huấn luyện mô hình
        public static ITransformer BuildAndTrainModel(MLContext mlContext, IDataView trainingDataView)
        {
            IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion
                .MapValueToKey(outputColumnName: "CustomerIdEncoded", inputColumnName: "CustomerId")
                .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "ProductIdEncoded", inputColumnName: "ProductId"));

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "CustomerIdEncoded",
                MatrixRowIndexColumnName = "ProductIdEncoded",
                LabelColumnName = "Label",
                NumberOfIterations = 20,
                ApproximationRank = 100
            };

            var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

            Console.WriteLine("=============== Training the model ===============");
            ITransformer model = trainerEstimator.Fit(trainingDataView);

            return model;
        }

        // Đánh giá mô hình
        public static void EvaluateModel(MLContext mlContext, IDataView testDataView, ITransformer model)
        {
            Console.WriteLine("=============== Evaluating the model ===============");
            var prediction = model.Transform(testDataView);

            var metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "Label", scoreColumnName: "Score");

            Console.WriteLine("Root Mean Squared Error : " + metrics.RootMeanSquaredError.ToString());
            Console.WriteLine("RSquared: " + metrics.RSquared.ToString());
        }

        public static List<Guid> UseModelForMultiPrediction(MLContext mlContext, ITransformer model, Guid customerId, List<Guid> productIds)
        {
            Console.WriteLine("=============== Making a prediction ===============");
            var predictionEngine = mlContext.Model.CreatePredictionEngine<ProductInteractive, ProductInteractivePrediction>(model);

            List<Guid> result = new List<Guid>();

            foreach (var productId in productIds)
            {
                var testInput = new ProductInteractive { CustomerId = GetFiveDigitNumberFromGuid(customerId), ProductId = GetFiveDigitNumberFromGuid(productId) };

                var productPrediction = predictionEngine.Predict(testInput);

                Console.WriteLine("Score " + productPrediction.Score + " -- " + Math.Round(productPrediction.Score, 1));
                if (Math.Round(productPrediction.Score, 1) >= 0.5)
                {
                    result.Add(productId);
                }
            }

            return result;
        }

        public static long GetFiveDigitNumberFromGuid(Guid guid)
        {
            byte[] bytes = guid.ToByteArray();
            long longFromGuid = BitConverter.ToInt64(bytes, 0);

            // Sử dụng modulo để thu được một số trong phạm vi 0 đến 99999
            long fiveDigitNumber = Math.Abs(longFromGuid) % 100000;

            return fiveDigitNumber;
        }
    }
}
