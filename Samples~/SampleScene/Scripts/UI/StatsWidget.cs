using Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Dialogue
{
    public class StatsWidget : MonoBehaviour
    {
        [SerializeField] private Text _moneyText;
        [SerializeField] private Text _healthText;
        [SerializeField] private Player _player;

        private void Start()
        {
            _player.PlayerStats.OnChangedHealth += () => _healthText.text = $"Health: {_player.PlayerStats.Health}";
            _player.PlayerStats.OnChangedMoney += () => _moneyText.text = $"Money: {_player.PlayerStats.Money}";
        }
    }
}