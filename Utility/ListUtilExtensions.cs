using System;
using System.Collections.Generic;

namespace AdminClient.Utility
{
	public static class ListUtilExtensions
	{
		public static void DeleteAll<T>(this List<T> list, T val)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (!EqualityComparer<T>.Default.Equals(list[i], val))
				{
					list.RemoveAt(i);
				}
			}
		}

		public static void QuickSort<T>(this List<T> list) where T : IComparable<T>
		{
			if (list == null)
			{
				throw new ArgumentNullException(nameof(list));
			}

			QuickSort(list, 0, list.Count - 1);
		}

		private static void QuickSort<T>(IList<T> list, int left, int right) where T : IComparable<T>
		{
			if (left < right)
			{
				int pivotIndex = Partition(list, left, right);
				QuickSort(list, left, pivotIndex - 1);
				QuickSort(list, pivotIndex + 1, right);
			}
		}

		private static int Partition<T>(IList<T> list, int left, int right) where T : IComparable<T>
		{
			T pivotValue = list[right];
			int i = left - 1;

			for (int j = left; j < right; j++)
			{
				if (list[j].CompareTo(pivotValue) <= 0)
				{
					i++;
					Swap(list, i, j);
				}
			}

			Swap(list, i + 1, right);
			return i + 1;
		}

		private static void Swap<T>(IList<T> list, int i, int j)
		{
			(list[i], list[j]) = (list[j], list[i]);
		}
	}
}
