using System;
using OpenTK;
using RetinaOverflow.Transform;
using System.Windows.Forms;

namespace RetinaOverflow
{
    public class Camera : ICamera
    {

        private Transformation transform;

        public Camera()
        {
            this.transform = new Transformation();
        }

        public Matrix4 getViewMatrix()
        {
            return this.toMat4().Inverted();
        }

        public Transformation getTransformation()
        {
            return this.transform;
        }

        public Vector3 getUpVector()
        {
            return Vector4.Transform(new Vector4(0.0f, 1.0f, 0.0f, 0.0f), this.rotationToMat4()).Xyz;
        }

        public Vector3 getViewDirection()
        {
            return Vector4.Transform(new Vector4(0.0f, 0.0f, -1.0f, 0.0f), this.rotationToMat4()).Xyz;
        }

        public Vector3 getRightVector()
        {
            return Vector4.Transform(new Vector4(1.0f, 0.0f, 0.0f, 0.0f), this.rotationToMat4()).Xyz;
        }

        public void lookAt(Vector3 point)
        {
            var transform = this.getTransformation();
            var lookAtMatrix = Matrix4.LookAt(transform.position, point, this.getUpVector()).Inverted();

            //transform.position = lookAtMatrix.ExtractTranslation();
            transform.rotation = lookAtMatrix.ExtractRotation(true);
        }

        public void look(float deltaX, float deltaY)
        {
            var qy = Quaternion.FromAxisAngle(new Vector3(1, 0, 0), -deltaY);
            var qx = Quaternion.FromAxisAngle(new Vector3(0, 1, 0), -deltaX);

            var delta = Quaternion.Multiply(qx, qy);

            this.rotate(delta);
        }
    }
}

