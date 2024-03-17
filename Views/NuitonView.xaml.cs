using math_in.Models;
using math_in.Models.InputHandler;
using math_in.Models.MethodNuiton;
using math_in.Views.Message_Boxes;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace math_in.Views {
  public partial class NuitonView : Page {
    public NuitonView() {
      InitializeComponent();
    }

    private void Calculate_Button_Click(object sender, RoutedEventArgs e) {
      if (!InputHandler.IsCorrectInput(TextBox_Function, TextBox_Start_Point, TextBox_End_Point, TextBox_Precision)) {
        return;
      }

      try {
        Func.TextFunction = TextBox_Function.Text;
      } catch (Exception ex) {
        MessageBox_Custom.Show("Внимание!", $"{ex.Message}", $"{ex.StackTrace}");
        return;
      }

      Func.x = Func.a;
      double pointXMin;

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
      Chart.Plot.AddScatter(xValues, yValues, color: Color.Orange);
      Chart.Refresh();
    }
  }
}
