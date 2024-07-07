using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Customs.SupportFunctions
{
    public static class CheckInput
    {
        public static string CheckInputName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            var normalized = System.Text.RegularExpressions.Regex.Replace(input.Trim(), @"\s+", " ");
            return normalized;
        }

        public static string CheckInputCode(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            return input.Trim().ToUpper();
        }
        public static bool IsAlphaNumeric(string input)
        {
            // Loại bỏ khoảng trắng ở đầu và cuối
            input = input.Trim();

            // Kiểm tra xem input chỉ chứa chữ cái và số và không chứa khoảng trắng ở giữa
            return Regex.IsMatch(input, @"^[a-zA-Z0-9]+$");
        }
    }
}
