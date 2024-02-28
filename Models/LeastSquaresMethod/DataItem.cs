using System.ComponentModel;

namespace math_in.Models.LeastSquaresMethod {
  public class DataItem : INotifyPropertyChanged {
    private double _x;
    private double _y;

    public double X {
      get { return _x; }
      set {
        if (_x != value) {
          _x = value;
          OnPropertyChanged(nameof(X));
        }
      }
    }

    public double Y {
      get { return _y; }
      set {
        if (_y != value) {
          _y = value;
          OnPropertyChanged(nameof(Y));
        }
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName) {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
