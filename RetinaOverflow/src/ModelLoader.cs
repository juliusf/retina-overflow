using System;
using System.IO;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using OpenTK;
using ObjLoader.Loader.Loaders;
using ObjLoader.Loader.Data;
using ObjLoader.Loader.Data.Elements;


namespace RetinaOverflow
{
    public class ModelLoader
    {

        private  enum ParserState{PARSING_VERTS, PARSING_NORMS, PARSING_FACES, PARSING_UV};
        public ModelLoader()
        {



        }

        public Model loadModel(String modelFile)
        {   
            var theModel = new Model();
            
            var filePath = Path.Combine(Config.assetFolder, modelFile);

            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();
            var fileStream = new FileStream(filePath, FileMode.Open);
            var result = objLoader.Load(fileStream);
            var buffer = new List<float>();
            theModel.vertexBuffer = new Vector3[result.Vertices.Count];
            for (var i = 0;i < result.Vertices.Count; i++ )
            {
                theModel.vertexBuffer[i] = new Vector3(result.Vertices[i].X, result.Vertices[i].Y, result.Vertices[i].Z);
            }


            foreach (var grp in result.Groups)
            {
                foreach (var face in grp.Faces)
                {   
                    if (face.Count == 3)
                    {
                        for (var i = 0; i < face.Count; i++)
                        {
                            var theVertex = result.Vertices[face[i].VertexIndex - 1];
                            var theNormal = result.Normals[face[i].NormalIndex - 1];
                            buffer.Add(theVertex.X);
                            buffer.Add(theVertex.Y);
                            buffer.Add(theVertex.Z);

                            buffer.Add(theNormal.X);
                            buffer.Add(theNormal.Y);
                            buffer.Add(theNormal.Z);
                        }
                    }
                    else if (face.Count == 4) // quads
                    {
                        
                        for (var i = 0; i < 3 ; i++)
                        {
                            var theVertex = result.Vertices[face[i].VertexIndex - 1];
                            var theNormal = result.Normals[face[i].NormalIndex -1 ];
                            buffer.Add(theVertex.X);
                            buffer.Add(theVertex.Y);
                            buffer.Add(theVertex.Z);

                            buffer.Add(theNormal.X);
                            buffer.Add(theNormal.Y);
                            buffer.Add(theNormal.Z);
                        }

                        for (var i = 2; i <= 4 ; i++)
                        {
                            var idx = i == 4 ? 0 : i; // hack to correctly choose last face if we're using quads
                            var theVertex = result.Vertices[face[idx].VertexIndex -1];
                            var theNormal = result.Normals[face[idx].NormalIndex -1];
                            buffer.Add(theVertex.X);
                            buffer.Add(theVertex.Y);
                            buffer.Add(theVertex.Z);

                            buffer.Add(theNormal.X);
                            buffer.Add(theNormal.Y);
                            buffer.Add(theNormal.Z);
                        }
                    }
                }
            }

            theModel.normals = new Vector3[result.Normals.Count];
            theModel.buffer = new float[buffer.Count];
            theModel.bufferSize = buffer.Count;
            buffer.CopyTo(theModel.buffer);

            return theModel;
        }
            


    }
}

