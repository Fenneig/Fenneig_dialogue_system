using UnityEditor;
using UnityEngine;

namespace Fenneig_Dialogue_Editor.Editor.String_Tool
{
    public class StringEventDefinition : ScriptableObject
    {
        [SerializeField] private string[] _stringEventsForEditor;
        public string[] StringEventsForEditor => _stringEventsForEditor;

        private static StringEventDefinition _instance;

        public static StringEventDefinition I => _instance == null ? LoadDef() : _instance;

        private static StringEventDefinition LoadDef()
        {
            _instance = Resources.Load<StringEventDefinition>("String event definition");

            if (_instance == null)
            {
                _instance = CreateInstance<StringEventDefinition>();

                AssetDatabase.CreateAsset(_instance, "Assets/Resources/String event definition.asset");
                AssetDatabase.SaveAssets();
            }

            return _instance;
        }
    }
}