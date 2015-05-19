namespace RetinaOverflow
{
	namespace Transform
	{
		using System;
		public interface ITransform
		{
			int getTransformation();
		}

		public static class TransformExtension
		{
			public static string getViewDirection(this ITransform theTransform, string foo)
			{
				return foo + theTransform.getTransformation().ToString();
			}
		}
	}
}