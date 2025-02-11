using System.Collections;
using VPG.Core.Configuration.Modes;

namespace VPG.Core.EntityOwners
{
    /// <summary>
    /// A process that activates and deactivates entities one after another.
    /// </summary>
    public abstract class EntityIteratingProcess<TEntity> : Process<IEntitySequenceDataWithMode<TEntity>> where TEntity : IEntity
    {
        protected EntityIteratingProcess(IEntitySequenceDataWithMode<TEntity> data) : base(data)
        {
        }

        /// <inheritdoc />
        public override void Start()
        {
        }

        /// <inheritdoc />
        public override IEnumerator Update()
        {
            TEntity current = default;
            Data.Current = current;

            while (TryNext(out current))
            {
                Data.Current = current;

                if (Data.Current == null)
                {
                    continue;
                }

                while (ShouldActivateCurrent() == false)
                {
                    yield return null;
                }

                Data.Current.LifeCycle.Activate();

                if ((Data.Current is IOptional && Data.Mode.CheckIfSkipped(Data.Current.GetType())))
                {
                    Data.Current.LifeCycle.MarkToFastForward();
                }

                while (current.LifeCycle.Stage == Stage.Activating)
                {
                    yield return null;
                }

                while (ShouldDeactivateCurrent() == false)
                {
                    yield return null;
                }

                if (Data.Current.LifeCycle.Stage != Stage.Inactive)
                {
                    Data.Current.LifeCycle.Deactivate();
                }

                while (Data.Current.LifeCycle.Stage != Stage.Inactive)
                {
                    yield return null;
                }
            }
        }

        /// <inheritdoc />
        public override void End()
        {
            Data.Current = default;
        }

        /// <inheritdoc />
        public override void FastForward()
        {
            TEntity current = Data.Current;

            while (current != null || TryNext(out current))
            {
                Data.Current = current;

                if (current.LifeCycle.Stage == Stage.Inactive)
                {
                    current.LifeCycle.Activate();
                }

                current.LifeCycle.MarkToFastForward();

                if (current.LifeCycle.Stage == Stage.Activating || current.LifeCycle.Stage == Stage.Active)
                {
                    current.LifeCycle.Deactivate();
                }

                current = default(TEntity);
            }
        }

        /// <summary>
        /// Returns true if the current entity has to be activated.
        /// </summary>
        protected abstract bool ShouldActivateCurrent();

        /// <summary>
        /// Returns true if the current entity has to be deactivated.
        /// </summary>
        protected abstract bool ShouldDeactivateCurrent();

        /// <summary>
        /// Try to get next child entity.
        /// </summary>
        protected abstract bool TryNext(out TEntity entity);
    }
}
