using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs
{
    public class ResponseAPI<T>
    {
        public bool IsSuccessed { get; set; }
        public int Code { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? Obj { get; set; }
        public ValidationNotify<string> ValidationNotify { get; set; }
    }
}
