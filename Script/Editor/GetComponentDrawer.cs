using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Yorozu.CustomProperty
{
	[CustomPropertyDrawer(typeof(GetComponentAttribute))]
	public class GetComponentDrawer : PropertyDrawer
	{
		private const float ButtonWidth = 32;
		
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType != SerializedPropertyType.ObjectReference)
			{
				EditorGUI.PropertyField(position, property, label);
				return;
			}

			if (property.objectReferenceValue != null)
			{
				EditorGUI.PropertyField(position, property, label);
				return;
			}

			position.width -= ButtonWidth;
			EditorGUI.PropertyField(position, property, label);
			position.x += position.width;
			position.width = ButtonWidth;
			if (GUI.Button(position, "Get"))
			{
				GetComponent(position, property);
			}
		}

		private void GetComponent(Rect position, SerializedProperty property)
		{
			var obj = property.serializedObject.targetObject as Component;
			if (obj == null)
			{
				Debug.LogError("GameObject Not Found");
				return;
			}

			var type = property.serializedObject.targetObject.GetType();
			var fieldInfo = type.GetField(property.propertyPath, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
			if (fieldInfo == null)
				return;
			
			var fieldType = fieldInfo.FieldType;
			
			var components = obj.GetComponentsInChildren(fieldType);
			if (components.Length == 0)
				Debug.Log("Component Not Found");
			else if (components.Length == 1)
				property.objectReferenceValue = components[0];
			else
				PopupWindow.Show(position, new Popup(property, components));
		}

		private class Popup : PopupWindowContent
		{
			private readonly TreeView _treeView;
			
			public Popup(SerializedProperty property, Component[] components)
			{
				_treeView = new PopupTreeView(new TreeViewState(), this, property, components);
				GUIUtility.keyboardControl = _treeView.treeViewControlID;
			}
			
			public override void OnGUI(Rect rect)
			{
				_treeView.OnGUI(rect);
			}
			
			private class PopupTreeView : TreeView
			{
				private readonly SerializedProperty _property;
				private readonly Popup _popup;
				private readonly Component[] _components;

				public PopupTreeView(TreeViewState state, Popup popup, SerializedProperty property, Component[] components) : base(state)
				{
					_property = property;
					_popup = popup;
					_components = components;
					Reload();
				}

				public PopupTreeView(TreeViewState state, MultiColumnHeader multiColumnHeader) : base(state, multiColumnHeader)
				{
				}
				
				protected override TreeViewItem BuildRoot()
				{
					var root = new TreeViewItem(-1, -1, "root");
					for (var i = 0; i < _components.Length; ++i)
						root.AddChild(new TreeViewItem(i, 0, _components[i].name));

					return root;
				}

				protected override void SingleClickedItem(int id)
				{
					DecideObject();
				}
				
				private void DecideObject()
				{
					if (state.selectedIDs.Count <= 0)
						return;

					_property.serializedObject.Update();
					_property.objectReferenceValue = _components[state.selectedIDs.First()];
					_property.serializedObject.ApplyModifiedProperties();
					_popup.editorWindow.Close();
				}
			}
		}
	}
}