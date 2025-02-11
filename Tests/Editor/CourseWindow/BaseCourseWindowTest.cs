using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using VPG.Core;
using VPG.Editor.TestTools;
using VPG.Editor.UI.Windows;
using UnityEditor;

namespace VPG.Editor.Tests.CourseWindowTests
{
    /// <summary>
    /// Base class for all training window tests.
    /// </summary>
    internal abstract class BaseCourseWindowTest : EditorImguiTest<CourseWindow>
    {
        /// <summary>
        /// Returns all <see cref="ITransition"/>s contained in given <see cref="IStep"/>.
        /// <remarks>
        /// It also asserts that given <see cref="IStep"/> contains valid <see cref="ITransition"/>s.
        /// </remarks>
        /// </summary>
        protected static IList<ITransition> GetTransitionsFromStep(IStep step)
        {
            IList<ITransition> transitions = step.Data.Transitions.Data.Transitions;
            Assert.NotNull(transitions);
            return transitions;
        }

        /// <summary>
        /// Returns the <see cref="ICourse"/> contained in given <see cref="CourseWindow"/>.
        /// </summary>
        protected static ICourse ExtractTraining(CourseWindow window)
        {
            ICourse course = window.GetCourse();
            Assert.NotNull(course);
            return course;
        }

        /// <summary>
        /// Tries to access targeted <see cref="IStep"/> from given <see cref="ITransition"/>.
        /// </summary>
        /// <param name="transition"><see cref="ITransition"/> where target <see cref="IStep"/> will be extracted.</param>
        /// <param name="step">Returned value from <see cref="ITransition"/>'s TargetStep.</param>
        /// <returns>True if given <see cref="ITransition"/> contains a target <see cref="IStep"/>, otherwise, false.</returns>
        protected static bool TryToGetStepFromTransition(ITransition transition, out IStep step)
        {
            step = transition.Data.TargetStep;
            return step != null;
        }

        /// <inheritdoc />
        public override string GivenDescription => "A training window with empty training and fixed size of 1024x512 pixels.";

        /// <inheritdoc />
        protected override string AssetFolderForRecordedActions => EditorUtils.GetCoreFolder() + "/Tests/Editor/CourseWindow/Records";

        /// <inheritdoc />
        protected override CourseWindow Given()
        {
            if (EditorUtils.IsWindowOpened<CourseWindow>())
            {
                EditorWindow.GetWindow<CourseWindow>().Close();
            }

            GlobalEditorHandler.SetStrategy(new EmptyTestStrategy());

            EditorUtils.ResetKeyboardElementFocus();
            CourseWindow window = ScriptableObject.CreateInstance<CourseWindow>();
            window.ShowUtility();
            window.position = new Rect(Vector2.zero, window.position.size);
            window.minSize = window.maxSize = new Vector2(1024f, 512f);
            window.SetCourse(new Course("Test", new Chapter("Test", null)));
            window.Focus();

            return window;
        }

        /// <inheritdoc />
        protected override void AdditionalTeardown()
        {
            if (EditorUtils.IsWindowOpened<CourseWindow>())
            {
                EditorWindow.GetWindow<CourseWindow>().Close();
            }

            base.AdditionalTeardown();
            GlobalEditorHandler.SetDefaultStrategy();
        }
    }
}
