#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;
#endif

namespace EblanDev.ScenarioCore.UtilsFramework.ID
{
	public static class AssetHelper
	{
#if UNITY_EDITOR
		public static void E_CreateFolders(string path)
		{
			var folders = path.Split('/', '\\');

			var allPaths = new string[folders.Length];

			for (var i = 0; i < allPaths.Length; i++)
			{
				var p = "";

				for (var j = 0; j < i; j++)
				{
					p += folders[j];
					p += '/';
				}

				p += folders[i];

				allPaths[i] = p;
			}

			for (var i = 1; i < allPaths.Length; i++)
			{
				if (AssetDatabase.IsValidFolder(allPaths[i]) == false)
				{
					AssetDatabase.CreateFolder(allPaths[i - 1], folders[i]);
				}
			}
		}
		
		public static bool E_HasFolder(string path)
		{
			var folders = path.Split('/', '\\');

			var allPaths = new string[folders.Length];

			for (var i = 0; i < allPaths.Length; i++)
			{
				var p = "";

				for (var j = 0; j < i; j++)
				{
					p += folders[j];
					p += '/';
				}

				p += folders[i];

				allPaths[i] = p;
			}

			for (var i = 1; i < allPaths.Length; i++)
			{
				if (AssetDatabase.IsValidFolder(allPaths[i]) == false)
				{
					return false;
				}
			}

			return true;
		}
		
		public static T[] GetAllScriptableObjectInstances<T>() where T : ScriptableObject
		{
			string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
			T[] a = new T[guids.Length];
			for(int i =0;i<guids.Length;i++)
			{
				string path = AssetDatabase.GUIDToAssetPath(guids[i]);
				a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
			}
 
			return a;
 
		}
		public static T E_CreateOrOpenScriptableObject<T>(string folderPath, string assetName)
			where T : ScriptableObject
		{
			E_CreateFolders(folderPath);
			
			var path = Path.Combine(folderPath, $"{assetName}.asset");
			var obj = AssetDatabase.LoadAssetAtPath<T>(path);

			if (obj == null)
			{
				obj = ScriptableObject.CreateInstance<T>();
				AssetDatabase.CreateAsset(obj, path);
				AssetDatabase.SaveAssets();
				obj = AssetDatabase.LoadAssetAtPath<T>(path);
			}

			return obj;
		}
#endif

	}
}