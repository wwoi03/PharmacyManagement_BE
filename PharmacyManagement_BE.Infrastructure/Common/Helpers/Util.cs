using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.Helpers
{
    public static class Util
    {
        // Phương thức chuyển đổi chuỗi thành camelCase
        public static string ConvertToCamelCase(string str)
        {
            if (string.IsNullOrEmpty(str) || char.IsLower(str[0]))
            {
                return str;
            }

            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        // Phương thức định dạng ngày tháng
        public static string FormatDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}
