using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class Affectations
    {
        List<String[]> Movements, Fades, Scales, Rotates;

        public Affectations()
        {
            Movements = new List<String[]>();
            Fades = new List<String[]>();
            Scales = new List<String[]>();
            Rotates = new List<String[]>();
        }

        public void AddMovement(String[] Parameters)
        {
            Movements.Add(Parameters);
        }

        public void AddFading(String[] Parameters)
        {
            Fades.Add(Parameters);
        }

        public void AddScaling(String[] Parameters)
        {
            Scales.Add(Parameters);
        }

        public void AddRotation(String[] Parameters)
        {
            Rotates.Add(Parameters);
        }

        public String Output()
        {
            String Output = null;

            foreach (String[] Movement in Movements)
            {
                if (Movement.Length == 4) Output += " M," + Movement[0] + "," + Movement[1] + ",," + Movement[2] + "," + Movement[3] + "\n";
                else Output += " M," + Movement[0] + "," + Movement[1] + "," + Movement[2] + "," + Movement[3] + "," + Movement[4] + "," + Movement[5] + "," + Movement[6] + "\n";
            }

            foreach (String[] Fade in Fades)
            {
                if (Fade.Length == 3) Output += " F," + Fade[0] + "," + Fade[1] + ",," + Fade[2] + "\n";
                else Output += " F," + Fade[0] + "," + Fade[1] + "," + Fade[2] + "," + Fade[3] + "," + Fade[4] + "\n";
            }

            foreach (String[] Scale in Scales)
            {
                if (Scale.Length == 3) Output += " S," + Scale[0] + "," + Scale[1] + ",," + Scale[2] + "\n";
                else Output += " S," + Scale[0] + "," + Scale[1] + "," + Scale[2] + "," + Scale[3] + "," + Scale[4] + "," + "\n";
            }

            foreach (String[] Rotate in Rotates)
            {
                if (Rotate.Length == 3) Output += " R," + Rotate[0] + "," + Rotate[1] + ",," + Rotate[2] + "\n";
                else Output += " R," + Rotate[0] + "," + Rotate[1] + "," + Rotate[2] + "," + Rotate[3] + "," + Rotate[4] + "," + "\n";
            }

            return Output;
        }
    }
}
