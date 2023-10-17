using System.Collections.Generic;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Editor.Graph_view;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.NodesData;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.SO;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fenneig_Dialogue_Editor.Dialogue_Editor.Editor.Nodes
{
    public class EventNode : BaseNode
    {
        private List<EventScriptableObjectData> _eventScriptableObjectDatas = new();
        private List<EventStringIdData> _eventStringIdDatas = new();

        public List<EventScriptableObjectData> EventScriptableObjectDatas => _eventScriptableObjectDatas;

        public List<EventStringIdData> EventStringIdDatas => _eventStringIdDatas;

        public EventNode() { }

        public EventNode(Vector2 position, DialogueEditorWindow editorWindow, DialogueGraphView graphView) : base(position, editorWindow, graphView)
        {
            title = "Event";
            
            AddInputPort("Input");
            AddOutputPort("Output");

            CreateAddEventButton();
        }

        private void CreateAddEventButton()
        {
            ToolbarMenu menu = new() { text = "Add event" };
            menu.menu.AppendAction("String ID",_ => CreateStringEvent());
            menu.menu.AppendAction("Scriptable object",_ => CreateScriptableEvent());

            titleContainer.Add(menu);
        }

        public void CreateStringEvent(EventStringIdData pyramidEventStringIdData = null)
        {
            EventStringIdData tempEventStringIdData = new();

            if (pyramidEventStringIdData != null)
            {
                tempEventStringIdData.StringEvent = pyramidEventStringIdData.StringEvent;
                tempEventStringIdData.IdNumber = pyramidEventStringIdData.IdNumber;
            }

            _eventStringIdDatas.Add(tempEventStringIdData);
            
            // Container of all objects
            Box boxContainer = new Box();
            boxContainer.AddToClassList("EventBox");
            
            // Text
            TextField textField = new();
            textField.AddToClassList("EventText");
            boxContainer.Add(textField); 

            textField.RegisterValueChangedCallback(value =>
            {
                tempEventStringIdData.StringEvent = value.newValue;
            });
            textField.SetValueWithoutNotify(tempEventStringIdData.StringEvent);
            
            // ID number
            IntegerField integerField = new();
            integerField.AddToClassList("EventInt");
            boxContainer.Add(integerField);
            
            integerField.RegisterValueChangedCallback(value =>
            {
                tempEventStringIdData.IdNumber = value.newValue;
            });
            integerField.SetValueWithoutNotify(tempEventStringIdData.IdNumber);
            
            // Remove button
            Button closeButton = new() { text = "X"};
            closeButton.clicked += () =>
            {
                DeleteBox(boxContainer);
                _eventStringIdDatas.Remove(tempEventStringIdData);
            };
            closeButton.AddToClassList("EventButton");
            boxContainer.Add(closeButton);

            mainContainer.Add(boxContainer);
            RefreshExpandedState();
        }

        public void CreateScriptableEvent(EventScriptableObjectData pyramidEventScriptableObjectData = null)
        {
            EventScriptableObjectData tempEventScriptableObjectData = new();

            if (pyramidEventScriptableObjectData != null)
            {
                tempEventScriptableObjectData.DialogueEventSO = pyramidEventScriptableObjectData.DialogueEventSO;
            }

            _eventScriptableObjectDatas.Add(tempEventScriptableObjectData);
            
            // Container of all objects
            Box boxContainer = new Box();
            boxContainer.AddToClassList("EventBox");

            ObjectField eventField = new()
            {
                objectType = typeof(DialogueEventSO),
                allowSceneObjects = false,
                value = tempEventScriptableObjectData.DialogueEventSO
            };
            eventField.AddToClassList("EventObject");
            eventField.RegisterValueChangedCallback(value =>
                tempEventScriptableObjectData.DialogueEventSO = value.newValue as DialogueEventSO);
            eventField.SetValueWithoutNotify(eventField.value);
            
            boxContainer.Add(eventField);

            // Remove button
            Button button = new() { text = "X"};
            button.clicked += () =>
            {
                DeleteBox(boxContainer);
                _eventScriptableObjectDatas.Remove(tempEventScriptableObjectData); 
            };
            button.AddToClassList("EventButton");
            boxContainer.Add(button);

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