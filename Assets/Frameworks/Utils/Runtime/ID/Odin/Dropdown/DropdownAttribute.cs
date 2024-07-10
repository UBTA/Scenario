using System;
using System.Diagnostics;

namespace EblanDev.ScenarioCore.UtilsFramework.ID
{
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("UNITY_EDITOR")]
	public class DropdownAttribute : Attribute
	{
		public int NumberOfItemsBeforeEnablingSearch;

		public bool IsUniqueList;

		public bool DrawDropdownForListElements;

		public bool DisableListAddButtonBehaviour;

		public bool ExcludeExistingValuesInList;

		public bool ExpandAllMenuItems;

		public bool AppendNextDrawer;

		public bool DisableGUIInAppendedDrawer;

		public bool DoubleClickToConfirm;

		public bool FlattenTreeView;

		public int DropdownWidth;

		public int DropdownHeight;

		public string DropdownTitle;

		public bool SortDropdownItems;

		public bool HideChildProperties;
		
		public DropdownAttribute()
		{
			NumberOfItemsBeforeEnablingSearch = 10;
			DrawDropdownForListElements = true;
		}
	}
}