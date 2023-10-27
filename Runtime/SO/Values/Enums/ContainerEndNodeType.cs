using System;
using Fenneig_Dialogue_Editor.Runtime.Enums;
using UnityEngine.UIElements;

namespace Fenneig_Dialogue_Editor.Runtime.SO.Values.Enums
{
    [Serializable]
    public class ContainerEndNodeType
    {
        public EnumField EnumField;
        public EndNodeType Value = EndNodeType.End;
    }
}