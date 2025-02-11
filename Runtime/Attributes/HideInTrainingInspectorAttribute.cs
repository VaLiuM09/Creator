using System;

namespace VPG.Core.Attributes
{
    /// <summary>
    /// Use this attribute to hide serializeable members in the training inspector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class HideInTrainingInspectorAttribute : Attribute { }
}
