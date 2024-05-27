using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs
{
    public class ResponseErrorAPI<T> : ResponseAPI<T>
    {
        public ResponseErrorAPI()
        {
            IsSuccessed = false;
        }

        public ResponseErrorAPI(int code)
        {
            IsSuccessed = true;
            Code = code;
        }

        public ResponseErrorAPI(string message)
        {
            IsSuccessed = false;
            Message = message;
        }

        public ResponseErrorAPI(T obj)
        {
            IsSuccessed = false;
            Obj = obj;
        }

        public ResponseErrorAPI(int code, string message)
        {
            IsSuccessed = false;
            Code = code;
            Message = message;
        }

        public ResponseErrorAPI(int code, T obj)
        {
            IsSuccessed = false;
            Code = code;
            Obj = obj;
        }

        public ResponseErrorAPI(int code, string message, T obj)
        {
            IsSuccessed = false;
            Code = code;    
            Message = message;
            Obj = obj;
        }
    }
}
