OxyPlot Rendering Performance Demo
===

Demo for the rendering performance problem described in SO

Reproduce the Problem with High Frequency Sine
---

* In MainWindow.xaml.cs make sure the line 34 is a comment:

```csharp
Plot.Model.Series.Add(BuildSeries(HIGH_FREQUENCY));
//Plot.Model.Series.Add(BuildSeries(LOW_FREQUENCY));
```

* Run the program
  * 20k HF sinus data points will be generated.
  * The rendering takes some seconds.
  * The plot area is almost covered with the signal.
  * Every action like zooming or panning is noticeably slow.

Prove That There Is No Problem Low Frequency Sine
---

* In MainWindow.xaml.cs make sure the line 33 is a comment:

```csharp
//Plot.Model.Series.Add(BuildSeries(HIGH_FREQUENCY));
Plot.Model.Series.Add(BuildSeries(LOW_FREQUENCY));
```

* Run the program
  * 20k LF sinus data points will be generated.
  * The rendering takes less than a second.
  * Most of the plot area is white. There is a sine line going from left to the right.
  * Actions like zooming and panning are fast.
