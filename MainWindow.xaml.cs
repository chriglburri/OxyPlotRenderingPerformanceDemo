using OxyPlot;
using OxyPlot.Series;
using System;
using System.Linq;
using System.Windows;

namespace RenderingPerformance
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private const double HIGH_FREQUENCY = 1d;
    private const double LOW_FREQUENCY = 1d / 50000d;
    private const string X_AXIS_KEY = nameof(X_AXIS_KEY);
    private const string Y_AXIS_KEY = nameof(Y_AXIS_KEY);
    private const string LEGEND_KEY = nameof(LEGEND_KEY);
    private bool _useDecimator;

    public MainWindow()
    {
      InitializeComponent();
      Application.Current.MainWindow.WindowState = WindowState.Maximized;

      Plot.Model = new PlotModel();

      Plot.Model.Axes.Add(new OxyPlot.Axes.LinearAxis() { Title = "X-Axis", Position = OxyPlot.Axes.AxisPosition.Bottom, Key = X_AXIS_KEY });
      Plot.Model.Axes.Add(new OxyPlot.Axes.LinearAxis() { Title = "Y-Axis", Position = OxyPlot.Axes.AxisPosition.Left, Key = Y_AXIS_KEY });
      Plot.Model.Legends.Add(new OxyPlot.Legends.Legend() { LegendTitle = "Legend", Key = LEGEND_KEY });

      _useDecimator = true;
      // TODO: change the omega here for the 2 different test cases:
      Plot.Model.Series.Add(BuildSeries(HIGH_FREQUENCY));
      //Plot.Model.Series.Add(BuildSeries(LOW_FREQUENCY));

      Plot.InvalidatePlot();
    }

    private Series BuildSeries(double omega)
    {
      int count = 100_000;
      LineSeries series = new LineSeries
      {
        Title = $"{count} Points with ω={omega}",
        XAxisKey = X_AXIS_KEY,
        YAxisKey = Y_AXIS_KEY,
        LegendKey = LEGEND_KEY
      };
      series.Points.AddRange(
        Enumerable.Range(0, count)
        .Select(t => new DataPoint(t / 100d, Math.Sin(t * omega)))
        );
      if (_useDecimator)
      {
        series.Decimator = Decimator.Decimate;
      }
      return series;
    }
  }
}


