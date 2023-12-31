using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Yorozu.CustomProperty
{
	[CustomPropertyDrawer(typeof(AddComponentAttribute))]
	public class AddComponentPropertyDrawer : PropertyDrawer
	{
		/// <summary>
		/// 取得できなかった場合何度も取得処理を行わせないため
		/// </summary>
		private int _getCount;
		
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.PropertyField(position, property, label);

			if (property.propertyType != SerializedPropertyType.ObjectReference)
				return;
			if (property.objectReferenceValue != null)
				return;

			if (_getCount++ > 0)
				return;
			var type = property.serializedObject.targetObject.GetType();
			var fieldInfo = type.GetField(property.propertyPath, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
			if (fieldInfo == null)
				return;
			
			var fieldType = fieldInfo.FieldType;
			if (!fieldType.IsSubclassOf(typeof(Component)))
				return;
			
			var comp = property.serializedObject.targetObject as Component;

			if (comp == null)
				return;

			var addComponent = comp.gameObject.GetComponent(fieldType);
			property.objectReferenceValue = addComponent != null ? addComponent : comp.gameObject.AddComponent(fieldType);
		}
	}
}