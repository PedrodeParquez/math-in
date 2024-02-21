using math_in.Models;
using math_in.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace math_in {
  public partial class MainWindow : Window {
    public MainWindow() {
      Func.idsNames = new string[] { "x", "e" };
      Func.idsValues = new double[] { default, Math.E };
      InitializeComponent();
      MainFrame.Content = new LeastSquaresMethodView();
      previousButton = LeastSquares_Method;
      LeastSquares_Method.Background = Brushes.Gray;
    }

    //Handler Buttons
    private Button previousButton;

    private void ChangeColor(Button button) {
      if (previousButton != null) {
        previousButton.Background = Brushes.LightGray;
      }
      button.Background = Brushes.Gray;
      previousButton = button;
    }

    private void SelectedTab(Button button) {
      ChangeColor(button);
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
      SelectedTab((Button)sender);
    }

    private void NuitonViewButton_Click(object sender, RoutedEventArgs e) {
      MainFrame.Content = new NuitonView();
      SelectedTab((Button)sender);
    }

    private void SortingViewButton_Click(object sender, RoutedEventArgs e) {
      MainFrame.Content = new SortingView();
      SelectedTab((Button)sender);
    }

    private void CoordinateDescentViewButton_Click(object sender, RoutedEventArgs e) {
      MainFrame.Content = new CoordinateDescentView();
      SelectedTab((Button)sender);
    }

    private void DefiniteIntegralButton_Click(object sender, RoutedEventArgs e) {
      MainFrame.Content = new DefiniteIntegralView();
      SelectedTab((Button)sender);
    }

    private void SLAEViewButton_Click(object sender, RoutedEventArgs e) {
      MainFrame.Content = new SLAEView();
      SelectedTab((Button)sender);
    }

    private void LeastSquaresMethodViewButton_Click(object sender, RoutedEventArgs e) {
      MainFrame.Content = new LeastSquaresMethodView();
      SelectedTab((Button)sender);
    }
  }
}