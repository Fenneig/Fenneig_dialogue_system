using System;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Editor.Graph_view;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.Enums;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fenneig_Dialogue_Editor.Dialogue_Editor.Editor.Nodes
{
    public class EndNode : BaseNode
    {
        private EndNodeType _endNodeType = EndNodeType.End;
        private EnumField _enumField;

        public EndNodeType EndNodeType
        {
            get => _endNodeType;
            set => _endNodeType = value;
        }
        
        public EndNode(){}

        public EndNode(Vector2 position, DialogueEditorWindow editorWindow, DialogueGraphView graphView) : base(position, editorWindow, graphView)
        {
            title = "End";
            
            AddInputPort("Input");
            
            _enumField = new EnumField
            {
                value = _endNodeType
            };

            _enumField.Init(_endNodeType);
            _enumField.RegisterCallback<ChangeEvent<Enum>>(evt =>
            {
                _endNodeType = (EndNodeType) evt.newValue;
            });
            _enumField.SetValueWithoutNotify(_endNodeType);
            
            mainContainer.Add(_enumField);
        }

        public override void LoadValueInToField()
        {
            _enumField.SetValueWithoutNotify(_endNodeType);
        }
    }
}