using System.Collections.Generic;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Editor.Graph_view;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.NodesData;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fenneig_Dialogue_Editor.Dialogue_Editor.Editor.Nodes
{
    public class BranchNode : BaseNode
    {
        private List<BranchStringIdData> _branchStringIdData = new();

        public List<BranchStringIdData> BranchStringIdData => _branchStringIdData;

        public BranchNode() { }

        public BranchNode(Vector2 position, DialogueEditorWindow editorWindow, DialogueGraphView dialogueGraphView) :
            base(position, editorWindow, dialogueGraphView)
        {
            title = "Branch";
            
            AddInputPort("Input");
            AddOutputPort("True");
            AddOutputPort("False");

            TopButton();
        }

        private void TopButton()
        {
            ToolbarMenu menu = new() {text = "Add condition"};
            
            menu.menu.AppendAction("String condition", _ => AddCondition());
            
            titleButtonContainer.Add(menu);
        }

        public void AddCondition(BranchStringIdData pyramidBranchStringIdData = null)
        {
            BranchStringIdData tempBranchStringIdData = new();

            if (pyramidBranchStringIdData != null)
            {
                tempBranchStringIdData.IdNumber = pyramidBranchStringIdData.IdNumber;
                tempBranchStringIdData.StringEvent = pyramidBranchStringIdData.StringEvent;
            }
            _branchStringIdData.Add(tempBranchStringIdData);
            
            // Container of all objects
            Box boxContainer = new();
            boxContainer.AddToClassList("BranchBox");
            
            // Text
            TextField textField = new();
            textField.AddToClassList("BranchText");
            boxContainer.Add(textField);
            textField.RegisterValueChangedCallback(value =>
            {
                tempBranchStringIdData.StringEvent = value.newValue;
            });
            textField.SetValueWithoutNotify(tempBranchStringIdData.StringEvent);
            
            // ID number
            IntegerField integerField = new();
            integerField.AddToClassList("BranchID");
            boxContainer.Add(integerField);
            integerField.RegisterValueChangedCallback(value =>
            {
                tempBranchStringIdData.IdNumber = value.newValue;
            });
            integerField.SetValueWithoutNotify(tempBranchStringIdData.IdNumber);
            
            // Remove button
            Button closeButton = new()
            {
                text = "X"
            };
            closeButton.clicked += () =>
            {
                DeleteBox(boxContainer);
                _branchStringIdData.Remove(tempBranchStringIdData);
            };
            closeButton.AddToClassList("BranchButton");
            boxContainer.Add(closeButton);
            
            mainContainer.Add(boxContainer);
            RefreshExpandedState();
        }
        
        private void DeleteBox(Box boxContainer)
        {
            mainContainer.Remove(boxContainer);
            RefreshExpandedState();
        }
    }
}