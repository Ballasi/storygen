using System;

namespace storygen
{
    class ControlPoint
    {
        ControlPointTypes Type;
        int Offset;
        double MillisecPerBeat;
        double BPM;

        public ControlPoint(ControlPointTypes Type, int Offset, double MillisecPerBeat, double LastBPM)
        {
            this.Type = Type;
            this.Offset = Offset;
            this.MillisecPerBeat = MillisecPerBeat;
            if (Type == ControlPointTypes.Timing)
                BPM = 60000 / MillisecPerBeat;
            else
                BPM = LastBPM;
        }

        public ControlPointTypes getType() => Type;
        public int getOffset() => Offset;
        public double getMillisecPerBeat() => MillisecPerBeat;
        public double getBPM() => Math.Round(BPM, 3);
        public double getSVMuliplier() => Type == ControlPointTypes.Timing ? 1.0 : Math.Round(-100 / MillisecPerBeat, 2);
    }

    public enum ControlPointTypes
    {
        Timing,
        Inherited
    }
}
