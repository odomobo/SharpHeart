﻿using System;
using System.Collections.Generic;
using System.Text;
using Dragonfly.Engine.CoreTypes;

namespace Dragonfly.Engine.Interfaces
{
    public interface IEvaluator
    {
        short Evaluate(Position position);
    }
}
