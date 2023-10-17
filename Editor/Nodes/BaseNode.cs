using System;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Editor.Graph_view;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fenneig_Dialogue_Editor.Dialogue_Editor.Editor.Nodes
{
    public class BaseNode : Node
    {
        private string _nodeGuid;
        protected DialogueGraphView GraphView;
        protected DialogueEditorWindow EditorWindow;
        protected static readonly Vector2 DefaultNodeSize = new(200, 250);

        public string NodeGuid
        {
            get => _nodeGuid;
            set => _nodeGuid = value;
        }

        public BaseNode()
        {
        }

        public BaseNode(Vector2 position, DialogueEditorWindow editorWindow, DialogueGraphView graphView)
        {
            EditorWindow = editorWindow;
            GraphView = graphView;

            SetPosition(new Rect(position, DefaultNodeSize));
            NodeGuid = Guid.NewGuid().ToString();
            
            StyleSheet styleSheet = Resources.Load<StyleSheet>("NodeStyleSheet");
            styleSheets.Add(styleSheet);
        }

        public void AddOutputPort(string name, Port.Capacity capacity = Port.Capacity.Single)
        {
            Port outputPort = GetPortInstance(Direction.Output, capacity);
            outputPort.portName = name;
            outputContainer.Add(outputPort);
        }

        public void AddInputPort(string name, Port.Capacity capacity = Port.Capacity.Multi)
        {
            Port inputPort = GetPortInstance(Direction.Input, capacity);
            inputPort.portName = name;
            inputContainer.Add(inputPort);
        }

        public Port GetPortInstance(Direction nodeDirection, Port.Capacity capacity = Port.Capacity.Single) =>
            InstantiatePort(Orientation.Horizontal, nodeDirection, capacity, typeof(float));

        public virtual void LoadValueInToField(){}
        
        public virtual void ReloadLanguage(){}
    }
}