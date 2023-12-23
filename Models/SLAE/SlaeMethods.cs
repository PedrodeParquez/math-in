using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace math_in.Models.SLAE {
  public class SlaeMethods {
    public static double[][] result = new double[3][];

    public static double[] MethodСramer(double[,] matrixParam, double[] resSlau, int countParam) {
      result[0] = new double[countParam];

      double[,] matrix = matrixParam.Clone() as double[,];
      double[] res = resSlau.Clone() as double[];
      double delta = Determinant(matrix, countParam);

      if (delta == 0) {
        throw new Exception("У данного СЛАУ нет тривиального решения!");
      }

      for (int indexColumn = 0; indexColumn < countParam; ++indexColumn) {
        matrix = matrixParam.Clone() as double[,];

        for (int indexRow = 0; indexRow < countParam; ++indexRow) {
          matrix[indexRow, indexColumn] = res[indexRow];
        }

        result[0][indexColumn] = Determinant(matrix, countParam) / delta;
      }

      return result[0];
    }

    public static double[] MethodGauss(double[,] matrixParam, double[] resSlau, int countParam) {
      result[1] = new double[countParam];

      double[,] matrix = matrixParam.Clone() as double[,];
      double[] res = resSlau.Clone() as double[];
      double delta = Determinant(matrix, countParam);

      if (delta == 0) {
        throw new Exception("У данного СЛАУ нет тривиального решения !!!");
      }

      for (int indexRow = 1; indexRow < countParam; ++indexRow) {
        if (matrix[indexRow - 1, indexRow - 1] != 1) {
          double del = matrix[indexRow - 1, indexRow - 1];

          for (int index = 0; index < countParam; ++index) {
            matrix[indexRow - 1, index] /= del;
          }

          res[indexRow - 1] /= del;
        }

        for (int index = 0; index < indexRow - 1; ++index) {
          double element = matrix[indexRow, index];

          for (int indexColumn = 0; indexColumn < countParam; ++indexColumn) {
            matrix[indexRow, indexColumn] = matrix[index, indexColumn] * (-element) + matrix[indexRow, indexColumn];
          }

          res[indexRow] = res[index] * (-element) + res[indexRow];
        }

        double el = matrix[indexRow, indexRow - 1];

        for (int index = indexRow - 1; index < countParam; ++index) {
          matrix[indexRow, index] = matrix[indexRow - 1, index] * (-el) + matrix[indexRow, index];
        }

        res[indexRow] = res[indexRow - 1] * (-el) + res[indexRow];
      }

      result[1][countParam - 1] = res[countParam - 1] / matrix[countParam - 1, countParam - 1];

      for (int indexRow = countParam - 2; indexRow >= 0; --indexRow) {
        double sum = 0;

        for (int indexRes = countParam - 1; indexRes >= indexRow + 1; --indexRes) {
          sum += matrix[indexRow, indexRes] * result[1][indexRes];
        }

        result[1][indexRow] = (res[indexRow] - sum) / matrix[indexRow, indexRow];
      }

      return result[1];
    }

    public static double[] MethodGaussJordano(double[,] matrixParam, double[] resSlau, int countParam) {
      result[2] = new double[countParam];

      double[,] matrix = matrixParam.Clone() as double[,];
      double[] res = resSlau.Clone() as double[];
      double delta = Determinant(matrix, countParam);

      if (delta == 0) {
        throw new Exception("У данного СЛАУ нет тривиального решения !!!");
      }

      for (int indexRow = 1; indexRow < countParam; ++indexRow) {
        if (matrix[indexRow - 1, indexRow - 1] != 1) {
          double del = matrix[indexRow - 1, indexRow - 1];

          for (int index = 0; index < countParam; ++index) {
            matrix[indexRow - 1, index] /= del;
          }

          res[indexRow - 1] /= del;
        }

        for (int i = 0; i < indexRow - 1; ++i) {
          double element = matrix[indexRow, i];

          for (int indexColumn = 0; indexColumn < countParam; ++indexColumn) {
            matrix[indexRow, indexColumn] = matrix[i, indexColumn] * (-element) + matrix[indexRow, indexColumn];
          }

          res[indexRow] = res[i] * (-element) + res[indexRow];
        }

        double el = matrix[indexRow, indexRow - 1];

        for (int i = indexRow - 1; i < countParam; ++i) {
          matrix[indexRow, i] = matrix[indexRow - 1, i] * (-el) + matrix[indexRow, i];
        }

        res[indexRow] = res[indexRow - 1] * (-el) + res[indexRow];
      }

      if (matrix[countParam - 1, countParam - 1] != 1) {
        double del = matrix[countParam - 1, countParam - 1];

        matrix[countParam - 1, countParam - 1] /= del;
        res[countParam - 1] /= del;
      }

      result[2][countParam - 1] = res[countParam - 1] / matrix[countParam - 1, countParam - 1];

      for (int indexRow = countParam - 2; indexRow >= 0; --indexRow) {
        if (matrix[indexRow + 1, indexRow + 1] != 1) {
          double del = matrix[indexRow + 1, indexRow + 1];

          for (int i = 0; i < countParam; ++i) {
            matrix[indexRow + 1, i] /= del;
          }

          res[indexRow + 1] /= del;
        }

        for (int i = countParam - 1; i > indexRow; --i) {
          double element = matrix[indexRow, i];

          for (int indexColumn = countParam - 1; indexColumn >= 0; --indexColumn) {
            matrix[indexRow, indexColumn] = matrix[i, indexColumn] * (-element) + matrix[indexRow, indexColumn];
          }

          res[indexRow] = res[i] * (-element) + res[indexRow];
        }
      }

      for (int i = 0; i < countParam; ++i) {
        result[2][i] = res[i];
      }

      return result[2];
    }

    public static double Determinant(double[,] matrix, int matrixSide) {
      if (matrixSide == 2) {
        return (matrix[0, 0] * matrix[1, 1] -
          matrix[0, 1] * matrix[1, 0]);
      } else if (matrixSide == 1) {
        return matrix[0, 0];
      } else if (matrixSide >= 3) {
        double[,] MatrixForDeterminant = new double[matrixSide - 1, matrixSide - 1];
        double MatrixDeterminant = default;

        int MatrixForDeterminantRowIndex, MatrixForDeterminantColumnIndex;

        for (int RowElementsIndex = 0; RowElementsIndex < matrixSide; ++RowElementsIndex) {
          MatrixForDeterminantRowIndex = default;

          for (int RowIndex = 1; RowIndex < matrixSide; ++RowIndex) {
            MatrixForDeterminantColumnIndex = default;

            for (int ColumnIndex = 0; ColumnIndex < matrixSide; ++ColumnIndex) {
              if (ColumnIndex != RowElementsIndex) {
                MatrixForDeterminant[MatrixForDeterminantRowIndex, MatrixForDeterminantColumnIndex] =
                  matrix[RowIndex, ColumnIndex];
                ++MatrixForDeterminantColumnIndex;
              }
            }

            ++MatrixForDeterminantRowIndex;
          }

          MatrixDeterminant += Math.Pow(-1, RowElementsIndex + 2) * matrix[0, RowElementsIndex]
            * Determinant(MatrixForDeterminant, matrixSide - 1);
        }

        return MatrixDeterminant;
      } else {
        throw new Exception("Сторона квадратной матрицы равна нулю! Неверное значение стороны!");
      }
    }
  }
}
