using storygen.Other;
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

        public Vector2 getPositionAt(int Time)
        {
            double[] Output;
            int InitialValue = 360;
            double PositionX = InitialValue;

            if (Affectations.Movements != null)
            {
                Output = Check(Time, InitialValue, Affectations.Movements, new int[] { 0, 1, 2, 3, 5 });
                PositionX = Output[0];
            }
            else if (Affectations.MovementsX != null)
            {
                Output = Check(Time, InitialValue, Affectations.MovementsX, new int[] { 0, 1, 2, 3, 4 });
                PositionX = Output[0];
            }
            
            InitialValue = 240;
            double PositionY = InitialValue;

            if (Affectations.Movements != null)
            {
                Output = Check(Time, InitialValue, Affectations.Movements, new int[] { 0, 1, 2, 4, 6 });
                PositionY = Output[0];
            }
            else if (Affectations.MovementsY != null)
            {
                Output = Check(Time, InitialValue, Affectations.MovementsY, new int[] { 0, 1, 2, 3, 4 });
                PositionY = Output[0];
            }

            return new Vector2( PositionX, PositionY );
        }

        public double getYPositionAt(int Time)
        {
            double[] Output;
            int InitialValue = 240;
            double Position = InitialValue;

            if (Affectations.Movements != null)
            {
                Output = Check(Time, InitialValue, Affectations.Movements, new int[] { 0, 1, 2, 4, 6 });
                Position = Output[0];
            }
            else if (Affectations.MovementsY != null)
            {
                Output = Check(Time, InitialValue, Affectations.MovementsY, new int[] { 0, 1, 2, 3, 4 });
                Position = Output[0];
            }

            return Position;
        }

        public double getOpacityAt(int Time)
        {
            double[] Output;
            double InitialValue = 1.0;
            double Position = InitialValue;

            Output = Check(Time, InitialValue, Affectations.Fades, new int[] { 0, 1, 2, 3, 4 });
            Position = Output[0];

            return Position;
        }

        public double getScaleAt(int Time)
        {
            double[] Output;
            double InitialValue = 1.0;
            double Position = InitialValue;

            Output = Check(Time, InitialValue, Affectations.Scales, new int[] { 0, 1, 2, 3, 4 });
            Position = Output[0];

            return Position;
        }

        public double getXScaleAt(int Time)
        {
            double[] Output;
            int InitialValue = 360;
            double Position = InitialValue;

            if (Affectations.Scales != null && Affectations.VecScales == null)
            {
                Output = Check(Time, InitialValue, Affectations.Movements, new int[] { 0, 1, 2, 3, 4 });
                Position = Output[0];
            }
            else if (Affectations.VecScales != null)
            {
                Output = Check(Time, InitialValue, Affectations.VecScales, new int[] { 0, 1, 2, 3, 5 });
                Position = Output[0];
            }

            return Position;
        }

        public double getYScaleAt(int Time)
        {
            double[] Output;
            int InitialValue = 360;
            double Position = InitialValue;

            if (Affectations.Scales != null && Affectations.VecScales == null)
            {
                Output = Check(Time, InitialValue, Affectations.Movements, new int[] { 0, 1, 2, 3, 4 });
                Position = Output[0];
            }
            else if (Affectations.VecScales != null)
            {
                Output = Check(Time, InitialValue, Affectations.VecScales, new int[] { 0, 1, 2, 4, 6 });
                Position = Output[0];
            }

            return Position;
        }

        public double getRotationAt(int Time)
        {
            double[] Output;
            double InitialValue = 0.0;
            double Position = InitialValue;

            Output = Check(Time, InitialValue, Affectations.Rotations, new int[] { 0, 1, 2, 3, 4 });
            Position = Output[0];

            return Position;
        }

        private double[] Check(int Time, double InitialValue, List<String[]> Affectations, int[] ToCheck)
        {
            double Output = InitialValue;
            double At = 0;

            foreach (String[] Affectation in Affectations)
            {
                int EasingID = Int32.Parse(Affectation[ToCheck[0]]);
                int FuncStart = Int32.Parse(Affectation[ToCheck[1]]);

                if (Affectation[3] == "")
                {
                    int FuncPos = Int32.Parse(Affectation[ToCheck[3]]);
                    At = FuncStart;
                    if (FuncStart <= Time) Output = FuncPos;
                }
                else
                {
                    int FuncEnd = Int32.Parse(Affectation[ToCheck[2]]);

                    if (Time > FuncStart && Time <= FuncEnd)
                    {
                        double FuncPosStart = Double.Parse(Affectation[ToCheck[3]]);
                        double FuncPosEnd = Double.Parse(Affectation[ToCheck[4]]);

                        Output = Compare(Time, EasingID, FuncStart, FuncEnd, FuncPosStart, FuncPosEnd);
                    }
                    else if (Time > FuncEnd)
                    {
                        int FuncPos = Int32.Parse(Affectation[ToCheck[4]]);
                        if (FuncStart <= Time) Output = FuncPos;
                    }

                    At = FuncEnd;
                }
            }

            return new double[] { Output, At };
        }

        private double Compare(double Time, double EasingID, double FuncStart, double FuncEnd, double FuncPosStart, double FuncPosEnd)
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

        public void ScaleVec(int Time, double X, double Y)
            { ScaleVec(0, Time, X, Y); }
        public void ScaleVec(int StartTime, int EndTime, double StartX, double StartY, double EndX, double EndY)
            { ScaleVec(0, StartTime, EndTime, StartX, StartY, EndX, EndY); }

        public void Rotate(int Time, double Angle)
            { Rotate(0, Time, Angle); }
        public void Rotate(int StartTime, int EndTime, double StartAngle, double EndAngle)
            { Rotate(0, StartTime, EndTime, StartAngle, EndAngle); }

        public void Color(int Time, double R, double G, double B)
            { Color(0, Time, R, G, B); }
        public void Color(int StartTime, int EndTime, double StartR, double StartG, double StartB, double EndR, double EndG, double EndB)
            { Color(0, StartTime, EndTime, StartR, StartG, StartB, EndR, EndG, EndB); }

        public void HFlip(int StartTime, int EndTime)
            { HFlip(0, StartTime, EndTime); }
        public void VFlip(int StartTime, int EndTime)
            { VFlip(0, StartTime, EndTime); }
        public void Additive(int StartTime, int EndTime)
            { Additive(0, StartTime, EndTime); }

        // Events by Easings
        public void Move(Easing Easing, int StartTime, int EndTime, int StartX, int StartY, int EndX, int EndY)
            { Move(Easing.getID(), StartTime, EndTime, StartX, StartY, EndX, EndY); }
        
        public void MoveX(Easing Easing, int StartTime, int EndTime, int StartX, int EndX)
            { MoveX(Easing.getID(), StartTime, EndTime, StartX, EndX); }
        
        public void MoveY(Easing Easing, int StartTime, int EndTime, int StartY, int EndY)
            { MoveY(Easing.getID(), StartTime, EndTime, StartY, EndY); }
        
        public void Fade(Easing Easing, int StartTime, int EndTime, double StartOpacity, double EndOpacity)
            { Fade(Easing.getID(), StartTime, EndTime, StartOpacity, EndOpacity); }
        
        public void Scale(Easing Easing, int StartTime, int EndTime, double StartRatio, double EndRatio)
            { Scale(Easing.getID(), StartTime, EndTime, StartRatio, EndRatio); }
        
        public void ScaleVec(Easing Easing, int StartTime, int EndTime, double StartX, double StartY, double EndX, double EndY)
            { ScaleVec(Easing.getID(), StartTime, EndTime, StartX, StartY, EndX, EndY); }
        
        public void Rotate(Easing Easing, int StartTime, int EndTime, double StartAngle, double EndAngle)
            { Rotate(Easing.getID(), StartTime, EndTime, StartAngle, EndAngle); }
        
        public void Color(Easing Easing, int StartTime, int EndTime, double StartR, double StartG, double StartB, double EndR, double EndG, double EndB)
            { Color(Easing.getID(), StartTime, EndTime, StartR, StartG, StartB, EndR, EndG, EndB); }

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

        public void ScaleVec(int EasingID, int Time, double X, double Y)
            { Affectations.AddVecScaling(new String[] { EasingID.ToString(), Time.ToString(), "", X.ToString(), Y.ToString() }); }
        public void ScaleVec(int EasingID, int StartTime, int EndTime, double StartX, double StartY, double EndX, double EndY)
            { Affectations.AddVecScaling(new String[] { EasingID.ToString(), StartTime.ToString(), EndTime.ToString(), StartX.ToString(), StartY.ToString(), EndX.ToString(), EndY.ToString() }); }

        public void Rotate(int EasingID, int Time, double Angle)
            { Affectations.AddRotation(new String[] { EasingID.ToString(), Time.ToString(), "", Angle.ToString() }); }
        public void Rotate(int EasingID, int StartTime, int EndTime, double StartAngle, double EndAngle)
            { Affectations.AddRotation(new String[] { EasingID.ToString(), StartTime.ToString(), EndTime.ToString(), StartAngle.ToString(), EndAngle.ToString() }); }

        public void Color(int EasingID, int Time, double R, double G, double B)
            { Affectations.AddColor(new String[] { EasingID.ToString(), Time.ToString(), "", ((int) (R * 255)).ToString(), ((int)(G * 255)).ToString(), ((int)(B * 255)).ToString() }); }
        public void Color(int EasingID, int StartTime, int EndTime, double StartR, double StartG, double StartB, double EndR, double EndG, double EndB)
            { Affectations.AddColor(new String[] { EasingID.ToString(), StartTime.ToString(), EndTime.ToString(), ((int)(StartR * 255)).ToString(), ((int)(StartG * 255)).ToString(), ((int)(StartB * 255)).ToString(), ((int)(EndR * 255)).ToString(), ((int)(EndG * 255)).ToString(), ((int)(EndB * 255)).ToString() }); }

        public void HFlip(int EasingID, int StartTime, int EndTime)
            { Affectations.AddHFlip(new String[] { EasingID.ToString(), StartTime.ToString(), EndTime.ToString(), "H" }); }
        public void VFlip(int EasingID, int StartTime, int EndTime)
            { Affectations.AddHFlip(new String[] { EasingID.ToString(), StartTime.ToString(), EndTime.ToString(), "V" }); }
        public void Additive(int EasingID, int StartTime, int EndTime)
            { Affectations.AddHFlip(new String[] { EasingID.ToString(), StartTime.ToString(), EndTime.ToString(), "A" }); }
    }
}
