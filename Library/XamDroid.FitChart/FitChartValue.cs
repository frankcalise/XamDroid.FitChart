using System;
using Android.Graphics;

namespace com.frankcalise.widgets
{
	public class FitChartValue
	{
		public float Value { get; private set; }

		public Color Color { get; private set; }

		private Paint _paint;
		public Paint Paint
		{
			get
			{
				return _paint;
			} 
			set
			{
				_paint = value;
				_paint.Color = Color;
			}
		}

		public float StartAngle { get; set; }

		public float SweepAngle { get; set; }

		public FitChartValue(float value, Color color)
		{
			Value = value;
			Color = color;
		}
	}
}

