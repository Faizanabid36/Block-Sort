using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the Size of Array: ");
            int num = Convert.ToInt32(Console.ReadLine());
            if (num <= 0)
            {
                Console.WriteLine("Invalid Input Given");
            }
            else
            {
                BlockSort mySort = new BlockSort(num);
                mySort.getUserInput();
                mySort.arrBuffer();
                mySort.printArray();
                mySort.printfinal();
            }
        }
    }
    class BlockSort
    {
        private int[] finalArray;
        private int[] sortedArray;
        private int bufferController = 0;
        private int[] array;
        private int arrSize;
        private int[] mergedArray;
        private double buffer;
        private double numBlocks;
        public BlockSort(int size)
        {
            array = new int[size];
            arrSize = array.Length;
        }
        //To Assign Random Values
        public void initializeArray()
        {
            Random rn = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rn.Next(0, 40);
            }
        }
        public void printArray()
        {
            Console.WriteLine("Printing Unsorted Array");
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
        public void arrBuffer()
        {
            //It is the Formula of Buffer.

            buffer = Math.Sqrt(arrSize);

            buffer = (int)buffer;
            numBlocks = arrSize / buffer;

            int[] temp = new int[(int)buffer];
            bufferController = 0;
            mergedArray = new int[(int)buffer];

            //If Input Size of Array Perfectly Fits Into Buffer Formula
            if ((arrSize % buffer) == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Number of Blocks: " + (numBlocks) + ", Number of Elements in each Block Should be: " + buffer); Console.WriteLine();

                for (int i = 0; i < arrSize; i = i + (int)buffer)
                {
                    //Executon of This Loop depends on Buffer. Using it because we want to send elements to make it a block
                    for (int ii = bufferController; ii < bufferController + (int)buffer && ii < array.Length; ii++)
                    {
                        temp[ii % (int)buffer] = array[ii];
                    }



                    bufferController = bufferController + (int)buffer;
                    //0 Because we have No Remaining Part
                    MergeExtract(temp, (int)numBlocks, (int)buffer, 0);

                }
                numBlocks = (int)numBlocks;
            }
            else
            {
                double remainingPart = arrSize % buffer;

                Console.WriteLine("Number of Blocks: " + ((int)numBlocks + 1) + ", Number of Elements in each Block Should be: " + buffer);
                Console.WriteLine("But Last Block Will Contain " + remainingPart + " Element/s"); Console.WriteLine();

                for (int i = 0; i < arrSize; i = i + (int)buffer)
                {
                    //Executon of This Loop depends on Buffer. Using it because we want to send elements to make it a block
                    for (int ii = bufferController; ii < bufferController + (int)buffer && ii < array.Length; ii++)
                    {
                        temp[ii % (int)buffer] = array[ii];
                    }
                    bufferController = bufferController + (int)buffer;

                    if (i == numBlocks * buffer - remainingPart)
                    {
                        MergeExtract(temp, (int)numBlocks, (int)remainingPart, (int)remainingPart);
                    }
                    else
                    {
                        MergeExtract(temp, (int)numBlocks, (int)buffer, (int)remainingPart);
                    }
                }
                numBlocks = (int)numBlocks;
            }
        }
        private void MergeExtract(int[] arr, int numBlocks, int buffer, int remainingPart)
        {

            List<int> unsorted = new List<int>();
            List<int> sorted;
            if (remainingPart != 0)
            {
                //Adding The Elemnts of Array in a List
                for (int x = 0; x < buffer; x++)
                {
                    unsorted.Add(arr[x]);
                }
            }
            else
            {
                //Adding The Elemnts of Array in a List
                for (int x = 0; x < buffer; x++)
                {
                    unsorted.Add(arr[x]);
                }
            }

            //Uncomment the Below Line to See how Array was Broken Into Blocks and each Block Conatins what and how many elements. 
            //Console.WriteLine(); printBlocks(unsorted.ToArray());


            sorted = MergeSort(unsorted);
            sortedArray = sorted.ToArray();

            //Uncomment the Below Line to See the Elements of Blocks Merged, using Merge Sort. 
            //Console.WriteLine();printBlocksMergeSorted(sortedArray);
            mergeBlocks(sortedArray, bufferController);
        }
        private List<int> MergeSort(List<int> unsorted)
        {
            if (unsorted.Count <= 1)
                return unsorted;
            List<int> Left = new List<int>();
            List<int> Right = new List<int>();
            int middle = unsorted.Count / 2;
            for (int i = 0; i < middle; i++)
            {
                Left.Add(unsorted[i]);
            }
            for (int i = middle; i < unsorted.Count; i++)
            {
                Right.Add(unsorted[i]);
            }
            Left = MergeSort(Left);
            Right = MergeSort(Right);

            return Merge(Left, Right);

        }
        //Merges the Divided Parts of List that was sent to Sort/.
        private List<int> Merge(List<int> left, List<int> right)
        {
            List<int> result = new List<int>();
            while (left.Count > 0 || right.Count > 0)
            {
                if (left.Count > 0 && right.Count > 0)
                {
                    //Comparing First two elements to see which is smaller
                    if (left.First() <= right.First())
                    {
                        result.Add(left.First());

                        left.Remove(left.First());
                    }
                    else
                    {
                        result.Add(right.First());
                        right.Remove(right.First());
                    }
                }
                else if (left.Count > 0)
                {
                    result.Add(left.First());
                    left.Remove(left.First());
                }
                else if (right.Count > 0)
                {
                    result.Add(right.First());

                    right.Remove(right.First());
                }
            }
            return result;
        }
        private void mergeBlocks(int[] arr, int increaseBy)
        {
            int remainingPart = arr.Length % (int)buffer;
            int test = increaseBy - (int)buffer;
            if (arr.Length % (int)buffer == 0)
            {
                //Assigns every Block in One Array c/a Merged Array.
                for (int x = 0; x < arr.Length; x++)
                {
                    mergedArray[test] = arr[x % mergedArray.Length];
                    test++;
                }
                finalArray = mergedArray;
            }
            else
            {
                //It will Resize array depending on the remaining part that was not perfectly divisible by Buffer. e.g 2 in 11
                Array.Resize(ref mergedArray, mergedArray.Length + remainingPart - (int)buffer);
                //Because we want to assign the elements that were not divisible(Remaining Part).
                for (int x = 0; x < remainingPart; x++)
                {
                    mergedArray[test] = arr[x % mergedArray.Length];
                    test++;
                }
                Array.Resize(ref finalArray, mergedArray.Length);
                finalArray = mergedArray;
            }

            //The Two Blocks are Merged and Insertion Sort is applied on it repeadetly.

            insertionSort(mergedArray);





            //--------------------------------------------------------------------------------------------

            //Uncomment this to see the blocks that were merged after applying Merge Sort on Broken Block.
            //foreach (var item in mergedArray)
            //{
            //    Console.WriteLine("Merged Array Element: " + item);
            //}
            //Console.WriteLine("____________________________");




            if (mergedArray.Length <= array.Length)
            {
                Array.Resize(ref mergedArray, arr.Length + increaseBy - remainingPart);
            }
        }
        private void insertionSort(int[] arr)
        {
            int i, key, j;
            for (i = 1; i < arr.Length; i++)
            {
                key = arr[i];
                j = i - 1;
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
            }
        }
        public void getUserInput()
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write("Enter the Element :");
                array[i] = Convert.ToInt32(Console.ReadLine());
            }
        }
        public void printfinal()
        {
            Console.WriteLine("Printing The Sorted Array");
            foreach (var item in finalArray)
            {
                Console.Write(item + " ");
            }
        }
        public void printBlocks(int[] arr)
        {
            Console.Write("Array that was broken Into Blocks has Following Elements\n");
            for (int x = 0; x < arr.Length; x++)
            {
                Console.WriteLine(arr[x]);
            }
        }
        public void printBlocksMergeSorted(int[] arr)
        {
            Console.WriteLine("After Applying Merge Sort on Every block");
            foreach (var item in sortedArray)
            {
                Console.WriteLine(item + " ");
            }
        }
    }
}