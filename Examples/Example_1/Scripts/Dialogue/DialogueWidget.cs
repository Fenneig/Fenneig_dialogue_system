using System;
using System.Collections.Generic;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.Enums;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Dialogue
{
    public class DialogueWidget : MonoBehaviour
    {
        [SerializeField] private GameObject _dialogueUI;
        [Header("Text")]
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _dialogueText;
        [Header("Image")] 
        [SerializeField] private PortraitController _leftImage;
        [SerializeField] private PortraitController _rightImage;
        [Header("Button")]
        [SerializeField] private GameObject _buttonPanel;
        [SerializeField] private ButtonController _buttonPrefab;

        private List<ButtonController> _buttons = new();

        private void Awake()
        {
            ShowDialogue(false);
            _leftImage.Show(false);
            _rightImage.Show(false);
        }

        public void ShowDialogue(bool state)
        {
            _dialogueUI.SetActive(state);
        }

        public void SetText(string nameText, string text)
        {
            _nameText.text = nameText;
            _dialogueText.text = text;
        }

        public void SetImage(Sprite image, DialogueSideType side)
        {
            _leftImage.Show(false);
            _rightImage.Show(false);

            if (image == null) throw new NullReferenceException("Trying to state dialogue image without image!");
            
            if (side is DialogueSideType.Left)
            {
                _leftImage.Show(true, image);
            }
            else
            {
                _rightImage.Show(true, image);
            }
        }

        public void SetButtons(List<string> texts, List<UnityAction> unityActions)
        {
            _buttons.ForEach(button => button.gameObject.SetActive(false));

            CheckAndFillDialogueAnswerRooms(texts);
            for (int i = 0; i < texts.Count; i++)
            {
                _buttons[i].gameObject.SetActive(true);
                _buttons[i].SetText(texts[i]);
                _buttons[i].GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
                _buttons[i].GetComponent<Button>().onClick.AddListener(unityActions[i]);
            }
        }

        private void CheckAndFillDialogueAnswerRooms(List<string> texts)
        {
            if (_buttons.Count >= texts.Count) return;
            int buttonsToCreate = texts.Count - _buttons.Count;
            for (int i = 0; i < buttonsToCreate; i++)
            {
                ButtonController button = Instantiate(_buttonPrefab, _buttonPanel.transform);
                _buttons.Add(button);
            }
        }
    }
}