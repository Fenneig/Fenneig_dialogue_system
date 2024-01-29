using System;
using Fenneig_Dialogue_Editor.Runtime.Enums;
#if UNITY_EDITOR
using UnityEditor.UIElements;
#endif

namespace Fenneig_Dialogue_Editor.Runtime.SO.Values.Enums
{
    [Serializable]
    public class ContainerChoiceStateType
    {
#if UNITY_EDITOR
        public EnumField EnumField;
#endif
        public ChoiceStateType Value = ChoiceStateType.Hide;
    }
}