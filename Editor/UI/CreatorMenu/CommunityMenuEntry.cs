using UnityEditor;
using UnityEngine;

namespace VPG.CreatorEditor.CreatorMenu
{
    internal static class CommunityMenuEntry
    {
        /// <summary>
        /// Allows to open the URL to Innoactive community.
        /// </summary>
        [MenuItem("VPG/Innoactive Help/Community", false, 80)]
        private static void OpenCommunityPage()
        {
            Application.OpenURL("https://innoactive.io/creator/community");
        }
    }
}
