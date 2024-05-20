using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies
{
    public class ValidationNotify<T>
    {
        public bool IsSuccessed { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Obj { get; set; }
    }
}
