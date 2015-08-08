using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using OpenTK;
using RetinaOverflow.Transform;
using NUnit.Framework;
using RetinaOverflow.Drawable;

namespace RetinaOverflow
{
    public class Model : ITransformable, IDrawable
    {
        private Transformation transform;

        public string name;
        public string materialName;
        public string materialFile;
        public List<Mesh> meshes;
        public bool drawAxes = false;


        public Model()
        {
            this.transform = new Transformation();
            this.meshes = new List<Mesh>();
        }

        public void addMesh(ref Mesh mesh)
        {
            mesh.getTransformation().parent = this;
            meshes.Add(mesh);
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

