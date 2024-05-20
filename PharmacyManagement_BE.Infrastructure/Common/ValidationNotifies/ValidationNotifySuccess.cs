using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies
{
    public class ValidationNotifySuccess<T> : ValidationNotify<T>
    {
        public ValidationNotifySuccess()
        {
            IsSuccessed = true;
        }

        public ValidationNotifySuccess(string message)
        {
            IsSuccessed = true;
            Message = message;
        }

        public ValidationNotifySuccess(T obj)
        {
            IsSuccessed = true;
            Obj = obj;
        }

        public ValidationNotifySuccess(string message, T obj)
        {
            IsSuccessed = true;
            Message = message;
            Obj = obj;
        }
    }
}
