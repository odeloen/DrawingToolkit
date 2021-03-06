﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkitv01.Interfaces
{
    interface IToolbox
    {
        void AddTool(ITool tool);
        void RemoveTool(ITool tool);
        void SetCanvas(ICanvas canvas);
    }
}
