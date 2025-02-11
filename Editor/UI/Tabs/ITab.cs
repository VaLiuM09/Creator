using UnityEngine;

namespace VPG.Editor.Tabs
{
    /// <summary>
    /// A tab in the <seealso cref="ITabsGroup"/>
    /// </summary>
    internal interface ITab
    {
        /// <summary>
        /// A label to display in the tab view.
        /// </summary>
        GUIContent Label { get; }

        /// <summary>
        /// When user selects this tab, the Step Inspector displays the value from this method.
        /// </summary>
        object GetValue();

        /// <summary>
        /// When user has modified the object under this tab, the Step Inspector invokes this method. It should assign the new value to the actual property.
        /// </summary>
        void SetValue(object value);

        /// <summary>
        /// Will be called when this tab is selected.
        /// </summary>
        void OnSelected();

        /// <summary>
        /// Will be called when this tab is unselected.
        /// </summary>
        void OnUnselect();
    }
}
