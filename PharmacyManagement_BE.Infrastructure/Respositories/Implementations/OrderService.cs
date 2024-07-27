using Dapper;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Domain.Types;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.OrderDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.OrderDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.OrderEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.StatisticDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.DBContext.Dapper;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Implementations
{
    public class OrderService : RepositoryService<Order>, IOrderService
    {
        private readonly PharmacyManagementContext _context;
        private readonly PMDapperContext _dapperContext;

        public OrderService(PharmacyManagementContext context,PMDapperContext dapperContext) : base(context)
        {
            this._context = context;
            this._dapperContext = dapperContext;
        }

        #region EF
        public async Task<List<Order>> GetAllOrderByStaffId(Guid id)
        {
            return _context.Orders.Where(x => x.StaffId == id).ToList();
        }

        public async Task<Order?> GetOrderByCode(string codeOrder)
        {
            return _context.Orders.FirstOrDefault(i => i.CodeOrder.Equals(codeOrder));
        }

        public async Task<List<ItemOrderDTO>> GetMyOrders(Guid customerId)
        {
            // Lấy danh sách đơn hàng của khách hàng
            var orders = _context.Orders
                .Where(x => x.CustomerId == customerId)
                .OrderByDescending(x => x.OrderDate)
                .ToList();

            // thông tin đơn hàng
            var result = orders.Select(order =>
            {
                // Lấy sản phẩm đầu tiên
                var firstOrderDetails = _context.OrderDetails
                    .Where(item => item.OrderId == order.Id)
                    .Include(item => item.ShipmentDetails)
                    .Include(item => item.ShipmentDetails.Product)
                    .FirstOrDefault();

                // Lấy đơn vị
                var unitOrderDetails = _context.Units
                    .FirstOrDefault(item => item.Id == firstOrderDetails.UnitId);

                // tính số lượng sản phẩm của đơn hàng
                var productQuantity = _context.OrderDetails
                    .Where(item => item.OrderId == order.Id)
                    .Count();

                return new ItemOrderDTO
                {
                    OrderId = order.Id, // Hoặc sử dụng orderId nếu thực sự cần thiết
                    OrderDate = order.OrderDate,
                    CodeOrder = order.CodeOrder,
                    Status = order.Status,
                    FinalAmount = order.FinalAmount,
                    ProductQuantity = productQuantity,
                    NameFirstProduct = firstOrderDetails.ShipmentDetails.Product.Name,
                    ImageFirstProduct = firstOrderDetails.ShipmentDetails.Product.Image,
                    PriceFirstProduct = firstOrderDetails.PricePerUnit,
                    QuantityFirstProduct = firstOrderDetails.Quantity,
                    UnitFirstProduct = unitOrderDetails.NameDetails
                };
            }).ToList();
            
            return result;
        }
        #endregion EF

        #region Dapper
        // Thống kê đơn hàng
        public async Task<List<StatisticDTO>> StatisticOrder(TimeType type)
        {
            var parameters = new DynamicParameters();
            var listStatistic = new List<StatisticDTO>();

            if (type == TimeType.week)
            {
                // Lặp qua 7 ngày trước đó
                for (int i = 0; i < 7; i++)
                {
                    DateTime dateTime = DateTime.Now.AddDays(-i); // Lấy ngày hôm nay trừ đi số ngày i
                    
                    // Chuẩn bị câu lệnh SQL với tham số, thêm status để sau***********************
                    string sql = @"
                    SELECT COUNT(*)
                    FROM Orders
                    WHERE OrderDate = @OrderDate";

                    // Đặt giá trị cho tham số
                    parameters.Add("@OrderDate", dateTime.Date, DbType.Date);

                    // Thực hiện truy vấn và lấy kết quả
                    int count = _dapperContext.GetConnection.QueryFirst<int>(sql, parameters);

                    // Tạo đối tượng StatisticDTO và thêm vào danh sách
                    var statistic = new StatisticDTO
                    {
                        Title = dateTime.DayOfWeek.ToString(), // Lấy tên ngày trong tuần
                        Statistic = count // Số lượng đơn hàng cho ngày này
                    };
                    listStatistic.Add(statistic);

                }
            }

            else if (type == TimeType.month)
            {
                // Lặp qua 12 tháng trước đó
                for (int i = 0; i < 12; i++)
                {
                    DateTime dateTime = DateTime.Now.AddMonths(-i); // Lấy tháng trừ đi số tháng i

                    // Chuẩn bị câu lệnh SQL với tham số, thêm status để sau***********************
                    string sql = @"
                    SELECT COUNT(*)
                    FROM Orders
                    WHERE MONTH(OrderDate) = MONTH(@OrderDate)
                    AND YEAR(OrderDate) = YEAR(@OrderDate)";

                    // Đặt giá trị cho tham số
                    parameters.Add("@OrderDate", dateTime.Date, DbType.Date);

                    // Thực hiện truy vấn và lấy kết quả
                    int count = _dapperContext.GetConnection.QueryFirst<int>(sql, parameters);

                    // Tạo đối tượng StatisticDTO và thêm vào danh sách
                    var statistic = new StatisticDTO
                    {
                        Title = dateTime.ToString("MMM"), // Lấy tên tháng 
                        Statistic = count // Số lượng đơn hàng cho tháng này
                    };
                    listStatistic.Add(statistic);
                }
            }
            else if (type == TimeType.year)
            {
                string sql = @"
                SELECT DISTINCT YEAR(OrderDate) 
                FROM Orders
                ORDER BY OrderYear ASC";

                // Thực hiện truy vấn và lấy danh sách các năm
                var years = _dapperContext.GetConnection.Query<int>(sql).ToList();

                //Truy xuất các năm
                foreach (var i in years)
                {
                    // Chuẩn bị câu lệnh SQL với tham số, thêm status để sau***********************
                    sql = @"
                    SELECT COUNT(*)
                    FROM Orders
                    WHERE YEAR(OrderDate) = @OrderDate";

                    // Đặt giá trị cho tham số
                    parameters.Add("@OrderDate", i, DbType.Int16);

                    // Thực hiện truy vấn và lấy kết quả
                    int count = _dapperContext.GetConnection.QueryFirst<int>(sql, parameters);

                    // Tạo đối tượng StatisticDTO và thêm vào danh sách
                    var statistic = new StatisticDTO
                    {
                        Title = i.ToString(), // Lấy tên năm 
                        Statistic = count // Số lượng đơn hàng cho năm này
                    };
                    listStatistic.Add(statistic);
                }

            }
            return listStatistic;
        }

        // Thống kê doanh thu
        public async Task<List<StatisticDTO>> StatisticRevenue(TimeType type)
        {
            var parameters = new DynamicParameters();
            var listStatistic = new List<StatisticDTO>();

            if (type == TimeType.week)
            {
                // Lặp qua 7 ngày trước đó
                for (int i = 0; i < 7; i++)
                {
                    DateTime dateTime = DateTime.Now.AddDays(-i); // Lấy ngày hôm nay trừ đi số ngày i

                    // Chuẩn bị câu lệnh SQL với tham số, thêm status để sau***********************
                    string sql = @"
                    SELECT SUM(PaymentAmount)
                    FROM Orders
                    WHERE PaymentDate = @PaymentDate";

                    // Đặt giá trị cho tham số
                    parameters.Add("@PaymentDate", dateTime.Date, DbType.Date);

                    // Thực hiện truy vấn và lấy kết quả
                    double count = _dapperContext.GetConnection.QueryFirst<double>(sql, parameters);

                    // Tạo đối tượng StatisticDTO và thêm vào danh sách
                    var statistic = new StatisticDTO
                    {
                        Title = dateTime.DayOfWeek.ToString(), // Lấy tên ngày trong tuần
                        Statistic = count // Số lượng đơn hàng cho ngày này
                    };
                    listStatistic.Add(statistic);

                }
            }

            else if (type == TimeType.month)
            {
                // Lặp qua 12 tháng trước đó
                for (int i = 0; i < 12; i++)
                {
                    DateTime dateTime = DateTime.Now.AddMonths(-i); // Lấy tháng trừ đi số tháng i

                    // Chuẩn bị câu lệnh SQL với tham số, thêm status để sau***********************
                    string sql = @"
                    SELECT SUM(PaymentAmount)
                    FROM Orders
                    WHERE MONTH(PaymentDate) = MONTH(@PaymentDate)
                    AND YEAR(PaymentDate) = YEAR(@PaymentDate)";

                    // Đặt giá trị cho tham số
                    parameters.Add("@PaymentDate", dateTime.Date, DbType.Date);

                    // Thực hiện truy vấn và lấy kết quả
                    double count = _dapperContext.GetConnection.QueryFirst<double>(sql, parameters);

                    // Tạo đối tượng StatisticDTO và thêm vào danh sách
                    var statistic = new StatisticDTO
                    {
                        Title = dateTime.ToString("MMM"), // Lấy tên tháng 
                        Statistic = count // Số lượng đơn hàng cho tháng này
                    };
                    listStatistic.Add(statistic);
                }
            }
            else if (type == TimeType.year)
            {

                string sql = @"
                SELECT DISTINCT YEAR(PaymentDate) 
                FROM Orders
                ORDER BY OrderYear ASC";

                // Thực hiện truy vấn và lấy danh sách các năm
                var years = _dapperContext.GetConnection.Query<int>(sql).ToList();

                //Truy xuất các năm
                foreach (var i in years)
                {
                    // Chuẩn bị câu lệnh SQL với tham số, thêm status để sau***********************
                    sql = @"
                    SELECT SUM(PaymentAmount)
                    FROM Orders
                    WHERE YEAR(PaymentDate) = @PaymentDate";

                    // Đặt giá trị cho tham số
                    parameters.Add("@PaymentDate", i, DbType.Int16);

                    // Thực hiện truy vấn và lấy kết quả
                    double count = _dapperContext.GetConnection.QueryFirst<double>(sql, parameters);

                    // Tạo đối tượng StatisticDTO và thêm vào danh sách
                    var statistic = new StatisticDTO
                    {
                        Title = i.ToString(), // Lấy tên năm 
                        Statistic = count // Số lượng đơn hàng cho năm này
                    };
                    listStatistic.Add(statistic);
                }

            }
            return listStatistic;
        }

        //Lấy danh sách yêu cầu hủy đơn
        public async Task<List<OrderDTO>> GetRequestCancellations()
        {
            var parameters = new DynamicParameters();
            var listOrder = new List<OrderDTO>();
            parameters.Add("@Status", OrderType.RequestCancelOrder);

            string sql = @"
                   SELECT *
                    FROM Orders 
                    WHERE Status = @Status";

            // Thực hiện truy vấn và lấy kết quả

            listOrder = (await _dapperContext.GetConnection.QueryAsync<OrderDTO>(sql, parameters)).AsList<OrderDTO>();

            return listOrder;
        }

        #endregion Dapper
    }
}

