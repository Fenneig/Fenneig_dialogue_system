using Fenneig_Dialogue_Editor.Runtime.SO;
using Fenneig_Dialogue_Editor.Runtime.SO.Dialogue;
using UnityEngine;

namespace Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Dialogue
{
    public class DialogueGetData : MonoBehaviour
    {
        [SerializeField] protected DialogueContainerSO DialogueContainer;

        protected BaseData GetNodeByGuid(string targetNodeGuid) =>
            DialogueContainer.AllData.Find(node => node.NodeGuid == targetNodeGuid); 

        protected BaseData GetNodeByNodePort(DialogueDataPort nodePort) =>
            DialogueContainer.AllData.Find(node => node.NodeGuid == nodePort.InputGuid);

        protected BaseData GetNextNode(BaseData baseNodeData)
        {
            LinkData nodeLinkData = DialogueContainer.NodeLinkData.Find(edge => edge.BaseNodeGuid == baseNodeData.NodeGuid);
            return GetNodeByGuid(nodeLinkData.TargetNodeGuid);
        }
    }
}