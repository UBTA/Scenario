using System.Collections.Generic;
using EblanDev.ScenarioCore.UtilsFramework.Extensions;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace EblanDev.ScenarioCore.UtilsFramework.ID
{
	public class ScriptableIdContainer : ScriptableObject, IIDContainer
	{
		[ReadOnly]
		[SerializeField] private int id;

		
		public int GetID() => id;


		#region EDITOR
		private void Reset()
		{
			RefreshId();
		}

		[Button(ButtonSizes.Medium)]
		private void RefreshId()
		{
#if UNITY_EDITOR
			var paths = AssetDatabase.FindAssets($"t: {nameof(ScriptableIdContainer)}");

			var allSettings = new List<ScriptableIdContainer>();

			foreach (var guid in paths)
			{
				var assetPath = AssetDatabase.GUIDToAssetPath(guid);
				var idContainer = AssetDatabase.LoadAssetAtPath<ScriptableIdContainer>(assetPath);

				if (idContainer.GetType() == GetType())
				{
					allSettings.Add(idContainer);
				}
			}

			id = -1;
			id = allSettings.FindNewId();
#endif
		}
		#endregion
	}
}