using OpenTK;

namespace RetinaOverflow
{
	namespace Transform
	{
		using System;
        using OpenTK.Graphics;
		public interface ITransformable
		{
			Transformation getTransformation();
		}

		public static class TransformExtension
		{
			public static void move(this ITransformable transform, Vector3 direction)
			{
                transform.getTransformation().position = Vector3.Add(transform.getTransformation().position, direction);
			}

            public static void rotate(this ITransformable transform, Quaternion rot)
            {
                Quaternion.Multiply(transform.getTransformation().rotation, rot);
            }

            public static Vector3 getPosition(this ITransformable transform)
            {
                return transform.getPosition();
            }

            public static Vector3 getWorldPosition(this ITransformable transform)
            {
                var transformation = transform.getTransformation();

                if (transformation.parent == null)
                {
                    return transformation.position;
                }
                else
                {
                    return Vector3.Add(transformation.position, transformation.parent.getWorldPosition());
                }
            }

            public static Matrix4 getWorldRotation(this ITransformable transform)
            {
                var trans = transform.getTransformation();

                if (trans.parent == null)
                {
                    return Matrix4.CreateFromQuaternion(trans.rotation);
                }
                else
                {
                    return Matrix4.Mult(trans.toMat4(), trans.parent.getWorldRotation());
                }

            }

            public static Matrix4 toMat4(this ITransformable transform)
            {
                var trans = transform.getTransformation();

                return Matrix4.Mult(Matrix4.CreateFromQuaternion(trans.rotation), Matrix4.CreateTranslation(trans.position));
            }
		}

        public class Transformation
        {
            public Vector3 position
            {
                get;
                set;
            }

            public Quaternion rotation
            {
                get;
                set;
            }

            public ITransformable parent
            {
                get;
                set;
            }
        }
	}
}