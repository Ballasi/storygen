using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class ControlPoint
    {
        ControlPointTypes Type;
        int Offset;
        double MillisecPerBeat;

        public ControlPoint(ControlPointTypes Type, int Offset, double MillisecPerBeat)
        {
            this.Type = Type;
            this.Offset = Offset;
            this.MillisecPerBeat = MillisecPerBeat;
        }

        public ControlPointTypes getType() => Type;
        public int getOffset() => Offset;
        public double getMillisecPerBeat() => MillisecPerBeat;
    }

    public enum ControlPointTypes
    {
        Timing,
        Inherited
    }
}
