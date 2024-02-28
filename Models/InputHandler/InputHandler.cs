using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace math_in.Models.InputHandler {
  public class InputHandler {
    public static bool IsCorrectInput(TextBox functionField, TextBox startPointField, TextBox endPointField, TextBox presicionField) {
      if (functionField.Text == "") {
        MessageBox.Show("Поле функции не заполнено!");
        return false;
      }

      if (startPointField.Text == "") {
        MessageBox.Show("Поле начальной точки не заполнено!");
        return false;
      }

      if (endPointField.Text == "") {
        MessageBox.Show("Поле конечной точки не заполнено!");
        return false;
      }

      if (presicionField.Text == "") {
        MessageBox.Show("Поле погрешности не заполнено!");
        return false;
      }

      if (!double.TryParse(startPointField.Text, out Func.a)) {
        MessageBox.Show("Неправильный формат данных для начальной точки!");
        return false;
      }

      if (!double.TryParse(endPointField.Text, out Func.b)) {
        MessageBox.Show("Неправильный формат данных для конечной точки!");
        return false;
      }

      if (Func.a > Func.b) {
        MessageBox.Show("Начальная точка должна быть меньше или равна конечной!");
        return false;
      }

      if (!Regex.IsMatch(presicionField.Text, @"(1|10+)|(0,(1|0+1))")
         || (!double.TryParse(presicionField.Text, out Func.e)
         || presicionField.Text[0] == '-')) {
        MessageBox.Show("Неправильный формат данных для точности!");
        return false;
      }

      return true;
    }

    public static bool IsCorrectInputIntegral(TextBox functionField, TextBox lowerField, TextBox upperField,TextBox presicionField){
      if (functionField.Text == "") {
        MessageBox.Show("Поле функции не заполнено!");
        return false;
      }

      if (lowerField.Text == "") {
        MessageBox.Show("Поле нижнего предела не заполнено!");
        return false;
      }

      if (upperField.Text == "") {
        MessageBox.Show("Поле верхнего предела не заполнено!");
        return false;
      }

      if (presicionField.Text == "") {
        MessageBox.Show("Поле погрешности не заполнено!");
        return false;
      }

      if (FunctionParser.Expression.IsExpression(lowerField.Text + "+0*x", Func.idsNames)) {
        FunctionParser.Expression expression = new FunctionParser.Expression(lowerField.Text + "+0*x", Func.idsNames, null);

        Func.idsValues[0] = 0d;
        Func.a = expression.CalculateValue(Func.idsValues);
      } 
      
      if (!double.TryParse(lowerField.Text, out Func.a)) {
        MessageBox.Show("Неправильный формат данных для значения нижнего предела!");
        return false;
      }

      if (FunctionParser.Expression.IsExpression(upperField.Text + "+0*x", Func.idsNames)) {
        FunctionParser.Expression expression = new FunctionParser.Expression(upperField.Text + "+0*x", Func.idsNames, null);

        Func.idsValues[0] = 0d;

        Func.b = expression.CalculateValue(Func.idsValues);
      } else if (!double.TryParse(upperField.Text, out Func.b)) {
        MessageBox.Show("Неправильный формат данных для значения верхнего предела!");
        return false;
      }

      if (Func.a > Func.b) {
        MessageBox.Show("Значения должны быть такие, что: \n Нижний предел <= Верхний предел!");
        return false;
      }

      if (!double.TryParse(presicionField.Text, out Func.e) || !Regex.IsMatch(presicionField.Text, @"(1|10+)|(0,(1|0+1))") || presicionField.Text[0] == '-') {
        MessageBox.Show("Неправильный формат данных для точности!");
        return false;
      }

      return true;
    }
  }
}
