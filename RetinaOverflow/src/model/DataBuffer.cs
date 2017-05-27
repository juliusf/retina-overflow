using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using ObjLoader.Loader.Data;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;
using System.Security.Authentication.ExtendedProtection;
using System.Diagnostics;

namespace RetinaOverflow
{
    public class BufferChannels
    {
        private string name;

        private BufferChannels(string name, int size, int offset)
        {
            this.name = name;
            this.size = size;
            this.offset = offset;
        }

        public int size
        {
            get;
        }

        public int sizeInByte
        {
            get{ return this.size * 4;}
        }

        public int offset
        {
            get;
        }
            
        public static readonly BufferChannels POSITION = new BufferChannels("Position", 3, 0);
        public static readonly BufferChannels NORMALS = new BufferChannels("Normals", 3, BufferChannels.POSITION.size);
        public static readonly BufferChannels TEXCOORDS = new BufferChannels("TexCoords", 2, BufferChannels.POSITION.size + BufferChannels.NORMALS.size);

    }
    
    public class DataBuffer : IEnumerable
    {
        public  float[] theBuffer;
        public int bufferLength;
        public int bufferLengthInBytes
        {
            get{return bufferLength * 4;}
        }
        private HashSet<BufferChannels> dataChannelsInBuffer;

        public int stride = 0;
        public int strideInBytes
        {
            get{
                return stride * 4;
            }
        }
        public int size = 0;

        public DataBuffer(HashSet<BufferChannels> channels, int size)
        {
            this.dataChannelsInBuffer = channels;
            int strideLength = 0;


            foreach (var channel in channels)
            {
                strideLength += channel.size;
            }

            this.stride = strideLength;

            this.theBuffer = new float[size * stride];
            this.bufferLength = size * stride;
            this.size = size;
        }

        public List<float> getChannelItems()
        {
            return null;
        }

        public void writeToChannel(BufferChannels channel, List<Vector3> data)
        {
            Debug.Assert((data.Count <= bufferLength / stride), "The data list is too big for the Buffer");

            if (!this.dataChannelsInBuffer.Contains(channel))
            {
                Debug.Assert((false), "the buffer does not contain the channel desired");
            }

            for (int i = 0; i < data.Count; i++)
            {
                var item = data[i];
                var idx = i * this.stride + channel.offset;

                this.theBuffer[idx] = item[0];
                this.theBuffer[idx + 1] = item[1];
                this.theBuffer[idx + 2] = item[2];
            }
        }

        public List<Vector3> readToVec3(BufferChannels channel)
        {

            if (!this.dataChannelsInBuffer.Contains(channel))
            {
                Debug.Assert( (false), "the buffer does not contain the channel desired");
            }
            var ret = new List<Vector3>();

            for (int i = channel.offset; i < this.bufferLength; i += this.stride)
            {
                var vec = new Vector3();
                vec.X = this.theBuffer[i];
                vec.Y = this.theBuffer[i + 1];
                vec.Z = this.theBuffer[i + 2];
                ret.Add(vec);
            }

            return ret;
        }

        public IEnumerator<Vector3> GetPositions()
        {
            var channel = BufferChannels.POSITION;
            if (!this.dataChannelsInBuffer.Contains(channel))
            {
                Debug.Assert( (false), "the buffer does not contain the channel desired");
            }
            //var ret = new List<Vector3>();
            var vec = new Vector3();
            for (int i = channel.offset; i < this.bufferLength; i += this.stride)
            {
                
                vec.X = this.theBuffer[i];
                vec.Y = this.theBuffer[i + 1];
                vec.Z = this.theBuffer[i + 2];
                yield return vec;
            }
            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetPositions();
        }




          

    }
}

