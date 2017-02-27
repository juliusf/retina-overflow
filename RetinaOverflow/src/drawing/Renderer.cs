using OpenTK;
using OpenTK.Graphics.OpenGL;
using RetinaOverflow.src.util.Exception;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RetinaOverflow.src.drawing
{
    class Renderer
    {
        World world;
        Matrix4 projectionMatrix = Matrix4.Identity;

        int ID_SHADER = -1;

        public Renderer(World world) {
            this.world = world;
        }

        public void initialize() {
            ID_SHADER = compileShaders();
            world.initializeContent();
        }

        public void onResize(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)width / (float) height, 1.0f, 3000.0f);
            GL.Enable(EnableCap.DepthTest);
        }

        public void renderFrame() {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            var viewMatrix = World.activeCam.getViewMatrix();
            GL.UseProgram(ID_SHADER);
            GL.UniformMatrix4(GL.GetUniformLocation(ID_SHADER, "viewMatrix"), false, ref viewMatrix);
            GL.UniformMatrix4(GL.GetUniformLocation(ID_SHADER, "projectionMatrix"), false, ref projectionMatrix);
            world.draw();
        }



        private int compileShaders()
        {
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, File.ReadAllText(@"assets\shaders\vertexShader.glsl"));
            GL.CompileShader(vertexShader);
            handleShaderError(vertexShader);



            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, File.ReadAllText(@"assets\shaders\fragmentShader.glsl"));
            GL.CompileShader(fragmentShader);
            handleShaderError(fragmentShader);

            var program = GL.CreateProgram();
            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);
            GL.LinkProgram(program);

            return program;
        }

        private  void handleGLError()
        {
            var error = GL.GetError();
            if (error != ErrorCode.NoError)
            {
                throw new RetinaGLErrorException("GL ERROR: " + error.ToString());
            }
        }

        private void handleShaderError(int shader)
        {
            String shaderInfo = "";
            shaderInfo = GL.GetShaderInfoLog(shader);
            if (shaderInfo != "")
            {
                throw new RetinaGLErrorException("Could not compile shader:\n" + shaderInfo);
            }
        }
    }
}
