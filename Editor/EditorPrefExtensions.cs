﻿using Microsoft.Win32;
using System;
using UnityEditor;
using UnityEngine;

namespace VPG.Editor.Analytics
{
    internal static class EditorPrefExtensions
    {
        /// <summary>
        /// Returns a value from editor preferences as enum.
        /// </summary>
        /// <param name="key">key of the entry</param>
        /// <param name="defaultValue">Value which should be returned if no entry is found</param>
        /// <typeparam name="T">Enum Type</typeparam>
        public static T GetEnum<T>(string key, T defaultValue) where T : Enum
        {
            string value = EditorPrefs.GetString(key, null);
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Sets an enum value as string into EditorPrefs
        /// </summary>
        /// <param name="key">Key of the entry</param>
        /// <param name="value">value which should be stored</param>
        /// <typeparam name="T">Enum Type</typeparam>
        public static void SetEnum<T>(string key, T value) where T : Enum
        {
            EditorPrefs.SetString(key, value.ToString());
        }
    }
}
