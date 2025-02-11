using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VPG.Editor.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;

namespace VPG.Editor.UI
{
    /// <summary>
    /// Layout extension for the VR Process Gizmo.
    /// </summary>
    public static class VPGGUILayout
    {
        /// <summary>
        /// Draws a clickable link which opens a website.
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        /// <param name="url">url to be opened inside the browser</param>
        /// <param name="indent">Intend on the left</param>
        public static void DrawLink(string text, string url, int indent = VPGEditorStyles.Indent)
        {
            DrawLink(text, () =>
            {
                try
                {
                    Process.Start(url);
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }
            }, indent);
        }

        /// <summary>
        /// Draws a clickable link which looks like a hyperlink.
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        /// <param name="action">action done on click</param>
        /// <param name="indent">Intend on the left</param>
        public static void DrawLink(string text, Action action, int indent = VPGEditorStyles.Indent)
        {
            if (GUILayout.Button(text, VPGEditorStyles.ApplyPadding(VPGEditorStyles.Link, indent)))
            {
                action.Invoke();
            }

            Rect buttonRect = GUILayoutUtility.GetLastRect();
            GUI.Label(new Rect(buttonRect.x, buttonRect.y + 1, buttonRect.width, buttonRect.height), new String('_', 256), VPGEditorStyles.ApplyPadding(VPGEditorStyles.Link, indent));
            EditorGUIUtility.AddCursorRect(buttonRect, MouseCursor.Link);
        }

        public static string DrawTextField(string content, int charLimit = -1, params GUILayoutOption[] options)
        {
            return DrawTextField(content, charLimit, 0, options);
        }

        public static string DrawTextField(string content, int charLimit = -1, int indent = 0, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal();
            {
                GUIStyle style = VPGEditorStyles.TextField;
                if (indent != 0)
                {
                    style = VPGEditorStyles.ApplyPadding(style, indent);
                }

                content = GUILayout.TextField(content, charLimit, style, options);

                Rect textFieldRect = GUILayoutUtility.GetLastRect();
                EditorGUIUtility.AddCursorRect(textFieldRect, MouseCursor.Text);
                if (charLimit > 0)
                {
                    GUILayout.Label($"{content.Length}/{charLimit}");
                }
            }
            GUILayout.EndHorizontal();
            return content;
        }

        public static T DrawToggleGroup<T>(T selection, List<T> entries, List<string> content)
        {
            return DrawToggleGroup(selection, entries, content.Select(str => new GUIContent(str)).ToList());
        }

        public static T DrawToggleGroup<T>(T selection, List<T> entries, List<GUIContent> content)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                if (GUILayout.Toggle(entries[i].Equals(selection), content[i], VPGEditorStyles.Toggle))
                {
                    if (!selection.Equals(entries[i]))
                    {
                        selection = entries[i];
                    }
                }
            }

            return selection;
        }
    }
}
