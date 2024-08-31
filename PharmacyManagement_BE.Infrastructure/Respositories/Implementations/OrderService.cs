using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Domain.Types;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.OrderDTOs;
﻿using Dapper;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Domain.Types;
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
using PharmacyManagement_BE.Infrastructure.Customs.SupportFunctions;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Implementations
{
    public class OrderService : RepositoryService<Order>, IOrderService
    {
        private readonly PharmacyManagementContext _context;
        private readonly PMDapperContext _dapperContext;
        private readonly IMapper _mapper;

        public OrderService(PharmacyManagementContext context, PMDapperContext dapperContext, IMapper mapper) : base(context)
        {
            this._context = context;
            this._dapperContext = dapperContext;
            this._mapper = mapper;
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
        public async Task<List<StatisticOrderDTO>> StatisticOrder(TimeType type, DateTime dateTime)
        {
            var listStatistic = new List<StatisticOrderDTO>();


            if (type == TimeType.week)
            {
                // Lặp qua 7 ngày trước đó + 1 ngày gốc làm thống kê = 8
                for (int i = 0; i < 8; i++)
                {
                    DateTime date = dateTime.AddDays(-i); // Lấy ngày từ inputDateTime trừ đi số ngày i

                    // Đặt giá trị cho tham số
                    var parameters = new DynamicParameters();

                    // Đặt giá trị cho tham số Status để đếm đơn hàng
                    parameters.Add("@Status", null, DbType.String);
                    parameters.Add("@PaymentStatus", null, DbType.String);

                    parameters.Add("@OrderDate", date.Date, DbType.Date);

                    // Danh sách tất cả order Order 
                    string sql = @"
                        SELECT COUNT(*)
                        FROM Orders
                        WHERE OrderDate = @OrderDate
                        AND (@Status IS NULL OR Status = @Status)
                        AND (@PaymentStatus IS NULL OR PaymentStatus = @PaymentStatus)";

                    // Thực hiện truy vấn và lấy kết quả tổng đơn hàng
                    int order = _dapperContext.GetConnection.QueryFirst<int>(sql, parameters);

                    // Đặt giá trị cho tham số Status để lấy số liệu đơn hủy
                    parameters.Add("@Status", OrderType.CancellationOrderApproved.ToString(), DbType.String);
                    int cancellation = _dapperContext.GetConnection.QueryFirst<int>(sql, parameters);

                    // Tạo truy vấn mới để kiểm tra payment
                    parameters = new DynamicParameters();
                    parameters.Add("@Status", null, DbType.String);
                    parameters.Add("@OrderDate", date.Date, DbType.Date);
                    parameters.Add("@PaymentStatus", PaymentStatus.PaymentPaid.ToString(), DbType.String);
                    int payment = _dapperContext.GetConnection.QueryFirst<int>(sql, parameters);

                    // Tạo đối tượng StatisticDTO và thêm vào danh sách
                    var statistic = new StatisticOrderDTO
                    {
                        Title = Transfer.GetDayOfWeekInVietnamese(date), // Lấy tên ngày trong tuần
                        Order = order, // Số lượng đơn hàng cho ngày này
                        Cancellation = cancellation, // Số lượng đơn hủy cho ngày này
                        Payment = payment
                    };
                    listStatistic.Add(statistic);
                }
            }
            else if (type == TimeType.month)
            {
                // Lặp qua 12 tháng trước đó
                for (int i = 0; i < 13; i++)
                {
                    DateTime date = dateTime.AddMonths(-i); // Lấy tháng từ inputDateTime trừ đi số tháng i

                    // Đặt giá trị cho tham số
                    var parameters = new DynamicParameters();

                    // Đặt giá trị cho tham số Status để đếm đơn hàng
                    parameters.Add("@Status", null, DbType.String);
                    parameters.Add("@PaymentStatus", null, DbType.String);

                    parameters.Add("@OrderDate", date.Date, DbType.Date);

                    // Chuẩn bị câu lệnh SQL với tham số, thêm status để sau
                    string sql = @"
                        SELECT COUNT(*)
                        FROM Orders
                        WHERE MONTH(OrderDate) = MONTH(@OrderDate)
                        AND YEAR(OrderDate) = YEAR(@OrderDate)
                        AND (@Status IS NULL OR Status = @Status)
                        AND (@PaymentStatus IS NULL OR PaymentStatus = @PaymentStatus)";

                    // Thực hiện truy vấn và lấy kết quả tổng đơn hàng
                    int order = _dapperContext.GetConnection.QueryFirst<int>(sql, parameters);

                    // Đặt giá trị cho tham số Status để lấy số liệu đơn hủy
                    parameters.Add("@Status", OrderType.CancellationOrderApproved.ToString(), DbType.String);
                    int cancellation = _dapperContext.GetConnection.QueryFirst<int>(sql, parameters);

                    // Tạo truy vấn mới để kiểm tra payment
                    parameters = new DynamicParameters();
                    parameters.Add("@Status", null, DbType.String);
                    parameters.Add("@OrderDate", date.Date, DbType.Date);
                    parameters.Add("@PaymentStatus", PaymentStatus.PaymentPaid.ToString(), DbType.String);
                    int payment = _dapperContext.GetConnection.QueryFirst<int>(sql, parameters);

                    // Tạo đối tượng StatisticDTO và thêm vào danh sách
                    var statistic = new StatisticOrderDTO
                    {
                        Title = "Tháng " + date.Month, // Lấy tên tháng 
                        Order = order, // Số lượng đơn hàng cho tháng này
                        Cancellation = cancellation, // Số lượng đơn hủy cho tháng này
                        Payment = payment // Số lượng đơn hàng đã thanh toán cho tháng này
                    };
                    listStatistic.Add(statistic);
                }
            }
            else if (type == TimeType.year)
            {
                string sql = @"
                    SELECT DISTINCT YEAR(OrderDate) 
                    FROM Orders
                    ORDER BY YEAR(OrderDate) ASC";

                // Thực hiện truy vấn và lấy danh sách các năm
                var years = _dapperContext.GetConnection.Query<int>(sql).ToList();

                // Truy xuất các năm
                foreach (var year in years)
                {
                    // Đặt giá trị cho tham số
                    var parameters = new DynamicParameters();

                    // Đặt giá trị cho tham số Status để đếm đơn hàng
                    parameters.Add("@Status", null, DbType.String);
                    parameters.Add("@PaymentStatus", null, DbType.String);

                    parameters.Add("@OrderDate", year, DbType.Int32);

                    // Chuẩn bị câu lệnh SQL với tham số, thêm status để sau
                    sql = @"
                        SELECT COUNT(*)
                        FROM Orders
                        WHERE YEAR(OrderDate) = @OrderDate
                        AND (@Status IS NULL OR Status = @Status)
                        AND (@PaymentStatus IS NULL OR PaymentStatus = @PaymentStatus)";

                    // Thực hiện truy vấn và lấy kết quả tổng đơn hàng
                    int order = _dapperContext.GetConnection.QueryFirst<int>(sql, parameters);

                    // Đặt giá trị cho tham số Status để lấy số liệu đơn hủy
                    parameters.Add("@Status", OrderType.CancellationOrderApproved.ToString(), DbType.String);
                    int cancellation = _dapperContext.GetConnection.QueryFirst<int>(sql, parameters);

                    // Tạo truy vấn mới để kiểm tra payment
                    parameters = new DynamicParameters();
                    parameters.Add("@Status", null, DbType.String);
                    parameters.Add("@OrderDate", year, DbType.Int32);
                    parameters.Add("@PaymentStatus", PaymentStatus.PaymentPaid.ToString(), DbType.String);
                    int payment = _dapperContext.GetConnection.QueryFirst<int>(sql, parameters);

                    // Tạo đối tượng StatisticDTO và thêm vào danh sách
                    var statistic = new StatisticOrderDTO
                    {
                        Title = year.ToString(), // Lấy tên năm 
                        Order = order, // Số lượng đơn hàng cho năm này
                        Cancellation = cancellation, // Số lượng đơn hủy cho năm này
                        Payment = payment // Số lượng đơn hàng đã thanh toán cho năm này
                    };
                    listStatistic.Add(statistic);
                }
            }
            return listStatistic;
        }

        // Thống kê doanh thu
        public async Task<List<StatisticRevenueDTO>> StatisticRevenue(TimeType type, DateTime dateTime)
        {
            var listStatistic = new List<StatisticRevenueDTO>();

            if (type == TimeType.week)
            {
                // Lặp qua 7 ngày trước đó
                for (int i = 0; i < 8; i++)
                {
                    DateTime date = dateTime.AddDays(-i); // Lấy ngày từ inputDateTime trừ đi số ngày i

                    // Đặt giá trị cho tham số
                    var parameters = new DynamicParameters();
                    parameters.Add("@PaymentDate", date.Date, DbType.Date);

                    // Chuẩn bị câu lệnh SQL với tham số
                    string sql = @"SELECT COALESCE(SUM(PaymentAmount), 0)
                        FROM Orders
                        WHERE (PaymentDate IS NOT NULL AND CONVERT(DATE, PaymentDate) = CONVERT(DATE, @PaymentDate))
                       OR PaymentDate IS NULL";

                    // Thực hiện truy vấn và lấy kết quả
                    double revenue = _dapperContext.GetConnection.QueryFirstOrDefault<double>(sql, parameters);

                    // Tạo đối tượng StatisticDTO và thêm vào danh sách
                    var statistic = new StatisticRevenueDTO
                    {
                        Title = Transfer.GetDayOfWeekInVietnamese(date), // Lấy tên ngày trong tuần
                        Statistic = revenue // Doanh thu cho ngày này
                    };
                    listStatistic.Add(statistic);
                }
            }
            else if (type == TimeType.month)
            {
                // Lặp qua 12 tháng trước đó
                for (int i = 0; i < 13; i++)
                {
                    DateTime date = dateTime.AddMonths(-i); // Lấy tháng từ inputDateTime trừ đi số tháng i

                    // Đặt giá trị cho tham số
                    var parameters = new DynamicParameters();
                    parameters.Add("@PaymentDate", date.Date, DbType.Date);

                    // Chuẩn bị câu lệnh SQL với tham số
                    string sql = @"
                        SELECT COALESCE(SUM(PaymentAmount), 0)
                        FROM Orders
                        WHERE  (PaymentDate IS NOT NULL 
                         AND MONTH(PaymentDate) = MONTH(CONVERT(DATE, @PaymentDate))
                         AND YEAR(PaymentDate) = YEAR(CONVERT(DATE, @PaymentDate)))
                         OR PaymentDate IS NULL ";

                    // Thực hiện truy vấn và lấy kết quả
                    double revenue = _dapperContext.GetConnection.QueryFirstOrDefault<double>(sql, parameters);

                    // Tạo đối tượng StatisticDTO và thêm vào danh sách
                    var statistic = new StatisticRevenueDTO
                    {
                        Title = "Tháng " + date.Month, // Lấy tên tháng
                        Statistic = revenue // Doanh thu cho tháng này
                    };
                    listStatistic.Add(statistic);
                }
            }
            else if (type == TimeType.year)
            {
                string sql = @"
                    SELECT DISTINCT YEAR(PaymentDate) 
                    FROM Orders
                    ORDER BY YEAR(PaymentDate) ASC";

                // Thực hiện truy vấn và lấy danh sách các năm
                var years = _dapperContext.GetConnection.Query<int>(sql).ToList();

                // Truy xuất các năm
                foreach (var year in years)
                {
                    // Đặt giá trị cho tham số
                    var parameters = new DynamicParameters();
                    parameters.Add("@PaymentDate", year, DbType.Int32);

                    // Chuẩn bị câu lệnh SQL với tham số
                    sql = @"
                       SELECT COALESCE(SUM(PaymentAmount), 0)
                        FROM Orders
                        WHERE PaymentDate IS NULL OR YEAR(PaymentDate) = @PaymentDate;";

                    // Thực hiện truy vấn và lấy kết quả
                    double revenue = _dapperContext.GetConnection.QueryFirstOrDefault<double>(sql, parameters);

                    // Tạo đối tượng StatisticDTO và thêm vào danh sách
                    var statistic = new StatisticRevenueDTO
                    {
                        Title = year.ToString(), // Lấy tên năm
                        Statistic = revenue // Doanh thu cho năm này
                    };
                    listStatistic.Add(statistic);
                }
            }
            return listStatistic;
        }


        //Lấy danh sách yêu cầu hủy đơn
        public async Task<List<OrderDTO>> GetCanceledOrder()
        {
            try
            {
                var parameters = new DynamicParameters();
                var listOrder = new List<Order>();
                parameters.Add("@Status", OrderType.CancellationOrderApproved.ToString());

                string sql = @"
                   SELECT *
                    FROM Orders 
                    WHERE Status = @Status";

                // Thực hiện truy vấn và lấy kết quả

                listOrder = (await _dapperContext.GetConnection.QueryAsync<Order>(sql, parameters)).ToList();

                return _mapper.Map<List<OrderDTO>>(listOrder);
            }catch(Exception ex)
            {
                return new List<OrderDTO>();
            }
        }

        //Thống kê theo ngày thời gian thực
        public async Task<GeneralStatisticsDTO> RealTimeStatistc()
        {
            //Đếm order
            string sql = @"SELECT count (*) AS NumOrder
            FROM Orders
            WHERE CONVERT(date, CreatedTime) = CONVERT(date, GETDATE())";

            int order = (await _dapperContext.GetConnection.QueryFirstAsync<int>(sql));

            sql = @"SELECT ISNULL(SUM(PaymentAmount), 0) AS SalePrice
            FROM Orders
            WHERE CONVERT(date, PaymentDate) = CONVERT(date, GETDATE())";

            decimal revenue = (await _dapperContext.GetConnection.QueryFirstAsync<decimal>(sql));

            GeneralStatisticsDTO genStatistic = new GeneralStatisticsDTO()
            {
                NumOrder = order,
                SalePrice = revenue,
            };

            return genStatistic;
        }

        #endregion Dapper

        //Lấy chi tiết đơn hàng dựa vào branch và id order
        public async Task<OrderDTO> GetOrderByBranch(Guid Id, Guid BranchId)
        {
            //Lấy danh sách chi tiết đơn hàng
            List<OrderDetailsDTO> orderDetails = _mapper.Map<List<OrderDetailsDTO>>(await _context.OrderDetails
                .Include(r => r.ShipmentDetails)
                    .ThenInclude(s => s.Product)
                .Where(x => x.OrderId == Id).ToListAsync());

            //Lấy thông tin order
            OrderDTO order = _mapper.Map<OrderDTO>(await _context.Orders
                .Include(r => r.PaymentMethod)
                .FirstOrDefaultAsync(x => x.Id == Id));

            //Gán OrderDetails
            order.OrderDetails = orderDetails;

            //Trả về kết quả
            return order;
        }

        //Lấy danh sách Order dựa trên branch và status
        public async Task<List<OrderDTO>> GetOrdersByBranch(Guid BranchId, OrderType type)
        {

            List<Order> listOrder = new List<Order>();

            //Lấy toàn bộ danh sách
            if (type == OrderType.GetAll)
            {
                listOrder = await _context.Orders.Include(r => r.Customer).Include(r => r.PaymentMethod)
                    .OrderByDescending(x => x.OrderDate)
                    .ToListAsync();
            }
            else
            {
                listOrder = await _context.Orders.Where(r => r.Status == type.ToString())
                    .Include(r => r.Customer).Include(r => r.PaymentMethod)
                    .OrderByDescending(x => x.OrderDate)
                    .ToListAsync();
            }
            return _mapper.Map<List<OrderDTO>>(listOrder);
        }

        public bool CheckUpdateStatus(Order order, OrderType status)
        {
            OrderType currentStatus = (OrderType)Enum.Parse(typeof(OrderType), order.Status);

            //Kiểm tra trạng thái của đơn hàng
            //Trạng thái không lấy getall
            if ((int)status == -1)
                return false;
            //Trạng thái không được phép quay ngược
            if ((int)currentStatus >= (int)status)
                return false;
            //Khách hàng yêu cầu hủy thì Cửa hàng không được yêu cầu hủy
            if (((int)currentStatus == 4 || (int)currentStatus == 5) && (int)status == 6 )
                return false;
            //Tình trạng đơn hàng không được nhảy bước
            if ((int)currentStatus + 1 != (int)status && (int)status < 4)
                return false;
            //Đã giao không thể hủy hàng
            if ((int)currentStatus == 3)
                return false;
            return true;
        }
    }
}

