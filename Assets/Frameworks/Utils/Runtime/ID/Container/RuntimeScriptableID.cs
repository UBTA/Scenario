using System.Collections.Generic;
using EblanDev.ScenarioCore.UtilsFramework.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace EblanDev.ScenarioCore.UtilsFramework.ID
{
	public class RuntimeScriptableID : ScriptableObject
	{
		[ListDrawerSettings(CustomAddFunction = nameof(AddInfo))]
		[SerializeField] private List<Info> ids = new List<Info>();
		
		[HideInInspector] [SerializeField] private RuntimeScriptableID parent;
		[HideInInspector] [SerializeField] private string parentPrefix = "";
		[HideInInspector] [SerializeField] private int minID;

		public string GetKey(int id)
		{
			foreach (var info in ids)
			{
				if (info.id == id)
				{
					return info.name;
				}
			}

			return "unknown";
		}
		
		public int GetID(string key)
		{
			foreach (var info in ids)
			{
				if (info.name == key)
				{
					return info.id;
				}
			}

			return IDHelper.Invalid;
		}
		
		public ValueDropdownList<int> ToDropdownList()
		{
			var list = new ValueDropdownList<int>();

			list.Add("INVALID", IDHelper.Invalid);
			
			foreach (var info in ids)
			{
				list.Add($"{info.niceName.ToSpacedCase()}", info.id);
			}

			return list;
		}
		
		public ValueDropdownList<string> ToStringDropdownList()
		{
			var list = new ValueDropdownList<string>();
			
			foreach (var info in ids)
			{
				list.Add($"{info.niceName.ToSpacedCase()}", info.name);
			}

			return list;
		}
		
		private void AddInfo()
		{
			var defaultName = ids.FindDefaultName("default_");

			var item = new Info
			{
				id = ids.FindNewId(minID),
				name = defaultName,
				niceName = defaultName
			};
			
			ids.Add(item);
		}

		[OnInspectorGUI]
		private void OnInspectorGUI()
		{
			if (ids.Count == 0)
			{
				AddInfo();
			}
			
			var checkedInfos = new List<Info>();

			foreach (var info in ids)
			{
				info.niceName = info.name.ToSnakeCase();

				if (string.IsNullOrEmpty(info.niceName) || checkedInfos.ContainsName(info.niceName))
				{
					info.niceName = checkedInfos.FindDefaultName("default_");
				}
				
				checkedInfos.Add(info);
			}

			if (parent != null)
			{
				RefreshParent();
			}
		}

		private void RefreshParent()
		{
			foreach (var info in ids)
			{
				if (!parent.ids.ContainsId(info.id))
				{
					parent.ids.Add(new Info
					{
						id = info.id,
						name = parentPrefix + info.name,
						niceName = parentPrefix + info.niceName,
					});
					continue;
				}

				var parentInfo = parent.ids.FindById(info.id);
				parentInfo.name = parentPrefix + info.name;
				parentInfo.niceName = parentPrefix + info.niceName;
			}

#if UNITY_EDITOR
			EditorUtility.SetDirty(parent);
#endif
		}

		[System.Serializable]
		private class Info : IIDContainer, INameContainer
		{
			[ReadOnly]
			[HorizontalGroup("M", MaxWidth = 60)] [HideLabel] 
			public int id;
			[HorizontalGroup("M")] [LabelWidth(40)]
			public string name = "";
			[ReadOnly]
			[HorizontalGroup("M", MaxWidth = 100)] [HideLabel]
			public string niceName = "";
			
			public int GetID() => id;
			public string GetName() => niceName;
		}
	}
}