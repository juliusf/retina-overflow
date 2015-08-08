using System;
using RetinaOverflow;

namespace RetinaOverflowTests
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var modelLoaderTests = new ModelLoaderTest();
            modelLoaderTests.LoadSimpleModel();
            modelLoaderTests.testBuffer();
            modelLoaderTests.testReadBuffer();
            modelLoaderTests.testReadAndWriteBuffer();

            var mathTests = new MathTests();
            mathTests.testDirection();
            mathTests.testRotation();
        }
    }
}
