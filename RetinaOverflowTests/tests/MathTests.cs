using System;
using NUnit.Framework;
using RetinaOverflow;
using OpenTK;
using RetinaOverflow.Transform;

namespace RetinaOverflowTests
{
    public class MathTests
    {
        public MathTests()
        {
        }

        [Test()]
        public void testDirection()
        {
            var cam = new Camera();
            Assert.AreEqual(cam.getViewDirection(), new Vector3(0, 0, -1));

            cam.rotate(Quaternion.FromAxisAngle(new Vector3(0, 1, 0), (float)Math.PI));
            //           Assert.AreEqual(cam.getViewDirection(), new Vector3(0, 0, 1)); 
            Assert.AreEqual(true, Comparison.epsilonCompareVector3(cam.getViewDirection(), new Vector3(0, 0, 1)));


        }
    }
}

