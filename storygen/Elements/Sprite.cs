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
        bool Hidden;

        public Sprite()
        {
        }

        public Sprite(String Path, Origin Origin)
        {
            this.Path = Path;
            this.Origin = Origin;
            Hidden = false;

            Affectations = new Affectations();
        }

        public String getPath() => Path;
        public Origin getOrigin() => Origin;
        public String getAffections() => Affectations.Output();
        public virtual String getFirstLine() => getOrigin().getName() + ",\"" + getPath() + "\",320,240";

        public void BeginLoop(int Time, int Count)
            => Affectations.StartLoop(new String[] { Time.ToString(), Count.ToString() });

        public void EndLoop()
            => Affectations.EndLoop();

        public void OnTrigger(int StartTime, int EndTime, String TriggerType)
            => Affectations.StartTrigger(new String[] { TriggerType, StartTime.ToString(), EndTime.ToString() });

        public void EndTrigger()
            => Affectations.EndTrigger();

        public Vector2 getPositionAt(int Time)
        {
            double[] Output;
            int InitialValue = 360;
            double PositionX = InitialValue;

            if (Affectations.MovementsX != null)
            {
                Output = Check(Time, InitialValue, Affectations.MovementsX, new int[] { 0, 1, 2, 3, 4 });
                PositionX = Output[0];
            }
            else if(Affectations.Movements != null)
            {
                Output = Check(Time, InitialValue, Affectations.Movements, new int[] { 0, 1, 2, 3, 5 });
                PositionX = Output[0];
            }
            
            InitialValue = 240;
            double PositionY = InitialValue;

            if (Affectations.MovementsY != null)
            {
                Output = Check(Time, InitialValue, Affectations.MovementsY, new int[] { 0, 1, 2, 3, 4 });
                PositionY = Output[0];
            }
            else if (Affectations.Movements != null)
            {
                Output = Check(Time, InitialValue, Affectations.Movements, new int[] { 0, 1, 2, 4, 6 });
                PositionY = Output[0];
            }

            return new Vector2( PositionX, PositionY );
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

                if (Affectation[2] == "")
                {
                    double FuncPos = Double.Parse(Affectation[ToCheck[3]]);
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
                        double FuncPos = Double.Parse(Affectation[ToCheck[4]]);
                        if (FuncStart <= Time) Output = FuncPos;
                    }

                    At = FuncEnd;
                }
            }

            return new double[] { Output, At };
        }

        private double Compare(double Time, int EasingID, double FuncStart, double FuncEnd, double StartValue, double EndValue)
        {
            double Progress = Easing.Ease(EasingID, (Time - FuncStart) / (FuncEnd - FuncStart));
            double Output = StartValue + (EndValue - StartValue) * Progress;
            return Output;
        }

        public void Hide()
            => Hidden = true;

        public bool IsHidden()
            => Hidden;

            // Linear Events
        public void Move(double Time, double X, double Y)
            => Affectations.AddMovement(new String[] { "0", ((int)Time).ToString(), "", X.ToString(), Y.ToString() });
        public void Move(double StartTime, double EndTime, double StartX, double StartY, double EndX, double EndY)
            => Move(0, StartTime, EndTime, StartX, StartY, EndX, EndY);

        public void Move(double Time, Vector2 Position)
            => Move(Time, Position.X, Position.Y);
        public void Move(double StartTime, double EndTime, Vector2 StartPosition, Vector2 EndPosition)
            => Move(0, StartTime, EndTime, StartPosition, EndPosition);

        public void MoveX(double Time, double X)
            => Affectations.AddMovementX(new String[] { "0", ((int)Time).ToString(), "", X.ToString() });
        public void MoveX(double StartTime, double EndTime, double StartX, double EndX)
            => MoveX(0, StartTime, EndTime, StartX, EndX);

        public void MoveY(double Time, double Y)
            => Affectations.AddMovementY(new String[] { "0", ((int)Time).ToString(), "", Y.ToString() });
        public void MoveY(double StartTime, double EndTime, double StartY, double EndY)
            => MoveY(0, StartTime, EndTime, StartY, EndY);

        public void Fade(double Time, double Opacity)
            => Affectations.AddFading(new String[] { "0", ((int)Time).ToString(), "", Opacity.ToString() });
        public void Fade(double StartTime, double EndTime, double StartOpacity, double EndOpacity)
            => Fade(0, StartTime, EndTime, StartOpacity, EndOpacity);

        public void Scale(double Time, double Ratio)
            => Affectations.AddScaling(new String[] { "0", ((int)Time).ToString(), "", Ratio.ToString() });
        public void Scale(double StartTime, double EndTime, double StartRatio, double EndRatio)
            => Scale(0, StartTime, EndTime, StartRatio, EndRatio);

        public void ScaleVec(double Time, double X, double Y)
            => Affectations.AddVecScaling(new String[] { "0", ((int)Time).ToString(), "", X.ToString(), Y.ToString() });
        public void ScaleVec(double StartTime, double EndTime, double StartX, double StartY, double EndX, double EndY)
            => ScaleVec(0, StartTime, EndTime, StartX, StartY, EndX, EndY);

        public void Rotate(double Time, double Angle)
            => Affectations.AddRotation(new String[] { "0", ((int)Time).ToString(), "", Angle.ToString() });
        public void Rotate(double StartTime, double EndTime, double StartAngle, double EndAngle)
            => Rotate(0, StartTime, EndTime, StartAngle, EndAngle);

        public void Color(double Time, double R, double G, double B)
            => Affectations.AddColor(new String[] { "0", ((int)Time).ToString(), "", ((int)(R * 255)).ToString(), ((int)(G * 255)).ToString(), ((int)(B * 255)).ToString() });
        public void Color(double StartTime, double EndTime, double StartR, double StartG, double StartB, double EndR, double EndG, double EndB)
            => Color(0, StartTime, EndTime, StartR, StartG, StartB, EndR, EndG, EndB);

        public void Color(int Time, Color Color)
            => this.Color(Time, Color.R, Color.G, Color.B);
        public void Color(int StartTime, int EndTime, Color StartColor, Color EndColor)
            => this.Color(StartTime, EndTime, StartColor.R, StartColor.G, StartColor.B, EndColor.R, EndColor.G, EndColor.B);

        public void HFlip(double StartTime, double EndTime)
            => HFlip(0, StartTime, EndTime);
        public void VFlip(double StartTime, double EndTime)
            => VFlip(0, StartTime, EndTime);
        public void Additive(double StartTime, double EndTime)
            => Additive(0, StartTime, EndTime);

        // Events by Easings
        public void Move(Easing Easing, double StartTime, double EndTime, double StartX, double StartY, double EndX, double EndY)
            => Move(Easing.getID(), StartTime, EndTime, StartX, StartY, EndX, EndY);

        public void Move(Easing Easing, double StartTime, double EndTime, Vector2 StartPosition, Vector2 EndPosition)
            => Move(Easing.getID(), StartTime, EndTime, StartPosition, EndPosition);

        public void MoveX(Easing Easing, double StartTime, double EndTime, double StartX, double EndX)
            => MoveX(Easing.getID(), StartTime, EndTime, StartX, EndX);
        
        public void MoveY(Easing Easing, double StartTime, double EndTime, double StartY, double EndY)
            => MoveY(Easing.getID(), StartTime, EndTime, StartY, EndY);
        
        public void Fade(Easing Easing, double StartTime, double EndTime, double StartOpacity, double EndOpacity)
            => Fade(Easing.getID(), StartTime, EndTime, StartOpacity, EndOpacity);
        
        public void Scale(Easing Easing, double StartTime, double EndTime, double StartRatio, double EndRatio)
            => Scale(Easing.getID(), StartTime, EndTime, StartRatio, EndRatio);
        
        public void ScaleVec(Easing Easing, double StartTime, double EndTime, double StartX, double StartY, double EndX, double EndY)
            => ScaleVec(Easing.getID(), StartTime, EndTime, StartX, StartY, EndX, EndY);
        
        public void Rotate(Easing Easing, double StartTime, double EndTime, double StartAngle, double EndAngle)
            => Rotate(Easing.getID(), StartTime, EndTime, StartAngle, EndAngle);
        
        public void Color(Easing Easing, double StartTime, double EndTime, double StartR, double StartG, double StartB, double EndR, double EndG, double EndB)
            => Color(Easing.getID(), StartTime, EndTime, StartR, StartG, StartB, EndR, EndG, EndB);

        public void Color(Easing Easing, double StartTime, double EndTime, Color StartColor, Color EndColor)
            => Color(Easing.getID(), StartTime, EndTime, StartColor, EndColor);

        public void HFlip(Easing Easing, double StartTime, double EndTime)
            => HFlip(Easing.getID(), StartTime, EndTime);
        public void VFlip(Easing Easing, double StartTime, double EndTime)
            => VFlip(Easing.getID(), StartTime, EndTime);
        public void Additive(Easing Easing, double StartTime, double EndTime)
            => Additive(Easing.getID(), StartTime, EndTime);

        // Events by Easing IDs
        public void Move(int EasingID, double StartTime, double EndTime, double StartX, double StartY, double EndX, double EndY)
            => Affectations.AddMovement(new String[] { EasingID.ToString(), ((int)StartTime).ToString(), ((int)EndTime).ToString(), StartX.ToString(), StartY.ToString(), EndX.ToString(), EndY.ToString() });

        public void Move(int EasingID, double StartTime, double EndTime, Vector2 StartPosition, Vector2 EndPosition)
            => Move(EasingID, StartTime, EndTime, StartPosition.X, StartPosition.Y, EndPosition.X, EndPosition.Y);
        
        public void MoveX(int EasingID, double StartTime, double EndTime, double StartX, double EndX)
            => Affectations.AddMovementX(new String[] { EasingID.ToString(), ((int)StartTime).ToString(), ((int)EndTime).ToString(), StartX.ToString(), EndX.ToString() });
        
        public void MoveY(int EasingID, double StartTime, double EndTime, double StartY, double EndY)
            => Affectations.AddMovementY(new String[] { EasingID.ToString(), ((int)StartTime).ToString(), ((int)EndTime).ToString(), StartY.ToString(), EndY.ToString() });
        
        public void Fade(int EasingID, double StartTime, double EndTime, double StartOpacity, double EndOpacity)
            => Affectations.AddFading(new String[] { EasingID.ToString(), ((int)StartTime).ToString(), ((int)EndTime).ToString(), StartOpacity.ToString(), EndOpacity.ToString() });
        
        public void Scale(int EasingID, double StartTime, double EndTime, double StartRatio, double EndRatio)
            => Affectations.AddScaling(new String[] { EasingID.ToString(), ((int)StartTime).ToString(), ((int)EndTime).ToString(), StartRatio.ToString(), EndRatio.ToString() });
        
        public void ScaleVec(int EasingID, double StartTime, double EndTime, double StartX, double StartY, double EndX, double EndY)
            => Affectations.AddVecScaling(new String[] { EasingID.ToString(), ((int)StartTime).ToString(), ((int)EndTime).ToString(), StartX.ToString(), StartY.ToString(), EndX.ToString(), EndY.ToString() });
        
        public void Rotate(int EasingID, double StartTime, double EndTime, double StartAngle, double EndAngle)
            => Affectations.AddRotation(new String[] { EasingID.ToString(), ((int)StartTime).ToString(), ((int)EndTime).ToString(), StartAngle.ToString(), EndAngle.ToString() });
        
        public void Color(int EasingID, double StartTime, double EndTime, double StartR, double StartG, double StartB, double EndR, double EndG, double EndB)
            => Affectations.AddColor(new String[] { EasingID.ToString(), ((int)StartTime).ToString(), ((int)EndTime).ToString(), ((int)(StartR * 255)).ToString(), ((int)(StartG * 255)).ToString(), ((int)(StartB * 255)).ToString(), ((int)(EndR * 255)).ToString(), ((int)(EndG * 255)).ToString(), ((int)(EndB * 255)).ToString() });

        public void Color(int EasingID, double StartTime, double EndTime, Color StartColor, Color EndColor)
            => this.Color(EasingID, StartTime, EndTime, StartColor.R, StartColor.G, StartColor.B, EndColor.R, EndColor.G, EndColor.B);

        public void HFlip(int EasingID, double StartTime, double EndTime)
            => Affectations.AddHFlip(new String[] { EasingID.ToString(), ((int)StartTime).ToString(), ((int)EndTime).ToString(), "H" });
        public void VFlip(int EasingID, double StartTime, double EndTime)
            => Affectations.AddVFlip(new String[] { EasingID.ToString(), ((int)StartTime).ToString(), ((int)EndTime).ToString(), "V" });
        public void Additive(int EasingID, double StartTime, double EndTime)
            => Affectations.AddAdditive(new String[] { EasingID.ToString(), ((int)StartTime).ToString(), ((int)EndTime).ToString(), "A" });
    }
}
