using System;
using UnityEditor;
using UnityEngine;

namespace VPG.Editor.UI.Drawers
{
    /// <summary>
    /// Training drawer for boolean members.
    /// </summary>
    [DefaultTrainingDrawer(typeof(bool))]
    internal class BoolDrawer : AbstractDrawer
    {
        /// <inheritdoc />
        public override Rect Draw(Rect rect, object currentValue, Action<object> changeValueCallback, GUIContent label)
        {
            rect.height = EditorDrawingHelper.SingleLineHeight;

            bool oldValue = (bool)currentValue;
            bool newValue = EditorGUI.ToggleLeft(rect, label, oldValue);

            if (oldValue != newValue)
            {
                ChangeValue(() => newValue, () => oldValue, changeValueCallback);
            }

            return rect;
        }
    }
}
