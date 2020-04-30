using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Yorozu.CustomProperty
{
	[CustomPropertyDrawer(typeof(InspectorAttribute))]
	[CustomPropertyDrawer(typeof(Text))]
	[CustomPropertyDrawer(typeof(Image))]
	public class InspectorDrawer : PropertyDrawer
	{
		private Editor _cacheEditor;
		
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			position.height = base.GetPropertyHeight(property, label);
			EditorGUI.PropertyField(position, property, label);
			if (property.objectReferenceValue == null)
				return;

			property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);

			if (!property.isExpanded)
				return;
			
			if (_cacheEditor == null)
				_cacheEditor = Editor.CreateEditor(property.objectReferenceValue);

			using (new EditorGUI.IndentLevelScope())
			{
				using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
				{
					_cacheEditor.OnInspectorGUI();
				}
			}
		}
	}
}