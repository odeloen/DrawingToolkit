using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;
using DrawingToolkitv01.Interfaces;

namespace DrawingToolkitv01.DrawingObjectClasses
{
    class LineConnector : Line, ILocationMonitor
    {
        IDrawingObject _a;
        IDrawingObject _b;

        public IDrawingObject A
        {
            get { return this._a; }
            set
            {
                this._a = value;
                this.start.X = (value.Start.X + value.End.X) / 2;
                this.start.Y = (value.Start.Y + value.End.Y) / 2;                
                value.LocationChanged += LocationHasChanged;
            }
        }
        public IDrawingObject B
        {
            get { return this._b; }
            set
            {
                this._b = value;
                this.end.X = (value.Start.X + value.End.X) / 2;
                this.end.Y = (value.Start.Y + value.End.Y) / 2;
                value.LocationChanged += LocationHasChanged;
            }
        }

        public LineConnector()
        {
            
        }        

        void LocationHasChanged(object sender, EventArgs e)
        {
            this.start.X = (_a.Start.X + _a.End.X) / 2;
            this.start.Y = (_a.Start.Y + _a.End.Y) / 2;
            this.end.X = (_b.Start.X + _b.End.X) / 2;
            this.end.Y = (_b.Start.Y + _b.End.Y) / 2;
        }

        double GetSlope(Point locA, Point locB)
        {
            double m = (double)(locB.Y - locA.Y) / (double)(locB.X - locA.X);
            return m;
        }
    }
}
