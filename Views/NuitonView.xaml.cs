using math_in.Models;
using math_in.Models.MethodNuiton;
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
  public partial class NuitonView : Page {
    public NuitonView() {
      InitializeComponent();
    }

    private void Calculate_Nuiton_Button_Click(object sender, RoutedEventArgs e) {
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

      double pointXMin = default;

      try {
        pointXMin = Math.Round(MethodNuiton.Nutone(Func.a, Func.b, Func.e), Math.Abs((int)Math.Log10(Func.e)));
      } catch (Exception ex) {
        TextBlock_Result.Text += ex.Message;
        return;
      }

      if (double.IsNaN(pointXMin)) {
        TextBlock_Result.Text = "Функция не подходит под метод ньютона !!!\n\n";
        return;
      }

      double pointYMin = Func.Fun(pointXMin);

      TextBlock_Result.Text = $"Экстремум функции на отрезке [{Func.a}; {Func.b}]:\n\tx = {pointXMin}\n\ty = " + $"{pointYMin}\n\n";

      if (MethodNuiton.Derivative2(pointXMin) > 0) {
        TextBlock_Result.Text += $"Этот экстремум: точка минимума !!!\n\n";
      } else if (MethodNuiton.Derivative2(pointXMin) < 0) {
        TextBlock_Result.Text += $"Этот экстремум: точка максимума !!!\n\n";
      }

      Func.x = Func.a;

      try {
        Func.TextFunction = TextBox_Function.Text;
      } catch (Exception ex) {
        TextBlock_Result.Text = ex.Message;
        return;
      }

      double pointX;

      try {
        pointX = Math.Round((double)MethodNuiton.NutoneIntersection(Func.a, Func.b, Func.e), Math.Abs((int)Math.Log10(Func.e)));
      } catch (Exception ex) {
        TextBlock_Result.Text = ex.Message;
        return;
      }

      double pointY = Math.Truncate(Func.Fun(pointX));

      TextBlock_Result.Text += $"Решение уравнения f(x) = 0 на отрезке [{Func.a}; {Func.b}]:\n\tx = {pointX}\n\ty = {pointY}\n\n\t";

      var (xValues, yValues) = Func.CalculateFunctionValuesInRange();
      Chart.Plot.AddScatter(xValues, yValues);
      Chart.Refresh();
    }
  }
}
