using System;
using System.Collections.Generic;

namespace storygen
{
    class Affectations
    {
        public List<Loop> Loops;
        public List<Affectation> Movements, MovementsX, MovementsY, Fades, Scales, VecScales, Rotations, Triggers, Colors, ElemParameters;
        String Content;
        Loop CurrentLoop;
        int GroupDepth;

        public Affectations()
        {
            Movements = new List<Affectation>();
            MovementsX = new List<Affectation>();
            MovementsY = new List<Affectation>();
            Fades = new List<Affectation>();
            Scales = new List<Affectation>();
            VecScales = new List<Affectation>();
            Rotations = new List<Affectation>();
            Colors = new List<Affectation>();
            ElemParameters = new List<Affectation>();
            Loops = new List<Loop>();
            Triggers = new List<Affectation>();

            GroupDepth = 0;
            Content = null;
            CurrentLoop = null;
        }

        public void StartLoop(String[] Parameters)
        {
            Loop Affectation = new Loop("L", GroupDepth, Parameters);
            Loops.Add(Affectation);
            Content += Affectation.getEquivalentLine();
            GroupDepth++;
            CurrentLoop = Affectation;
        }

        public void EndLoop()
        {
            CurrentLoop = null;
            GroupDepth--;
        }

        public void StartTrigger(String[] Parameters)
        {
            Affectation Affectation = new Affectation("T", GroupDepth, Parameters);
            Triggers.Add(Affectation);
            Content += Affectation.getEquivalentLine();
            GroupDepth++;
        }

        public void EndTrigger()
        {
            GroupDepth--;
        }

        public void AddMovement(String[] Parameters)
        {
            Affectation Affectation = new Affectation("M", GroupDepth, Parameters, CurrentLoop);
            Movements.Add(Affectation);
            Content += Affectation.getEquivalentLine();
        }

        public void AddMovementX(String[] Parameters)
        {
            Affectation Affectation = new Affectation("MX", GroupDepth, Parameters, CurrentLoop);
            MovementsX.Add(Affectation);
            Content += Affectation.getEquivalentLine();
        }

        public void AddMovementY(String[] Parameters)
        {
            Affectation Affectation = new Affectation("MY", GroupDepth, Parameters, CurrentLoop);
            MovementsY.Add(Affectation);
            Content += Affectation.getEquivalentLine();
        }

        public void AddFading(String[] Parameters)
        {
            Affectation Affectation = new Affectation("F", GroupDepth, Parameters, CurrentLoop);
            Fades.Add(Affectation);
            Content += Affectation.getEquivalentLine();
        }

        public void AddScaling(String[] Parameters)
        {
            Affectation Affectation = new Affectation("S", GroupDepth, Parameters, CurrentLoop);
            Scales.Add(Affectation);
            Content += Affectation.getEquivalentLine();
        }

        public void AddVecScaling(String[] Parameters)
        {
            Affectation Affectation = new Affectation("V", GroupDepth, Parameters, CurrentLoop);
            VecScales.Add(Affectation);
            Content += Affectation.getEquivalentLine();
        }

        public void AddRotation(String[] Parameters)
        {
            Affectation Affectation = new Affectation("R", GroupDepth, Parameters, CurrentLoop);
            Rotations.Add(Affectation);
            Content += Affectation.getEquivalentLine();
        }

        public void AddColor(String[] Parameters)
        {
            Affectation Affectation = new Affectation("C", GroupDepth, Parameters, CurrentLoop);
            Colors.Add(Affectation);
            Content += Affectation.getEquivalentLine();
        }

        public void AddHFlip(String[] Parameters)
        {
            Affectation Affectation = new Affectation("P", GroupDepth, Parameters, CurrentLoop);
            ElemParameters.Add(Affectation);
            Content += Affectation.getEquivalentLine();
        }

        public void AddVFlip(String[] Parameters)
        {
            Affectation Affectation = new Affectation("P", GroupDepth, Parameters, CurrentLoop);
            ElemParameters.Add(Affectation);
            Content += Affectation.getEquivalentLine();
        }

        public void AddAdditive(String[] Parameters)
        {
            Affectation Affectation = new Affectation("P", GroupDepth, Parameters, CurrentLoop);
            ElemParameters.Add(Affectation);
            Content += Affectation.getEquivalentLine();
        }

        public String Output()
        {
            return Content;
        }
    }

    class Affectation
    {
        public string Type { get; }
        public Loop Loop { get; }
        public int GroupDepth;
        public String[] Parameters { get; }

        public Affectation(string Type, int GroupDepth, String[] Parameters, Loop Loop = null)
        {
            this.Type = Type;
            this.GroupDepth = GroupDepth;
            this.Parameters = Parameters;
            this.Loop = Loop;

            if (Loop != null)
                Loop.AddTimes(Parameters[1], Parameters[2]);
        }

        public String getEquivalentLine()
        {
            String Line = new String(' ', GroupDepth + 1);
            Line += Type;

            foreach (String Parameter in Parameters)
            {
                Line += "," + Parameter;
            }

            return Line + "\n";
        }
    }

    class Loop : Affectation
    {
        int ElemStart = -1;
        int ElemEnd = -1;

        public int getDuration() => ElemEnd - ElemStart;
        public int getStartTime() => Int32.Parse(Parameters[0]);
        public int getLoopCount() => Int32.Parse(Parameters[1]);

        public Loop(string Type, int GroupDepth, String[] Parameters) : base(Type, GroupDepth, Parameters)
        {
        }

        public void AddTimes(String Start, String End)
        {
            int StartParsed = Int32.Parse(Start);
            if (StartParsed < ElemStart || ElemStart == -1)
                ElemStart = StartParsed;

            if (End != "")
            {
                int EndParsed = Int32.Parse(End);
                if (EndParsed > ElemEnd || ElemEnd == -1)
                    ElemEnd = EndParsed;
            }
        }

    }
}
