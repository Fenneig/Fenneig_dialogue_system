using System.Collections.Generic;
using System.Linq;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Editor.Nodes;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.NodesData;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.SO;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using Edge = UnityEditor.Experimental.GraphView.Edge;

namespace Fenneig_Dialogue_Editor.Dialogue_Editor.Editor.Graph_view
{
    public class DialogueSaveAndLoad
    {
        private DialogueGraphView _graphView;
        private List<BaseNode> Nodes => _graphView.nodes.ToList().Where(node => node is BaseNode).Cast<BaseNode>().ToList();
        private List<Edge> Edges => _graphView.edges.ToList();

        public DialogueSaveAndLoad(DialogueGraphView graphView)
        {
            _graphView = graphView;
        }

        public void Save(DialogueContainerSO dialogueContainer)
        {
            SaveEdges(dialogueContainer);
            SaveNodes(dialogueContainer);
            
            EditorUtility.SetDirty(dialogueContainer);
            AssetDatabase.SaveAssets();
        }

        public void Load(DialogueContainerSO dialogueContainer)
        {
            ClearGraph();
            GenerateNodes(dialogueContainer);
            ConnectNodes(dialogueContainer);
        }

        #region Save

        private void SaveEdges(DialogueContainerSO dialogueContainer)
        {
            dialogueContainer.NodeLinkData.Clear();

            Edge[] connectedEdges = Edges.Where(edge => edge.input.node != null).ToArray();
            foreach (var connectedEdge in connectedEdges)
            {
                BaseNode outputNode = (BaseNode)connectedEdge.output.node;
                BaseNode inputNode = (BaseNode) connectedEdge.input.node;
                
                dialogueContainer.NodeLinkData.Add(new NodeLinkData
                {
                    InputGuid =  outputNode.NodeGuid,
                    TargetGuid = inputNode.NodeGuid,
                    InputPortName = connectedEdge.output.portName,
                    TargetPortName = connectedEdge.input.portName
                });
            }
        }

        private void SaveNodes(DialogueContainerSO dialogueContainer)
        {
            dialogueContainer.ClearNodes();

            Nodes.ForEach(node =>
            {
                switch (node)
                {
                    case StartNode startNode:
                        dialogueContainer.StartNodeData.Add(SaveNodeData(startNode));
                        SaveNodeData((StartNode)node);
                        break;
                    case DialogueNode dialogueNode:
                        dialogueContainer.DialogueNodeData.Add(SaveNodeData(dialogueNode));
                        SaveNodeData((DialogueNode)node);
                        break;
                    case BranchNode branchNode:
                        dialogueContainer.BranchNodeData.Add(SaveNodeData(branchNode));
                        SaveNodeData((BranchNode)node);
                        break;
                    case EventNode eventNode:
                        dialogueContainer.EventNodeData.Add(SaveNodeData(eventNode));
                        SaveNodeData((EventNode)node);
                        break;
                    case EndNode endNode:
                        dialogueContainer.EndNodeData.Add(SaveNodeData(endNode));
                        SaveNodeData((EndNode)node);
                        break;
                    default: 
                        break;
                }
            });
        }

        private DialogueNodeData SaveNodeData(DialogueNode node)
        {
            DialogueNodeData dialogueNodeData = new DialogueNodeData
            {
                NodeGuid = node.NodeGuid,
                Position =  node.GetPosition().position,
                DialogueNodePorts = new List<DialogueNodePort>(node.DialogueNodePorts),
                Texts = node.Texts,
                AudioClips = node.AudioClips,
                FaceImage = node.FaceImage,
                CharacterName = node.CharacterName,
                FaceImageSideType =  node.FaceImageSideType,
            };

            foreach (var nodePort in dialogueNodeData.DialogueNodePorts)
            {
                nodePort.TargetGuid = string.Empty;
                
                foreach (var edge in Edges.Where(edge => edge.output.portName == nodePort.PortGuid))
                {
                    nodePort.TargetGuid = (edge.input.node as BaseNode)?.NodeGuid;
                }
            }

            return dialogueNodeData;
        }

        private StartNodeData SaveNodeData(StartNode node)
        {
            StartNodeData startNodeData = new StartNodeData
            {
                NodeGuid = node.NodeGuid,
                Position = node.GetPosition().position
            };

            return startNodeData;
        }

        private EndNodeData SaveNodeData(EndNode node)
        {
            EndNodeData endNodeData = new EndNodeData
            {
                NodeGuid = node.NodeGuid,
                Position = node.GetPosition().position,
                EndNodeType = node.EndNodeType
                
            };

            return endNodeData;
        }

        private EventNodeData SaveNodeData(EventNode node)
        {
            EventNodeData eventNodeData = new EventNodeData
            {
                NodeGuid = node.NodeGuid,
                Position = node.GetPosition().position,
                EventScriptableObjectDatas = node.EventScriptableObjectDatas,
                EventStringIdDatas = node.EventStringIdDatas 
            };

            return eventNodeData;
        }

