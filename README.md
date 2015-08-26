
XamDroid.FitChart
================

Ported and Maintained by:
Frank Calise ([@frankcalise](http://www.twitter.com/frankcalise))

Original FitChart by:
([Txus Ballesteros on GitHub](https://github.com/txusballesteros/fit-chart))

FitChart is an Android library that helps create a View similar to Google's Fit application to display a wheel chart.

## Demo
[View Demo Video on YouTube](https://youtu.be/dQvMh-rtKzk)

## Getting started

###Installing the library
Clone and add project or dll to your solution.


###Code
Adding a FitChart view by inserting it into your axml layout file
```xml
<com.frankcalise.widgets.FitChart
    style="@style/chart_style"
    android:layout_gravity="center"
    app:animationMode="overdraw"
    app:valueStrokeColor="#ff3d00"
    app:backStrokeColor="#f0f0f0"
    android:id="@+id/FitChart" />
```

Now in your activities `onCreate()` or your fragments `onCreateView()` you would want to do something like this
```
var fitChart = FindViewById<com.frankcalise.widgets.FitChart> (Resource.Id.FitChart);
fitChart.MinValue = 0f;
fitChart.MaxValue = 100f;

var values = new List<FitChartValue>();
values.Add(new FitChartValue(30f, Resources.GetColor(Resource.Color.chart_value_1)));
values.Add(new FitChartValue(20f, Resources.GetColor(Resource.Color.chart_value_2)));
values.Add(new FitChartValue(15f, Resources.GetColor(Resource.Color.chart_value_3)));
values.Add(new FitChartValue(10f, Resources.GetColor(Resource.Color.chart_value_4)));

fitChart.SetValues(values);
```

Add some value colors to your Colors.xml resouce file
```xml
<?xml version="1.0" encoding="UTF-8" ?>
<resources>
    <color name="chart_value_1">#2d4302</color>
    <color name="chart_value_2">#75a80d</color>
    <color name="chart_value_3">#8fc026</color>
    <color name="chart_value_4">#B5CC84</color>
</resources>
```

## Limitations
TODO

## Contributions
TODO

## Original License

    Copyright Txus Ballesteros 2015 (@txusballesteros)

    This file is part of some open source application.

    Licensed to the Apache Software Foundation (ASF) under one or more 
    contributor license agreements. See the NOTICE file distributed with
    this work for additional information regarding copyright ownership. 
    The ASF licenses this file to you under the Apache License, Version 2.0
    (the "License"); you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

        http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.

    Contact: Txus Ballesteros txus.ballesteros@gmail.com


## My License

    Copyright 2015 Frank Calise

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.