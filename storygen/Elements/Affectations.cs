using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class Affectations
    {
        public List<String[]> Movements, MovementsX, MovementsY, Fades, Scales, VecScales, Rotations, Loops, Colors, Parameters;
        String Content;
        bool InLoop;

        public Affectations()
        {
            Movements = new List<String[]>();
            MovementsX = new List<String[]>();
            MovementsY = new List<String[]>();
            Fades = new List<String[]>();
            Scales = new List<String[]>();
            VecScales = new List<String[]>();
            Rotations = new List<String[]>();
            Loops = new List<String[]>();
            Colors = new List<String[]>();
            Parameters = new List<String[]>();

            InLoop = false;
            Content = null;
        }

        public void StartLoop(String[] Parameters)
        {
            Loops.Add(Parameters);
            Content += getEquivalentLine("L", Parameters);
            InLoop = true;
        }

        public void EndLoop()
        {
            InLoop = false;
        }

        public void AddMovement(String[] Parameters)
        {
            Movements.Add(Parameters);
            Content += getEquivalentLine("M", Parameters);
        }

        public void AddMovementX(String[] Parameters)
        {
            MovementsX.Add(Parameters);
            Content += getEquivalentLine("MX", Parameters);
        }

        public void AddMovementY(String[] Parameters)
        {
            MovementsY.Add(Parameters);
            Content += getEquivalentLine("MY", Parameters);
        }

        public void AddFading(String[] Parameters)
        {
            Fades.Add(Parameters);
            Content += getEquivalentLine("F", Parameters);
        }

        public void AddScaling(String[] Parameters)
        {
            Scales.Add(Parameters);
            Content += getEquivalentLine("S", Parameters);
        }

        public void AddVecScaling(String[] Parameters)
        {
            VecScales.Add(Parameters);
            Content += getEquivalentLine("V", Parameters);
        }

        public void AddRotation(String[] Parameters)
        {
            Rotations.Add(Parameters);
            Content += getEquivalentLine("R", Parameters);
        }

        public void AddColor(String[] Parameters)
        {
            Colors.Add(Parameters);
            Content += getEquivalentLine("C", Parameters);
        }

        public void AddHFlip(String[] Parameters)
        {
            Colors.Add(Parameters);
            Content += getEquivalentLine("P", Parameters);
        }

        public void AddVFlip(String[] Parameters)
        {
            Colors.Add(Parameters);
            Content += getEquivalentLine("P", Parameters);
        }

        public void AddAdditive(String[] Parameters)
        {
            Colors.Add(Parameters);
            Content += getEquivalentLine("P", Parameters);
        }

        private String getEquivalentLine(String Type, String[] Parameters)
        {
            String Line = " ";
            if (InLoop) Line += " ";
            Line += Type;
            
            foreach (String Parameter in Parameters)
            {
                Line += "," + Parameter;
            }

            return Line + "\n";
        }

        public String Output()
        {
            return Content;
        }
    }
}
