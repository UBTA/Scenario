using System.Collections.Generic;
using EblanDev.ScenarioCore.UtilsFramework.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace EblanDev.ScenarioCore.UtilsFramework.ID
{
	public class ScriptableID : ScriptableObject
	{
		[ListDrawerSettings(CustomAddFunction = nameof(AddInfo))]
		[SerializeField] private List<Info> ids = new List<Info>();
		
		[HideInInspector] [SerializeField] private ScriptableID parent;
		[HideInInspector] [SerializeField] private string parentPrefix = "";
		[HideInInspector] [SerializeField] private int minID;
		
		public List<Info> ToList => ids;

		public ValueDropdownList<int> ToDropdownList()
		{
			var list = new ValueDropdownList<int>();

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

		public string IDToString(int id)
		{
			var nIds = ids.Count;
			for (var iId = 0; iId < nIds; iId++)
			{
				var idInfo = ids[iId];
				if (idInfo.id == id)
				{
					return idInfo.niceName;
				}
			}

			return "INVALID";
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
		
		public static ScriptableID Load(string key)
		{
			var path = $"IDs/{key}";
			return Resources.Load<ScriptableID>(path);
		}

#if UNITY_EDITOR
		public static ScriptableID E_Load(string key)
		{
			var path = $"Assets/Scenario/IDs/{key}.asset";
			var instance = AssetDatabase.LoadAssetAtPath<ScriptableID>(path);
			if (instance == null)
			{
				var so = CreateInstance<ScriptableID>();
				AssetHelper.E_CreateFolders("Assets/Scenario/IDs");
				AssetDatabase.CreateAsset(so, path);
				instance = AssetDatabase.LoadAssetAtPath<ScriptableID>(path);
			}

			return instance;
		}
		
		public void E_Setup(ScriptableID newParent, string newParentPrefix, int newMinID)
		{
			parent = newParent;
			parentPrefix = newParentPrefix;
			minID = newMinID;
			
			EditorUtility.SetDirty(this);
		}
#endif

		[System.Serializable]
		public class Info : IIDContainer, INameContainer
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