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
    public class ContainerStringEventModifierType
    {
        public EnumField EnumField;
        public StringEventModifierType Value = StringEventModifierType.SetTrue;
    }
}