using System;
using Fenneig_Dialogue_Editor.Runtime.SO.Values.Enums;

namespace Fenneig_Dialogue_Editor.Runtime.SO.Dialogue
{
    [Serializable]
    public class EndData : BaseData
    {
        public ContainerEndNodeType EndNodeType = new();
    }
}