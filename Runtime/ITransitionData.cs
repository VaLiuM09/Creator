using System.Collections.Generic;
using VPG.Core.Conditions;
using VPG.Core.EntityOwners;

namespace VPG.Core
{
    // An interface for a transition's data.
    public interface ITransitionData : IEntityCollectionDataWithMode<ICondition>, ICompletableData
    {
        /// <summary>
        /// A list of conditions. When you complete all of them, this transition will trigger.
        /// </summary>
        IList<ICondition> Conditions { get; set; }

        /// <summary>
        /// The next step to take after this transition triggers.
        /// </summary>
        IStep TargetStep { get; set; }
    }
}
