using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Types
{
    public enum PriceType
    {
        Under100 = 0, // dưới 100
        From100To300 = 1, 
        From300To500 =2,
        MoreThan500 =3,
    }
}
