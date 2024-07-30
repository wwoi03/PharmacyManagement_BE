using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Domain.Types;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.OrderDTOs;
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
        private readonly IMapper _mapper;

        public OrderService(PharmacyManagementContext context, PMDapperContext dapperContext, IMapper mapper) : base(context)
        {
            this._context = context;
            this._dapperContext = dapperContext;
            this._mapper = mapper;
        }

        public async Task<List<Order>> GetAllOrderByStaffId(Guid id)
        {
            return _context.Orders.Where(x => x.StaffId == id).ToList();
        }

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
                ORDER BY YEAR(OrderDate) ASC";

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
                SELECT DISTINCT YEAR(OrderDate) 
                FROM Orders
                ORDER BY YEAR(OrderDate) ASC";

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
            List<OrderDetailsDTO> orderDetails =  _mapper.Map<List<OrderDetailsDTO>>(await _context.OrderDetails
                .Include(r => r.ShipmentDetails)
                    .ThenInclude(s => s.Product)
                .Where(x => x.OrderId == Id).ToListAsync());

            //Lấy thông tin order
            OrderDTO order = _mapper.Map<OrderDTO>(await _context.Orders
                .Include(r => r.PaymentMethod)
                .FirstOrDefaultAsync(x => x.Id == Id && x.BranchId == BranchId));

            //Gán OrderDetails
            order.OrderDetails = orderDetails;

            //Trả về kết quả
            return order;
        }

        //Lấy danh sách Order dựa trên branch và status
        public async Task<List<OrderDTO>> GetOrdersByBranch(Guid BranchId,OrderType type)
        {

            List<Order> listOrder = new List<Order>();

            //Lấy toàn bộ danh sách
            if(type == OrderType.GetAll)
            {
                listOrder = await _context.Orders.Where(r => r.BranchId == BranchId)
                    .Include(r=> r.Customer).Include(r=> r.PaymentMethod)
                    .ToListAsync();
            }
            else
            {
                listOrder = await _context.Orders.Where(r => r.BranchId == BranchId && r.Status == type.ToString())
                    .Include(r => r.Customer).Include(r => r.PaymentMethod)
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

