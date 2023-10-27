using System;
using Fenneig_Dialogue_Editor.Runtime.Enums;
using UnityEngine.UIElements;

namespace Fenneig_Dialogue_Editor.Runtime.SO.Values.Enums
{
    [Serializable]
    public class ContainerStringEventConditionType
    {
        public EnumField EnumField;
        public StringEventConditionType Value = StringEventConditionType.True;
    }
}