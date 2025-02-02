using UnityEditor;
using UnityEngine;

namespace Yorozu.CustomProperty
{
    [CustomPropertyDrawer(typeof(ObjectRestrictAttribute))]
    public class ObjectRestrictDrawer : PropertyDrawer
    {
        private ObjectRestrictAttribute _attribute => attribute as ObjectRestrictAttribute;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = base.GetPropertyHeight(property, label);
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                EditorGUI.PropertyField(position, property, label);
                if (check.changed && property.propertyType == SerializedPropertyType.ObjectReference)
                    if (!CanUpdateObject(property))
                        property.objectReferenceValue = null;
            }
        }

        private bool CanUpdateObject(SerializedProperty property)
        {
            switch (_attribute.target)
            {
                case TargetType.ChildOnly:
                {
                    var obj = property.serializedObject.targetObject as Component;
                    var target = property.objectReferenceValue as GameObject;
                    if (target == null || !target.activeInHierarchy)
                        return false;
                    return HasChildRecursive(obj.transform, target.transform);
                }

                case TargetType.InHierarchy:
                {
                    var target = property.objectReferenceValue as GameObject;
                    return target != null && target.activeInHierarchy;
                }

                case TargetType.InProject:
                {
                    return !string.IsNullOrEmpty(AssetDatabase.GetAssetPath(property.objectReferenceValue));
                }
            }

            return true;
        }

        private bool HasChildRecursive(Transform parent, Transform target)
        {
            foreach (Transform child in parent)
            {
                if (child.Equals(target))
                    return true;

                if (HasChildRecursive(child, target))
                    return true;
            }

            return false;
        }
    }
}