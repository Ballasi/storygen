using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen.Elements
{
    class Animation : Sprite
    {
        int FrameCount, FrameDelay;
        LoopType LoopType;

        public Animation(string Path, Origin Origin, double X, double Y, int FrameCount, int FrameDelay, LoopType LoopType) : base(Path, Origin, X, Y)
        {
            this.FrameCount = FrameCount;
            this.FrameDelay = FrameDelay;
            this.LoopType = LoopType;
        }

        public int getFrameCount() => FrameCount;
        public int getFrameDelay() => FrameDelay;
        public LoopType getLoopType() => LoopType;
        public override String getFirstLine() => getOrigin().getName() + ",\"" + getPath() + "\"," + getX() + "," + getY() + "," + getFrameCount() + "," + getFrameDelay() + "," + getLoopType().getName();
    }

    class LoopType
    {
        String Name;

        public LoopType(String Name)
        {
            this.Name = Name;
        }

        public String getName() => Name;
    }
}
