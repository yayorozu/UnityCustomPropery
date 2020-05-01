using UnityEditor;
using UnityEngine;

namespace Yorozu.CustomProperty
{
	[CustomPropertyDrawer(typeof(InspectorAttribute))]
	public class InspectorDrawer : PropertyDrawer
	{
		private Texture2D _texture;
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.objectReferenceValue == null)
			{
				EditorGUI.PropertyField(position, property, label);
				return;
			}

			var rect = new Rect(position); 
			rect.width -= 20;
			EditorGUI.PropertyField(rect, property, label);
			rect.x += rect.width;
			rect.width = 20;

			if (_texture == null)
				_texture = (Texture2D) EditorGUIUtility.Load("UnityEditor.InspectorWindow");
			
			if (GUI.Button(rect, _texture, GUI.skin.label))
				PopupWindow.Show(position, new Popup(property, position.width));
		}
		
		private class Popup : PopupWindowContent
		{
			private readonly Editor _cacheEditor;
			private readonly float _width;
			private Vector2 _position;
            		
			public Popup(SerializedProperty property, float width)
			{
				_cacheEditor = Editor.CreateEditor(property.objectReferenceValue);
				_width = width;
				_position = Vector2.zero;
			}

			public override void OnGUI(Rect rect)
			{
				using (new EditorGUI.IndentLevelScope())
				{
					using (var scroll = new EditorGUILayout.ScrollViewScope(_position))
					{
						_position = scroll.scrollPosition;
						EditorGUIUtility.hierarchyMode = true;
						_cacheEditor.OnInspectorGUI();
					}
				}
				if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Escape)
				{
					editorWindow.Close();
				}
			}
			
			public override Vector2 GetWindowSize()
			{
				// CustomEditorの高さが取得できないので固定
				return new Vector2(_width, 300f);
			}
		}
	}
}