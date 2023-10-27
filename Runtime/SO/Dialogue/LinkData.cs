using System;

namespace Fenneig_Dialogue_Editor.Runtime.SO.Dialogue
{
    [Serializable]
    public class LinkData
    {
        public string BaseNodeGuid;
        public string BasePortGuid;
        public string TargetNodeGuid;
        public string TargetPortGuid;
    }
}