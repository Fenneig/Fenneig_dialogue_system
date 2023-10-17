using Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Dialogue;
using UnityEngine;

namespace Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Gameplay
{
    [RequireComponent(typeof(DialogueSpeak))]
    public class StartSpeak : MonoBehaviour
    {
        [SerializeField] private Canvas _speakBubbleCanvas;

        private DialogueSpeak _dialogueSpeak;

        private void Awake()
        {
            _speakBubbleCanvas.gameObject.SetActive(false);
            _dialogueSpeak = GetComponent<DialogueSpeak>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && _speakBubbleCanvas.gameObject.activeSelf)
            {
                _dialogueSpeak.StartDialogue();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player _))
            {
                _speakBubbleCanvas.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player _))
            {
                _speakBubbleCanvas.gameObject.SetActive(false);
            }
        }
    }
}