using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EblanDev.ScenarioCore.UtilsFramework.ID;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEngine;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.Drawers;
using Sirenix.Utilities.Editor;
#endif

namespace EblanDev.ScenarioCore.UtilsFramework.ID
{
	public abstract class DropdownAttributeDrawer<TAttribute, TItem>
#if UNITY_EDITOR
		: OdinAttributeDrawer<TAttribute>
#endif
		where TAttribute : DropdownAttribute
	{
#if UNITY_EDITOR
		private string error;
		private GUIContent label;
		private bool isList;
		private bool isListElement;
		private IEnumerable<object> result;
		private bool enableMultiSelect;
		private Dictionary<object, string> nameLookup;
		private LocalPersistentContext<bool> isToggled;

		protected ValueDropdownList<TItem> List { get; private set; }

		protected override void Initialize()
		{
			isToggled = this.GetPersistentValue("Toggled", SirenixEditorGUI.ExpandFoldoutByDefault);
			isList = Property.ChildResolver is ICollectionResolver;
			isListElement = Property.Parent?.ChildResolver is ICollectionResolver;
			
			List = GetDropdownList();
			
			ReloadDropdownCollections();
		}
#endif

		protected abstract ValueDropdownList<TItem> GetDropdownList();

#if UNITY_EDITOR
		private IEnumerable<object> GetSelection() => Property.ValueEntry.WeakValues.Cast<object>();

		private IEnumerable<ValueDropdownItem> GetValues()
		{
			return List.Select(tItem => new ValueDropdownItem(tItem.Text, tItem.Value));
		}

		private void ReloadDropdownCollections()
		{
			if (error != null)
				return;
			object obj1 = null;

			var obj2 = List;

			if (obj2 != null)
			{
				obj1 = obj2.FirstOrDefault();
			}

			if (obj1 != null)
			{
				var valueDropdownItems = GetValues();
				nameLookup =
					new Dictionary<object, string>(
						new IDropdownEqualityComparer(false));
				foreach (var valueDropdownItem in valueDropdownItems)
					nameLookup[valueDropdownItem] = valueDropdownItem.Text;
			}
			else
				nameLookup = null;
		}

		private static IEnumerable<ValueDropdownItem> ToValueDropdowns(
			IEnumerable<object> query)
		{
			return query.Select(x =>
			{
				switch (x)
				{
					case ValueDropdownItem valueDropdownItem3:
						return valueDropdownItem3;
					case IValueDropdownItem valueDropdownItem2:
						return new ValueDropdownItem(valueDropdownItem2.GetText(), valueDropdownItem2.GetValue());
					default:
						return new ValueDropdownItem((string) null, x);
				}
			});
		}

		protected override void DrawPropertyLayout(GUIContent label)
		{
			this.label = label;
			
			if (Property.ValueEntry == null)
			{
				CallNextDrawer(label);
			}
			else if (error != null)
			{
				SirenixEditorGUI.ErrorMessageBox(error);
				CallNextDrawer(label);
			}
			else if (isList)
			{
				if (Attribute.DisableListAddButtonBehaviour)
				{
					CallNextDrawer(label);
				}
				else
				{
					var customAddFunction = CollectionDrawerStaticInfo.NextCustomAddFunction;
					
					CollectionDrawerStaticInfo.NextCustomAddFunction = OpenSelector;
					
					CallNextDrawer(label);
					
					if (result != null)
					{
						AddResult(result);
						result = null;
					}

					CollectionDrawerStaticInfo.NextCustomAddFunction = customAddFunction;
				}
			}
			else if (Attribute.DrawDropdownForListElements || !isListElement)
			{
				DrawDropdown();
			}
			else
			{
				CallNextDrawer(label);
			}
		}

		private void AddResult(IEnumerable<object> query)
		{
			if (isList)
			{
				var childResolver = Property.ChildResolver as ICollectionResolver;
				if (this.enableMultiSelect)
					childResolver.QueueClear();
				foreach (object obj in query)
				{
					object[] values = new object[this.Property.ParentValues.Count];
					for (int index = 0; index < values.Length; ++index)
						values[index] = SerializationUtility.CreateCopy(obj);
					childResolver.QueueAdd(values);
				}
			}
			else
			{
				object obj = query.FirstOrDefault<object>();
				for (int index = 0; index < this.Property.ValueEntry.WeakValues.Count; ++index)
					this.Property.ValueEntry.WeakValues[index] = SerializationUtility.CreateCopy(obj);
			}
		}

		private void DrawDropdown()
		{
			IEnumerable<object> objects = null;
			
			if (Attribute.AppendNextDrawer && !isList)
			{
				GUILayout.BeginHorizontal();
				
				var width = 15f;
				if (label != null)
				{
					width += GUIHelper.BetterLabelWidth;
				}
				
				objects = OdinSelector<object>.DrawSelectorDropdown(label, GUIContent.none,
					ShowSelector, GUIStyle.none,
					GUILayoutOptions.Width(width));
				if (Event.current.type == EventType.Repaint)
				{
					var position = GUILayoutUtility.GetLastRect().AlignRight(15f);
					position.y += 4f;
					SirenixGUIStyles.PaneOptions.Draw(position, GUIContent.none, 0);
				}

				GUILayout.BeginVertical();
				var inAppendedDrawer = Attribute.DisableGUIInAppendedDrawer;
				if (inAppendedDrawer)
				{
					GUIHelper.PushGUIEnabled(false);
				}
				
				CallNextDrawer(null);
				
				if (inAppendedDrawer)
					GUIHelper.PopGUIEnabled();
				GUILayout.EndVertical();
				GUILayout.EndHorizontal();
			}
			else
			{
				string currentValueName = this.GetCurrentValueName();
				if (!this.Attribute.HideChildProperties && this.Property.Children.Count > 0)
				{
					Rect valueRect;
					this.isToggled.Value = SirenixEditorGUI.Foldout(this.isToggled.Value, this.label, out valueRect);
					objects = OdinSelector<object>.DrawSelectorDropdown(valueRect, currentValueName,
						new Func<Rect, OdinSelector<object>>(this.ShowSelector));
					if (SirenixEditorGUI.BeginFadeGroup((object) this, this.isToggled.Value))
					{
						++UnityEditor.EditorGUI.indentLevel;
						for (int index = 0; index < this.Property.Children.Count; ++index)
						{
							InspectorProperty child = this.Property.Children[index];
							child.Draw(child.Label);
						}

						--UnityEditor.EditorGUI.indentLevel;
					}

					SirenixEditorGUI.EndFadeGroup();
				}
				else
					objects = OdinSelector<object>.DrawSelectorDropdown(this.label, currentValueName,
						new Func<Rect, OdinSelector<object>>(this.ShowSelector), (GUIStyle) null);
			}

			if (objects == null || !objects.Any<object>())
				return;
			this.AddResult(objects);
		}

		private void OpenSelector()
		{
			this.ReloadDropdownCollections();
			this.ShowSelector(new Rect(Event.current.mousePosition, Vector2.zero)).SelectionConfirmed +=
				(Action<IEnumerable<object>>) (x => this.result = x);
		}

		private OdinSelector<object> ShowSelector(Rect rect)
		{
			GenericSelector<object> selector = this.CreateSelector();
			rect.x = (float) (int) rect.x;
			rect.y = (float) (int) rect.y;
			rect.width = (float) (int) rect.width;
			rect.height = (float) (int) rect.height;
			if (this.Attribute.AppendNextDrawer && !this.isList)
				rect.xMax = GUIHelper.GetCurrentLayoutRect().xMax;
			selector.ShowInPopup(rect,
				new Vector2((float) this.Attribute.DropdownWidth, (float) this.Attribute.DropdownHeight));
			return (OdinSelector<object>) selector;
		}

		private GenericSelector<object> CreateSelector()
		{
			this.Attribute.IsUniqueList = !(this.Property.ChildResolver is IOrderedCollectionResolver) ||
			                              (this.Attribute.IsUniqueList || this.Attribute.ExcludeExistingValuesInList);
			IEnumerable<ValueDropdownItem> source = GetValues() ?? Enumerable.Empty<ValueDropdownItem>();
			if (source.Any<ValueDropdownItem>())
			{
				if (this.isList && this.Attribute.ExcludeExistingValuesInList ||
				    this.isListElement && this.Attribute.IsUniqueList)
				{
					var list = source.ToList<ValueDropdownItem>();
					var parent = this.Property.FindParent(
						(Func<InspectorProperty, bool>) (x => x.ChildResolver is ICollectionResolver), true);
					var comparer = new IDropdownEqualityComparer(false);
					parent.ValueEntry.WeakValues.Cast<IEnumerable>()
						.SelectMany<IEnumerable,
							object>((Func<IEnumerable, IEnumerable<object>>) (x => x.Cast<object>()))
						.ForEach<object>((Action<object>) (x =>
							list.RemoveAll((Predicate<ValueDropdownItem>) (c => comparer.Equals((object) c, x)))));
					source = (IEnumerable<ValueDropdownItem>) list;
				}

				if (this.nameLookup != null)
				{
					foreach (ValueDropdownItem valueDropdownItem in source)
					{
						if (valueDropdownItem.Value != null)
							this.nameLookup[valueDropdownItem.Value] = valueDropdownItem.Text;
					}
				}
			}

			bool flag = this.Attribute.NumberOfItemsBeforeEnablingSearch == 0 || source != null &&
				source.Take<ValueDropdownItem>(this.Attribute.NumberOfItemsBeforeEnablingSearch)
					.Count<ValueDropdownItem>() == this.Attribute.NumberOfItemsBeforeEnablingSearch;
			GenericSelector<object> genericSelector = new GenericSelector<object>(this.Attribute.DropdownTitle, false,
				source.Select<ValueDropdownItem, GenericSelectorItem<object>>(
					(Func<ValueDropdownItem, GenericSelectorItem<object>>) (x =>
						new GenericSelectorItem<object>(x.Text, x.Value))));
			this.enableMultiSelect =
				this.isList && this.Attribute.IsUniqueList && !this.Attribute.ExcludeExistingValuesInList;
			if (this.Attribute.FlattenTreeView)
				genericSelector.FlattenedTree = true;
			if (this.isList && !this.Attribute.ExcludeExistingValuesInList && this.Attribute.IsUniqueList)
				genericSelector.CheckboxToggle = true;
			else if (!this.Attribute.DoubleClickToConfirm && !this.enableMultiSelect)
				genericSelector.EnableSingleClickToSelect();
			if (this.isList && this.enableMultiSelect)
			{
				genericSelector.SelectionTree.Selection.SupportsMultiSelect = true;
				genericSelector.DrawConfirmSelectionButton = true;
			}

			genericSelector.SelectionTree.Config.DrawSearchToolbar = flag;
			IEnumerable<object> selection = Enumerable.Empty<object>();
			if (!this.isList)
				selection = GetSelection();
			else if (this.enableMultiSelect)
				selection = GetSelection()
					.SelectMany<object, object>(
						(Func<object, IEnumerable<object>>) (x => (x as IEnumerable).Cast<object>()));
			genericSelector.SetSelection(selection);
			genericSelector.SelectionTree.EnumerateTree().AddThumbnailIcons(true);
			if (this.Attribute.ExpandAllMenuItems)
				genericSelector.SelectionTree.EnumerateTree((Action<OdinMenuItem>) (x => x.Toggled = true));
			if (this.Attribute.SortDropdownItems)
				genericSelector.SelectionTree.SortMenuItemsByName();
			return genericSelector;
		}

		private string GetCurrentValueName()
		{
			if (UnityEditor.EditorGUI.showMixedValue)
				return "—";
			object weakSmartValue = this.Property.ValueEntry.WeakSmartValue;
			string name = (string) null;
			if (this.nameLookup != null && weakSmartValue != null)
				this.nameLookup.TryGetValue(weakSmartValue, out name);
			return new GenericSelectorItem<object>(name, weakSmartValue).GetNiceName();
		}
#endif
	}
}