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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace math_in.Views {
  public partial class MessageBox_Sorting : Window {

    public int EnteredNumber { get; private set; }

    public MessageBox_Sorting() {
      InitializeComponent();
    }

    private void OK_Button_Click(object sender, RoutedEventArgs e) {
      if (int.TryParse(TextBox_Number.Text, out int result)) {
        EnteredNumber = result;
        DialogResult = true;
        Close();
      } else {
        MessageBox.Show("Введите корректное число", "Ошибка");
      }
    }
  }
}
