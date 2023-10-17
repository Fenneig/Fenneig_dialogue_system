using Fenneig_Dialogue_Editor.Dialogue_Editor.Editor.Graph_view;
using UnityEngine;

namespace Fenneig_Dialogue_Editor.Dialogue_Editor.Editor.Nodes
{
    public class StartNode : BaseNode
    {
        public StartNode() { }

        public StartNode(Vector2 position, DialogueEditorWindow editorWindow, DialogueGraphView graphView) : base(position, editorWindow, graphView)
        {
            title = "Start";
            
            AddOutputPort("Output");
            
            RefreshExpandedState();
            RefreshPorts();
        }
    }
}