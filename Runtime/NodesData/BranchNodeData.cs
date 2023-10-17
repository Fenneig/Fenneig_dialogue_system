using System;
using System.Collections.Generic;

namespace Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.NodesData
{
    [Serializable]
    public class BranchNodeData : BaseNodeData
    {
        public string TrueGuidNode;
        public string FalseGuidNode;
        public List<BranchStringIdData> BranchStringIdDatas;
    }
}