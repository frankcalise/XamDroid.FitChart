using System;
using Android.Graphics;

namespace com.frankcalise.widgets
{
	public abstract class BaseRenderer
	{
		public FitChartValue Value { get; private set; }

		public RectF DrawingArea { get; private set; }

		public BaseRenderer (RectF drawingArea, FitChartValue value)
		{
			DrawingArea = drawingArea;
			Value = value;
		}
	}
}

