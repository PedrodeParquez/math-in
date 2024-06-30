using System.Windows;

namespace math_in.Views.Message_Boxes {
  public partial class MessageBox_LeastSquaresMethod_Result : Window {
    public MessageBox_LeastSquaresMethod_Result() {
      InitializeComponent();
    }

    private void OK_Button_Click(object sender, RoutedEventArgs e) {
      DialogResult = true;
    }

    public static void Show(string firstString, string secondString, string thirdString, string resultString) {
      MessageBox_LeastSquaresMethod_Result messageBox = new MessageBox_LeastSquaresMethod_Result();

      messageBox.DefineMessageBox(firstString, secondString, thirdString, resultString);


      if (messageBox.ShowDialog() == true) {
        return;
      }
    }

    private void DefineMessageBox(string firstString, string secondString, string thirdString, string resultString) {
      FirstString_TextBlock.Text = firstString;
      SecondString_TextBlock.Text = secondString;
      ThirdString_TextBlock.Text = thirdString;
      Result_TextBlock.Text = resultString;
    }
  }
}
