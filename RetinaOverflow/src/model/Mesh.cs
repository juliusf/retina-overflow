﻿using System;
using RetinaOverflow.Transform;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using RetinaOverflow.Drawable;
using RetinaOverflow.src.drawing;

namespace RetinaOverflow
{
    public class Mesh : ITransformable, IDrawable
    {
        private Transformation transform;
        private DataBuffer modelBuffer;
        private int ID_VBO = -1;
        private int ID_VAO = -1;

        public Color color;
        public String name
        {
            get;
            set;
        }

        public Material material;

        public Mesh(ref DataBuffer modelBuffer)
        {
            this.transform = new Transformation();
            this.modelBuffer = modelBuffer;
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

            GL.GenVertexArrays(1, out ID_VAO);
            GL.BindVertexArray(ID_VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID_VBO); // Set it up as array buffer
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, modelBuffer.strideInBytes, 0);  // position

            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, modelBuffer.strideInBytes, 3 * 4);  // normals
            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, modelBuffer.strideInBytes, 2 * 3 * 4); // tex coords
            GL.EnableVertexAttribArray(0);
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); // This seems to be good practice

            var foo = GL.IsVertexArray(1);
            GL.BindVertexArray(ID_VAO);
        }

        public void draw()
        {
            var modelMatrix = this.toMat4();
            var shaderID = GlobalManager.instance.renderer.shaderID;

            GL.UniformMatrix4(GL.GetUniformLocation(shaderID, "modelMatrix"), false, ref modelMatrix);
            GL.Enable(EnableCap.Texture2D);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, material.diffuseTextureID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID_VBO);

            GL.BindVertexArray(ID_VAO);
            GL.DrawArrays(PrimitiveType.Triangles, 0, modelBuffer.size);
        }
    }
}

