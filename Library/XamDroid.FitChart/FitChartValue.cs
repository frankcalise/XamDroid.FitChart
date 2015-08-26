using System;
using Android.Graphics;

namespace com.frankcalise.widgets
{
	public class FitChartValue
	{
		public float Value { get; private set; }

		public int Color { get; private set; }

		public Paint Paint { get; set; }

		public float StartAngle { get; set; }

		public float SweepAngle { get; set; }

		public FitChartValue(float value, int color)
		{
			Value = value;
			Color = color;
		}
	}
}

