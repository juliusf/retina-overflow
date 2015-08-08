using System;
using RetinaOverflow.Transform;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using RetinaOverflow.Drawable;

namespace RetinaOverflow
{
    public class Mesh : ITransformable, IDrawable
    {
        private Transformation transform;
        private DataBuffer modelBuffer;
        public Color color;
        public Vector3 tmpVec;
        public Vector3 tmp2;
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
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(color);
            foreach (Vector3 vertex in modelBuffer)
            {
                Vector3 vert;
                vert = vertex;
                tmp2 = this.getWorldPosition();
                Vector3.Add(ref tmp2, ref vert, out tmpVec);
                GL.Vertex3(tmpVec);
            }


        }
    }
}

