using EditorUtilities.Editor.Extensions;
using UnityEditor;
using UnityEngine;

namespace GMTK.Utilities
{
    [CustomPropertyDrawer(typeof(RandomArray<>))]
    public class RandomArrayDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            var dataProperty = property.FindPropertyRelative("m_Data");

            DrawArray(position, dataProperty, property);
        }

        public static void DrawArray(Rect position, SerializedProperty dataProperty,SerializedProperty property, bool drawLabel = true)
        {
            if (dataProperty.arraySize == 0)
            {
                dataProperty.InsertArrayElementAtIndex(0);
            }
            
            if (dataProperty.arraySize <= 1)
            {
                var buttonSize = EditorGUIUtility.singleLineHeight * 2;
                var buttonRect = new Rect(position);
                buttonRect.height = EditorGUIUtility.singleLineHeight;
                
                
                buttonRect.ShrinkLeft(position.width - buttonSize);
                position.ShrinkRight(buttonSize);
                if (drawLabel)
                {
                    var labelContent = new GUIContent(property.displayName);
                    var labelRect = new Rect(position);
                    var labelSize = GUI.skin.label.CalcSize(labelContent).x + EditorGUIUtility.singleLineHeight * 1.5f;
                    labelRect.width = GUI.skin.label.CalcSize(labelContent).x;
                    EditorGUI.LabelField(labelRect, labelContent);

                    position.ShrinkLeft(labelSize);
                }

                position.height = EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(position, dataProperty.GetArrayElementAtIndex(0), GUIContent.none);
                if (GUI.Button(buttonRect, "+"))
                {
                    dataProperty.InsertArrayElementAtIndex(1);
                    dataProperty.isExpanded = true;
                }


                return;
            }

            EditorGUI.PropertyField(position, dataProperty, new GUIContent(property.displayName));
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var dataProperty = property.FindPropertyRelative("m_Data");
            if (dataProperty.arraySize <= 1)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            return EditorGUI.GetPropertyHeight(dataProperty);
            
        }
    }
}