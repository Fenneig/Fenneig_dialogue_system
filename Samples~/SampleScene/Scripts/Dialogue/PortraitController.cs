using UnityEngine;
using UnityEngine.UI;

namespace Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Dialogue
{
    public class PortraitController : MonoBehaviour
    {
        [SerializeField] private Image _faceImage;
        
        public void Show(bool state, Sprite image = null)
        {
            _faceImage.gameObject.SetActive(state);
            _faceImage.sprite = image;
        }
    }
}