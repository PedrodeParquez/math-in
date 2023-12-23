using math_in.Models;
using math_in.Models.CoordinateDescent;
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
      if (!double.TryParse(TextBox_Start_Point.Text, out Function.a)) {
        MessageBox.Show("Неправильный формат данных для начальной точки!");
        return;
      }

      if (!double.TryParse(TextBox_End_Point.Text, out Function.a)) {
        MessageBox.Show("Неправильный формат данных для конечной точки!");
        return;
      }

      if (Function.a > Function.b) {
        MessageBox.Show("Начальная точка должна быть больше конечной!");
        return;
      }

      if (!Regex.IsMatch(TextBox_Precision.Text, @"(1|10+)|(0,(1|0+1))")
         || (!double.TryParse(TextBox_Precision.Text, out Function.e)
         || TextBox_Precision.Text[0] == '-')) {
        MessageBox.Show("Неправильный формат данных для точности!");
        return;
      }

      Function.x = Function.a;

      try {
        Function.TextFunction = TextBox_Function.Text;
      } catch (Exception ex) {
        MessageBox.Show(ex.Message); 

        return;
      }

      double pointX;

      try {
        pointX = Math.Round((double)CoordinateDescent.CoordinateDescentLocMin(Function.a, Function.b, Function.e), Math.Abs((int)Math.Log10(Function.e)));
      } catch (Exception ex) {
        MessageBox.Show(ex.Message);

        return;
      }

      double pointY = Function.Fun(pointX);

      TextBlock_Result.Text = $"Минимум на отрезке [{Function.a}; {Function.b}]:\n\tx = {pointX}\n\ty = {pointY}\n\n\t";

      //PaintFun();
    }
  }
}
