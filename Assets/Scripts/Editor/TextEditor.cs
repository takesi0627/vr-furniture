using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

namespace hekira
{
    [CustomEditor(typeof(Text))]
    public class TextEditor : UnityEditor.UI.TextEditor
    {
        #region GUI_STRINGS
        private static GUIContent TEXT_INDEX = new GUIContent("Text Index", "Text Index");
        #endregion

        Text uiText;

        public override void OnInspectorGUI()
        {
            uiText = (Text)target;

            base.OnInspectorGUI();

            EditorGUI.BeginChangeCheck();
            uiText.TextIndex = (TextIndex)EditorGUILayout.EnumPopup(TEXT_INDEX, uiText.TextIndex);
            if (EditorGUI.EndChangeCheck()) {
                uiText.text = TextUtility.GetText(uiText.TextIndex);
            }
        }
    }
}