using Fenneig_Dialogue_Editor.Editor.Graph_view;
using Fenneig_Dialogue_Editor.Runtime.SO.Dialogue;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.UIElements;
#endif

namespace Fenneig_Dialogue_Editor.Editor.Nodes
{
    public class EndNode : BaseNode
    {
        private const string END_NODE_STYLE_SHEET = "USS/Nodes/EndNodeStyleSheet";
        public EndData EndData { get; set; } = new();

        public EndNode()
        {
        }

        public EndNode(Vector2 position, DialogueEditorWindow editorWindow, DialogueGraphView graphView) : base(
            position, editorWindow, graphView, END_NODE_STYLE_SHEET)
        {
            title = "End";

            AddInputPort("Input");
            MakeMainContainer();
        }

        private void MakeMainContainer()
        {
#if UNITY_EDITOR
            EnumField enumField = GetNewEnumFieldEndNodeType(EndData.EndNodeType);
#endif
            
            mainContainer.Add(enumField);
            RefreshExpandedState();
        }

        public override void LoadValueInToField()
        {
            if (EndData.EndNodeType.EnumField != null)
                EndData.EndNodeType.EnumField.SetValueWithoutNotify(EndData.EndNodeType.Value);
        }
    }
}