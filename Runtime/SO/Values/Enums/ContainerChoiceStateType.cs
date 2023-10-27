using System;
using Fenneig_Dialogue_Editor.Runtime.Enums;
using UnityEngine.UIElements;

namespace Fenneig_Dialogue_Editor.Runtime.SO.Values.Enums
{
    [Serializable]
    public class ContainerChoiceStateType
    {
        public EnumField EnumField;
        public ChoiceStateType Value = ChoiceStateType.Hide;
    }
}