using System;

using ObjLoader.Loader;

namespace RetinaOverflow
{
    public static class MaterialFactory
    {
        public static Material createMaterial(ObjLoader.Loader.Data.Material loaderMaterial)
        {
            GlobalManager.instance.logging.info(String.Format("loading material: {0}", loaderMaterial.Name));
            var material = new Material();
            material.name = loaderMaterial.Name;
            material.diffuseTexturePath = loaderMaterial.DiffuseTextureMap;
            material.bumpTexturePath = loaderMaterial.BumpMap;
            material.specTexturePath = loaderMaterial.SpecularTextureMap;
            return material;
        }
    }
}

