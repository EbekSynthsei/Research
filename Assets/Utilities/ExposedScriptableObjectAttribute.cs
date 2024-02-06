using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LaniakeaCode.Utilities
{
    /// <summary>
    /// Attribute to be used to expose ScriptableObjects in other objects
    /// Use without the Attribute name
    /// </summary>
    public class ExposedScriptableObjectAttribute : PropertyAttribute
    {

    }

    /// <summary>
    /// Drawer for the ExposedScriptableObject Attribute
    /// </summary>
    [CustomPropertyDrawer(typeof(ExposedScriptableObjectAttribute))]
    public class ExposedScriptableObjectAttributeDrawer : PropertyDrawer
    {
        private Editor editor = null;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //LABEL
            EditorGUI.PropertyField(position, property, label, true);
            
            //DRAW ARROW
            if(property.objectReferenceValue != null)
            {
                Rect foldoutPosition = new Rect(position.x + EditorGUI.indentLevel * 15f, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
                property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);
            }

            //DRAW FOLDOUT PROPERTIES
            if (property.isExpanded && property.objectReferenceValue != null)
            {

                //Make Child indented
                EditorGUI.indentLevel++;

                //DRAW OBJ PROPERTIES
                if (!editor && property.objectReferenceValue != null)
                {
                    
                    Editor.CreateEditor(property.objectReferenceValue);

                }
                if (editor != null)
                {
                    editor.OnInspectorGUI();
                }

                //RESET INDENT
                EditorGUI.indentLevel--;
            }
        }
    }
}