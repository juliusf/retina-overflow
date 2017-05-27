using System;
using OpenTK;
using RetinaOverflow.Transform;
using System.Windows.Forms;

namespace RetinaOverflow
{
    public class Camera : ICamera, ITransformable
    {

        private Transformation transform;
        private Angle absolutePitch;
        private Angle absoluteYaw;

        public Camera()
        {
            this.transform = new Transformation();
        }

        public void reset()
        {
            transform = new Transformation();
            absolutePitch = new Angle();
            absoluteYaw = new Angle();
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
            transform.rotation = lookAtMatrix.ExtractRotation(true);
        }



        public void look(Angle deltaYaw, Angle deltaPitch)
        {
            absolutePitch += deltaPitch;
            absoluteYaw += deltaYaw;
     
            Quaternion yawQuat = Quaternion.FromAxisAngle(Transformation.worldUp, absoluteYaw.Radians);
            Quaternion pitchQuat = Quaternion.FromAxisAngle(Transformation.worldRight, absolutePitch.Radians);
      
            var transform = this.getTransformation();

            transform.rotation = yawQuat * pitchQuat;
        }
    }
}

