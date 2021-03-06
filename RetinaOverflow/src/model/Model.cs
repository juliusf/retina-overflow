﻿using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using OpenTK;
using RetinaOverflow.Transform;
using RetinaOverflow.Drawable;
using RetinaOverflow.src.util.Exception;

namespace RetinaOverflow
{
    public class Model : ITransformable, IDrawable
    {
        private Transformation transform;

        public string name;
        private string materialName;
        private string materialFile;
        private List<Mesh> meshes;
        private Dictionary<String, int> meshNames;
        private bool drawAxes = false;

        public Model()
        {
            this.transform = new Transformation();
            this.meshes = new List<Mesh>();
            this.meshNames = new Dictionary<string, int>();
        }

        public void addMesh(ref Mesh mesh)
        {
            mesh.getTransformation().parent = this;
            meshNames.Add(mesh.name, meshes.Count);
            meshes.Add(mesh);
        }

        public Mesh getMeshForName(String meshName)
        {
            int idx = -1;
            try {
              idx   = meshNames[meshName];
            }
            catch (KeyNotFoundException)
            {
                GlobalManager.instance.logging.error(String.Format("Could not find requested Mesh: {0}", meshName));
                throw new RetinaGLErrorException("mesh not found: " + meshName);
            }
            return meshes[idx];
        }

        public void initialize()
        {
            foreach (var mesh in meshes)
            {
                mesh.initialize();
            }
        }

        public Transformation getTransformation()
        {
            return this.transform;
        }

        public void draw()
        {
            foreach (var mesh in meshes)
            {
                mesh.draw();
                if (this.drawAxes)
                {
                    mesh.drawAxes();
                }
            }
        }



    }
}

