using System;
using System.Collections.Generic;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.Enums;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.NodesData;
using UnityEngine;
using UnityEngine.Events;

namespace Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Dialogue
{
    [RequireComponent(typeof(AudioSource))]
    public class DialogueSpeak : DialogueGetData
    {
        [SerializeField] private DialogueWidget _dialogueWidget;
        
        private AudioSource _audioSource;
        private DialogueNodeData _currentDialogueNodeData;
        private DialogueNodeData _lastDialogueNodeData;

        private void Awake()
        {
            _dialogueWidget = FindObjectOfType<DialogueWidget>();
            _audioSource = GetComponent<AudioSource>();
        }

        public void StartDialogue()
        {
            CheckNodeType(GetNextNode(DialogueContainer.StartNodeData[0]));
            _dialogueWidget.ShowDialogue(true);
        }

        private void CheckNodeType(BaseNodeData baseNodeData)
        {
            switch (baseNodeData)
            {
                case DialogueNodeData nodeData:
                    RunNode(nodeData);
                    break;
                case EndNodeData nodeData:
                    RunNode(nodeData);
                    break;
                case EventNodeData nodeData:
                    RunNode(nodeData);
                    break;
                case StartNodeData nodeData:
                    RunNode(nodeData);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(baseNodeData));
            }
        }

        private void RunNode(StartNodeData nodeData)
        {
            CheckNodeType(GetNextNode(DialogueContainer.StartNodeData[0]));
        }

        private void RunNode(DialogueNodeData nodeData)
        {
            if (_currentDialogueNodeData != nodeData)
            {
                _lastDialogueNodeData = _currentDialogueNodeData;
                _currentDialogueNodeData = nodeData;
            }

            string nodeDataName = nodeData.CharacterName.Find(text => text.LanguageType == LanguageController.Instance.Language).LanguageGenericType;
            string nodeDataText = nodeData.Texts.Find(text => text.LanguageType == LanguageController.Instance.Language).LanguageGenericType;

            _dialogueWidget.SetText(nodeDataName, nodeDataText);
            _dialogueWidget.SetImage(nodeData.FaceImage, nodeData.FaceImageSideType);
            
            MakeButtons(nodeData.DialogueNodePorts);
            
            AudioClip clip = nodeData.AudioClips.Find(clip => clip.LanguageType == LanguageController.Instance.Language).LanguageGenericType;

            if (clip == null) return;
            
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        private void RunNode(EventNodeData nodeData)
        {
            nodeData.EventScriptableObjectDatas.ForEach(soEvent =>
            {
                if (soEvent.DialogueEventSO != null) soEvent.DialogueEventSO.RunEvent();
            });
            
            CheckNodeType(GetNextNode(nodeData));
        }

        private void RunNode(EndNodeData nodeData)
        {
            switch (nodeData.EndNodeType)
            {
                case EndNodeType.End:
                    _dialogueWidget.ShowDialogue(false);
                    break;
                case EndNodeType.Repeat:
                    CheckNodeType(GetNodeByGuid(_currentDialogueNodeData.NodeGuid));
                    break;
                case EndNodeType.Goback:
                    CheckNodeType(GetNodeByGuid(_lastDialogueNodeData.NodeGuid));
                    break;
                case EndNodeType.ReturnToStart:
                    CheckNodeType(GetNextNode(DialogueContainer.StartNodeData[0]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void MakeButtons(List<DialogueNodePort> nodePorts)
        {
            List<string> texts = new();
            List<UnityAction> unityActions = new();

            nodePorts.ForEach(nodePort =>
            {
                string choiceText = nodePort.TextLanguages.Find(text => text.LanguageType == LanguageController.Instance.Language).LanguageGenericType;
                texts.Add(choiceText);
                
                void OnSelectNodeChoice()
                {
                    CheckNodeType(GetNodeByGuid(nodePort.TargetGuid));
                    _audioSource.Stop();
                }

                unityActions.Add(OnSelectNodeChoice);
            });

            _dialogueWidget.SetButtons(texts, unityActions);
        }
    }
}