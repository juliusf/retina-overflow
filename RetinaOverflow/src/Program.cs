using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;
using System.Text.RegularExpressions;
using OpenTK.Input;
using ObjLoader.Loader.Data;
using RetinaOverflow.Transform;
using System.Windows.Forms;
using System.IO;

namespace RetinaOverflow
{
    class RetinaOverflow
    {
        public static void Main(string[] args)
        {
            GlobalManager.instance.logging.info("Retina Renderer starting");
            var theWorld = new World();
            theWorld.addModel("meshes/sponza.obj");
            var camera = new Camera();
            World.activeCam = camera;

            double lastFrameTime = 0;
            double timeSinceLastUpdate = 0;

            MouseState current;
            MouseState previous = Mouse.GetState();

            Matrix4 projectionMatrix = Matrix4.Identity;
            int shaderProgram = 0;

            using (var game = new GameWindow())
            {
                game.Load += (sender, e) =>
                {
                    // setup settings, load textures, sounds
                    game.VSync = VSyncMode.On;
                    shaderProgram = compileShaders();
                    theWorld.initializeWorld();

                };

                game.Resize += (sender, e) =>
                {
                    GL.Viewport(0, 0, game.Width, game.Height);
                    projectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)game.Width / (float)game.Height, 1.0f, 3000.0f);
                    GL.Enable(EnableCap.DepthTest);
                };

                game.UpdateFrame += (sender, e) =>
                {
                    // add game logic, input handling
                    float movementSpeed = 1000.0f * (float)e.Time;

                    if (game.Keyboard[Key.Escape])
                    {
                        game.Exit();
                    }

                    if (game.Keyboard[Key.W])
                    {
                        camera.move(Vector3.Multiply(camera.getViewDirection(), movementSpeed));
                    }

                    if (game.Keyboard[Key.S])
                    {
                        camera.move(Vector3.Multiply(-camera.getViewDirection(), movementSpeed));
                    }

                    if (game.Keyboard[Key.A])
                    {
                        camera.move(Vector3.Multiply(-camera.getRightVector(), movementSpeed));
                    }

                    if (game.Keyboard[Key.D])
                    {
                        camera.move(Vector3.Multiply(camera.getRightVector(), movementSpeed));
                    }
                        current = Mouse.GetState();
                        if (current != previous)
                        {
                            float mouseSpeed = 0.05f * (float) e.Time;
                            int xdelta = current.X - previous.X;
                            int ydelta = current.Y  - previous.Y;
                            int zdelta = current.Wheel - previous.Wheel;
                            camera.look(xdelta * mouseSpeed, ydelta * mouseSpeed);

                        }
                        previous = current;
                       // Mouse.SetPosition(game.Bounds.Left + game.Bounds.Width / 2, game.Bounds.Top + game.Bounds.Height / 2); // reset mouse to mid of screen in order to grab
                   

                };

                game.RenderFrame += (sender, e) =>
                {
                    if (lastFrameTime != 0)
                    {
                        double elapsedTime = e.Time * 0.1 + lastFrameTime * 0.9;
                        if (timeSinceLastUpdate > 0.5)
                        {
                            timeSinceLastUpdate = 0;
                            game.Title = String.Format("Retina Overflow - {0:0} FPS - {1:F2} ms", 1.0 / elapsedTime, elapsedTime);
                        }
                        else
                        {
                            timeSinceLastUpdate += e.Time;
                        }
                    }


                    lastFrameTime = e.Time;
                    // render graphics


                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                   
                    var viewMatrix = World.activeCam.getViewMatrix();
                    GL.UseProgram(shaderProgram);
                    handleGLError();
                    GL.UniformMatrix4(GL.GetUniformLocation(shaderProgram, "viewMatrix"), false, ref viewMatrix);
                    handleGLError();
                    GL.UniformMatrix4(GL.GetUniformLocation(shaderProgram, "projectionMatrix"), false, ref projectionMatrix);
                    handleGLError();


                    theWorld.draw();
                    handleGLError();
                    game.SwapBuffers();
                    handleGLError();
                };

                // Run the game at 60 updates per second
                game.Width = 1280;
                game.Height = 720;
                game.Title = "Retina Overflow";
                game.CursorVisible = false;


                var cursor = game.Cursor;
                game.Run();

            }
        }

        public static int compileShaders()
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
            //GL.DetachShader(program, vertexShader);
            //GL.DetachShader(program, fragmentShader);
            //GL.DeleteShader(vertexShader);
            //GL.DeleteShader(fragmentShader);

            return program;
        }

        public static void handleShaderError(int shader) {
                String shaderInfo = "";
                shaderInfo = GL.GetShaderInfoLog(shader);
                if (shaderInfo != "")
                {
                    GlobalManager.instance.logging.error(shaderInfo);
                }
        }

        public static void handleGLError() {
            var error = GL.GetError();
            if (error != ErrorCode.NoError) {

                throw new Exception("GL ERROR: " + error.ToString());
            }
        }
    }
}
