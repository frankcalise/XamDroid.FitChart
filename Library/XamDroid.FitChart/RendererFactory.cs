using System;
using Android.Graphics;

namespace com.frankcalise.widgets
{
	public class RendererFactory
	{
		public static IRenderer Renderer(AnimationMode mode, FitChartValue value, RectF drawingArea)
		{
			if (mode == AnimationMode.Linear)
			{
				return new LinearValueRenderer (drawingArea, value);
			}
			else
			{
				return new OverdrawValueRenderer (drawingArea, value);
			}
		}
	}
}

