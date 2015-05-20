using System;
using OpenTK;
using RetinaOverflow.Transform;

namespace RetinaOverflow
{
    public interface ICamera : ITransformable
    {
        Matrix4 getViewMatrix();
    }
}

