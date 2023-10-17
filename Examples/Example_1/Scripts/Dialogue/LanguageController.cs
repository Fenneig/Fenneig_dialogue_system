using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.Enums;
using UnityEngine;

namespace Fenneig_Dialogue_Editor.Examples.Example_1.Scripts.Dialogue
{
    public class LanguageController : MonoBehaviour
    {
        [SerializeField] private LanguageType _language;

        public LanguageType Language
        {
            get => _language;
            set => _language = value;
        }

        public static LanguageController Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}