using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using OpenTK;

namespace RetinaOverflow
{
    public class Model 
    {
        public class Subset
        {
            public int vertexStart;
            public int vertexCount;
            public int faceStart;
            public int faceCount;
        }

        public Vector3[] vertexBuffer;
        public Vector3[] normals;
        public float[] buffer;
        public int bufferSize;
        public int vertexStride;

        private List<Subset> subsetTable;

        public string name;
        public string materialName;
        public string materialFile;

        public Model()
        {
        }
    }
}

