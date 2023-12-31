using UnityEditor;
using UnityEngine;

namespace Yorozu.CustomProperty
{
	[CustomPropertyDrawer(typeof(PreviewAttribute))]
	public class DisplayDrawer : PropertyDrawer
	{
		private Editor _cacheEditor;
		private const int PreviewMin = 150;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType != SerializedPropertyType.ObjectReference || property.objectReferenceValue == null)
			{
				EditorGUI.PropertyField(position, property, label);
				return;
			}

			position.height = base.GetPropertyHeight(property, label);
			EditorGUI.PropertyField(position, property, label);
			property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);

			if (!property.isExpanded)
				return;

			var height = Mathf.Min(PreviewMin, EditorGUIUtility.currentViewWidth);
			position.y += position.height;
			position.height = height;
			if (_cacheEditor == null)
				_cacheEditor = Editor.CreateEditor(property.objectReferenceValue);

			_cacheEditor.DrawPreview(position);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (!property.isExpanded)
				return base.GetPropertyHeight(property, label);

			if (property.propertyType != SerializedPropertyType.ObjectReference ||
			    property.objectReferenceValue == null)
				return base.GetPropertyHeight(property, label);

			var height = Mathf.Min(PreviewMin, EditorGUIUtility.currentViewWidth);

			return base.GetPropertyHeight(property, label) + height;
		}
	}
}