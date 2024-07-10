using Sirenix.OdinInspector;

namespace EblanDev.ScenarioCore.UtilsFramework.ID
{
	public abstract class ScriptableIDDropdownAttribute : DropdownAttribute
	{
		public string Key { get; }
		public string ParentKey { get; }
		public string ParentPrefix { get; }
		public int MinID { get; }


		public ScriptableIDDropdownAttribute(string key, string parentKey = "", string parentPrefix = "", int minID = 0)
		{
			Key = string.IsNullOrEmpty(key) ? "default" : key;
			ParentKey = parentKey;
			ParentPrefix = parentPrefix;
			MinID = minID;
		}
	}

	public abstract class ScriptableIDDropdownDrawer<TAttribute> : DropdownAttributeDrawer<TAttribute, int>
		where TAttribute : ScriptableIDDropdownAttribute
	{
#if UNITY_EDITOR
		protected override void Initialize()
		{
			base.Initialize();


			var so = ScriptableID.E_Load(Attribute.Key);

			ScriptableID parentSo = null;

			if (!string.IsNullOrEmpty(Attribute.ParentKey))
			{
				parentSo = ScriptableID.E_Load(Attribute.ParentKey);
			}
			
			so.E_Setup(parentSo, Attribute.ParentPrefix, Attribute.MinID);
		}
#endif

		protected override ValueDropdownList<int> GetDropdownList()
		{
#if UNITY_EDITOR
			return ScriptableID.E_Load(Attribute.Key).ToDropdownList();
#endif
			return new ValueDropdownList<int>();
		}

#if UNITY_EDITOR
		private static ScriptableID LoadScriptableID(string key) => ScriptableID.E_Load(key);
#endif
	}
}