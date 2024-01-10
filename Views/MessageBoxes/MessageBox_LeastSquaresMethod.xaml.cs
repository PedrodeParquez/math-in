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

    public int AmountPoints { get; private set; }
    public int MinNumber    { get; private set; }
    public int MaxNumber    { get; private set; }


    public MessageBox_LeastSquaresMethod() {
      InitializeComponent();
    }

    private void OK_Button_Click(object sender, RoutedEventArgs e) {
      if (int.TryParse(TextBox_Amount_Points.Text, out int firstValue)) {
        AmountPoints = firstValue;    
      } else {
        MessageBox.Show("Введите корректное число!", "Ошибка");
      }

      if (int.TryParse(TextBox_Min_Value.Text, out int secondValue)) {
        MinNumber = secondValue;
      } else {
        MessageBox.Show("Введите корректное число!", "Ошибка");
      }

      if (int.TryParse(TextBox_Max_Value.Text, out int thirdValue)) {
        MaxNumber = thirdValue;
      } else {
        MessageBox.Show("Введите корректное число!", "Ошибка");
      }

      Close();
    }
  }
}
