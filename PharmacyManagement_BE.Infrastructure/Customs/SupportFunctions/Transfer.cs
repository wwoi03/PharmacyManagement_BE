using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Customs.SupportFunctions
{
    public static class Transfer
    {
        public static string GetDayOfWeekInVietnamese(DateTime date)
        {
            // Tạo từ điển để ánh xạ các ngày trong tuần từ tiếng Anh sang tiếng Việt
            var daysOfWeek = new Dictionary<DayOfWeek, string>
        {
            { DayOfWeek.Monday, "Thứ 2" },
            { DayOfWeek.Tuesday, "Thứ 3" },
            { DayOfWeek.Wednesday, "Thứ 4" },
            { DayOfWeek.Thursday, "Thứ 5" },
            { DayOfWeek.Friday, "Thứ 6" },
            { DayOfWeek.Saturday, "Thứ 7" },
            { DayOfWeek.Sunday, "CN" }
        };

            return daysOfWeek[date.DayOfWeek];
        }
    }
}
