using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.NodesData
{
    [Serializable]
    public class DialogueNodePort
    {
        public string PortGuid;
        public string TargetGuid;
        public TextField TextField;
        public List<LanguageGeneric<string>> TextLanguages = new();
    }
}