using System;
using UnityEngine;
using UnityEditor;

namespace Yorozu.CustomProperty
{
	[CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
	public sealed class EnumFlagsDrawer : PropertyDrawer
	{
		private EnumFlagsAttribute attr => attribute as EnumFlagsAttribute;
		
		private float? _maxWidth;
		
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (!attr.IsButton)
			{
				property.intValue = EditorGUI.MaskField(position, label, property.intValue, property.enumNames);
				return;
			}

			position.height = base.GetPropertyHeight(property, label);
			var rect = EditorGUI.PrefixLabel(position, label);
			rect.height = base.GetPropertyHeight(property, label);
			
			if (property.isExpanded)
				if (GUI.Button(rect, "Nothing"))
					property.intValue = 0;
			
			property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);
			if (!property.isExpanded)
			{
				EditorGUI.LabelField(rect, property.intValue.ToString());
				return;
			}

			rect.y += rect.height;
			if (GUI.Button(rect, "Everything"))
			{
				foreach (var name in property.enumNames)
					property.intValue |= GetValue(name);
			}
			
			foreach (var name in property.enumNames)
			{
				rect.y += rect.height;

				using (var check = new EditorGUI.ChangeCheckScope())
				{
					var hasValue = HasValue(property.intValue, name);
					GUI.Toggle(rect, hasValue, name, GUI.skin.button);
					if (check.changed)
					{
						var value = GetValue(name);
						if (hasValue)
							property.intValue &= ~value;
						else
							property.intValue |= value;
					}
				}
			}
		}

		private bool HasValue(int current, params string[] names)
		{
			foreach (var name in names)
			{
				var value = GetValue(name);
				if ((current & value) != value)
					return false;
			}
			return true;
		}

		private int GetValue(string name)
		{
			var values = Enum.GetValues(fieldInfo.FieldType);
			foreach (var value in values)
				if (name == Enum.GetName(fieldInfo.FieldType, value))
					return (int) value;

			return 0;
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (!property.isExpanded || !attr.IsButton)
				return base.GetPropertyHeight(property, label);
				
			return base.GetPropertyHeight(property, label) * (property.enumNames.Length + 2);
		}
	}
}