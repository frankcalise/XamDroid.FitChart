using System;
using Android.Graphics;

namespace com.frankcalise.widgets
{
	public interface IRenderer
	{
		Path BuildPath(float animationProgress, float animationSeek);
	}
}

