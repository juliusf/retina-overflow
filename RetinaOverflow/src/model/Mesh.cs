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
        private int ID_VBO;

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

        public void initialize()
        {
            GL.GenBuffers(1, out ID_VBO); // Generate a single buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID_VBO); // Set it up as array buffer
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)modelBuffer.bufferLengthInBytes, modelBuffer.theBuffer, BufferUsageHint.StaticDraw); // Copy the buffer data to the graphics card

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); // This seems to be good practice
        }

        public void draw()
        {
       /*     GL.Begin(PrimitiveType.Triangles);
            GL.Color3(color);
            foreach (Vector3 vertex in modelBuffer)
            {
                Vector3 vert;
                vert = vertex;
                tmp2 = this.getWorldPosition();
                Vector3.Add(ref tmp2, ref vert, out tmpVec);
                GL.Vertex3(tmpVec);
            } */

            GL.Color3(color);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID_VBO);
            GL.VertexPointer(3, VertexPointerType.Float, modelBuffer.strideInBytes,0); // TODO: get the offset more easily accsessible in modelBuffer
            GL.DrawArrays(PrimitiveType.Triangles, 0, modelBuffer.size);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DisableClientState(ArrayCap.VertexArray);
        }
    }
}

