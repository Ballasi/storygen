using storygen.Util;
using System;
using System.Collections.Generic;

namespace storygen
{
    class Sprite
    {
        String Path;
        Origin Origin;
        Affectations Affectations;
        bool Hidden;
        double X, Y;

        public Sprite()
        {
        }

        public Sprite(String Path, Origin Origin, double X, double Y)
        {
            this.Path = Path;
            this.Origin = Origin;
            Hidden = false;
            this.X = Math.Round(X, 4);
            this.Y = Math.Round(Y, 4);

            Affectations = new Affectations();
        }

        public String getPath() => Path;
        public Origin getOrigin() => Origin;
        public String getAffections() => Affectations.Output();
        public double getX() => X;
        public double getY() => Y;
        public virtual String getFirstLine() => getOrigin().getName() + ",\"" + getPath() + "\"," + X + "," + Y;

        public void BeginLoop(double Time, double Count)
            => Affectations.StartLoop(new String[] { ((int) Time).ToString(), ((int) Count).ToString() });

        public void EndLoop()
            => Affectations.EndLoop();

        public void OnTrigger(double StartTime, double EndTime, String TriggerType)
            => Affectations.StartTrigger(new String[] { TriggerType, ((int) StartTime).ToString(), ((int) EndTime).ToString() });

        public void EndTrigger()
            => Affectations.EndTrigger();

