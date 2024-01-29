using Fenneig_Dialogue_Editor.Runtime.Enums;
using UnityEngine.Events;

namespace Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Dialogue
{
    public class DialogueButtonContainer
    {
        public UnityAction UnityAction { get; set; }
        public string Text { get; set; }
        public bool ConditionCheck { get; set; }
        public ChoiceStateType ChoiceStateType { get; set; }
    }
}