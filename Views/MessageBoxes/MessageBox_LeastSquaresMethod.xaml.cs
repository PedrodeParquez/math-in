using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace math_in.Views.MessagBoxes {
  public partial class MessageBox_LeastSquaresMethod : Window {

    public double AmountPoints { get; private set; }
    public double MinNumber    { get; private set; }
    public double MaxNumber    { get; private set; }


    public MessageBox_LeastSquaresMethod() {
      InitializeComponent();
    }

    private void OK_Button_Click(object sender, RoutedEventArgs e) {
      if (TextBox_Max_Value.Text == "" && TextBox_Min_Value.Text == "" && TextBox_Amount_Points.Text == "") {
        DialogResult = false;
        return;
      }


      if (double.TryParse(TextBox_Amount_Points.Text, out double firstValue)) {
        AmountPoints = firstValue;    
      } else {
        MessageBox.Show("Введите корректное число элементов!", "Ошибка");
      }

      if (double.TryParse(TextBox_Min_Value.Text, out double secondValue)) {
        MinNumber = secondValue;
      } else {
        MessageBox.Show("Введите корректное минимальное число!", "Ошибка");
      }

      if (double.TryParse(TextBox_Max_Value.Text, out double thirdValue)) {
        MaxNumber = thirdValue;
      } else {
        MessageBox.Show("Введите корректное максимальное число!", "Ошибка");
      }

      DialogResult = true;
    }
  }
}