        public Vector2 getPositionAt(int Time)
        {
            double[] Output;
            int InitialValue = (int)X;
            double PositionX = InitialValue;

            if (Affectations.MovementsX.Count != 0)
            {
                Output = Check(Time, InitialValue, Affectations.MovementsX, new int[] { 0, 1, 2, 3, 4 });
                PositionX = Output[0];
            }
            else if(Affectations.Movements.Count != 0)
            {
                Output = Check(Time, InitialValue, Affectations.Movements, new int[] { 0, 1, 2, 3, 5 });
                PositionX = Output[0];
            }
            
            InitialValue = (int)Y;
            double PositionY = InitialValue;

            if (Affectations.MovementsY.Count != 0)
            {
                Output = Check(Time, InitialValue, Affectations.MovementsY, new int[] { 0, 1, 2, 3, 4 });
                PositionY = Output[0];
            }
            else if (Affectations.Movements.Count != 0)
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

            if (Affectations.Scales.Count != 0 && Affectations.VecScales.Count == 0)
            {
                Output = Check(Time, InitialValue, Affectations.Movements, new int[] { 0, 1, 2, 3, 4 });
                Position = Output[0];
            }
            else if (Affectations.VecScales.Count != 0)
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

            if (Affectations.Scales.Count != 0 && Affectations.VecScales.Count == 0)
            {
                Output = Check(Time, InitialValue, Affectations.Movements, new int[] { 0, 1, 2, 3, 4 });
                Position = Output[0];
            }
            else if (Affectations.VecScales.Count != 0)
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

        private double[] Check(int Time, double InitialValue, List<Affectation> Affectations, int[] ToCheck)
        {
            double Output = InitialValue;
            double At = 0;

            foreach (Affectation Affectation in Affectations)
            {
                String[] Parameters = Affectation.Parameters;
                Loop Loop = Affectation.Loop;
                int AdditiveTime = 0;
                int Repeat = 1;
                int AddTime = 0;
                if (Loop != null)
                {
                    AdditiveTime = Loop.getStartTime();
                    Repeat = Loop.getLoopCount();
                    AddTime = Loop.getDuration();
                }

                int EasingID = Int32.Parse(Parameters[ToCheck[0]]);

                for (int l = 0; l < Repeat; l++)
                {
                    int Dec = AdditiveTime + l * AddTime;
                    int FuncStart = Int32.Parse(Parameters[ToCheck[1]]) + Dec - 1;

                    if (Parameters[2] == "")
                    {
                        double FuncPos = Double.Parse(Parameters[ToCheck[3]]);
                        if (FuncStart <= Time)
                        {
                            Output = FuncPos;
                            At = FuncStart + 1;
                        }
                    }
                    else
                    {
                        int FuncEnd = Int32.Parse(Parameters[ToCheck[2]]) + Dec + 1;

                        if (Time > FuncStart && Time <= FuncEnd)
                        {
                            double FuncPosStart = Double.Parse(Parameters[ToCheck[3]]);
                            double FuncPosEnd = Double.Parse(Parameters[ToCheck[4]]);

                            Output = Compare(Time, EasingID, FuncStart, FuncEnd, FuncPosStart, FuncPosEnd);
                        }
                        else if (Time > FuncEnd)
                        {
                            double FuncPos = Double.Parse(Parameters[ToCheck[4]]);
                            // Output = FuncPos;
                            // TODO: Correct this
                        }

                        At = FuncEnd - 1;
                    }
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
            => Affectations.AddMovementX(new String[] { "0", ((int)Time).ToString(), "", Math.Round(X, 4).ToString() });
        public void MoveX(double StartTime, double EndTime, double StartX, double EndX)
            => MoveX(0, StartTime, EndTime, StartX, EndX);

        public void MoveY(double Time, double Y)
            => Affectations.AddMovementY(new String[] { "0", ((int)Time).ToString(), "", Math.Round(Y, 4).ToString() });
        public void MoveY(double StartTime, double EndTime, double StartY, double EndY)
            => MoveY(0, StartTime, EndTime, StartY, EndY);

        public void Fade(double Time, double Opacity)
            => Affectations.AddFading(new String[] { "0", ((int)Time).ToString(), "", Math.Round(Opacity, 4).ToString() });
        public void Fade(double StartTime, double EndTime, double StartOpacity, double EndOpacity)
            => Fade(0, StartTime, EndTime, StartOpacity, EndOpacity);

        public void Scale(double Time, double Ratio)
            => Affectations.AddScaling(new String[] { "0", ((int)Time).ToString(), "", Math.Round(Ratio, 4).ToString() });
        public void Scale(double StartTime, double EndTime, double StartRatio, double EndRatio)
            => Scale(0, StartTime, EndTime, StartRatio, EndRatio);

        public void ScaleVec(double Time, double X, double Y)
            => Affectations.AddVecScaling(new String[] { "0", ((int)Time).ToString(), "", Math.Round(X, 4).ToString(), Math.Round(Y, 4).ToString() });
        public void ScaleVec(double StartTime, double EndTime, double StartX, double StartY, double EndX, double EndY)
            => ScaleVec(0, StartTime, EndTime, StartX, StartY, EndX, EndY);

        public void Rotate(double Time, double Angle)
            => Affectations.AddRotation(new String[] { "0", ((int)Time).ToString(), "", Math.Round(Angle, 4).ToString() });
        public void Rotate(double StartTime, double EndTime, double StartAngle, double EndAngle)
            => Rotate(0, StartTime, EndTime, StartAngle, EndAngle);

        public void Color(double Time, double R, double G, double B)
            => Affectations.AddColor(new String[] { "0", ((int)Time).ToString(), "", ((int)(R * 255)).ToString(), ((int)(G * 255)).ToString(), ((int)(B * 255)).ToString() });
        public void Color(double StartTime, double EndTime, double StartR, double StartG, double StartB, double EndR, double EndG, double EndB)
            => Color(0, StartTime, EndTime, StartR, StartG, StartB, EndR, EndG, EndB);

        public void Color(double Time, Color Color)
            => this.Color(Time, Color.R, Color.G, Color.B);
        public void Color(double StartTime, double EndTime, Color StartColor, Color EndColor)
            => this.Color(StartTime, EndTime, StartColor.R, StartColor.G, StartColor.B, EndColor.R, EndColor.G, EndColor.B);

        public void HFlip(double StartTime, double EndTime)
            => HFlip(0, StartTime, EndTime);
        public void VFlip(double StartTime, double EndTime)
            => VFlip(0, StartTime, EndTime);
        public void Additive(double StartTime, double EndTime)
            => Additive(0, StartTime, EndTime);

        // Events by Easings
        public void Move(Easing Easing, double StartTime, double EndTime, double StartX, double StartY, double EndX, double EndY)
            => Move(Easing.getID(), StartTime, EndTime, Math.Round(StartX, 4), Math.Round(StartY, 4), Math.Round(EndX, 4), Math.Round(EndY, 4));

        public void Move(Easing Easing, double StartTime, double EndTime, Vector2 StartPosition, Vector2 EndPosition)
            => Move(Easing.getID(), StartTime, EndTime, StartPosition, EndPosition);

        public void MoveX(Easing Easing, double StartTime, double EndTime, double StartX, double EndX)
            => MoveX(Easing.getID(), StartTime, EndTime, Math.Round(StartX, 4), Math.Round(EndX, 4));
        
        public void MoveY(Easing Easing, double StartTime, double EndTime, double StartY, double EndY)
            => MoveY(Easing.getID(), StartTime, EndTime, Math.Round(StartY, 4), Math.Round(EndY, 4));
        
        public void Fade(Easing Easing, double StartTime, double EndTime, double StartOpacity, double EndOpacity)
            => Fade(Easing.getID(), StartTime, EndTime, Math.Round(StartOpacity, 4), Math.Round(EndOpacity, 4));
        
        public void Scale(Easing Easing, double StartTime, double EndTime, double StartRatio, double EndRatio)
            => Scale(Easing.getID(), StartTime, EndTime, Math.Round(StartRatio, 4), Math.Round(EndRatio, 4));
        
        public void ScaleVec(Easing Easing, double StartTime, double EndTime, double StartX, double StartY, double EndX, double EndY)
            => ScaleVec(Easing.getID(), StartTime, EndTime, Math.Round(StartX, 4), Math.Round(StartY, 4), Math.Round(EndX, 4), Math.Round(EndY, 4));
        
        public void Rotate(Easing Easing, double StartTime, double EndTime, double StartAngle, double EndAngle)
            => Rotate(Easing.getID(), StartTime, EndTime, Math.Round(StartAngle, 4), Math.Round(EndAngle, 4));
        
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
            => Affectations.AddMovement(new String[] { EasingID.ToString(), ((int)StartTime).ToString(), ((int)EndTime).ToString(), Math.Round(StartX, 4).ToString(), Math.Round(StartY, 4).ToString(), Math.Round(EndX, 4).ToString(), Math.Round(EndY, 4).ToString() });

        public void Move(int EasingID, double StartTime, double EndTime, Vector2 StartPosition, Vector2 EndPosition)
            => Move(EasingID, StartTime, EndTime, StartPosition.X, StartPosition.Y, EndPosition.X, EndPosition.Y);
        
        public void MoveX(int EasingID, double StartTime, double EndTime, double StartX, double EndX)
            => Affectations.AddMovementX(new String[] { EasingID.ToString(), ((int)StartTime).ToString(), ((int)EndTime).ToString(), Math.Round(StartX, 4).ToString(), Math.Round(EndX, 4).ToString() });
        
        public void MoveY(int EasingID, double StartTime, double EndTime, double StartY, double EndY)
            => Affectations.AddMovementY(new String[] { EasingID.ToString(), ((int)StartTime).ToString(), ((int)EndTime).ToString(), Math.Round(StartY, 4).ToString(), Math.Round(EndY, 4).ToString() });
        
        public void Fade(int EasingID, double StartTime, double EndTime, double StartOpacity, double EndOpacity)
            => Affectations.AddFading(new String[] { EasingID.ToString(), ((int)StartTime).ToString(), ((int)EndTime).ToString(), Math.Round(StartOpacity, 4).ToString(), Math.Round(EndOpacity, 4).ToString() });
        
        public void Scale(int EasingID, double StartTime, double EndTime, double StartRatio, double EndRatio)
            => Affectations.AddScaling(new String[] { EasingID.ToString(), ((int)StartTime).ToString(), ((int)EndTime).ToString(), Math.Round(StartRatio, 4).ToString(), Math.Round(EndRatio, 4).ToString() });
        
        public void ScaleVec(int EasingID, double StartTime, double EndTime, double StartX, double StartY, double EndX, double EndY)
            => Affectations.AddVecScaling(new String[] { EasingID.ToString(), ((int)StartTime).ToString(), ((int)EndTime).ToString(), Math.Round(StartX, 4).ToString(), Math.Round(StartY, 4).ToString(), Math.Round(EndX, 4).ToString(), Math.Round(EndY, 4).ToString() });
        
        public void Rotate(int EasingID, double StartTime, double EndTime, double StartAngle, double EndAngle)
            => Affectations.AddRotation(new String[] { EasingID.ToString(), ((int)StartTime).ToString(), ((int)EndTime).ToString(), Math.Round(StartAngle, 4).ToString(), Math.Round(EndAngle, 4).ToString() });
        
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
