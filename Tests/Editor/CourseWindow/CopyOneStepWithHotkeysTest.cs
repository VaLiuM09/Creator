using System.Collections.Generic;
using VPG.Core;
using VPG.Editor.UI.Windows;
using NUnit.Framework;

namespace VPG.Editor.Tests.CourseWindowTests
{
    internal class CopyOneStepWithHotkeysTest : BaseCourseWindowTest
    {
        public override string WhenDescription => "Add one step to the workflow. Copy it with CTRL+C, CTRL+V.";

        public override string ThenDescription => "Then there are two identical steps.";

        protected override void Then(CourseWindow window)
        {
            IList<IStep> steps = window.GetCourse().Data.FirstChapter.Data.Steps;
            Assert.AreEqual(2, steps.Count);
            Assert.AreEqual("Copy of " + steps[0].Data.Name, steps[1].Data.Name);
            Assert.True(steps[0].Data.Behaviors.Data.Behaviors.Count == 0);
            Assert.True(steps[1].Data.Behaviors.Data.Behaviors.Count == 0);
            Assert.True(steps[0].Data.Transitions.Data.Transitions.Count == 1);
            Assert.True(steps[1].Data.Transitions.Data.Transitions.Count == 1);
            Assert.Null(steps[0].Data.Transitions.Data.Transitions[0].Data.TargetStep);
            Assert.Null(steps[1].Data.Transitions.Data.Transitions[0].Data.TargetStep);
        }
    }
}
