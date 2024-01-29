using System;
using Fenneig_Dialogue_Editor.Runtime.SO;
using UnityEngine;

namespace Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Events
{
    [CreateAssetMenu(menuName = "Dialogue/Color event")]
    [Serializable]
    public class Event_RandomColors : DialogueEventSO
    {
        [SerializeField] private int _number;
        public override void RunEvent()
        {
            GameEvents.Instance.CallRandomColorModel(_number);
        }
    }
}