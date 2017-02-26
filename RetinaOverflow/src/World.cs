﻿using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
namespace RetinaOverflow
{
    public class World
    {
        private List<Model> scene; // TODO: Convert to octree or similar fast data Structure
        private ModelLoader loader;

        public Dictionary<String, Material> materials;

        public static Camera activeCam;

        public World()
        {
            scene = new List<Model>();
            loader = new ModelLoader(this);
            materials = new Dictionary<String, Material>();
        }

        public void initializeWorld()
        {
            foreach (var model in scene)
            {
                model.initialize();
            }

            foreach (var material in materials.Values)
            {
                material.initialize();
            }

            var foo = GL.IsVertexArray(1);
        }

        public void addModel(ref Model model)
        {
            scene.Add(model);
        }

        public void addModel(String modelPath)
        {
            scene.Add(loader.loadModel(modelPath));  
        }

        public void draw()
        {
            foreach (var model in scene)
            {
                model.draw();
            }
        }
    }
}

