using math_in.Models;
using math_in.Models.MethodDichtomii;
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
  public partial class DichtomyView : Page {
    public DichtomyView() {
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

      try {
        Func.TextFunction = TextBox_Function.Text;
      } catch (Exception ex) {
        MessageBox.Show($"Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
        return;
      }

      Func.x = Func.a;

      double pointXMin = Math.Round(MethodDichtomii.DychotomyLocMin(Func.a, Func.b, Func.e), Math.Abs((int)Math.Log10(Func.e)));
      double pointYMin = Func.Fun(pointXMin);

      double pointXMax = Math.Round(MethodDichtomii.DychotomyLocMax(Func.a, Func.b, Func.e), Math.Abs((int)Math.Log10(Func.e)));
      double pointYMax = Func.Fun(pointXMax);

      TextBlock_Result.Text  = $"Минимум функции на отрезке [{Func.a}; {Func.b}]:\nx = {pointXMin}\ny = "  + $"{pointYMin}\n";
      TextBlock_Result.Text += $"Максимум функции на отрезке [{Func.a}; {Func.b}]:\nx = {pointXMax}\ny = " + $"{pointYMax}\n";


      var (xValues, yValues) = Func.CalculateFunctionValuesInRange();
      Chart.Plot.AddScatter(xValues, yValues);
      Chart.Refresh();
    }
  }
}
