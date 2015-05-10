using System;
using System.IO;
using Assimp;
using Assimp.Configs;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using NUnit.Framework;
using OpenTK;

namespace RetinaOverflow
{
    public class ModelLoader
    {

        private  enum ParserState{PARSING_VERTS, PARSING_NORMS, PARSING_FACES, PARSING_UV};
        public ModelLoader()
        {
        }

        public List<Mesh> loadModel(String modelFile)
        {   
            var meshes = new List<Mesh>();
            
            var filePath = Path.Combine(Config.assetFolder, modelFile);
            StreamReader file = new StreamReader(filePath);
            String currentLine;
            int line = 0;
            var currentMesh = new Mesh();
            var state = ParserState.PARSING_VERTS;
            var vertexList = new List<Vector3>();

            while ((currentLine = file.ReadLine()) != null)
            {
                line++;
                string[] tokens = currentLine.Split();
           
                switch (tokens[0])
                {
                    case "#": //comment
                        break;
                    case "v": //vertex
                        if (state == ParserState.PARSING_FACES) // create new mesh
                        {
                            
                        }
                        vertexList.Add(stringToVec3(tokens, line));
                        state = ParserState.PARSING_VERTS;
                        break;
                    case "vn"://normals
                        state = ParserState.PARSING_NORMS;
                        break;
                    case "f": //faces
                        state = ParserState.PARSING_FACES;
                        break;
                    case "mtllib": //material file
                        currentMesh.materialFile = tokens[1];
                        break;
                    case "usemtl": // material name
                        currentMesh.materialName = tokens[1];
                        break;
                    case "o":
                        meshes.Add(currentMesh);
                        currentMesh = new Mesh();
                        currentMesh.name = tokens[1];
                        break;
                    case "s": // smooth shading
                        break;
                    default:
                        throw new ModelLoaderException(String.Format("Unknown parser token {0} at Line {1}", tokens[0], line));
                     
                }

            }
            return meshes;
        }

        private Vector3 stringToVec3(string[] input, int lineNumber)
        {
            var theVector = new Vector3();
            if (input.Length != 4)
            {
                throw new ModelLoaderException(String.Format("Invalid number of floats occured in vec3 at Line {0}", lineNumber));
            }
            theVector.X = Convert.ToSingle(input[1]);
            theVector.Y = Convert.ToSingle(input[2]);
            theVector.Z = Convert.ToSingle(input[3]);

            return theVector;
        }


    }
}

