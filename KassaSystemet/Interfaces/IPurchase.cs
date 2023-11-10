using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Interfaces
{
    public interface IPurchase
    {
        public int ProductID { get; }
        public decimal Amount { get; }
    }
}
