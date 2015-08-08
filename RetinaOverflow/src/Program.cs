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

namespace RetinaOverflow
{
    class RetinaOverflow
    {
        public static void Main(string[] args)
        {
            GlobalManager.instance.logging.info("Retina Renderer starting");
            ModelLoader loader = new ModelLoader();
            var box = loader.loadModel("meshes/sponza.obj");
            var camera = new Camera();
            double lastFrameTime = 0;
            double timeSinceLastUpdate = 0;
            GlobalManager.instance.logging.info(String.Format("View: {0}, {1}, {2}", camera.getViewDirection()[0], camera.getViewDirection()[1], camera.getViewDirection()[2]));
            GlobalManager.instance.logging.info(String.Format("Up: {0}, {1}, {2}", camera.getUpVector()[0], camera.getUpVector()[1], camera.getUpVector()[2]));
            camera.move(new Vector3(100, 25, 0));
           // camera.lookAt(new Vector3(0, 0, 0));
            //camera.move(new Vector3(-))
            MouseState current;
            MouseState previous = Mouse.GetState();

            using (var game = new GameWindow())
            {
                game.Load += (sender, e) =>
                {
                    // setup settings, load textures, sounds
                    game.VSync = VSyncMode.On;
                };

                game.Resize += (sender, e) =>
                {
                    GL.Viewport(0, 0, game.Width, game.Height);
                    Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)game.Width / (float)game.Height, 1.0f, 3000.0f);
                    GL.Enable(EnableCap.DepthTest);
                    GL.MatrixMode(MatrixMode.Projection);
                    GL.LoadMatrix(ref projection);
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
                            float mouseSpeed = 0.01f * (float) e.Time;
                            int xdelta = current.X - previous.X;
                            int ydelta = current.Y - previous.Y;
                            int zdelta = current.Wheel - previous.Wheel;
                            camera.look(xdelta * mouseSpeed, ydelta * mouseSpeed);

                        }
                        previous = current;




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
                    GL.MatrixMode(MatrixMode.Modelview);
                    var viewMatrix = camera.getViewMatrix();
                    GL.LoadMatrix(ref viewMatrix);

                    //GL.Ortho(-10.0, 10.0, -10.0, 10.0, 00.0, 4.0);
                    GL.Color3(1.0f, 0, 0);

                    box.draw();
					

                    /* GL.Color3(Color.MidnightBlue);
                        GL.Vertex2(-1.0f, 1.0f);
                        GL.Color3(Color.SpringGreen);
                        GL.Vertex2(0.0f, -1.0f);
                        GL.Color3(Color.Ivory);
                        GL.Vertex2(1.0f, 1.0f); */
                    GL.End();
                    GL.PopMatrix();
                    game.SwapBuffers();
                };

                // Run the game at 60 updates per second
                game.Width = 1280;
                game.Height = 720;
                game.Title = "Retina Overflow";
                game.CursorVisible = false;
                game.Run();

            }
        }
    }
}
