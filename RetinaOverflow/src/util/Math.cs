using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetinaOverflow
{
    public struct Angle
    {
        // Stored as radians.
        float value;

        public float Radians
        {
            get { return value; }
            set { this.value = value; }
        }

        public float Degrees
        {
            get { return MathHelper.RadiansToDegrees(value); }
            set { this.value = MathHelper.DegreesToRadians(value); }
        }

        public static Angle FromRadians(float radians) { return new Angle { Radians = radians }; }
        public static Angle FromDegrees(float degrees) { return new Angle { Degrees = degrees }; }

        public static Angle operator +(Angle A, Angle B) { return new Angle { value = A.value + B.value }; }
        public static Angle operator -(Angle A, Angle B) { return new Angle { value = A.value - B.value }; }
    }
}
