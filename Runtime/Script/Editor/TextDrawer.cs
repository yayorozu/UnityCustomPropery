using TMPro;
using UnityEditor;
using UnityEngine;

namespace Yorozu.CustomProperty
{
    [CustomPropertyDrawer(typeof(TextAttribute))]
    public class TextDrawer : PropertyDrawer
    {
        private TextMeshProUGUI _cache;
 
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_cache == null && property.objectReferenceValue != null)
            {
                _cache = property.objectReferenceValue as TextMeshProUGUI;
            }
 
            if (_cache == null)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }
 
            position.height = base.GetPropertyHeight(property, label);
            EditorGUI.PropertyField(position, property, label);
            position.xMin += 10;
            position.y += position.height;
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                _cache.text = EditorGUI.TextField(position, "Text", _cache.text);
                if (check.changed)
                {
                    EditorUtility.SetDirty(_cache);
                }
            }
        }
 
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (_cache == null)
                return base.GetPropertyHeight(property, label);
 
            return base.GetPropertyHeight(property, label) * 2;
        }
    }
}