using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace RetinaOverflow
{
    public class Mesh 
    {
        public class Subset
        {
            public int vertexStart;
            public int vertexCount;
            public int faceStart;
            public int faceCount;
        }

        private uint[] vertexBuffer;
        private uint[] indexBuffer;
        private int vertexStride;

        private List<Subset> subsetTable;

        public string name;
        public string materialName;
        public string materialFile;

        public Mesh()
        {
        }
    }
}

