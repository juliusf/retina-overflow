using System;
using OpenTK;


namespace RetinaOverflow
{
    public static class Comparison
    {
        static Boolean epsilonCompare(float lhs, float rhs)
        {
            if (Math.Abs(lhs - rhs) < 1.0E-7)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public static Boolean epsilonCompareVector3(Vector3 lhs, Vector3 rhs)
        {
            for (int i = 0; i < 3; i++)
            {
                if (!epsilonCompare(lhs[i], rhs[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}


