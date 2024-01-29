using UnityEngine;
using UnityEngine.Events;

namespace Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Gameplay
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private int _money = 10;
        [SerializeField] private int _health = 70;
        [SerializeField] private bool _talkedAlready;

        public UnityAction OnChangedMoney;
        public UnityAction OnChangedHealth;

        public int Money => _money;

        public int Health => _health;

        public bool TalkedAlready
        {
            get => _talkedAlready;
            set => _talkedAlready = value;
        }

        private void Start()
        {
            OnChangedHealth?.Invoke();
            OnChangedMoney?.Invoke();
        }

        public void ModifyMoney(int value)
        {
            _money += value;
            OnChangedMoney?.Invoke();
        }

        public void ModifyHealth(int value)
        {
            _health += value;
            OnChangedHealth?.Invoke();
        }
    }
}