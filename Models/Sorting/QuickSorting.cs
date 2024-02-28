using System.Diagnostics;

namespace math_in.Models.Sorting {
  public class QuickSorting : Sorting {
    static int iteration;

    static int Partition(int[] resArray, int minIndex, int maxIndex) {
      int pivot = minIndex - 1;
      for (var i = minIndex; i < maxIndex; i++) {
        if (resArray[i] < resArray[maxIndex]) {
          pivot++;
          Swap(ref resArray[pivot], ref resArray[i]);
        }
      }

      pivot++;
      Swap(ref resArray[pivot], ref resArray[maxIndex]);
      iteration++;

      return pivot;
    }

    public static int[] QuickSort(int[] resArray, int minIndex, int maxIndex) {
      if (minIndex >= maxIndex) {
        return resArray;
      }

      var pivotIndex = Partition(resArray, minIndex, maxIndex);
      QuickSort(resArray, minIndex, pivotIndex - 1);
      QuickSort(resArray, pivotIndex + 1, maxIndex);

      return resArray;
    }

    public static long QuickSort(int[] array) {
      iteration = 0;
      int[] resArray = array.Clone() as int[];

      var timer = Stopwatch.StartNew();

      QuickSort(resArray, 0, resArray.Length - 1);

      timer.Stop();

      return (timer.ElapsedTicks * 1_000_000) / Stopwatch.Frequency;
     
    }

    public static int GetIterationCount() {
      return iteration;
    }
  }
}
