using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class Affectations
    {
        public List<String[]> Movements, MovementsX, MovementsY, Fades, Scales, Rotations, Loops;
        String Content;
        bool InLoop;

        public Affectations()
        {
            Movements = new List<String[]>();
            MovementsX = new List<String[]>();
            MovementsY = new List<String[]>();
            Fades = new List<String[]>();
            Scales = new List<String[]>();
            Rotations = new List<String[]>();
            Loops = new List<String[]>();

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

        public void AddRotation(String[] Parameters)
        {
            Rotations.Add(Parameters);
            Content += getEquivalentLine("R", Parameters);
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
