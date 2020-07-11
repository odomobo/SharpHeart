﻿using System;
using System.Collections.Generic;
using System.Text;
using Dragonfly.Engine.CoreTypes;

namespace Dragonfly.Engine.Interfaces
{
    public interface IQSearch
    {
        // sets the objects needed for this search
        // TODO: better name?
        void StartSearch(ITimeStrategy timeStrategy, Statistics statistics);

        Score Search(Position position, int ply);
    }
}