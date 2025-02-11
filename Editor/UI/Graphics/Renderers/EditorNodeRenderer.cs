﻿using UnityEngine;

namespace VPG.Editor.UI.Graphics.Renderers
{
    /// <summary>
    /// Base class for rendering nodes of the Workflow window (entry node, exit node, and step node).
    /// </summary>
    internal abstract class EditorNodeRenderer<TOwner> : ColoredGraphicalElementRenderer<TOwner> where TOwner : EditorNode
    {
        ///<inheritdoc />
        public override Color NormalColor
        {
            get
            {
                return ColorPalette.ElementBackground;
            }
        }

        public EditorNodeRenderer(TOwner owner, WorkflowEditorColorPalette colorPalette) : base(owner, colorPalette)
        {
        }
    }
}
