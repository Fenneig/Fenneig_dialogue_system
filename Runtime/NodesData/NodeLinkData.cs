using System;

namespace Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.NodesData
{
    [Serializable]
    public class NodeLinkData
    {
        public string InputGuid;
        public string InputPortName;
        public string TargetGuid;
        public string TargetPortName;
    }
}