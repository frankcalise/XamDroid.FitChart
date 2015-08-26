using System;
using System.Collections.Generic;

using Android.Content;
using Android.Views;
using Android.Util;
using Android.Graphics;
using Android.Animation;
using Android.Views.Animations;

using Java.Interop;

namespace com.frankcalise.widgets
{
	public class FitChart : View
	{
		const int AnimationDuration = 1000;
		const int AnimationModeLinear = 0;
		const int AnimationModeOverdraw = 1;
		const int DefaultViewRadius = 0;
		const int DefaultMinValue = 0;
		const int DefaultMaxValue = 100;
		const int DesignModeSweepAngle = 216;
		const float InitialAnimationProgress = 0.0f;
		const float MaximumSweepAngle = 360f;

		public const float StartAngle = -90;

		public float MinValue { get; set; }

		public float MaxValue { get; set; }

        public AnimationMode AnimationMode { get; set; }

		List<FitChartValue> chartValues;

		float animationProgress = FitChart.InitialAnimationProgress;
		float maxSweepAngle = FitChart.MaximumSweepAngle;
		Paint backStrokePaint;
		Paint valueDesignPaint;
		RectF drawingArea;
		Color backStrokeColor;
		Color valueStrokeColor;
		float strokeSize;

		public FitChart (Context context) : base(context)
		{
			InitializeView (null);	
		}

		public FitChart(Context context, IAttributeSet attrs) : base(context, attrs)
		{
			InitializeView (attrs);
		}

