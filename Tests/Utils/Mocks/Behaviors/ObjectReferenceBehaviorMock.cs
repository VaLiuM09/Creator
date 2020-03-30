﻿using Innoactive.Creator.Core;
using Innoactive.Creator.Core.Behaviors;
using Innoactive.Creator.Core.Properties;
using Innoactive.Creator.Core.SceneObjects;

namespace Innoactive.Creator.Tests.Utils.Mocks
{
    /// <summary>
    /// Helper behavior for testing that has a reference object.
    /// </summary>
    public class ObjectReferenceBehaviorMock : Behavior<ObjectReferenceBehaviorMock.EntityData>
    {
        public class EntityData : IBehaviorData
        {
            /// <summary>
            /// Target scene object to be referenced.
            /// </summary>
            public ScenePropertyReference<ISceneObjectProperty> ReferenceObject { get; set; }

            public Metadata Metadata { get; set; }
            public string Name { get; set; }
        }

        public ObjectReferenceBehaviorMock(string sceneObjectName)
        {
            Data = new EntityData
            {
                ReferenceObject = new ScenePropertyReference<ISceneObjectProperty>(sceneObjectName)
            };
        }

        private readonly IProcess<EntityData> process = new Process<EntityData>(new EmptyStageProcess<EntityData>(),
            new EmptyStageProcess<EntityData>(), new EmptyStageProcess<EntityData>());

        protected override IProcess<EntityData> Process
        {
            get { return process; }
        }
    }
}
