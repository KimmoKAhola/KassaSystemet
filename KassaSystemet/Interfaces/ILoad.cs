﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KassaSystemet.Models;

namespace KassaSystemet.Interfaces
{
    public interface ILoad
    {
        Dictionary<int, Product> LoadProductList();
        void LoadDiscountList();
        string LoadInfoMenu();
    }
}