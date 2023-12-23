using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace math_in.Models.Sorting {
  public class RandomArray {
    public static int[] GenerateRandomArray(int len, int minValue, int maxValue) {
      Random random = new Random();
      int[] array = new int[len];

      for (int i = 0; i < len; i++) {
        array[i] = random.Next(minValue, maxValue + 1);
      }

      return array;
    }

    public static string ArrayToString(int[] array) {
      StringBuilder stringArray = new StringBuilder();

      foreach (var element in array) {
        stringArray.Append(element);
        stringArray.Append(" ");
      }

      return stringArray.ToString();
    }
  }
}
