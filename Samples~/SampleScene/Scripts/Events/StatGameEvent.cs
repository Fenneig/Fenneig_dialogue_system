using Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Dialogue;
using Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Gameplay;
using Fenneig_Dialogue_Editor.Runtime.Enums;
using Fenneig_Dialogue_Editor.Runtime.String_Tool;
using UnityEngine;

namespace Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Events
{
    public class StatGameEvent : GameEvents
    {
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] [StringEvent] private string _moneyEvent;
        [SerializeField] [StringEvent] private string _healthEvent;
        [SerializeField] [StringEvent] private string _talkedAlreadyEvent;
        
        public override bool DialogueConditionEvents(string stringEvent, StringEventConditionType conditionType, float value = 0)
        {
            if (stringEvent == _moneyEvent)
            {
                return UseStringEventCondition.ConditionFloatCheck(_playerStats.Money, value, conditionType);
            }

            if (stringEvent == _healthEvent)
            {
                return UseStringEventCondition.ConditionFloatCheck(_playerStats.Health, value, conditionType);
            }

            if (stringEvent == _talkedAlreadyEvent)
            {
                return UseStringEventCondition.ConditionBoolCheck(_playerStats.TalkedAlready, conditionType);
            }

            return false;
        }

        public override void DialogueModifierEvents(string stringEvent, StringEventModifierType modifierType, float value = 0)
        {
            if (stringEvent == _moneyEvent)
            {
                _playerStats.ModifyMoney((int) UseStringEventModifier.ModifierFloatCheck(value, modifierType));
            }

            if (stringEvent == _healthEvent)
            {
                _playerStats.ModifyHealth((int) UseStringEventModifier.ModifierFloatCheck(value, modifierType));
            }

            if (stringEvent == _talkedAlreadyEvent)
            {
                _playerStats.TalkedAlready = UseStringEventModifier.ModifierBoolCheck(modifierType);
            }
        }
    }
}