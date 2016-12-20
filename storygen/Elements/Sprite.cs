using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace storygen
{
    class Sprite
    {
        String Path;
        Origin Origin;
        Affectations Affectations;

        public Sprite(String Path, Origin Origin)
        {
            this.Path = Path;
            this.Origin = Origin;
            Affectations = new Affectations();
        }

        public String getPath()
        {
            return Path;
        }

        public Origin getOrigin()
        {
            return Origin;
        }

        public String getAffections()
        {
            return Affectations.Output();
        }

        public void BeginLoop(int Time, int Count)
        {
            Affectations.StartLoop(new String[] { Time.ToString(), Count.ToString() });
        }

        public void EndLoop()
        {
            Affectations.EndLoop();
        }

        public double getXPositionAt(int Time)
        {
            double Position = 360;

            foreach (String[] Movement in Affectations.Movements)
            {
                int EasingID = Int32.Parse(Movement[0]);
                int FuncStart = Int32.Parse(Movement[1]);

                if (Movement.Length == 5)
                {
                    int FuncPos = Int32.Parse(Movement[3]);
                    if (FuncStart <= Time) Position = FuncPos;
                }
                else
                {
                    int FuncEnd = Int32.Parse(Movement[2]);
                    if (Time > FuncStart && Time < FuncEnd)
                    {
                        int FuncPosStart = Int32.Parse(Movement[3]);
                        int FuncPosEnd = Int32.Parse(Movement[5]);

                        Position = Compare(Time, EasingID, FuncStart, FuncEnd, FuncPosStart, FuncPosEnd);
                    }
                    else if (Time > FuncEnd)
                    {
                        int FuncPos = Int32.Parse(Movement[5]);
                        if (FuncStart <= Time) Position = FuncPos;
                    }
                }
            }

            return Position;
        }

        public double getYPositionAt(int Time)
        {
            double Position = 360;

            foreach (String[] Movement in Affectations.Movements)
            {
                int EasingID = Int32.Parse(Movement[0]);
                int FuncStart = Int32.Parse(Movement[1]);

                if (Movement.Length == 5)
                {
                    int FuncPos = Int32.Parse(Movement[4]);
                    if (FuncStart <= Time) Position = FuncPos;
                }
                else
                {
                    int FuncEnd = Int32.Parse(Movement[2]);
                    if (Time > FuncStart && Time < FuncEnd)
                    {
                        int FuncPosStart = Int32.Parse(Movement[4]);
                        int FuncPosEnd = Int32.Parse(Movement[6]);

                        Position = Compare(Time, EasingID, FuncStart, FuncEnd, FuncPosStart, FuncPosEnd);
                    }
                    else if (Time > FuncEnd)
                    {
                        int FuncPos = Int32.Parse(Movement[6]);
                        if (FuncStart <= Time) Position = FuncPos;
                    }
                }
            }

            return Position;
        }

        public double Compare(double Time, double EasingID, double FuncStart, double FuncEnd, double FuncPosStart, double FuncPosEnd)
        {
            if (EasingID < 3)
            {
                double startVelocityMultipler = 0, endVelocityMultipler = 0;
                if (EasingID == 0)
                {
                    startVelocityMultipler = 1;
                    endVelocityMultipler = 1;
                }
                if (EasingID == 1)
                {
                    startVelocityMultipler = 2;
                    endVelocityMultipler = 0;
                }
                if (EasingID == 2)
                {
                    startVelocityMultipler = 0;
                    endVelocityMultipler = 2;
                }
                double shift = FuncPosEnd - FuncPosStart;
                double time = FuncEnd - FuncStart;
                double startV = (shift / time) * startVelocityMultipler;
                double endV = (shift / time) * endVelocityMultipler;
                double elapsedTime = Time - FuncStart;
                double acceleration = (endV - startV) / (FuncEnd - FuncStart);
                return FuncPosStart + startV * elapsedTime + acceleration * Math.Pow(elapsedTime, 2) / 2;
            }
            if (EasingID == 12)
            {
                Time -= FuncStart;
                double duration = FuncEnd - FuncStart;
                double change = FuncPosEnd - FuncPosStart;
                Time /= duration;
                return change * Math.Pow(Time, 5) + FuncPosStart;
            }
            else
                return 0;
        }

        // Linear Events
        public void Move(int Time, int X, int Y)
            { Move(0, Time, X, Y); }
        public void Move(int StartTime, int EndTime, int StartX, int StartY, int EndX, int EndY)
            { Move(0, StartTime, EndTime, StartX, StartY, EndX, EndY); }

        public void MoveX(int Time, int X)
            { MoveX(0, Time, X); }
        public void MoveX(int StartTime, int EndTime, int StartX, int EndX)
            { MoveX(0, StartTime, EndTime, StartX, EndX); }

        public void MoveY(int Time, int Y)
            { MoveY(0, Time, Y); }
        public void MoveY(int StartTime, int EndTime, int StartY, int EndY)
            { MoveY(0, StartTime, EndTime, StartY, EndY); }

        public void Fade(int Time, double Opacity)
            { Fade(0, Time, Opacity); }
        public void Fade(int StartTime, int EndTime, double StartOpacity, double EndOpacity)
            { Fade(0, StartTime, EndTime, StartOpacity, EndOpacity); }

        public void Scale(int Time, double Ratio)
            { Scale(0, Time, Ratio); }
        public void Scale(int StartTime, int EndTime, double StartRatio, double EndRatio)
            { Scale(0, StartTime, EndTime, StartRatio, EndRatio); }

        public void Rotate(int Time, double Angle)
            { Rotate(0, Time, Angle); }
        public void Rotate(int StartTime, int EndTime, double StartAngle, double EndAngle)
            { Rotate(0, StartTime, EndTime, StartAngle, EndAngle); }

        // Events by Easings
        public void Move(Easing Easing, int Time, int X, int Y)
            { Move(Easing.getID(), Time, X, Y); }
        public void Move(Easing Easing, int StartTime, int EndTime, int StartX, int StartY, int EndX, int EndY)
            { Move(Easing.getID(), StartTime, EndTime, StartX, StartY, EndX, EndY); }

        public void MoveX(Easing Easing, int Time, int X)
            { MoveX(Easing.getID(), Time, X); }
        public void MoveX(Easing Easing, int StartTime, int EndTime, int StartX, int EndX)
            { MoveX(Easing.getID(), StartTime, EndTime, StartX, EndX); }

        public void MoveY(Easing Easing, int Time, int Y)
            { MoveY(Easing.getID(), Time, Y); }
        public void MoveY(Easing Easing, int StartTime, int EndTime, int StartY, int EndY)
            { MoveY(Easing.getID(), StartTime, EndTime, StartY, EndY); }

        public void Fade(Easing Easing, int Time, double Opacity)
            { Fade(Easing.getID(), Time, Opacity); }
        public void Fade(Easing Easing, int StartTime, int EndTime, double StartOpacity, double EndOpacity)
            { Fade(Easing.getID(), StartTime, EndTime, StartOpacity, EndOpacity); }

        public void Scale(Easing Easing, int Time, double Ratio)
            { Scale(Easing.getID(), Time, Ratio); }
        public void Scale(Easing Easing, int StartTime, int EndTime, double StartRatio, double EndRatio)
            { Scale(Easing.getID(), StartTime, EndTime, StartRatio, EndRatio); }

        public void Rotate(Easing Easing, int Time, double Angle)
            { Rotate(Easing.getID(), Time, Angle); }
        public void Rotate(Easing Easing, int StartTime, int EndTime, double StartAngle, double EndAngle)
            { Rotate(Easing.getID(), StartTime, EndTime, StartAngle, EndAngle); }

        // Events by Easing IDs
        public void Move(int EasingID, int Time, int X, int Y)
            { Affectations.AddMovement(new String[] { EasingID.ToString(), Time.ToString(), "", X.ToString(), Y.ToString() }); }
        public void Move(int EasingID, int StartTime, int EndTime, int StartX, int StartY, int EndX, int EndY)
            { Affectations.AddMovement(new String[] { EasingID.ToString(), StartTime.ToString(), EndTime.ToString(), StartX.ToString(), StartY.ToString(), EndX.ToString(), EndY.ToString() });  }

        public void MoveX(int EasingID, int Time, int X)
            { Affectations.AddMovementX(new String[] { EasingID.ToString(), Time.ToString(), "", X.ToString() }); }
        public void MoveX(int EasingID, int StartTime, int EndTime, int StartX, int EndX)
            { Affectations.AddMovementX(new String[] { EasingID.ToString(), StartTime.ToString(), EndTime.ToString(), StartX.ToString(), EndX.ToString() }); }

        public void MoveY(int EasingID, int Time, int Y)
            { Affectations.AddMovementY(new String[] { EasingID.ToString(), Time.ToString(), "", Y.ToString() }); }
        public void MoveY(int EasingID, int StartTime, int EndTime, int StartY, int EndY)
            { Affectations.AddMovementY(new String[] { EasingID.ToString(), StartTime.ToString(), EndTime.ToString(), StartY.ToString(), EndY.ToString() }); }

        public void Fade(int EasingID, int Time, double Opacity)
            { Affectations.AddFading(new String[] { EasingID.ToString(), Time.ToString(), "", Opacity.ToString() }); }
        public void Fade(int EasingID, int StartTime, int EndTime, double StartOpacity, double EndOpacity)
            { Affectations.AddFading(new String[] { EasingID.ToString(), StartTime.ToString(), EndTime.ToString(), StartOpacity.ToString(), EndOpacity.ToString() }); }

        public void Scale(int EasingID, int Time, double Ratio)
            { Affectations.AddScaling(new String[] { EasingID.ToString(), Time.ToString(), "", Ratio.ToString() }); }
        public void Scale(int EasingID, int StartTime, int EndTime, double StartRatio, double EndRatio)
            { Affectations.AddScaling(new String[] { EasingID.ToString(), StartTime.ToString(), EndTime.ToString(), StartRatio.ToString(), EndRatio.ToString() }); }

        public void Rotate(int EasingID, int Time, double Angle)
            { Affectations.AddRotation(new String[] { EasingID.ToString(), Time.ToString(), "", Angle.ToString() }); }
        public void Rotate(int EasingID, int StartTime, int EndTime, double StartAngle, double EndAngle)
            { Affectations.AddRotation(new String[] { EasingID.ToString(), StartTime.ToString(), EndTime.ToString(), StartAngle.ToString(), EndAngle.ToString() }); }
    }
}
