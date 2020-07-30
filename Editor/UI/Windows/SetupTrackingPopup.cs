﻿using UnityEditor;
using UnityEngine;

namespace Innoactive.CreatorEditor.Analytics
{
    internal class SetupTrackingPopup : EditorWindow
    {
        private static SetupTrackingPopup instance;

        public static void Open()
        {
            EditorApplication.update += ShowWindow;
        }

        private static void ShowWindow()
        {
            EditorApplication.update -= ShowWindow;
            if (instance == null)
            {
                instance = GetWindow<SetupTrackingPopup>(true);
                AssemblyReloadEvents.beforeAssemblyReload += HideWindow;
                instance.ShowUtility();
            }

            instance.minSize = new Vector2(280f, 245f);
            instance.maxSize = new Vector2(280f, 245f);
            instance.Focus();
        }

        private static void HideWindow()
        {
            instance.Close();
            instance = null;

            AssemblyReloadEvents.beforeAssemblyReload -= HideWindow;
        }

        private void OnGUI()
        {
            titleContent = new GUIContent("Usage statistics");

            EditorGUILayout.Space(4f);
            EditorGUILayout.HelpBox(new GUIContent("To improve the Creator, we collect anonymous data about your software configuration. This data excludes any sensitive data like source code, file names, or your courses structure. Right now we are tracking:\n\n * The Creator version\n * The Unity version\n * The system language\n\nYou can check the source code of our analytics engine in the following folder: Core/Editor/Analytics\n\nIf you want to disable tracking, open Innoactive > Creator > Windows > Analytics Settings in the Unity's menu bar."));
            EditorGUILayout.Space(4f);

            if (GUILayout.Button("Accept"))
            {
                AnalyticsUtils.SetTrackingTo(AnalyticsState.Enabled);
                Close();
            }

            GUIStyle hyperlink = new GUIStyle();
            hyperlink.normal.textColor = new Color(0.122f, 0.435f, 0.949f);
            Rect positionRect = new Rect(3, 222, 280, 50);

            GUILayout.BeginArea(positionRect);

            if (GUILayout.Button("Data Privacy Information", hyperlink, GUILayout.ExpandWidth(false)))
            {
                AnalyticsUtils.ShowDataPrivacyStatement();
            }

            GUILayout.EndArea();

            // Unity Editor UI has no way to underline text, so this is a fun workaround.
            positionRect.y += 1;
            GUILayout.BeginArea(positionRect);

            GUILayout.Label("____________________________", hyperlink);

            GUILayout.EndArea();
        }
    }

}
