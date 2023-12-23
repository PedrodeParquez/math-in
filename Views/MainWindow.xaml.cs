using math_in.Views;
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

namespace math_in {
  public partial class MainWindow : Window {
    public MainWindow() {
      InitializeComponent();
      MainFrame.Content = new SLAEView();
    }

    //Tool bar
    private void ExitButton_Click(object sender, RoutedEventArgs e) {
      MessageBoxResult result = MessageBox.Show("Точно ли вы хотите закрыть приложение?", "Подтверждение выхода", MessageBoxButton.YesNo);

      if (result == MessageBoxResult.Yes) {
        Close();
      }
    }

    private void HideButton_Click(object sender, RoutedEventArgs e) {
      WindowState = WindowState.Minimized;
    }

    private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e) {
      if (e.ChangedButton == MouseButton.Left) {
        DragMove();
      }
    }

    //Task bar
    private void DichtomyViewButton_Click(object sender, RoutedEventArgs e) {
      MainFrame.Content = new DichtomyView();
    }

    private void NuitonViewButton_Click(object sender, RoutedEventArgs e) {
      MainFrame.Content = new NuitonView();
    }

    private void SortingViewButton_Click(object sender, RoutedEventArgs e) {
      MainFrame.Content = new SortingView();
    }

    private void CoordinateDescentViewButton_Click(object sender, RoutedEventArgs e) {
      MainFrame.Content = new CoordinateDescentView();
    }

    private void DefiniteIntegralButton_Click(object sender, RoutedEventArgs e) {
      MainFrame.Content = new DefiniteIntegralView();
    }

    private void SLAEViewButton_Click(object sender, RoutedEventArgs e) {
      MainFrame.Content = new SLAEView();
    }

    private void LeastSquaresMethodViewButton_Click(object sender, RoutedEventArgs e) {
      MainFrame.Content = new LeastSquaresMethodView();
    }
  }
}