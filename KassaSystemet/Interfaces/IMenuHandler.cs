﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Interfaces
{
    public interface IMenuHandler<TEnum>
    {
        void HandleMenuOption(TEnum menuOption);
    }
}
