using VPG.CreatorEditor.Configuration;
using UnityEditor;

namespace VPG.CreatorEditor.CreatorMenu
{
    internal static class SetupSceneEntry
    {
        /// <summary>
        /// Setup the current unity scene to be a functioning training scene.
        /// </summary>
        [MenuItem("Innoactive/Setup Training Scene", false, 16)]
        public static void SetupScene()
        {
            TrainingSceneSetup.Run();
        }
    }
}
