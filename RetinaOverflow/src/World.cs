using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
namespace RetinaOverflow
{
    public class World
    {
        private List<Model> scene; // TODO: Convert to octree or similar fast data Structure
        private ModelLoader loader;

        public Dictionary<String, Material> materials;
        private Dictionary<String, int> modelNames;
        public static Camera activeCam;

        public World()
        {
            scene = new List<Model>();
            loader = new ModelLoader(this);
            materials = new Dictionary<String, Material>();
            modelNames = new Dictionary<string, int>();
        }

        public void initializeContent()
        {
            foreach (var model in scene)
            {
                model.initialize();
            }

            foreach (var material in materials.Values)
            {
                material.initialize();
            }
        }

        public void addModel(ref Model model)
        {
            scene.Add(model);
        }

        public void addModel(String modelPath)
        {
            GlobalManager.instance.logging.info(String.Format("adding Model: {0}", modelPath));
            var model = loader.loadModel(modelPath);
            modelNames.Add(modelPath, scene.Count);
            scene.Add(model);  
        }

        public Model getModelForName(String modelName)
        {
            int idx = 0;
            try
            {
                idx = modelNames[modelName];
            }
            catch (KeyNotFoundException)
            {
                GlobalManager.instance.logging.warning(String.Format("Could not find requested model: {0}", modelName));
            }
            
            return scene[idx];
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

