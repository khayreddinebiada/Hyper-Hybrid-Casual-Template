using System;
using System.Collections;
using System.Collections.Generic;

namespace HCEngine.Data
{
	public static class BinarySearchExtensions
	{
		public static void InsertSortedList<T>(this IList<T> list, T value) where T : IComparable<T>
		{
			InsertSortedList(list, value, (a, b) => a.CompareTo(b));
		}

		public static void InsertSortedList<T>(this IList<T> list, T value, Comparison<T> comparison)
		{
			if (!list.TryToFindItem(value, comparison, out int index))
				list.Insert(index, value);
		}

		public static void InsertSortedList(this IList list, IComparable value)
		{
			InsertSortedList(list, value, (a, b) => a.CompareTo(b));
		}

		public static void InsertSortedList(this IList list, IComparable value, Comparison<IComparable> comparison)
		{
			if (!list.TryToFindItem(value, comparison, out int index))
				list.Insert(index, value);
		}

		public static bool TryToFindItem<T>(this IList<T> list, T value, out int index) where T : IComparable<T>
		{
			return list.TryToFindItem(value, (a, b) => a.CompareTo(b), out index);
		}

		public static bool TryToFindItem(this IList list, IComparable value, out int index)
		{
			return list.TryToFindItem(value, (a, b) => a.CompareTo(b), out index);
		}

		public static bool TryToFindItem<T>(this IList<T> list, T value, Comparison<T> comparison, out int index)
		{
			var startIndex = 0;
			var endIndex = list.Count;
			while (endIndex > startIndex)
			{
				var windowSize = endIndex - startIndex;
				var middleIndex = startIndex + (windowSize / 2);
				var middleValue = list[middleIndex];
				var compareToResult = comparison(middleValue, value);
				if (compareToResult == 0)
				{
					index = middleIndex;
					return true;
				}
				else if (compareToResult < 0)
				{
					startIndex = middleIndex + 1;
				}
				else
				{
					endIndex = middleIndex;
				}
			}

			index = 0;
			return false;
		}

		public static bool TryToFindItem(this IList list, IComparable value, Comparison<IComparable> comparison, out int index)
		{
			var startIndex = 0;
			var endIndex = list.Count;
			while (endIndex > startIndex)
			{
				var windowSize = endIndex - startIndex;
				var middleIndex = startIndex + (windowSize / 2);
				var middleValue = (IComparable)list[middleIndex];
				var compareToResult = comparison(middleValue, value);
				if (compareToResult == 0)
				{
					index = middleIndex;
					return true;
				}
				else if (compareToResult < 0)
				{
					startIndex = middleIndex + 1;
				}
				else
				{
					endIndex = middleIndex;
				}
			}

			index = 0;
			return false;
		}
	}
}
