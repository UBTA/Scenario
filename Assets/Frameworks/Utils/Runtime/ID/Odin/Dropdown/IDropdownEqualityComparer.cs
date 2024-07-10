using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace EblanDev.ScenarioCore.UtilsFramework.ID
{
	internal class IDropdownEqualityComparer : IEqualityComparer<object>
	{
		private readonly bool isTypeLookup;

		public IDropdownEqualityComparer(bool isTypeLookup) => this.isTypeLookup = isTypeLookup;

		public bool Equals(object x, object y)
		{
			if (x is ValueDropdownItem xItem)
			{
				x = xItem.Value;
			}

			if (y is ValueDropdownItem yItem)
			{
				y = yItem.Value;
			}

			if (EqualityComparer<object>.Default.Equals(x, y))
			{
				return true;
			}

			if (x == null != (y == null) || !isTypeLookup)
			{
				return false;
			}

			if (!(x is Type type1))
			{
				type1 = x.GetType();
			}
			
			var type2 = type1;
			
			if (!(y is Type type3))
			{
				type3 = y.GetType();
			}
			
			var type4 = type3;
			
			return type2 == type4;
		}

		public int GetHashCode(object obj)
		{
			if (obj is ValueDropdownItem item)
			{
				obj = item.Value;
			}

			if (obj == null)
			{
				return -1;
			}

			if (!isTypeLookup)
			{
				return obj.GetHashCode();
			}

			if (!(obj is Type type))
			{
				type = obj.GetType();
			}
			
			return type.GetHashCode();
		}
	}
}