using UnityEditor;
using UnityEngine;

namespace VPG.CreatorEditor.UI.Wizard
{
    internal class WelcomePage : WizardPage
    {
        public WelcomePage() : base("Welcome")
        {

        }

        public override void Draw(Rect window)
        {
            GUILayout.BeginArea(window);
                GUILayout.Label("Welcome to the Creator", CreatorEditorStyles.Title);
                GUILayout.Label("We want to get you started with the Creator as fast as possible.\nThis Wizard guides you through the process.", CreatorEditorStyles.Paragraph);
            GUILayout.EndArea();
        }
    }
}
