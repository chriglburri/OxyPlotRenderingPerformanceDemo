using CsvHelper;
using CsvHelper.Configuration;
using OxyPlot;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using OxyPlot.Wpf;

namespace ScopeDataDemo
{
  public class XamlPlotView : PlotView
  {
    protected override IRenderContext CreateRenderContext()
    {
      return new XamlRenderContext(this.Canvas);
    }
  }
  public partial class MainWindow : Window
  {
    private const string X_AXIS_KEY = nameof(X_AXIS_KEY);
    private const string Y_AXIS_KEY = nameof(Y_AXIS_KEY);
    private const string LEGEND_KEY = nameof(LEGEND_KEY);

    public MainWindow()
    {
      InitializeComponent();
      Application.Current.MainWindow.WindowState = WindowState.Maximized;

      Plot.Model = new PlotModel();

      Plot.Model.Axes.Add(new OxyPlot.Axes.LinearAxis() { Title = "X-Axis", Position = OxyPlot.Axes.AxisPosition.Bottom, Key = X_AXIS_KEY });
      Plot.Model.Axes.Add(new OxyPlot.Axes.LinearAxis() { Title = "Y-Axis", Position = OxyPlot.Axes.AxisPosition.Left, Key = Y_AXIS_KEY });
      Plot.Model.Legends.Add(new OxyPlot.Legends.Legend() { LegendTitle = "Legend", Key = LEGEND_KEY });

      Plot.Model.Series.Add(BuildSeries());

      Plot.InvalidatePlot();
    }

    private Series BuildSeries()
    {
      var config = new CsvConfiguration(CultureInfo.InvariantCulture)
      {
        HasHeaderRecord = false,
        Delimiter = ";"
      };
      IEnumerable<DataPoint> records;
      using (var reader = new StreamReader("./Data/ScopeData.csv"))
      using (var csv = new CsvReader(reader, config))
      {
        csv.Context.RegisterClassMap<FooMap>();
        records = csv.GetRecords<DataPoint>().ToList();
      }

      LineSeries series = new LineSeries
      {
        Title = $"Scope Data with 4096 data points",
        XAxisKey = X_AXIS_KEY,
        YAxisKey = Y_AXIS_KEY,
        LegendKey = LEGEND_KEY
      };
      series.ItemsSource = records;
      series.Decimator = Decimator.Decimate;
      return series;
    }

    public class FooMap : ClassMap<DataPoint>
    {
      public FooMap()
      {
        Map(m => m.X).Index(0);
        Map(m => m.Y).Index(1);
      }

    }

    public class DataPoint: IDataPointProvider
    {
      public double X { get; set; }
      public double Y { get; set; }

      public OxyPlot.DataPoint GetDataPoint()
      {
        return new OxyPlot.DataPoint(X, Y);
      }
    }

  }
}