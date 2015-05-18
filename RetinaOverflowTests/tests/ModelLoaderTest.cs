using NUnit.Framework;
using System;
using RetinaOverflow;
using System.Collections.Generic;
using System.Reflection;
using OpenTK;

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

           // Assert.AreEqual(theModel.bu);
        }

        [Test()]
        public void testBuffer()
        {
            var channels = new HashSet<BufferChannels>();
            channels.Add(BufferChannels.POSITION);
            var buffer = new DataBuffer(channels, 10);

            Assert.AreEqual(30, buffer.bufferLength);
            Assert.AreEqual(3, buffer.stride);
           
            channels.Add(BufferChannels.NORMALS);
            buffer = new DataBuffer(channels, 10);
            Assert.AreEqual(60, buffer.bufferLength);
            Assert.AreEqual(6, buffer.stride);

            channels.Add(BufferChannels.TEXCOORDS);
            buffer = new DataBuffer(channels, 10);
            Assert.AreEqual(90, buffer.bufferLength);
            Assert.AreEqual(9, buffer.stride);

        } 
        [Test()]
        public void testReadBuffer()
        {
            //Reading of one channel
            var channels = new HashSet<BufferChannels>();
            channels.Add(BufferChannels.POSITION);
            var buffer = new DataBuffer(channels, 2);

            buffer.theBuffer = new float[]{ 1.0f, 1.0f, 1.0f, 2.0f, 2.0f, 2.0f };
            var result = buffer.readToVec3(BufferChannels.POSITION);
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result[0], new Vector3(1.0f, 1.0f, 1.0f));
            Assert.AreEqual(result[1], new Vector3(2.0f, 2.0f, 2.0f));

            //Reading of two channels
            channels.Add(BufferChannels.NORMALS);
            buffer = new DataBuffer(channels, 2);

            buffer.theBuffer = new float[]{ 1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 0.0f, 2.0f, 2.0f, 2.0f, 0.0f, 0.0f, 0.0f };
            result = buffer.readToVec3(BufferChannels.POSITION);
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result[0], new Vector3(1.0f, 1.0f, 1.0f));
            Assert.AreEqual(result[1], new Vector3(2.0f, 2.0f, 2.0f));

            result = buffer.readToVec3(BufferChannels.NORMALS);
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result[0], new Vector3(0.0f, 0.0f, 0.0f));
            Assert.AreEqual(result[1], new Vector3(0.0f, 0.0f, 0.0f));

            //Reading of three channels
            channels.Add(BufferChannels.TEXCOORDS);
            buffer = new DataBuffer(channels, 2);

            buffer.theBuffer = new float[]{ 1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 0.0f, -1.0f, -1.0f, -1.0f, 2.0f, 2.0f, 2.0f, 0.0f, 0.0f, 0.0f, -1.0f, -1.0f, -1.0f };
            result = buffer.readToVec3(BufferChannels.POSITION);
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result[0], new Vector3(1.0f, 1.0f, 1.0f));
            Assert.AreEqual(result[1], new Vector3(2.0f, 2.0f, 2.0f));

            result = buffer.readToVec3(BufferChannels.NORMALS);
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result[0], new Vector3(0.0f, 0.0f, 0.0f));
            Assert.AreEqual(result[1], new Vector3(0.0f, 0.0f, 0.0f));

            result = buffer.readToVec3(BufferChannels.TEXCOORDS);
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result[0], new Vector3(-1.0f, -1.0f, -1.0f));
            Assert.AreEqual(result[1], new Vector3(-1.0f, -1.0f, -1.0f));
        }
        [Test()]
        public void testReadAndWriteBuffer()
        {
            //Write one buffer channel
            var channels = new HashSet<BufferChannels>();
            channels.Add(BufferChannels.POSITION);
            var buffer = new DataBuffer(channels, 2);

            var positionList = new List<Vector3>();
            positionList.Add(new Vector3(1.0f, 1.0f, 1.0f));
            positionList.Add(new Vector3(1.0f, 1.0f, 1.0f));

            buffer.writeToChannel(BufferChannels.POSITION, positionList);
            Assert.AreEqual(buffer.readToVec3(BufferChannels.POSITION), positionList);

            //Write two buffer channels
            channels.Add(BufferChannels.NORMALS);
            buffer = new DataBuffer(channels, 2);

            var normalList = new List<Vector3>();
            normalList.Add(new Vector3(2.0f, 2.0f, 2.0f));
            normalList.Add(new Vector3(2.0f, 2.0f, 2.0f));

            buffer.writeToChannel(BufferChannels.POSITION, positionList);
            Assert.AreEqual(buffer.readToVec3(BufferChannels.POSITION), positionList);
    
            buffer.writeToChannel(BufferChannels.NORMALS, normalList);
            Assert.AreEqual(buffer.readToVec3(BufferChannels.NORMALS), normalList);

            //Write three buffer channels
            channels.Add(BufferChannels.TEXCOORDS);
            buffer = new DataBuffer(channels, 2);

            var textCoordList = new List<Vector3>();
            textCoordList.Add(new Vector3(3.0f, 3.0f, 3.0f));
            textCoordList.Add(new Vector3(.0f, 3.0f, 3.0f));

            buffer.writeToChannel(BufferChannels.POSITION, positionList);
            Assert.AreEqual(buffer.readToVec3(BufferChannels.POSITION), positionList);

            buffer.writeToChannel(BufferChannels.NORMALS, normalList);
            Assert.AreEqual(buffer.readToVec3(BufferChannels.NORMALS), normalList);

            buffer.writeToChannel(BufferChannels.TEXCOORDS, textCoordList);
            Assert.AreEqual(buffer.readToVec3(BufferChannels.TEXCOORDS), textCoordList);
        }
    }
}

