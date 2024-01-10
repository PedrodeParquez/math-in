using math_in.Models;
using math_in.Models.CoordinateDescent;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace math_in.Views {
  public partial class CoordinateDescentView : Page {
    public CoordinateDescentView() {
      InitializeComponent();
    }

    private void Calculate_Button_Click(object sender, RoutedEventArgs e) {
      if (!double.TryParse(TextBox_Start_Point.Text, out Func.a)) {
        MessageBox.Show("Неправильный формат данных для начальной точки!");
        return;
      }

      if (!double.TryParse(TextBox_End_Point.Text, out Func.b)) {
        MessageBox.Show("Неправильный формат данных для конечной точки!");
        return;
      }

      if (Func.a > Func.b) {
        MessageBox.Show("Начальная точка должна быть меньше или равна конечной!");
        return;
      }

      if (!Regex.IsMatch(TextBox_Precision.Text, @"(1|10+)|(0,(1|0+1))")
         || (!double.TryParse(TextBox_Precision.Text, out Func.e)
         || TextBox_Precision.Text[0] == '-')) {
        MessageBox.Show("Неправильный формат данных для точности!");
        return;
      }

      Func.x = Func.a;

      try {
        Func.TextFunction = TextBox_Function.Text;
      } catch (Exception ex) {
        MessageBox.Show($"Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
        return;
      }

      double minPointX;

      try {
        minPointX = Math.Round((double)CoordinateDescent.CoordinateDescentLocMin(Func.a, Func.b, Func.e), Math.Abs((int)Math.Log10(Func.e)));
      } catch (Exception ex) {
        MessageBox.Show(ex.Message);

        return;
      }

      double minPointY = Func.Fun(minPointX);

      double maxPointX;

      try {
        maxPointX = Math.Round((double)CoordinateDescent.CoordinateDescentLocMax(Func.a, Func.b, Func.e), Math.Abs((int)Math.Log10(Func.e)));
      } catch (Exception ex) {
        TextBlock_Result.Text = ex.Message;

        return;
      }

     double maxPointY = Func.Fun(maxPointX);

      TextBlock_Result.Text = $"Минимум на отрезке [{Func.a}; {Func.b}]:\nx = {minPointX}\ny = {minPointY}\nМаксимум на отрезке [{Func.a}; {Func.b}]:\nx = {maxPointX}\ny = {maxPointY}\n";

      var (xValues, yValues) = Func.CalculateFunctionValuesInRange();
      Chart.Plot.AddScatter(xValues, yValues);
      Chart.Refresh();
    }
  }
}

