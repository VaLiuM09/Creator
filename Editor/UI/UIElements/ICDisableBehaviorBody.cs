using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Innoactive.CreatorEditor.UI.Windows;

namespace Innoactive.CreatorEditor.UI.UIElements
{
    public class ICDisableBehaviorBody : VisualElement
    {


        public new class UxmlFactory : UxmlFactory<ICDisableBehaviorBody, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
       
            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as ICDisableBehaviorBody;
                ate.Clear();

                //load body into the visual tree of behavior
                var BodyTree = (VisualTreeAsset)Resources.Load("UI/ICBehaviorDisableBody");
                VisualElement bt = BodyTree.CloneTree();
                ate.Add(bt);    
            }



        }

    }
}
