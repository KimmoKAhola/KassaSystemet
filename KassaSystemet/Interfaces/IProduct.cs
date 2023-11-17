using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Interfaces
{
    public interface IProduct
    {
        public decimal UnitPrice { get; set; }
        public string PriceType { get; }
    }
}
