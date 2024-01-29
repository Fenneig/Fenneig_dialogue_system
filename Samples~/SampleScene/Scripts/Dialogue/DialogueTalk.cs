using System.Collections.Generic;
using System.Linq;
using Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Events;
using Fenneig_Dialogue_Editor.Runtime.Enums;
using Fenneig_Dialogue_Editor.Runtime.SO.Dialogue;
using UnityEngine;
using UnityEngine.Events;

namespace Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Dialogue
{
    [RequireComponent(typeof(AudioSource))]
    public class DialogueTalk : DialogueGetData
    {
        [SerializeField] private DialogueWidget _dialogueWidget;
        private AudioSource _audioSource;

        private DialogueData _currentDialogueNodeData;
        private DialogueData _lastDialogueNodeData;

        private List<DialogueDataBaseContainer> _dialogueNodes;
        private int _currentIndex = 0;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void StartDialogue()
        {
            _dialogueWidget.ShowDialogue(true);
            CheckNodeType(GetNextNode(DialogueContainer.StartData[0]));
        }

        private void CheckNodeType(BaseData baseNodeData)
        {
            switch (baseNodeData)
            {
                case StartData nodeData:
                    RunNode(nodeData);
                    break;
                case DialogueData nodeData:
                    RunNode(nodeData);
                    break;
                case EventData nodeData:
                    RunNode(nodeData);
                    break;
                case EndData nodeData:
                    RunNode(nodeData);
                    break;
                case BranchData nodeData:
                    RunNode(nodeData);
                    break;
            }
        }

        private void RunNode(StartData nodeData)
        {
            CheckNodeType(GetNextNode(DialogueContainer.StartData[0]));
        }

        private void RunNode(DialogueData nodeData)
        {
            _currentDialogueNodeData = nodeData;

            _dialogueNodes = new List<DialogueDataBaseContainer>();
            _dialogueNodes.AddRange(nodeData.DialogueDataImages);
            _dialogueNodes.AddRange(nodeData.DialogueDataNames);
            _dialogueNodes.AddRange(nodeData.DialogueDataTexts);

            _currentIndex = 0;
            
            _dialogueNodes.Sort((x,y) => x.ID.Value.CompareTo(y.ID.Value));

            DoTheDialogue();
        }

        private void DoTheDialogue()
        {
            _dialogueWidget.HideButtons();

            for (int i = _currentIndex; i < _dialogueNodes.Count; i++)
            {
                _currentIndex = i + 1;
                if (_dialogueNodes[i] is DialogueDataName dataName)
                {
                    _dialogueWidget.SetName(dataName.CharacterName.Find(text => text.LanguageType == LanguageController.Instance.CurrentLanguage).LanguageGenericType);
                }
                else if (_dialogueNodes[i] is DialogueDataImage dataImage)
                {
                    _dialogueWidget.SetImage(dataImage.LeftSprite.Value,dataImage.RightSprite.Value);
                }
                else if (_dialogueNodes[i] is DialogueDataText dataText)
                {
                    _dialogueWidget.SetText(dataText.Text.Find(text => text.LanguageType == LanguageController.Instance.CurrentLanguage).LanguageGenericType);
                    ShowButtons();
                    PlayAudio(dataText.AudioClips.Find(clip => clip.LanguageType == LanguageController.Instance.CurrentLanguage).LanguageGenericType);
                    break;
                }

            }
        }

        private void ShowButtons()
        {
            List<DialogueButtonContainer> dialogueButtonContainers = new();
            if (_currentIndex == _dialogueNodes.Count)
            {
                if (_currentDialogueNodeData.DialogueDataPorts.Count == 0)
                {
                    void OnUnityAction() => CheckNodeType(GetNextNode(_currentDialogueNodeData));
                    dialogueButtonContainers.Add(GetContinueButton(OnUnityAction));
                    _dialogueWidget.SetButtons(dialogueButtonContainers);
                }
                else
                {
                    _currentDialogueNodeData.DialogueDataPorts.ForEach(port =>
                    {
                        ChoiceCheck(port.InputGuid, dialogueButtonContainers);
                    });
                    _dialogueWidget.SetButtons(dialogueButtonContainers);
                }
            }
            else
            {
                dialogueButtonContainers.Add(GetContinueButton(DoTheDialogue));
                _dialogueWidget.SetButtons(dialogueButtonContainers);
            }
        }

        private DialogueButtonContainer GetContinueButton(UnityAction unityAction)
        {
            DialogueButtonContainer buttonContainer = new DialogueButtonContainer()
            {
                Text = "Continue",
                ConditionCheck = true,
                UnityAction = unityAction,
                ChoiceStateType = default
            };
            return buttonContainer;
        }

        private void ChoiceCheck(string guidID, List<DialogueButtonContainer> dialogueButtonContainers)
        {
            ChoiceData choiceNode = GetNodeByGuid(guidID) as ChoiceData;
            DialogueButtonContainer dialogueButtonContainer = new();

            bool checkBranch = choiceNode != null && choiceNode.EventDataStringConditions.All(item =>
                GameEvents.Instance.DialogueConditionEvents(item.StringEventText.Value, item.StringEventConditionType.Value, item.Number.Value));

            void OnUnityAction() => CheckNodeType(GetNextNode(choiceNode));

            dialogueButtonContainer.ChoiceStateType = choiceNode.ChoiceStateType.Value;
            dialogueButtonContainer.Text = choiceNode.Text
                .Find(text => text.LanguageType == LanguageController.Instance.CurrentLanguage).LanguageGenericType;
            dialogueButtonContainer.UnityAction = OnUnityAction;
            dialogueButtonContainer.ConditionCheck = checkBranch;

            dialogueButtonContainers.Add(dialogueButtonContainer);
        }

        private void PlayAudio(AudioClip audioClip)
        {
            _audioSource.Stop();
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }

        private void RunNode(EventData nodeData)
        {
            nodeData.ContainerDialogueEventSOs.ForEach(item => item.DialogueEventSO.RunEvent());
            nodeData.EventDataStringModifiers.ForEach(item =>
                GameEvents.Instance.DialogueModifierEvents(item.StringEventText.Value, item.StringEventModifierType.Value, item.Number.Value));
            
            CheckNodeType(GetNextNode(nodeData));
        }

        private void RunNode(EndData nodeData)
        {
            switch (nodeData.EndNodeType.Value)
            {
                case EndNodeType.End:
                    _dialogueWidget.ShowDialogue(false);
                    break;
                case EndNodeType.Repeat:
                    CheckNodeType(GetNodeByGuid(_currentDialogueNodeData.NodeGuid));
                    break;
                case EndNodeType.ReturnToStart:
                    CheckNodeType(GetNextNode(DialogueContainer.StartData[0]));
                    break;
            }
        }

        private void RunNode(BranchData nodeData)
        {
            bool checkBranch = nodeData.EventDataStringConditions.All(item =>
                GameEvents.Instance.DialogueConditionEvents(item.StringEventText.Value, item.StringEventConditionType.Value, item.Number.Value));

            string nextNode = checkBranch ? nodeData.TrueGuidNode : nodeData.FalseGuidNode;
            CheckNodeType(GetNodeByGuid(nextNode));
        }
    }
}