using math_in.Models;
using math_in.Models.CoordinateDescent;
using math_in.Models.InputHandler;
using math_in.Views.Message_Boxes;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace math_in.Views {
  public partial class CoordinateDescentView : Page {
    public CoordinateDescentView() {
      InitializeComponent();
    }

    private void Calculate_Button_Click(object sender, RoutedEventArgs e) {
      if (!InputHandler.IsCorrectInput(TextBox_Function, TextBox_Start_Point, TextBox_End_Point, TextBox_Precision)) {
        return;
      }

      Func.x = Func.a;

      try {
        Func.TextFunction = TextBox_Function.Text;
      } catch (Exception ex) {
        MessageBox_Custom.Show("Ошибка!", $"{ex.Message}", $"{ex.StackTrace}");
        return;
      }

      double minPointX;

      try {
        minPointX = Math.Round((double)CoordinateDescent.CoordinateDescentLocMin(Func.a, Func.b, Func.e), Math.Abs((int)Math.Log10(Func.e)));
      } catch (Exception ex) {
        MessageBox_Custom.Show("Ошибка!", $"{ex.Message}", "");
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
      Chart.Plot.AddScatter(xValues, yValues, color: Color.Orange);
      Chart.Refresh();
    }
  }
}

