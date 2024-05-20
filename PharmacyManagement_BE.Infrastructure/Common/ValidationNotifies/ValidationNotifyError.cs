using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies
{
    public class ValidationNotifyError<T> : ValidationNotify<T>
    {
        public ValidationNotifyError()
        {
            IsSuccessed = false;
        }

        public ValidationNotifyError(string message)
        {
            IsSuccessed = false;
            Message = message;
        }

        public ValidationNotifyError(T obj)
        {
            IsSuccessed = false;
            Obj = obj;
        }

        public ValidationNotifyError(string message, T obj)
        {
            IsSuccessed = false;
            Message = message;
            Obj = obj;
        }
    }
}
