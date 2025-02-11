using System;
using System.Linq;
#if CREATOR_PRO
using Innoactive.CreatorPro.Account;
#endif
using UnityEditor;
using UnityEngine;

namespace VPG.Editor.Analytics
{
    /// <summary>
    /// Checks on each recompile/start of the unity if we have already sent a hello.
    /// Adding -no-tracking when starting unity will disable analytics automatically.
    /// </summary>
    [InitializeOnLoad]
    internal class AnalyticsSetup
    {
        private const string KeyLastDayActive = "Innoactive.Creator.Analytics.LastDayActive";

        static AnalyticsSetup()
        {
            //VPG - Force analytics disabled.
            //AnalyticsState trackingState = AnalyticsUtils.GetTrackingState();
            AnalyticsState trackingState = AnalyticsState.Disabled;

            if (trackingState == AnalyticsState.Disabled)
            {
                return;
            }
            // Can be used by ci to deactivate tracking.
            if (Environment.GetCommandLineArgs().Contains("-no-tracking"))
            {
                AnalyticsUtils.SetTrackingTo(AnalyticsState.Disabled);
                return;
            }

            if (trackingState == AnalyticsState.Unknown)
            {
                SetupTrackingPopup.Open();
                AnalyticsUtils.SetTrackingTo(AnalyticsState.Enabled);
                return;
            }

            // Only run once a day.
            if (DateTime.Today.Ticks.ToString().Equals(EditorPrefs.GetString(KeyLastDayActive, null)) == false)
            {
                EditorPrefs.SetString(KeyLastDayActive, DateTime.Today.Ticks.ToString());
                IAnalyticsTracker tracker = AnalyticsUtils.CreateTracker();

                tracker.SendSessionStart();
                // Send the Unity Editor version.
                tracker.Send(new AnalyticsEvent() {Category = "unity", Action = "version", Label = Application.unityVersion});
                // Send the VPG Core version.
                tracker.Send(new AnalyticsEvent() {Category = "creator", Action = "version", Label = EditorUtils.GetCoreVersion()});
                // Send the Creator license type.
#if CREATOR_PRO
                tracker.Send(new AnalyticsEvent() {Category = "creator", Action = "license", Label = UserAccount.IsCustomer() ? "customer" : "trial"});
#else
                tracker.Send(new AnalyticsEvent() {Category = "creator", Action = "license", Label = "free"});
#endif
            }
        }
    }
}
