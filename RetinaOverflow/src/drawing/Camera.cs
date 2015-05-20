using System;
using OpenTK;
using RetinaOverflow.Transform;
using System.Windows.Forms;

namespace RetinaOverflow
{
	public class Camera : ICamera
	{
		private Matrix4 viewMatrix;
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
			return this.toMat4().Row1.Wxy;
		}

		public Vector3 getViewDirection()
		{
			return this.toMat4().Row2.Wxy;
		}

		public Vector3 getRightVector()
		{
			return this.toMat4().Row0.Wxy;
		}
	}
}

