using math_in.Models;
using math_in.Models.InputHandler;
using math_in.Models.MethodDichtomii;
using System;
using System.Windows;
using System.Windows.Controls;
using Color = System.Drawing.Color;

namespace math_in.Views {
  public partial class DichtomyView : Page {
    public DichtomyView() {
      InitializeComponent();
    }

    private void Calculate_Button_Click(object sender, RoutedEventArgs e) {
      if (!InputHandler.IsCorrectInput(TextBox_Function, TextBox_Start_Point, TextBox_End_Point, TextBox_Precision)) {
        return;
      }
      
      try {
        Func.TextFunction = TextBox_Function.Text;
      } catch (Exception ex) {
        MessageBox.Show($"Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
        return;
      }

      Func.x = Func.a;

      double pointXMin = Math.Round(MethodDichtomy.DychotomyLocMin(Func.a, Func.b, Func.e), Math.Abs((int)Math.Log10(Func.e)));
      double pointYMin = Func.Fun(pointXMin);

      double pointXMax = Math.Round(MethodDichtomy.DychotomyLocMax(Func.a, Func.b, Func.e), Math.Abs((int)Math.Log10(Func.e)));
      double pointYMax = Func.Fun(pointXMax);

      TextBlock_Result.Text  = $"Минимум функции на отрезке [{Func.a}; {Func.b}]:\nx = {pointXMin}\ny = "  + $"{pointYMin}\n";
      TextBlock_Result.Text += $"Максимум функции на отрезке [{Func.a}; {Func.b}]:\nx = {pointXMax}\ny = " + $"{pointYMax}\n";

      var (xValues, yValues) = Func.CalculateFunctionValuesInRange();
      Chart.Plot.AddScatter(xValues, yValues, color: Color.Orange);
      Chart.Refresh();
    }
  }
}
