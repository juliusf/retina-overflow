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
            Assert.AreEqual(true, Comparison.epsilonCompareVector3(cam.getRightVector(), new Vector3(1,0,0)));
            Assert.AreEqual(true, Comparison.epsilonCompareVector3(cam.getUpVector(), new Vector3(0,1,0)));
            cam.rotate(Quaternion.FromAxisAngle(new Vector3(0, 1, 0), (float)Math.PI));
            Assert.AreEqual(true, Comparison.epsilonCompareVector3(cam.getViewDirection(), new Vector3(0, 0, 1)));
            Assert.AreEqual(true, Comparison.epsilonCompareVector3(cam.getRightVector(), new Vector3(-1, 0,0)));
            Assert.AreEqual(true, Comparison.epsilonCompareVector3(cam.getUpVector(), new Vector3(0,1,0)));
        }

        [Test()]
        public void testRotation()
        {
            var cam = new Camera();
            Assert.AreEqual(cam.getViewDirection(), new Vector3(0, 0, -1));
            cam.rotate(Quaternion.FromAxisAngle(new Vector3(0, 1, 0), (float)Math.PI));
            Assert.AreEqual(true, Comparison.epsilonCompareVector3(cam.getViewDirection(), new Vector3(0, 0, 1)));

            cam.rotate(Quaternion.FromAxisAngle(new Vector3(0, 0, 1), (float)Math.PI));
            Assert.AreEqual(true, Comparison.epsilonCompareVector3(cam.getViewDirection(), new Vector3(0, 0, 1)));
            Assert.AreEqual(true, Comparison.epsilonCompareVector3(cam.getRightVector(), new Vector3(1,0,0)));
            cam.rotate(Quaternion.FromAxisAngle(new Vector3(1, 0, 0), (float)Math.PI / 2));

            Assert.AreEqual(true, Comparison.epsilonCompareVector3(cam.getViewDirection(), new Vector3(0, -1, 0)));
            Assert.AreEqual(true, Comparison.epsilonCompareVector3(cam.getRightVector(), new Vector3(1,0,0)));

        }
    }
}

