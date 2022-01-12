using System;
using System.Diagnostics;
using System.Linq;

namespace project_quick_sort
{
    class App
    {
        private int[] numbers;
        private void enter(int n)
        {
            Random r = new Random();
            numbers = new int[n];
            for (int i = 0; i < n; i++)
                numbers[i] = r.Next(0, 100);
        }
        private void show()
        {
            //Console.WriteLine();
            for (int i = 0; i < numbers.Length; i++)
                Console.Write(numbers[i] + "  ");
            Console.WriteLine();
        }

        private void swap(int[] arr, int a, int b)
        {
            int tmp = arr[a];
            arr[a] = arr[b];
            arr[b] = tmp;
        }
      
        #region endCase
        private int partition_e(int[] arr, int start, int end)
        {
            int pivot = arr[end];
            int left = start;
            int right = end - 1;
            while (true)
            {
                while (left <= right && arr[left] < pivot) left++;
                while (left <= right && arr[right] > pivot) right--;

                if (left >= right) break;
                swap(arr, left, right);
                show();
                left++;
                right--;
            }
            swap(arr, left, end);
            //show();
            return left;
        }
        private void endCase(int[] arr, int start, int end)
        {
            if (start < end)
            {
                int index = partition_e(arr, start, end);

                endCase(arr, start, index - 1);
                endCase(arr, index + 1, end);
            }
        }
        #endregion

        #region randomCase
        private int partition_r(int[] arr, int start, int end)
        {
            Random rand = new Random();
            //range [start; end]
            int pIndex = rand.Next() % (end - start + 1) + start;
            
            int pivot = arr[pIndex];
            Console.WriteLine("pivot: " + pivot);
            while (start < end)
            {
                while (start < end && arr[start] <= pivot) start++;
                while (start < end && arr[end] >= pivot) end--;
               
                if (start < end)
                {
                    swap(arr, start, end);
                    show();
                }
            }
            if (arr[start] < pivot)
                swap(arr, pIndex, start);

            return start;
        }
        private void randomCase(int[] arr, int start, int end)
        {
            if (start < end)
            {
                int pIndex = partition_r(arr, start, end);
                if (pIndex - 1 > start)
                    randomCase(arr, start, pIndex - 1);
                if (pIndex < end)
                    randomCase(arr, pIndex, end);
            }
        }
        #endregion

        #region middleCase
        private void middleCase(int[] arr, int start, int end)
        {
            if (arr == null || arr.Length == 0) return;
            if (start >= end) return;

            int middle = (start + end) / 2;
            int pivot = arr[middle];

            int i = start, j = end;
            while (i <= j)
            {
                while (arr[i] < pivot)
                    i++;
                while (arr[j] > pivot)
                    j--;
                if (i <= j)
                {
                    swap(arr, i, j);
                    show();
                    i++;
                    j--;
                }
            }
            if (start < j) middleCase(arr, start, j);
            if (end > i) middleCase(arr, i, end);
        }
        #endregion
        public void run()
        {
            Console.Write("Enter length: ");
            int n = int.Parse(Console.ReadLine());
            enter(n);
            show();
            int[] p1 = new int[numbers.Length];
            int[] p2 = new int[numbers.Length];

            Array.Copy(numbers, 0, p1, 0, numbers.Length);
            Array.Copy(numbers, 0, p2, 0, numbers.Length);

            Console.WriteLine("\n=================MIDDLE================");
            Stopwatch s1 = new Stopwatch();
            s1.Start();
            middleCase(numbers, 0, numbers.Length - 1);
            s1.Stop();
            Console.WriteLine("\nAfter Sort: ");
            show();
            Console.WriteLine("=======================================");

            Console.WriteLine("\n=================END===================");
            numbers = p1;

            Stopwatch s2 = new Stopwatch();
            s2.Start();
            endCase(numbers, 0, numbers.Length - 1);
            s2.Stop();
            Console.WriteLine("\nAfter Sort: ");
            show();
            Console.WriteLine("\n=======================================");


            Console.WriteLine("\n=================RANDOM================");
            numbers = p2;

            Stopwatch s3 = new Stopwatch();
            s3.Start();
            randomCase(numbers, 0, numbers.Length - 1);
            s3.Stop();
            Console.WriteLine("\nAfter Sort: ");
            show();
            Console.WriteLine("\n=======================================");

            Console.WriteLine($"\nTime(middle): { s1.Elapsed }, Time(end): { s2.Elapsed }," +
                $" Time(random): { s3.Elapsed }");
        }
    }
    class Program
    {
        static void Main(string[] args) {
            App app = new App();
            app.run();


        }
    }
}
