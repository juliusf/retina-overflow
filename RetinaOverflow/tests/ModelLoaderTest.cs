using NUnit.Framework;
using System;

namespace RetinaOverflow
{
    [TestFixture()]
    public class ModelLoaderTest
    {
        [Test()]
        public void LoadSimpleModel()
        {
            ModelLoader loader = new ModelLoader();
            var meshes = loader.loadModel("meshes/box.obj");

            Assert.AreEqual(meshes.Count, 1);
        }
    }
}

