using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetinaOverflow.src.util.Exception
{
    class RetinaGLErrorException : RetinaException
    {
        public RetinaGLErrorException()
        {
        }

        public RetinaGLErrorException(string message) : base(message)
        {
        }
    }
}
