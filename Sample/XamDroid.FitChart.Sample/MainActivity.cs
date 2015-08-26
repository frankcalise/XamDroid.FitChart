using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using com.frankcalise.widgets;

namespace XamDroid.FitChart.Sample
{
	[Activity (Label = "XamDroid.FitChart.Sample", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var fitChart = FindViewById<com.frankcalise.widgets.FitChart> (Resource.Id.FitChart);
			fitChart.MinValue = 0f;
			fitChart.MaxValue = 100f;

			var addButton = FindViewById<Button> (Resource.Id.AddButton);
			addButton.Click += (sender, e) => 
			{
				var values = new List<FitChartValue>();
				values.Add(new FitChartValue(30f, Resources.GetColor(Resource.Color.chart_value_1)));
				values.Add(new FitChartValue(20f, Resources.GetColor(Resource.Color.chart_value_2)));
				values.Add(new FitChartValue(15f, Resources.GetColor(Resource.Color.chart_value_3)));
				values.Add(new FitChartValue(10f, Resources.GetColor(Resource.Color.chart_value_4)));

				fitChart.SetValues(values);
			};
		}
	}
}


