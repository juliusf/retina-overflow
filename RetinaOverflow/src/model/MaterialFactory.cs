using System;

using ObjLoader.Loader;

namespace RetinaOverflow
{
    public static class MaterialFactory
    {
        public static Material createMaterial(ObjLoader.Loader.Data.Material loaderMaterial)
        {
            var material = new Material();
            material.name = loaderMaterial.Name;
            material.diffuseTexturePath = loaderMaterial.DiffuseTextureMap;
            return material;
        }
    }
}

