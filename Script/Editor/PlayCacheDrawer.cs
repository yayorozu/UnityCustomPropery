using UnityEditor;
using UnityEngine;

namespace Yorozu.CustomProperty
{
	[CustomPropertyDrawer(typeof(PlayCacheAttribute))]
	public class PlayCacheDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (!Application.isPlaying)
			{
				if (HasKey(property))
				{
					position.height = EditorGUIUtility.singleLineHeight;
					if (GUI.Button(position, "Apply Cache Data"))
						CheckCache(property);

					position.y += EditorGUIUtility.singleLineHeight;
					position.height = base.GetPropertyHeight(property, label);
				}

				EditorGUI.PropertyField(position, property, label);
				return;
			}

			using (var check = new EditorGUI.ChangeCheckScope())
			{
				EditorGUI.PropertyField(position, property, label);
				if (check.changed)
					SavePrefs(property);
			}
		}
		
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (Application.isPlaying)
				return base.GetPropertyHeight(property, label);
			
			if (HasKey(property))
				return base.GetPropertyHeight(property, label) + EditorGUIUtility.singleLineHeight + 10;
			
			return base.GetPropertyHeight(property, label);
		}

		private bool HasKey(SerializedProperty property)
		{
			return EditorPrefs.HasKey(Key(property));
		}

		private string Key(SerializedProperty property)
		{
			return $"{Application.productName}:{property.serializedObject.targetObject.GetType()}:{fieldInfo.FieldType}:{property.propertyPath}";
		}

		private void CheckCache(SerializedProperty property)
		{
			string key = Key(property);
			
			switch (property.propertyType)
			{
				case SerializedPropertyType.Integer:
				case SerializedPropertyType.LayerMask:
				case SerializedPropertyType.Enum:
					property.intValue = EditorPrefs.GetInt(key);
					break;
				case SerializedPropertyType.Boolean:
					property.boolValue = EditorPrefs.GetBool(key);
					break;
				case SerializedPropertyType.Float:
					property.floatValue = EditorPrefs.GetFloat(key);
					break;
				case SerializedPropertyType.String:
					property.stringValue = EditorPrefs.GetString(key); 
					break;
				case SerializedPropertyType.Color:
					property.colorValue = JsonUtility.FromJson<Color>(EditorPrefs.GetString(key));
					break;
				case SerializedPropertyType.Vector2:
					property.vector2Value = JsonUtility.FromJson<Vector2>(EditorPrefs.GetString(key));
					break;
				case SerializedPropertyType.Vector3:
					property.vector3Value = JsonUtility.FromJson<Vector3>(EditorPrefs.GetString(key));
					break;
				case SerializedPropertyType.Vector4:
					property.vector4Value = JsonUtility.FromJson<Vector4>(EditorPrefs.GetString(key));
					break;
				case SerializedPropertyType.Rect:
					property.rectValue = JsonUtility.FromJson<Rect>(EditorPrefs.GetString(key));
					break;
				case SerializedPropertyType.Bounds:
					property.boundsValue = JsonUtility.FromJson<Bounds>(EditorPrefs.GetString(key));
					break;
				case SerializedPropertyType.Quaternion:
					property.quaternionValue = JsonUtility.FromJson<Quaternion>(EditorPrefs.GetString(key));
					break;
				case SerializedPropertyType.Vector2Int:
					property.vector2IntValue = JsonUtility.FromJson<Vector2Int>(EditorPrefs.GetString(key));
					break;
				case SerializedPropertyType.Vector3Int:
					property.vector3IntValue = JsonUtility.FromJson<Vector3Int>(EditorPrefs.GetString(key));
					break;
				case SerializedPropertyType.RectInt:
					property.rectIntValue = JsonUtility.FromJson<RectInt>(EditorPrefs.GetString(key));
					break;
				case SerializedPropertyType.BoundsInt:
					property.boundsIntValue = JsonUtility.FromJson<BoundsInt>(EditorPrefs.GetString(key));
					break;
				case SerializedPropertyType.FixedBufferSize:
				case SerializedPropertyType.ExposedReference:
				case SerializedPropertyType.Character:
				case SerializedPropertyType.ArraySize:
				case SerializedPropertyType.Gradient:
				case SerializedPropertyType.AnimationCurve:
				case SerializedPropertyType.ObjectReference:
				case SerializedPropertyType.Generic:
					break;
			}
			EditorPrefs.DeleteKey(key);
		}

		private void SavePrefs(SerializedProperty property)
		{
			string key = Key(property);
			
			switch (property.propertyType)
			{
				case SerializedPropertyType.Integer:
				case SerializedPropertyType.LayerMask:
				case SerializedPropertyType.Enum:
					EditorPrefs.SetInt(key, property.intValue);
					break;
				case SerializedPropertyType.Boolean:
					EditorPrefs.SetBool(key, property.boolValue);
					break;
				case SerializedPropertyType.Float:
					EditorPrefs.SetFloat(key, property.floatValue);
					break;
				case SerializedPropertyType.String:
					EditorPrefs.SetString(key, property.stringValue);
					break;
				case SerializedPropertyType.Color:
					EditorPrefs.SetString(key, JsonUtility.ToJson(property.colorValue));
					break;
				case SerializedPropertyType.Vector2:
					EditorPrefs.SetString(key, JsonUtility.ToJson(property.vector2Value));
					break;
				case SerializedPropertyType.Vector3:
					EditorPrefs.SetString(key, JsonUtility.ToJson(property.vector3Value));
					break;
				case SerializedPropertyType.Vector4:
					EditorPrefs.SetString(key, JsonUtility.ToJson(property.vector4Value));
					break;
				case SerializedPropertyType.Rect:
					EditorPrefs.SetString(key, JsonUtility.ToJson(property.rectValue));
					break;
				case SerializedPropertyType.Bounds:
					EditorPrefs.SetString(key, JsonUtility.ToJson(property.boundsValue));
					break;
				case SerializedPropertyType.Quaternion:
					EditorPrefs.SetString(key, JsonUtility.ToJson(property.quaternionValue));
					break;
				case SerializedPropertyType.Vector2Int:
					EditorPrefs.SetString(key, JsonUtility.ToJson(property.vector2IntValue));
					break;
				case SerializedPropertyType.Vector3Int:
					EditorPrefs.SetString(key, JsonUtility.ToJson(property.vector3IntValue));
					break;
				case SerializedPropertyType.RectInt:
					EditorPrefs.SetString(key, JsonUtility.ToJson(property.rectIntValue));
					break;
				case SerializedPropertyType.BoundsInt:
					EditorPrefs.SetString(key, JsonUtility.ToJson(property.boundsIntValue));
					break;
				case SerializedPropertyType.FixedBufferSize:
				case SerializedPropertyType.ExposedReference:
				case SerializedPropertyType.Character:
				case SerializedPropertyType.ArraySize:
				case SerializedPropertyType.Gradient:
				case SerializedPropertyType.AnimationCurve:
				case SerializedPropertyType.ObjectReference:
				case SerializedPropertyType.Generic:
					break;
			}
		}
	}
}