using System;
using Fenneig_Dialogue_Editor.Runtime.Enums;
#if UNITY_2022_1_OR_NEWER
using UnityEngine.UIElements;
#else
using UnityEditor.UIElements;
#endif

namespace Fenneig_Dialogue_Editor.Runtime.SO.Values.Enums
{
    [Serializable]
    public class ContainerChoiceStateType
    {
        public EnumField EnumField;
        public ChoiceStateType Value = ChoiceStateType.Hide;
    }
}