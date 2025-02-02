using UnityEditor;
using UnityEngine;

namespace Yorozu.CustomProperty
{
    [CustomPropertyDrawer(typeof(ValidateNotNullAttribute))]
    public class ValidateNotNullDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = base.GetPropertyHeight(property, label);
            EditorGUI.PropertyField(position, property, label);
            position.y += position.height;
            if (IsRequire(property))
            {
                EditorGUI.HelpBox(position, "Null value detected. Please provide a value.", MessageType.Error);
            }
        }
 
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (IsRequire(property))
            {
                return base.GetPropertyHeight(property, label) * 2f;
            }
            return base.GetPropertyHeight(property, label);
        }
 
        private bool IsRequire(SerializedProperty property)
        {
            if (property.isArray)
                return property.arraySize == 0;
 
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    return property.intValue == 0;
                case SerializedPropertyType.Float:
                    return property.floatValue == 0f;
                case SerializedPropertyType.String:
                    return property.stringValue == "";
                case SerializedPropertyType.ObjectReference:
                    return property.objectReferenceValue == null;
            }
 
            return false;
        }
    }

}