using VPG.Editor.UI.Graphics.Renderers;
using UnityEngine;

namespace VPG.Editor.UI.Graphics
{
    internal class EntryJoint : GraphicalElement
    {
        private readonly Vector2 size = new Vector2(16f, 16f);
        private readonly EntryJointRenderer renderer;

        public EntryJoint(EditorGraphics editorGraphics, GraphicalElement parent = null) : base(editorGraphics, true, parent)
        {
            renderer = new EntryJointRenderer(this, editorGraphics.ColorPalette);
        }

        public override GraphicalElementRenderer Renderer
        {
            get
            {
                return renderer;
            }
        }

        public override Rect BoundingBox
        {
            get
            {
                return new Rect(Position - size, size * 2f);
            }
        }

        public override int Layer
        {
            get
            {
                return 0;
            }
        }
    }
}
