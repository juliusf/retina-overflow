using System;
using System.Collections.Generic;

namespace RetinaOverflow
{
    public class World
    {
        private List<Model> scene; // TODO: Convert to octree or similar fast data Structure
        private ModelLoader loader;


        public static Camera activeCam;

        public World()
        {
            scene = new List<Model>();
            loader = new ModelLoader();            
        }

        public void initializeWorld()
        {
            foreach (var model in scene)
            {
                model.initialize();
            }
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