        private BranchNodeData SaveNodeData(BranchNode node)
        {
            Edge trueOutput = Edges.FirstOrDefault(edge => edge.output.node == node && edge.output.portName == "True");
            Edge falseOutput = Edges.FirstOrDefault(edge => edge.output.node == node && edge.output.portName == "False");

            BranchNodeData branchNodeData = new BranchNodeData
            {
                NodeGuid = node.NodeGuid,
                Position = node.GetPosition().position,
                BranchStringIdDatas = node.BranchStringIdData,
                TrueGuidNode = trueOutput != null ? (trueOutput.input.node as BaseNode).NodeGuid : string.Empty,
                FalseGuidNode = falseOutput != null ? (falseOutput.input.node as BaseNode).NodeGuid : string.Empty
            };

            return branchNodeData;
        }

        #endregion

        #region Load

        private void ClearGraph()
        {
            Edges.ForEach(edge => _graphView.RemoveElement(edge));
            
            Nodes.ForEach(node => _graphView.RemoveElement(node));
        }

        private void GenerateNodes(DialogueContainerSO dialogueContainer)
        {
            //Start nodes
            dialogueContainer.StartNodeData.ForEach(node =>
            {
                StartNode tempNode = _graphView.CreateStartNode(node.Position);
                tempNode.NodeGuid = node.NodeGuid;
                
                _graphView.AddElement(tempNode);
            });
            
            //End nodes
            dialogueContainer.EndNodeData.ForEach(node =>
            {
                EndNode tempNode = _graphView.CreateEndNode(node.Position);
                tempNode.NodeGuid = node.NodeGuid;
                tempNode.EndNodeType = node.EndNodeType;
                
                tempNode.LoadValueInToField();
                _graphView.AddElement(tempNode);
            });
            
            //Branch
            dialogueContainer.BranchNodeData.ForEach(node =>
            {
                BranchNode tempNode = _graphView.CreateBranchNode(node.Position);
                tempNode.NodeGuid = node.NodeGuid;
                node.BranchStringIdDatas.ForEach(data => tempNode.AddCondition(data));
                
                tempNode.LoadValueInToField();
                _graphView.AddElement(tempNode);
            });
            
            //Events
            dialogueContainer.EventNodeData.ForEach(node =>
            {
                EventNode tempNode = _graphView.CreateEventNode(node.Position);
                tempNode.NodeGuid = node.NodeGuid;
                node.EventScriptableObjectDatas.ForEach(data => tempNode.CreateScriptableEvent(data));
                node.EventStringIdDatas.ForEach(data => tempNode.CreateStringEvent(data));
                
                tempNode.LoadValueInToField();
                _graphView.AddElement(tempNode);
            });
            
            //Dialogue
            dialogueContainer.DialogueNodeData.ForEach(node =>
            {
                DialogueNode tempNode = _graphView.CreateDialogueNode(node.Position);
                tempNode.NodeGuid = node.NodeGuid;
                tempNode.FaceImage = node.FaceImage;
                tempNode.FaceImageSideType = node.FaceImageSideType;
                
                node.Texts.ForEach(languageGeneric =>
                    tempNode.Texts.Find(language => language.LanguageType == languageGeneric.LanguageType)
                        .LanguageGenericType = languageGeneric.LanguageGenericType);
                
                node.AudioClips.ForEach(languageGeneric =>
                    tempNode.AudioClips.Find(language => language.LanguageType == languageGeneric.LanguageType)
                        .LanguageGenericType = languageGeneric.LanguageGenericType);
                
                node.CharacterName.ForEach(languageGeneric =>
                    tempNode.CharacterName.Find(language => language.LanguageType == languageGeneric.LanguageType)
                        .LanguageGenericType = languageGeneric.LanguageGenericType);
                
                node.DialogueNodePorts.ForEach(choicePort => tempNode.AddChoicePort(tempNode, choicePort));

                tempNode.LoadValueInToField();
                _graphView.AddElement(tempNode);
            });
        }

        private void ConnectNodes(DialogueContainerSO dialogueContainer)
        {
            Nodes.ForEach(node =>
            {
                List<NodeLinkData> connections = dialogueContainer.NodeLinkData.Where(edge => edge.InputGuid == node.NodeGuid).ToList();

                List<Port> allOutputPorts = node.outputContainer.Children().Where(port => port is Port).Cast<Port>().ToList();
                
                for (int i = 0; i < connections.Count; i++)
                {
                    string targetNodeGuid = connections[i].TargetGuid;
                    Port targetPort = (Port) Nodes.First(tempNode => tempNode.NodeGuid == targetNodeGuid).inputContainer[0];
                    
                    if (targetPort == null) continue;

                    allOutputPorts.ForEach(outputPort =>
                    {
                        if (outputPort.portName == connections[i].InputPortName)
                        {
                            LinkNodes(outputPort, targetPort);
                        }
                    });
                }
            });
        }

        private void LinkNodes(Port outputPort, Port inputPort)
        {
            Edge tempEdge = new Edge
            {
                output = outputPort,
                input = inputPort
            };
            tempEdge.input.Connect(tempEdge);
            tempEdge.output.Connect(tempEdge);
            _graphView.Add(tempEdge);
        }

        #endregion
    }
}