using System;
using Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Dialogue;
using Fenneig_Dialogue_Editor.Runtime.Enums;
using UnityEngine;

namespace Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Events
{
    public class GameEvents : MonoBehaviour
    {
        public event Action<int> RandomColorModel;
        
        public static GameEvents Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void CallRandomColorModel(int number)
        {
            RandomColorModel?.Invoke(number);
        }

        public virtual void DialogueModifierEvents(string stringEvent, StringEventModifierType modifierType, float value = 0)
        {
            
        }

        public virtual bool DialogueConditionEvents(string stringEvent, StringEventConditionType conditionType, float value = 0)
        {
            return false;
        }
    }
}