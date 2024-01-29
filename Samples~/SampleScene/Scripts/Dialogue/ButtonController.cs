using UnityEngine;
using UnityEngine.UI;

namespace Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Dialogue
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private Text _buttonText;

        public Text ButtonText => _buttonText;

        public void SetText(string text)
        {
            _buttonText.text = text;
        }
    }
}