using System;
using System.Collections.Generic;
using System.Linq;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Editor.Graph_view;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.Enums;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.NodesData;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fenneig_Dialogue_Editor.Dialogue_Editor.Editor.Nodes
{
    public class DialogueNode : BaseNode
    {
        private List<LanguageGeneric<string>> _texts = new();
        private List<LanguageGeneric<AudioClip>> _audioClips = new();
        private Sprite _faceImage;
        private List<LanguageGeneric<string>> _characterName = new();
        private DialogueSideType _faceImageSideType;

        private List<DialogueNodePort> _dialogueNodePorts = new();
        
        private TextField _textsField;
        private ObjectField _audioClipsField;
        private ObjectField _faceImageField;
        private Image _faceImagePreview;
        private TextField _nameField;
        private EnumField _sideTypeField;

        public List<LanguageGeneric<string>> Texts
        {
            get => _texts;
            set => _texts = value;
        }

        public List<LanguageGeneric<AudioClip>> AudioClips
        {
            get => _audioClips;
            set => _audioClips = value;
        }

        public List<LanguageGeneric<string>> CharacterName
        {
            get => _characterName;
            set => _characterName = value;
        }

        public DialogueSideType FaceImageSideType
        {
            get => _faceImageSideType;
            set => _faceImageSideType = value;
        }

        public List<DialogueNodePort> DialogueNodePorts
        {
            get => _dialogueNodePorts;
            set => _dialogueNodePorts = value;
        }

        public Sprite FaceImage
        {
            get => _faceImage;
            set => _faceImage = value;
        }

        public DialogueNode(){}

        public DialogueNode(Vector2 position, DialogueEditorWindow editorWindow, DialogueGraphView graphView) : base(position, editorWindow, graphView)
        {
            title = "Dialogue";
            
            AddInputPort("Input");

            foreach (LanguageType language in Enum.GetValues(typeof(LanguageType)))
            {
                _texts.Add(new LanguageGeneric<string>
                {
                    LanguageType = language,
                    LanguageGenericType = ""
                });
                
                _audioClips.Add(new LanguageGeneric<AudioClip>
                {
                    LanguageType = language,
                    LanguageGenericType = null
                });
                
                _characterName.Add(new LanguageGeneric<string>
                {
                    LanguageType = language,
                    LanguageGenericType = ""
                });
            }
            
            CreateFaceImageField();

            CreateFaceSideField();

            CreateAudioClipField();

            CreateNameField();

            CreateTextBoxField();

            CreateAddChoiceButton();
        }

        private void CreateAddChoiceButton()
        {
            Button button = new Button
            {
                text = "Add Choice"
            };
            button.clicked += () => { AddChoicePort(this); };

            titleButtonContainer.Add(button);
        }

        private void CreateTextBoxField()
        {
            Label labelTexts = new Label("Text box");
            labelTexts.AddToClassList("labelTexts");
            labelTexts.AddToClassList("Label");
            mainContainer.Add(labelTexts);

            _textsField = new TextField("");
            _textsField.AddToClassList("custom-text-box");
            _textsField.RegisterValueChangedCallback(value =>
                _texts.Find(text => text.LanguageType == EditorWindow.CurrentLanguage).LanguageGenericType = value.newValue);
            _textsField.SetValueWithoutNotify(_texts.Find(text => text.LanguageType == EditorWindow.CurrentLanguage)
                .LanguageGenericType);
            _textsField.multiline = true;

            _textsField.AddToClassList("TextBox");
            mainContainer.Add(_textsField);
        }

        private void CreateNameField()
        {
            Label labelName = new Label("Character name");
            labelName.AddToClassList("labelName");
            labelName.AddToClassList("Label");
            mainContainer.Add(labelName);

            _nameField = new TextField("");
            _nameField.RegisterValueChangedCallback(value =>
                _characterName.Find(characterName => characterName.LanguageType == EditorWindow.CurrentLanguage)
                    .LanguageGenericType = value.newValue);
            _nameField.SetValueWithoutNotify(_characterName.Find(text => text.LanguageType == EditorWindow.CurrentLanguage)
                .LanguageGenericType);
            _nameField.AddToClassList("TextName");
            mainContainer.Add(_nameField);
        }

        private void CreateAudioClipField()
        {
            _audioClipsField = new ObjectField
            {
                objectType = typeof(AudioClip),
                allowSceneObjects = false,
                value = _audioClips.Find(audioClip => audioClip.LanguageType == EditorWindow.CurrentLanguage)
                    .LanguageGenericType
            };
            _audioClipsField.RegisterValueChangedCallback(value =>
            {
                _audioClips.Find(audioClip => audioClip.LanguageType == EditorWindow.CurrentLanguage).LanguageGenericType =
                    value.newValue as AudioClip;
            });
            _audioClipsField.SetValueWithoutNotify(_audioClips
                .Find(audioClip => audioClip.LanguageType == EditorWindow.CurrentLanguage).LanguageGenericType);
            mainContainer.Add(_audioClipsField);
        }

        private void CreateFaceSideField()
        {
            _sideTypeField = new EnumField
            {
                value = _faceImageSideType
            };
            _sideTypeField.Init(_faceImageSideType);
            _sideTypeField.RegisterValueChangedCallback(value => { _faceImageSideType = (DialogueSideType) value.newValue; });
            mainContainer.Add(_sideTypeField);
        }

        private void CreateFaceImageField()
        {
            _faceImageField = new ObjectField
            {
                objectType = typeof(Sprite),
                allowSceneObjects = false,
                value = _faceImage
            };

            _faceImagePreview = new Image();
            _faceImagePreview.AddToClassList("FaceImagePreview");

            _faceImageField.RegisterValueChangedCallback(value =>
            {
                Sprite newSprite = value.newValue as Sprite;
                _faceImage = newSprite;
                _faceImagePreview.image = newSprite == null ? null : newSprite.texture;
            });
            
            mainContainer.Add(_faceImagePreview);
            mainContainer.Add(_faceImageField);
        }

        public override void ReloadLanguage()
        {
            _textsField.RegisterValueChangedCallback(value =>
                _texts.Find(text => 
                    text.LanguageType == EditorWindow.CurrentLanguage).LanguageGenericType = value.newValue);
            
            _textsField.SetValueWithoutNotify(_texts.Find(text => 
                text.LanguageType == EditorWindow.CurrentLanguage).LanguageGenericType);
            
            
            _audioClipsField.RegisterValueChangedCallback(value =>
                _audioClips.Find(audioClip => 
                    audioClip.LanguageType == EditorWindow.CurrentLanguage).LanguageGenericType = value.newValue as  AudioClip);
            
            _audioClipsField.SetValueWithoutNotify(_audioClips.Find(audioClip => 
                audioClip.LanguageType == EditorWindow.CurrentLanguage).LanguageGenericType);
            
            
            _nameField.RegisterValueChangedCallback(value =>
                _characterName.Find(text => 
                    text.LanguageType == EditorWindow.CurrentLanguage).LanguageGenericType = value.newValue);
            
            _nameField.SetValueWithoutNotify(_characterName.Find(text => 
                text.LanguageType == EditorWindow.CurrentLanguage).LanguageGenericType);

            foreach (DialogueNodePort nodePort in _dialogueNodePorts)
            {
                nodePort.TextField.RegisterValueChangedCallback(value =>
                    nodePort.TextLanguages.Find(language => 
                        language.LanguageType == EditorWindow.CurrentLanguage).LanguageGenericType = value.newValue);
                
                nodePort.TextField.SetValueWithoutNotify(nodePort.TextLanguages.Find(language => 
                    language.LanguageType == EditorWindow.CurrentLanguage).LanguageGenericType);
            }
        }

        public override void LoadValueInToField()
        {
            _textsField.SetValueWithoutNotify(_texts.Find(language => language.LanguageType == EditorWindow.CurrentLanguage).LanguageGenericType);
            _audioClipsField.SetValueWithoutNotify(_audioClips.Find(language => language.LanguageType == EditorWindow.CurrentLanguage).LanguageGenericType);
            _faceImageField.SetValueWithoutNotify(_faceImage);
            _sideTypeField.SetValueWithoutNotify(_faceImageSideType);
            _nameField.SetValueWithoutNotify(_characterName.Find(language => language.LanguageType == EditorWindow.CurrentLanguage).LanguageGenericType);
            if (_faceImage != null) _faceImagePreview.image = ((Sprite) _faceImageField.value).texture;
        }

        public Port AddChoicePort(BaseNode baseNode, DialogueNodePort existDialogueNodePort = null)
        {
            Port port = GetPortInstance(Direction.Output);

            string outputPortName = "Continue";

            DialogueNodePort dialogueNodePort = new DialogueNodePort();
            dialogueNodePort.PortGuid = Guid.NewGuid().ToString();

            foreach (LanguageType language in Enum.GetValues(typeof(LanguageType)))
            {
                dialogueNodePort.TextLanguages.Add(new LanguageGeneric<string>
                {
                    LanguageType = language,
                    LanguageGenericType = outputPortName
                });
            }

            if (existDialogueNodePort != null)
            {
                dialogueNodePort.TargetGuid = existDialogueNodePort.TargetGuid;
                dialogueNodePort.PortGuid = existDialogueNodePort.PortGuid;

                foreach (LanguageGeneric<string> languageGeneric in existDialogueNodePort.TextLanguages)
                    dialogueNodePort.TextLanguages.Find(language => language.LanguageType == languageGeneric.LanguageType).LanguageGenericType = languageGeneric.LanguageGenericType;
            }

            //Text for the port
            dialogueNodePort.TextField = new TextField();
            dialogueNodePort.TextField.AddToClassList("custom-text-field");
            dialogueNodePort.TextField.RegisterValueChangedCallback(value =>
                dialogueNodePort.TextLanguages.Find(language => language.LanguageType == EditorWindow.CurrentLanguage).LanguageGenericType = value.newValue);
            dialogueNodePort.TextField.SetValueWithoutNotify(dialogueNodePort.TextLanguages.Find(language => language.LanguageType == EditorWindow.CurrentLanguage).LanguageGenericType);
            port.contentContainer.Add(dialogueNodePort.TextField);
            
            //Delete button
            Button deleteButton = new(() => DeletePort(baseNode, port)) { text = "X" };
            port.contentContainer.Add(deleteButton);

            port.portName = dialogueNodePort.PortGuid;
            Label portNameLabel = port.contentContainer.Q<Label>("type");
            portNameLabel.AddToClassList("PortName");
            
            _dialogueNodePorts.Add(dialogueNodePort);

            baseNode.outputContainer.Add(port);
            
            //Refresh
            baseNode.RefreshPorts();
            baseNode.RefreshExpandedState();

            return port;
        }

        private void DeletePort(BaseNode node, Port port)
        {
            DialogueNodePort portToRemove = _dialogueNodePorts.Find(portToRemove => portToRemove.PortGuid == port.portName);
            _dialogueNodePorts.Remove(portToRemove);

            IEnumerable<Edge> portEdge = GraphView.edges.ToList().Where(edge => edge.output == port);
            if (portEdge.Any())
            {
                Edge edge = portEdge.First();
                edge.input.Disconnect(edge);
                edge.output.Disconnect(edge);
                GraphView.RemoveElement(edge);
            }

            node.outputContainer.Remove(port);
            
            //Refresh
            node.RefreshPorts();
            node.RefreshExpandedState();
        }
    }
}