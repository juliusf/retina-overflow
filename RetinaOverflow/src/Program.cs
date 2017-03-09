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
using RetinaOverflow.src.drawing;

namespace RetinaOverflow
{
    class RetinaOverflow
    {
        public static void Main(string[] args)
        {
            GlobalManager.instance.logging.info("Retina Renderer starting");
            var theWorld = new World();
            var renderer = GlobalManager.instance.renderer;
            theWorld.addModel("meshes/sponza.obj");
            
            var camera = new Camera();
            World.activeCam = camera;

            double lastFrameTime = 0;
            double timeSinceLastUpdate = 0;

            MouseState current;
            MouseState previous = Mouse.GetState();

            Mesh vase = theWorld.getModelForName("meshes/sponza.obj").getMeshForName("sponza_01");

            using (var game = new GameWindow())
            {
                game.Load += (sender, e) =>
                {
                    game.VSync = VSyncMode.On;
                    renderer.initialize(theWorld);
                };

                game.Resize += (sender, e) =>
                {
                    renderer.onResize(game.Width, game.Height);
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

                    if (game.Keyboard[Key.R])
                    {
                        vase.move(Vector3.Multiply(new Vector3(1,0,0), movementSpeed));
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
                    renderer.renderFrame();

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
