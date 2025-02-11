﻿﻿using System;
 using System.Runtime.Serialization;

namespace VPG.Core.SceneObjects
{
    /// <summary>
    /// Simple container for UniqueName.
    /// </summary>
    [DataContract(IsReference = true)]
    public abstract class UniqueNameReference
    {
        /// <summary>
        /// Serializable unique name of referenced object.
        /// </summary>
        [DataMember]
        public virtual string UniqueName { get; set; }

        protected UniqueNameReference() { }

        protected UniqueNameReference(string uniqueName)
        {
            UniqueName = uniqueName;
        }

        internal abstract Type GetReferenceType();
    }
}
