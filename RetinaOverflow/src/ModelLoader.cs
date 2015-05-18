using System;
using System.IO;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using NUnit.Framework;
using OpenTK;
using ObjLoader.Loader.Loaders;
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

            theModel.vertexBuffer = new Vector3[result.Vertices.Count];
            for (var i = 0;i < result.Vertices.Count; i++ )
            {
                theModel.vertexBuffer[i] = new Vector3(result.Vertices[i].X, result.Vertices[i].Y, result.Vertices[i].Z);
            }

            return theModel;
        }
            


    }
}

