using System;
using System.IO;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Diagnostics;
using OpenTK;
using ObjLoader.Loader.Loaders;
using ObjLoader.Loader.Data;
using ObjLoader.Loader.Data.Elements;


namespace RetinaOverflow
{
    public class ModelLoader
    {
        private World world;
        public ModelLoader(World world)
        {
            this.world = world;


        }

        public Model loadModel(String modelFile)
        {   
            var theModel = new Model();
            theModel.name = modelFile;
            var filePath = Path.Combine(Config.assetFolder, modelFile);

            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();
            var fileStream = new FileStream(filePath, FileMode.Open);
            var result = objLoader.Load(fileStream);


            var channels = new HashSet<BufferChannels>();
            channels.Add(BufferChannels.POSITION);
            channels.Add(BufferChannels.NORMALS);
            channels.Add(BufferChannels.TEXCOORDS);

            Dictionary<String, Material> materialSet = new Dictionary<String, Material>();

            foreach (var grp in result.Groups)
            {	
                var buffer = new List<float>();
                foreach (var face in grp.Faces)
                {   
                    if (face.Count == 3)
                    {
                        for (var i = 0; i < face.Count; i++)
                        {
                            var theVertex = result.Vertices[face[i].VertexIndex - 1];
                            var theNormal = result.Normals[face[i].NormalIndex - 1];
                            var theTexCoord = result.Textures[face[i].TextureIndex -1];

                            buffer.Add(theVertex.X);
                            buffer.Add(theVertex.Y);
                            buffer.Add(theVertex.Z);

                            buffer.Add(theNormal.X);
                            buffer.Add(theNormal.Y);
                            buffer.Add(theNormal.Z);

                            buffer.Add(theTexCoord.X);
                            buffer.Add(1 - theTexCoord.Y);
                        }
                    }
                    else if (face.Count == 4) // quads
                    {
                        
                        for (var i = 0; i < 3; i++)
                        {
                            var theVertex = result.Vertices[face[i].VertexIndex - 1];
                            var theNormal = result.Normals[face[i].NormalIndex - 1];
                            var theTexCoord = result.Textures[face[i].TextureIndex -1];

                            buffer.Add(theVertex.X);
                            buffer.Add(theVertex.Y);
                            buffer.Add(theVertex.Z);

                            buffer.Add(theNormal.X);
                            buffer.Add(theNormal.Y);
                            buffer.Add(theNormal.Z);

                            buffer.Add(theTexCoord.X);
                            buffer.Add(1 - theTexCoord.Y);
                        }

                        for (var i = 2; i <= 4; i++)
                        {
                            var idx = i == 4 ? 0 : i; // hack to correctly choose last face if we're using quads
                            var theVertex = result.Vertices[face[idx].VertexIndex - 1];
                            var theNormal = result.Normals[face[idx].NormalIndex - 1];
                            var theTexCoord = result.Textures[face[idx].TextureIndex -1];

                            buffer.Add(theVertex.X);
                            buffer.Add(theVertex.Y);
                            buffer.Add(theVertex.Z);

                            buffer.Add(theNormal.X);
                            buffer.Add(theNormal.Y);
                            buffer.Add(theNormal.Z);

                            buffer.Add(theTexCoord.X);
                            buffer.Add(1 - theTexCoord.Y);
                        }
                    }

                }
                var dataBuffer = new DataBuffer(channels, buffer.Count / 8);
                buffer.CopyTo(dataBuffer.theBuffer);
                var mesh = new Mesh(ref dataBuffer);


                mesh.name = grp.Name;
                GlobalManager.instance.logging.info(String.Format("loading Mesh: {0}", grp.Name));
                if (! world.materials.ContainsKey(grp.Material.Name))
                {
                    world.materials.Add(grp.Material.Name, MaterialFactory.createMaterial(grp.Material));
                }

                mesh.material = world.materials[grp.Material.Name];

                theModel.addMesh(ref mesh);



            }
            return theModel;
        }
            


    }
}

