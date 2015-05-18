using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;
using System.Text.RegularExpressions;
using OpenTK.Input;
using ObjLoader.Loader.Data;


namespace RetinaOverflow
{
	class RetinaOverflow
	{
		public static void Main (string[] args)
		{
			GlobalManager.instance.logging.info("Retina Renderer starting");
            ModelLoader loader = new ModelLoader();
            var box = loader.loadModel("meshes/box.obj");
            var foo = new Quaternion();

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
				};

				game.UpdateFrame += (sender, e) =>
				{
					// add game logic, input handling
					if (game.Keyboard[Key.Escape])
					{
						game.Exit();
					}
				};

				game.RenderFrame += (sender, e) =>
				{
					// render graphics
					GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

					GL.MatrixMode(MatrixMode.Projection);
					GL.LoadIdentity();
					GL.Ortho(-10.0, 10.0, -10.0, 10.0, 00.0, 4.0);
                    GL.Color3(1.0f,0,0);
					GL.Begin(PrimitiveType.Triangles);
                        GlobalManager.instance.logging.error("New Frame");
                        for (int i = 0 ; i < box.bufferSize; i += 6){
                            GL.Color3(Color.MidnightBlue);
                            GL.Vertex3(box.buffer[i], box.buffer[i+1], box.buffer[i+2]);

                            GlobalManager.instance.logging.info(String.Format("{0} {1} {2}", box.buffer[i], box.buffer[i+1], box.buffer[i+2]));
                        } 
                       /* GL.Color3(Color.MidnightBlue);
                        GL.Vertex2(-1.0f, 1.0f);
                        GL.Color3(Color.SpringGreen);
                        GL.Vertex2(0.0f, -1.0f);
                        GL.Color3(Color.Ivory);
                        GL.Vertex2(1.0f, 1.0f); */
					GL.End();

					game.SwapBuffers();
				};

				// Run the game at 60 updates per second
				game.Run(60.0);
			}
		}
	}
}
