using System;
using RetinaOverflow;
using RetinaOverflow.Transform;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System.Drawing;

namespace RetinaOverflow
{
    public interface IDrawable
    {
        void draw();
    }

    namespace Drawable
    {
        public static class DrawableExtension
        {
            public static void drawAxes(this ITransformable transformable)
            {

                var position = transformable.getWorldPosition();
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(Color.Red);
                GL.Vertex3(position);
                GL.Vertex3(position + new OpenTK.Vector3(100,0,0));

                GL.Color3(Color.Blue);
                GL.Vertex3(position);
                GL.Vertex3(position + new OpenTK.Vector3(0,100,0));

                GL.Color3(Color.Green);
                GL.Vertex3(position);
                GL.Vertex3(position + new OpenTK.Vector3(0,0,100));
            }


        }
    }
}


