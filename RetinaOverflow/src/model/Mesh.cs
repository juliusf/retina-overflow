using System;
using RetinaOverflow.Transform;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace RetinaOverflow
{
	public class Mesh : ITransformable, IDrawable
	{
		private Transformation transform;
		private DataBuffer modelBuffer;
		public String name
		{
			get;
			set;
		}

		public Mesh(ref DataBuffer modelBuffer)
		{
			this.transform = new Transformation();
			this.modelBuffer = modelBuffer;
		}

		public Transformation getTransformation()
		{
			return this.transform;
		}

		public void draw()
		{
			foreach (var vertex in modelBuffer.readToVec3(BufferChannels.POSITION))
			{
				GL.Color3(Color.MidnightBlue);
				GL.Vertex3(Vector3.Add(this.getWorldPosition(), vertex));
			}
		}
	}
}

