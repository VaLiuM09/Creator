using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using VPG.Core.Attributes;
using VPG.Core.Configuration.Modes;
using VPG.Core.EntityOwners;
using VPG.Core.EntityOwners.ParallelEntityCollection;

namespace VPG.Core
{
    /// <summary>
    /// A collection of <see cref="ITransition"/>s.
    /// </summary>
    [DataContract(IsReference = true)]
    public class TransitionCollection : Entity<TransitionCollection.EntityData>, ITransitionCollection
    {
        /// <summary>
        /// The data class of the <see cref="ITransition"/>s' collection.
        /// </summary>
        [DataContract(IsReference = true)]
        public class EntityData : EntityCollectionData<ITransition>, ITransitionCollectionData
        {
            ///<inheritdoc />
            [DataMember]
            [DisplayName(""), KeepPopulated(typeof(Transition)), ReorderableListOf(typeof(FoldableAttribute), typeof(DeletableAttribute)), ExtendableList]
            public virtual IList<ITransition> Transitions { get; set; }

            ///<inheritdoc />
            public override IEnumerable<ITransition> GetChildren()
            {
                return Transitions.ToArray();
            }

            ///<inheritdoc />
            public IMode Mode { get; set; }
        }

        private class ActiveProcess : Process<EntityData>
        {
            public ActiveProcess(EntityData data) : base(data)
            {
            }

            ///<inheritdoc />
            public override void Start()
            {
            }

            ///<inheritdoc />
            public override IEnumerator Update()
            {
                while (Data.Transitions.All(transition => transition.IsCompleted == false))
                {
                    yield return null;
                }
            }

            ///<inheritdoc />
            public override void End()
            {
            }

            ///<inheritdoc />
            public override void FastForward()
            {
            }
        }

        ///<inheritdoc />
        protected override IConfigurator GetConfigurator()
        {
            return new ParallelConfigurator<ITransition>(Data);
        }

        ///<inheritdoc />
        ITransitionCollectionData IDataOwner<ITransitionCollectionData>.Data
        {
            get { return Data; }
        }

        public TransitionCollection()
        {
            Data.Transitions = new List<ITransition>();
        }

        ///<inheritdoc />
        public override IProcess GetActivatingProcess()
        {
            return new ParallelActivatingProcess<EntityData>(Data);
        }

        ///<inheritdoc />
        public override IProcess GetActiveProcess()
        {
            return new CompositeProcess(new ParallelActiveProcess<EntityData>(Data), new ActiveProcess(Data));
        }

        ///<inheritdoc />
        public override IProcess GetDeactivatingProcess()
        {
            return new ParallelDeactivatingProcess<EntityData>(Data);
        }
    }
}
