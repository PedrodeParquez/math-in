using math_in.Models;
using math_in.Models.LeastSquaresMethod;
using math_in.Views.MessagBoxes;
using math_in.Views.Message_Boxes;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace math_in.Views {
  public partial class LeastSquaresMethodView : Page {
    public ObservableCollection<DataItem> Data { get; set; } 

    public List<double> XValues { get; set; }
    public List<double> YValues { get; set; }

    string[] func = new string[3];
    double[] deters = new double[3];

    public LeastSquaresMethodView() {
      InitializeComponent();
      XValues = new List<double>();
      YValues = new List<double>();
      Data = new ObservableCollection<DataItem>();
      Data.Add(new DataItem());
      DataContext = this;
    }

    bool isRandom = false;

    private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e) {
      var editedItem = e.Row.Item as DataItem;
      var newIndex = Data.IndexOf(editedItem) + 1;

      if (newIndex == Data.Count) {
        Data.Add(new DataItem());
        dataGrid.ScrollIntoView(Data[newIndex]);
      }
    }

    private void Calculate_First_Button_Click(object sender, RoutedEventArgs e) {
      double x = double.Parse(TextBox_Linear_X.Text);

      Func.TextFunction = func[0];

      TextBox_Linear_Result.Text = Func.Fun(x).ToString();
    }

    private void Calculate_Second_Button_Click(object sender, RoutedEventArgs e) {
      double x = double.Parse(TextBox_Parabolic_X.Text);

      Func.TextFunction = func[1];

      TextBox_Parabolic_Result.Text = Func.Fun(x).ToString();
    }

    private void Calculate_Third_Button_Click(object sender, RoutedEventArgs e) {
      double x = double.Parse(TextBox_Exponential_X.Text);

      Func.TextFunction = func[2];

      TextBox_Exponential_Result.Text = Func.Fun(x).ToString();
    }

    private void PrintPoints(double[] arrayX, double[] arrayY) {
      for (int i = 0; i < arrayX.Length; i++) {
          Wplot.Plot.AddMarker(arrayX[i], arrayY[i], size: 5, color: Color.Green);
      }
    }

    private void PrintFunc(string textFunc, Color col, string desc) {
      List<double> xValues = new List<double>();
      List<double> yValues = new List<double>();

      Func.TextFunction = textFunc;


      double a = XValues.Min();
      double b = XValues.Max();

      if (a - b == 0) {
        a = -10;
        b = 10;
      }

      double x = a - 10;


      while (x <= b + 10) {
        double y = Func.Fun(x);

        xValues.Add(x);
        yValues.Add(y);

        x += 0.001;
      }

      double[] xArray = xValues.ToArray();
      double[] yArray = yValues.ToArray();

      Wplot.Plot.AddScatter(xArray, yArray, color: col, label: desc);
      Wplot.Plot.Legend(location: Alignment.UpperRight);
    }

    private void Calculate_Button_Click(object sender, RoutedEventArgs e) {

      string result = $"Оценка погрешности:";

      Wplot.Reset();

      if (Data.Count == 0) {
        MessageBox_Custom.Show("Пояснение", "Заполните таблицу значениями!", "");
        return;
      }

      if (Data.Count <= 1) {
        MessageBox_Custom.Show("Пояснение", "Введите больше одной", "пары чисел!");
        return;
      }

      if (CheckBox_Parabolic.IsChecked == false && CheckBox_Linear.IsChecked == false && CheckBox_Exponential.IsChecked == false) {
        MessageBox_Custom.Show("Пояснение", "Выберите хотя бы ", "одну регрессию!");
        return;
      }

      if (!isRandom) {
        List<DataItem> newData = new List<DataItem>();

        int itemCount = dataGrid.Items.Count;
        int currentIndex = 0;

        foreach (var item in dataGrid.Items) {
          bool isLastItem = currentIndex == itemCount - 1;

          if (!isLastItem && item is DataItem dataItem) {
            newData.Add(new DataItem { X = dataItem.X, Y = dataItem.Y });
          }

          currentIndex++;
        }

        Data.Clear();

        foreach (var newItem in newData) {
          Data.Add(newItem);
        }
      }

      foreach (var item in Data) {
        XValues.Add(item.X);
        YValues.Add(item.Y);
      }

      PrintPoints(XValues.ToArray(), YValues.ToArray());

      if (CheckBox_Linear.IsChecked == true) {
        double[] linearParams = LeastSquaresMethod.LinearRegression(XValues, YValues);

        TextBox_Linear.Text = $"f(x) = ";
        func[0] = $"{linearParams[0]}*x";

        if (linearParams[1] < 0) {
          func[0] += $" - {Math.Abs(linearParams[1])}";
        } else {
          func[0] += $" + {linearParams[1]}";
        }

        TextBox_Linear.Text += func[0];

        deters[0] = LeastSquaresMethod.ErrorEstimation(XValues, YValues, func[0]);

        result += $"\nЛинейная = {deters[0]}";

        PrintFunc(func[0], Color.Orange, "Линейная");
      }

      if (CheckBox_Parabolic.IsChecked == true) {

        double[] parabolParams = LeastSquaresMethod.ParabolRegression(XValues, YValues);

        TextBox_Parabolic.Text = $"f(x) = ";
        func[1] = $"{parabolParams[2]}*(x^2)";

        if (parabolParams[1] < 0) {
          func[1] += $" - {Math.Abs(parabolParams[1])}*x";
        } else {
          func[1] += $" + {parabolParams[1]}*x";
        }

        if (parabolParams[0] < 0) {
          func[1] += $" - {Math.Abs(parabolParams[0])}";
        } else {
          func[1] += $" + {parabolParams[0]}";
        }

        TextBox_Parabolic.Text += func[1];

        deters[1] = LeastSquaresMethod.ErrorEstimation(XValues, YValues, func[1]);

        result += $"\nПараболическая = {deters[1]}";
        PrintFunc(func[1], Color.Red, "Параболическая");
      }

      bool exp  = true;
      double[] yval = YValues.ToArray();

      for (int i = 0; i < yval.Length; i++) {
        if (yval[i] <= 0) {
          exp = false;
          MessageBox_Custom.Show("Пояснение", "Невозможно построить график", "для экспоненциальной формулы!");
          break;
        }
      }

      if ((CheckBox_Exponential.IsChecked == true) && (exp)) {
        double[] paramParams;

        paramParams = LeastSquaresMethod.ParamRegression(XValues, YValues); 

        TextBox_Exponential.Text = $"f(x) = ";
        func[2] = $"{paramParams[0]} * e^({paramParams[1]} * x)";

        TextBox_Exponential.Text += func[2];

        deters[2] = LeastSquaresMethod.ErrorEstimation(XValues, YValues, func[2]);

        result += $"\nПараметрическая = {deters[2]}";

        PrintFunc(func[2], Color.Blue, "Параметрическое");
      }

      double maxDet = deters.Min();

      if (deters[0].Equals(maxDet)) {
        result += "\nСледовательно, ближе всех Линейная регрессия";
      } else if (deters[1].Equals(maxDet)) {
        result += "\nСледовательно, ближе всех Параболическая регрессия";
      } else {
        result += "\nСледовательно, ближе всех Параметрическая регрессия";
      }   

      MessageBox.Show(result, "Результаты");

      Wplot.Refresh();
    }

    private void Random_Values_Button_Click(object sender, RoutedEventArgs e) {
      double amountPoints = 0;
      double minValue = 0;
      double maxValue = 0;

      MessageBox_LeastSquaresMethod messageBox = new MessageBox_LeastSquaresMethod();

      if (messageBox.ShowDialog() == true) {
        amountPoints = messageBox.AmountPoints;
        minValue = messageBox.MinNumber;
        maxValue = messageBox.MaxNumber;
      }

      Random random = new Random();

      if (amountPoints > 0) {
        Data.Clear();

        for (int i = 0; i < amountPoints; i++) {
          double randomX;
          double randomY;

          HashSet<double> uniqueXValues = new HashSet<double>();

          do {
            randomX = Math.Round(random.NextDouble() * (maxValue - minValue) + minValue, 2);
          } while (!uniqueXValues.Add(randomX));

          randomY = Math.Round(random.NextDouble() * (maxValue - minValue) + minValue, 2);

          Data.Add(new DataItem { X = randomX, Y = randomY });
        }
      }

      isRandom = true;
    }
  }
}
