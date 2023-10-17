using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.NodesData;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.SO;
using UnityEngine;

namespace Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Dialogue
{
    public class DialogueGetData : MonoBehaviour
    {
        [SerializeField] protected DialogueContainerSO DialogueContainer;

        protected BaseNodeData GetNodeByGuid(string targetNodeGuid)
        {
            return DialogueContainer.AllNodes.Find(node => node.NodeGuid == targetNodeGuid);
        }

        protected BaseNodeData GetNodeByNodePort(DialogueNodePort nodePort)
        {
            return DialogueContainer.AllNodes.Find(node => node.NodeGuid == nodePort.TargetGuid);
        }

        protected BaseNodeData GetNextNode(BaseNodeData baseNodeData)
        {
            NodeLinkData nodeLinkData = DialogueContainer.NodeLinkData.Find(edge => edge.InputGuid == baseNodeData.NodeGuid);
            return GetNodeByGuid(nodeLinkData.TargetGuid);
        }
    }
}