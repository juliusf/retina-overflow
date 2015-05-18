using NUnit.Framework;
using System;
using RetinaOverflow;

namespace RetinaOverflowTests
{
    [TestFixture()]
    public class ModelLoaderTest
    {
        [Test()]
        public void LoadSimpleModel()
        {
            ModelLoader loader = new ModelLoader();
            var theModel = loader.loadModel("meshes/box.obj");

            Assert.AreEqual(theModel.vertexBuffer.Length, 8);
        }
    }
}