        public FitChart(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {
            InitializeView(attrs);
        }

		public void SetValue(float value)
		{
			chartValues.Clear ();

			var chartValue = new FitChartValue (value, valueStrokeColor);
			chartValue.Paint = BuildPaintForValue();
			chartValue.StartAngle = StartAngle;
			chartValue.SweepAngle = CalculateSweepAngle(value);

			chartValues.Add(chartValue);

			maxSweepAngle = chartValue.SweepAngle;

			PlayAnimation();
		}

		public void SetValues(List<FitChartValue> values)
		{
			chartValues.Clear ();
			maxSweepAngle = 0;
			var offsetSweetAngle = StartAngle;

			foreach (var chartValue in values)
			{
				var sweepAngle = CalculateSweepAngle (chartValue.Value);
				chartValue.Paint = BuildPaintForValue();
				chartValue.StartAngle = offsetSweetAngle;
				chartValue.SweepAngle = sweepAngle;
				chartValues.Add (chartValue);

				offsetSweetAngle += sweepAngle;
				maxSweepAngle += sweepAngle;
			}

			PlayAnimation ();
		}

        [Export]
        private void setAnimationSeek(float value)
        {
            animationProgress = value;

            base.Invalidate();
        }

		private Paint BuildPaintForValue()
		{
			var paint = GetPaint ();
			paint.SetStyle (Paint.Style.Stroke);
			paint.StrokeWidth = strokeSize;
			paint.StrokeCap = Paint.Cap.Round;

			return paint;
		}

		private void InitializeView(IAttributeSet attrs) 
		{
            AnimationMode = AnimationMode.Linear;
            MaxValue = DefaultMaxValue;
            MinValue = DefaultMinValue;

			chartValues = new List<FitChartValue> ();
			InitializeBackground ();
			ReadAttributes (attrs);
			PreparePaints ();

            
		}

		private void InitializeBackground()
		{
			if (!IsInEditMode)
			{
				if (Background == null)
				{
					SetBackgroundColor (Context.Resources.GetColor (Resource.Color.default_back_color));
				}
			}
		}

		private void CalculateDrawableArea()
		{
			var drawPadding = strokeSize / 2;
			var width = (float)Width;
			var height = (float)Height;
			var left = drawPadding;
			var top = drawPadding;
			var right = width - drawPadding;
			var bottom = height - drawPadding;

			drawingArea = new RectF(left, top, right, bottom);
		}

		private void ReadAttributes(IAttributeSet attrs)
		{
			valueStrokeColor = Context.Resources.GetColor (Resource.Color.default_chart_value_color);
			backStrokeColor = Context.Resources.GetColor (Resource.Color.default_back_stroke_color);
			strokeSize = Context.Resources.GetDimension (Resource.Dimension.default_stroke_size);

			if (attrs != null) 
			{
				var attributes = Context.Theme.ObtainStyledAttributes (attrs, Resource.Styleable.FitChart, 0, 0);
				strokeSize = attributes.GetDimensionPixelSize (Resource.Styleable.FitChart_strokeSize, (int)strokeSize);
				valueStrokeColor = attributes.GetColor (Resource.Styleable.FitChart_valueStrokeColor, valueStrokeColor);
				backStrokeColor = attributes.GetColor (Resource.Styleable.FitChart_backStrokeColor, backStrokeColor);

				var attrAnimationMode = attributes.GetInt (Resource.Styleable.FitChart_animationMode, AnimationModeLinear);
				if (attrAnimationMode == AnimationModeLinear)
				{
					AnimationMode = AnimationMode.Linear;
				}
				else 
				{
					AnimationMode = AnimationMode.Overdraw;
				}

				attributes.Recycle ();
			}
		}

		private void PreparePaints()
		{
			backStrokePaint = GetPaint ();
			backStrokePaint.Color = backStrokeColor;
			backStrokePaint.SetStyle (Paint.Style.Stroke);
			backStrokePaint.StrokeWidth = strokeSize;

			valueDesignPaint = GetPaint ();
			valueDesignPaint.Color = valueStrokeColor;
			valueDesignPaint.SetStyle (Paint.Style.Stroke);
			valueDesignPaint.StrokeCap = Paint.Cap.Round;
			valueDesignPaint.StrokeWidth = strokeSize;
		}

		private Paint GetPaint() 
		{
			if (!IsInEditMode)
			{
				return new Paint (PaintFlags.AntiAlias);
			}
			else
			{
				return new Paint ();
			}
		}

		private float GetViewRadius()
		{
			if (drawingArea != null)
			{
				return (drawingArea.Width() / 2);
			}
			else
			{
				return DefaultViewRadius;
			}
		}

		protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
		{
			base.OnSizeChanged (w, h, oldw, oldh);
			CalculateDrawableArea ();
		}

		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
		{
			base.OnMeasure (widthMeasureSpec, heightMeasureSpec);
			var size = Math.Max (MeasuredWidth, MeasuredHeight);
			SetMeasuredDimension (size, size);

		}

		protected override void OnDraw(Canvas canvas)
		{
			base.OnDraw (canvas);

			RenderBack (canvas);
			RenderValues (canvas);
		}

		private void RenderBack(Canvas canvas)
		{
			var Path = new Path();
			var viewRadius = GetViewRadius ();
			Path.AddCircle (drawingArea.CenterX(), drawingArea.CenterY(), viewRadius, Path.Direction.Ccw);
			canvas.DrawPath (Path, backStrokePaint);
		}

		private void RenderValues(Canvas canvas)
		{
			if (!IsInEditMode)
			{
				var valuesCounter = chartValues.Count - 1;
				for (var index = valuesCounter; index >= 0; index--) 
				{
					RenderValue (canvas, chartValues [index]);
				}
			}
			else 
			{
				RenderValue (canvas, null);
			}
		}

		private void RenderValue(Canvas canvas, FitChartValue value)
		{
			if (!IsInEditMode)
			{
				var animationSeek = CalculateAnimationSeek ();
				var renderer = RendererFactory.Renderer (AnimationMode, value, drawingArea);
				var path = renderer.BuildPath (animationProgress, animationSeek);

				if (path != null) 
				{
					canvas.DrawPath (path, value.Paint);
				}
			}
			else 
			{ 
				var path = new Path ();
				path.AddArc (drawingArea, StartAngle, DesignModeSweepAngle);
				canvas.DrawPath (path, valueDesignPaint);
			}

		}

		private float CalculateAnimationSeek()
		{
			return ((MaximumSweepAngle * animationProgress) + StartAngle);
		}

		private float CalculateSweepAngle(float value) 
		{
			var chartValuesWindow = Math.Max (MinValue, MaxValue) - Math.Min (MinValue, MaxValue);
			var chartValuesScale = (360.0f / chartValuesWindow);

			return (value * chartValuesScale);
		}

		private void PlayAnimation()
		{
			var animator = ObjectAnimator.OfFloat (this, "animationSeek", 0.0f, 1.0f);
			var animatorSet = new AnimatorSet ();
			animatorSet.SetDuration (AnimationDuration);
			animatorSet.SetInterpolator (new DecelerateInterpolator ());
			animatorSet.SetTarget (this);
            animatorSet.Play(animator);
			animatorSet.Start();
		}
	}
}

