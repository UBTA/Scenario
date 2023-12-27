using System.Collections.Generic;
using UnityEngine;

namespace EblanDev.ScenarioCore.UtilsFramework.Extensions
{
	public static class ArrayExtensions
	{
		public static TData[] Insert<TData>(this TData[] array, TData data, int index)
		{
			var n = array.Length + 1;
			var newArray = new TData[n];
			var iOld= 0;

			for (var iNew = 0; iNew < n; iNew++)
			{
				if (iNew == index)
				{
					newArray[iNew] = data;
				}
				else
				{
					newArray[iNew] = array[iOld];
					iOld++;
				}
			}

			return newArray;
		}
		
		public static TData GetRandom<TData>(this TData[] array)
		{
			if (array.Length == 0)
			{
				return default;
			}

			var iRand = Random.Range(0, array.Length);

			return array[iRand];
		}

		public static bool TryGetValue<T>(this T[] array, int index, out T value)
		{
			if (array.Length > index && index >= 0)
			{
				value = array[index];
				return true;
			}

			value = default;
			return false;
		}

		public static TData[] GetRandomRange<TData>(this TData[] array, int count)
		{
			count = Mathf.Min(array.Length, count);

			var freeElements = new List<TData>(array);
			var randRange = new TData[count];

			for (var i = 0; i < count; i++)
			{
				var randElement = freeElements.GetRandom();
				freeElements.Remove(randElement);

				randRange[i] = randElement;
			}

			return randRange;
		}
	}
}