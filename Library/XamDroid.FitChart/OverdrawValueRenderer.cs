using System;
using Android.Graphics;

namespace com.frankcalise.widgets
{
	public class OverdrawValueRenderer : BaseRenderer, IRenderer
	{
		public OverdrawValueRenderer (RectF drawingArea, FitChartValue value) : base(drawingArea, value)
		{
		}

		#region IRenderer implementation

		public Path BuildPath (float animationProgress, float animationSeek)
		{
			var startAngle = FitChart.StartAngle;
			var valueSweepAngle = base.Value.StartAngle + base.Value.SweepAngle;
			valueSweepAngle -= startAngle;
			var sweepAngle = valueSweepAngle * animationProgress;

			var path = new Path ();
			path.AddArc (base.DrawingArea, startAngle, sweepAngle);

			return path;
		}

		#endregion
	}
}

