using System;
using UnityEngine;

namespace Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Events
{
    public class GameEvents : MonoBehaviour
    {
        public event Action<int> RandomColorModel;
        
        public static GameEvents Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void CallRandomColorModel(int number)
        {
            RandomColorModel?.Invoke(number);
        }
    }
}