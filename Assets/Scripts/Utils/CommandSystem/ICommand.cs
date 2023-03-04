﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Utils.CommandSystem
{
    public interface ICommand
    {
        bool Execute(Context contest);
    }
}
