using System;
using Android.Graphics;

namespace com.frankcalise.widgets
{
	public class LinearValueRenderer : BaseRenderer, IRenderer
	{
		public LinearValueRenderer (RectF drawingArea, FitChartValue value) : base(drawingArea, value)
		{
			
		}

		#region IRenderer implementation

		public Path BuildPath (float animationProgress, float animationSeek)
		{
			Path path = null;
			if (base.Value.StartAngle <= animationSeek)
			{
				path = new Path ();
				var sweepAngle = CalculateSweepAngle (animationSeek, base.Value);
				path.AddArc (base.DrawingArea, base.Value.StartAngle, sweepAngle);
			}

			return path;
		}

		#endregion

		private float CalculateSweepAngle(float animationSeek, FitChartValue value)
		{
			float result;
			var totalSizeOfValue = value.StartAngle + value.SweepAngle;

			if (totalSizeOfValue > animationSeek) {
				result = animationSeek - value.StartAngle;
			}
			else 
			{
				result = value.SweepAngle;
			}

			return result;	
		}
	}
}

