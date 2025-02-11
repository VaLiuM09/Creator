﻿﻿using System;

namespace VPG.Core.Behaviors
{
    [Flags]
    public enum BehaviorExecutionStages
    {
        Activation = 1 << 0,
        Deactivation = 1 << 1,
        ActivationAndDeactivation = ~0,
        None = 0,
    }
}
