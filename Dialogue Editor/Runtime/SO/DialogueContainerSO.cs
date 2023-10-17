using System;
using System.Collections.Generic;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.NodesData;
using UnityEngine;

namespace Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.SO
{
    [CreateAssetMenu(menuName = "Dialogue/Dialogue container")]
    [Serializable]
    public class DialogueContainerSO : ScriptableObject
    {
        public List<NodeLinkData> NodeLinkData = new();
        public List<StartNodeData> StartNodeData = new();
        public List<DialogueNodeData> DialogueNodeData = new();
        public List<EventNodeData> EventNodeData = new();
        public List<EndNodeData> EndNodeData = new();
        public List<BranchNodeData> BranchNodeData = new();

        public List<BaseNodeData> AllNodes
        {
            get
            {
                List<BaseNodeData> allNodes = new List<BaseNodeData>();
                allNodes.AddRange(StartNodeData);
                allNodes.AddRange(DialogueNodeData);
                allNodes.AddRange(EventNodeData);
                allNodes.AddRange(EndNodeData);
                allNodes.AddRange(BranchNodeData);
                return allNodes;
            }
        }

        public void ClearNodes()
        {
            StartNodeData.Clear();
            DialogueNodeData.Clear();
            EventNodeData.Clear();
            EndNodeData.Clear();
            BranchNodeData.Clear();
        }
    }
}