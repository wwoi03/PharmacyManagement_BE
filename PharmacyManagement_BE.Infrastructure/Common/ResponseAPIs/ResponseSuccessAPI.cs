using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs
{
    internal class ResponseSuccessAPI<T> : ResponseAPI<T>
    {
        public ResponseSuccessAPI()
        {
            IsSuccessed = true;
        }

        public ResponseSuccessAPI(int code)
        {
            IsSuccessed = true;
            Code = code;
        }

        public ResponseSuccessAPI(string message)
        {
            IsSuccessed = true;
            Message = message;
        }

        public ResponseSuccessAPI(T obj)
        {
            IsSuccessed = true;
            Obj = obj;
        }

        public ResponseSuccessAPI(int code, string message, T obj)
        {
            IsSuccessed = true;
            Code = code;
            Message = message;
            Obj = obj;
        }
    }
}
