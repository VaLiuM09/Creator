using VPG.Core;
using VPG.Tests.Utils;
using NUnit.Framework;
using UnityEngine;

namespace VPG.Tests
{
    public class EntityFactoryTests : RuntimeTests
    {
        [Test]
        public void CreateAStep()
        {
            string stepName = "Test Step";
            Vector2 stepPosition = default;

            IStep step = EntityFactory.CreateStep(stepName);

            Assert.NotNull(step);
            Assert.That(step is IStep);
            Assert.That(step.Data.Name == stepName);
            Assert.That(step.StepMetadata.Position == stepPosition);
        }

        [Test]
        public void CreateAStepWithPosition()
        {
            string stepName = "Test Step";
            Vector2 stepPosition = Vector2.left;
            IStep step = EntityFactory.CreateStep(stepName, stepPosition);

            Assert.NotNull(step);
            Assert.That(step is IStep);
            Assert.That(step.Data.Name == stepName);
            Assert.That(step.StepMetadata.Position == stepPosition);
        }

        [Test]
        public void CreateATransition()
        {
            ITransition transition = EntityFactory.CreateTransition();

            Assert.NotNull(transition);
            Assert.That(transition is ITransition);
        }

        [Test]
        public void CreateAChapter()
        {
            string chapterName = "Test Chapter";
            IChapter chapter = EntityFactory.CreateChapter(chapterName);

            Assert.NotNull(chapter);
            Assert.That(chapter is IChapter);
            Assert.That(chapter.Data.Name == chapterName);
        }

        [Test]
        public void CreateACourse()
        {
            string courseName = "Test Course";
            ICourse course = EntityFactory.CreateCourse(courseName);

            Assert.NotNull(course);
            Assert.That(course is ICourse);
            Assert.That(course.Data.Name == courseName);
        }
    }
}
