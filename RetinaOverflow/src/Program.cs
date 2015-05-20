﻿using System;
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
		public static void Main (string[] args)
		{
			GlobalManager.instance.logging.info("Retina Renderer starting");
            ModelLoader loader = new ModelLoader();
            var box = loader.loadModel("meshes/box.obj");
			var foo = new Quaternion();
            var camera = new Camera();
            GlobalManager.instance.logging.info(String.Format("View: {0}, {1}, {2}", camera.getViewDirection()[0], camera.getViewDirection()[1], camera.getViewDirection()[2]));
            //camera.move(new Vector3(-))

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
                    Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)game.Width / (float)game.Height, 1.0f, 64.0f);
                    GL.MatrixMode(MatrixMode.Projection);
                    GL.LoadMatrix(ref projection);
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
                        GL.Enable(EnableCap.CullFace);
                        GL.MatrixMode(MatrixMode.Modelview);
                        var viewMatrix = camera.getViewMatrix();
                        GL.LoadMatrix(ref viewMatrix);
					GL.Ortho(-10.0, 10.0, -10.0, 10.0, 00.0, 4.0);
                    GL.Color3(1.0f,0,0);
					GL.Begin(PrimitiveType.Triangles);
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
				game.Run(60.0);
			}
		}
	}
}
