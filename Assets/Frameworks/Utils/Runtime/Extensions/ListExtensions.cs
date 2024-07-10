using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using EblanDev.ScenarioCore.UtilsFramework.ID;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EblanDev.ScenarioCore.UtilsFramework.Extensions
{
	public static class ListExtensions
	{
		public static TItem Find<TItem>(this IReadOnlyList<TItem> list, Func<TItem, TItem, TItem> comparer)
		{
			TItem target = list[0];
			for (int i = 1; i < list.Count; i++)
			{
				target = comparer(target, list[i]);
			}
			return target;
		}
		public static IReadOnlyList<TItem> NullToNone<TItem>(this IReadOnlyList<TItem> item)
		{
			return item ?? Array.Empty<TItem>();
		}

		public static TItem[] NullToNone<TItem>(this TItem[] item)
		{
			return item ?? Array.Empty<TItem>();
		}

		public static TData GetRandom<TData>(this IReadOnlyList<TData> list)
		{
			if (list.Count == 0)
			{
				return default;
			}

			var iRand = Random.Range(0, list.Count);

			return list[iRand];
		}
		

		public static List<TData> GetRandomRange<TData>(this IReadOnlyList<TData> list, int count)
		{
			count = Mathf.Min(count, list.Count);

			var tmpList = new List<TData>(list);
			var randList = new List<TData>();

			for (var i = 0; i < count; i++)
			{
				var randData = tmpList.GetRandom();

				tmpList.Remove(randData);
				randList.Add(randData);
			}

			return randList;
		}

		public static TItem Last<TItem>(this IReadOnlyList<TItem> list)
		{
			if (list == null)
			{
				Debug.LogError("you try remove last from null list");
			}

			if (list.Count == 0)
			{
				Debug.LogError("you try remove last from empty list");
			}
			return list[list.Count - 1];
		}

		public static void Resize<TData>(this List<TData> list, int count)
			where TData : new()
		{
			list.ResizeTop(count);
			list.ResizeBottom(count);
		}

		public static void ResizeTop<TData>(this List<TData> list, int count)
			where TData : new()
		{
			for (var i = list.Count; i < count; i++)
			{
				list.Add(new TData());
			}
		}
		public static void Add(this List<Vector3> list, Bounds bounds)
		{
			list.Add(bounds.center - bounds.extents);
			list.Add(bounds.center + bounds.extents);
		}
		
		public static void ResizeBottom<TData>(this List<TData> list, int count)
		{
			var nRemove = list.Count - count;
			
			for (var i = 0; i < nRemove; i++)
			{
				list.RemoveAt(0);
			}
		}

		public static bool Has<T>(this IReadOnlyList<T> list, T value)
			where T : class
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] == value)
				{
					return true;
				}
			}

			return false;
		}
		
		public static TSource[] ToArrayOrEmpty<TSource>(this IEnumerable<TSource> source)
		{
			return source?.ToArray() ?? Array.Empty<TSource>();
		}
		
		public static string ToString<TSource>(this IEnumerable<TSource> source, string separator) =>
			string.Join(separator, source);

	
		public static int IndexOf<T>(this IReadOnlyList<T> list, T obj)
		{
			var nList = list.Count;
			for (var i = 0; i < nList; i++)
			{
				if (list[i].Equals(obj))
				{
					return i;
				}
			}

			return -1;
		}
		
		public static bool TryFindBy<T>(this IReadOnlyList<T> list, Func<T, bool> condition, out T item)
		{
			item = default;
			
			for (int i = 0; i < list.Count; i++)
			{
				item = list[i];
				if (condition.Invoke(item))
				{
					return true;
				}
			}

			item = default;
			return false;
		}

		public static T Find<T>(this IEnumerable<T> enumerable, Func<T, bool> condition)
		{
			if (TryFind(enumerable, condition, out var result))
			{
				return result;
			}

			Debug.LogError("cannot find item in list by condition");
			return default;
		}

		public static bool TryFind<T>(this IEnumerable<T> enumerable, Func<T, bool> condition, out T result)
		{
			foreach (var item in enumerable)
			{
				if (condition.Invoke(item))
				{
					result = item;
					return true;
				}
			}

			result = default;
			return false;
		}

		public static void DisposeItems<T>(this IList<T> list, bool clear = true)
			where T : IDisposable
		{
			for (int i = 0; i < list.Count; i++)
			{
				list[i].Dispose();
			}

			if (clear)
			{
				list.Clear();
			}
		}

		public static bool TryFind<T>(this IReadOnlyList<T> list, T item)
			where T : class
		{
			return list.TryFindBy((x => x == item), out item);
		}
		
		public static bool TryFind<TIn, TOut>(this IReadOnlyList<TIn> list, out TOut result)
		{
			result = default;
			
			for (int i = 0; i < list.Count; i++)
			{
				var item = list[i];
				if (item is TOut outItem)
				{
					result = outItem;
					return true;
				}
			}

			return false;
		}

		public static bool TryFind<T>(this IReadOnlyList<T> list, Func<T, bool> condition, out T item)
			where T : class
		{
			return list.TryFindBy(condition, out item);
		}

		public static bool TryFindFromEndBy<T>(this IReadOnlyList<T> list, Func<T, bool> condition, out T item)
		{
			item = default;

			for (int i = list.Count - 1; i >= 0 ; i--)
			{
				item = list[i];
				if (condition.Invoke(item))
				{
					return true;
				}
			}

			return false;
		}
		public static IEnumerable<T> Inverse<T>(this IReadOnlyList<T> list)
		{
			for (int i = list.Count - 1; i >= 0 ; i--)
			{
				yield return list[i];
			}
		}

		public static void AddRangeUnique<TData>(this List<TData> list, IReadOnlyList<TData> addList)
		{
			var nAddList = addList.Count;
			for (var iAddList = 0; iAddList < nAddList; iAddList++)
			{
				list.AddUnique(addList[iAddList]);
			}
		}
		
		public static string FindDefaultName<TData>(this List<TData> list, string prefix)
			where TData : INameContainer
		{
			var defaultNames = list.FindAll(data => data.GetName().Contains(prefix));
			var iDefaultAsset = defaultNames.Count == 0
				? -1
				: defaultNames.Max(assetData =>
					int.TryParse(assetData.GetName().Replace(prefix, ""), out var i) ? i : -1);

			return $"{prefix}{(iDefaultAsset + 1).ToString()}";
		}
		
		public static bool ContainsName<TData>(this IReadOnlyList<TData> list, string name)
			where TData : INameContainer => list.Count > 0 && list.Any(data => data.GetName() == name);
		
		public static bool ContainsId<TData>(this IReadOnlyList<TData> list, int id)
			where TData : IIDContainer
		{
			for (var i = 0; i < list.Count; i++)
			{
				if (list[i].GetID() == id)
				{
					return true;
				}
			}
			
			return false;
		}
		
		public static int FindNewId<TData>(this IReadOnlyList<TData> list, int minID = IDHelper.Default)
			where TData : IIDContainer
		{
			var nData = list.Count;

			if (nData == 0)
			{
				return minID;
			}

			var maxId = list.Max(data => data.GetID());

			if (maxId + 2 == nData + minID)
			{
				return maxId + 1;
			}

			for (var id = minID; id < maxId; id++)
			{
				if (list.ContainsId(id) == false)
				{
					return id;
				}
			}

			return maxId + 1;
		}
		
		public static TData FindById<TData>(this IReadOnlyList<TData> list, int id)
			where TData : IIDContainer
		{
			for (var i = 0; i < list.Count; i++)
			{
				if (list[i].GetID() == id)
				{
					return list[i];
				}
			}

			return default;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddUnique<TData>(this List<TData> list, TData data)
		{
			if (!list.Contains(data))
			{
				list.Add(data);
			}
		}
	}
}