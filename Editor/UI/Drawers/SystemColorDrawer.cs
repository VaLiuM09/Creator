using System;
using UnityEditor;
using UnityEngine;

namespace VPG.Editor.UI.Drawers
{
    /// <summary>
    /// Training drawer for `System.Color` members.
    /// </summary>
    [DefaultTrainingDrawer(typeof(Color))]
    internal class SystemColorDrawer : AbstractDrawer
    {
        /// <inheritdoc />
        public override Rect Draw(Rect rect, object currentValue, Action<object> changeValueCallback, GUIContent label)
        {
            rect.height = EditorDrawingHelper.SingleLineHeight;

            Color color = (Color)currentValue;
            Color newColor = EditorGUI.ColorField(rect, label, color);

            if (color != newColor)
            {
                ChangeValue(() => newColor, () => color, changeValueCallback);
            }

            return rect;
        }
    }
}
