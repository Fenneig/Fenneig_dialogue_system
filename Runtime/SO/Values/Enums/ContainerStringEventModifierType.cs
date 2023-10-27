using System;
using Fenneig_Dialogue_Editor.Runtime.Enums;
using UnityEngine.UIElements;

namespace Fenneig_Dialogue_Editor.Runtime.SO.Values.Enums
{
    [Serializable]
    public class ContainerStringEventModifierType
    {
        public EnumField EnumField;
        public StringEventModifierType Value = StringEventModifierType.SetTrue;
    }
}