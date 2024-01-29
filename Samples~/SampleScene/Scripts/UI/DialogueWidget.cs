using System.Collections.Generic;
using Fenneig_Dialogue_Editor.Runtime.Enums;
using UnityEngine;
using UnityEngine.InputSystem;
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
        [SerializeField] private GameObject _buttonContentPanel;
        [SerializeField] private ButtonController _buttonPrefab;
        [Header("Button color")]
        [SerializeField] private Color _textDisableColor;
        [SerializeField] private Color _buttonDisableColor;
        [Header("Interactable")]
        [SerializeField] private Color _textInteractableColor;

        private List<ButtonController> _buttons = new();

        private void Awake()
        {
            ShowDialogue(false);
            _leftImage.Show(false);
            _rightImage.Show(false);
        }

        public void ShowDialogue(bool state) => _dialogueUI.SetActive(state);

        public void SetName(string nameText) => _nameText.text = nameText;

        public void SetText(string text) => _dialogueText.text = text;

        public void SetImage(Sprite leftImage, Sprite rightImage)
        {
            _leftImage.Show(false);
            _rightImage.Show(false);

            if (leftImage != null) _leftImage.Show(true, leftImage);
            if (rightImage != null) _rightImage.Show(true, rightImage);

        }

        public void SetButtons(List<DialogueButtonContainer> buttonContainers)
        {
            HideButtons();
            CheckAndFillDialogueAnswerRooms(buttonContainers.Count);
            for (int i = 0; i < buttonContainers.Count; i++)
            {
                Button currentButton = _buttons[i].GetComponent<Button>();
                currentButton.onClick = new Button.ButtonClickedEvent();
                currentButton.interactable = true;
                _buttons[i].ButtonText.color = _textInteractableColor;
                if (buttonContainers[i].ConditionCheck || buttonContainers[i].ChoiceStateType == ChoiceStateType.GrayOut)
                {
                    _buttons[i].SetText($"{i + 1}: {buttonContainers[i].Text}");
                    _buttons[i].gameObject.SetActive(true);

                    if (!buttonContainers[i].ConditionCheck)
                    {
                        currentButton.interactable = false;
                        _buttons[i].ButtonText.color = _textDisableColor;
                        var colors = currentButton.colors;
                        colors.disabledColor = _buttonDisableColor;
                        currentButton.colors = colors;
                    }
                    else
                    {
                        currentButton.onClick.AddListener(buttonContainers[i].UnityAction);
                    }
                }
            }
        }

        private void Update()
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                if (Keyboard.current[Key.Digit1 + i].wasPressedThisFrame)
                {
                    Button currentButton = _buttons[i].GetComponent<Button>();
                    if (currentButton.gameObject.activeSelf) currentButton.onClick.Invoke();
                }
            }
        }

        private void CheckAndFillDialogueAnswerRooms(int buttonsCountToCreate)
        {
            if (_buttons.Count >= buttonsCountToCreate) return;
            int buttonsToCreate = buttonsCountToCreate - _buttons.Count;
            for (int i = 0; i < buttonsToCreate; i++)
            {
                ButtonController button = Instantiate(_buttonPrefab, _buttonContentPanel.transform);
                _buttons.Add(button);
                button.gameObject.SetActive(false);
            }
        }

        public void HideButtons()
        {
            _buttons.ForEach(button => button.gameObject.SetActive(false));
        }
    }

    
}