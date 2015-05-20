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
        public Color color;

        public String name
        {
            get;
            set;
        }

        public Mesh(ref DataBuffer modelBuffer)
        {
            this.transform = new Transformation();
            this.modelBuffer = modelBuffer;
            var rand = new Random();
            this.color = Color.FromArgb((byte)rand.Next(), (byte)rand.Next(), (byte)rand.Next());
        }

        public Transformation getTransformation()
        {
            return this.transform;
        }

        public void draw()
        {
            foreach (var vertex in modelBuffer.readToVec3(BufferChannels.POSITION))
            {
                GL.Color3(color);
                GL.Vertex3(Vector3.Add(this.getWorldPosition(), vertex));
            }
        }
    }
}

