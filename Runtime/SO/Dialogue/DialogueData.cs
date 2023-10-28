using System;
using System.Collections.Generic;
using Fenneig_Dialogue_Editor.Runtime.NodesData;
using Fenneig_Dialogue_Editor.Runtime.SO.Values;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor.UIElements;
#endif

namespace Fenneig_Dialogue_Editor.Runtime.SO.Dialogue
{
    [Serializable]
    public class DialogueData : BaseData
    {
        public List<DialogueDataBaseContainer> DialogueDataBaseContainers { get; set; } = new();
        public List<DialogueDataName> DialogueDataNames = new();
        public List<DialogueDataText> DialogueDataTexts = new();
        public List<DialogueDataImage> DialogueDataImages = new();
        public List<DialogueDataPort> DialogueDataPorts = new();
    }

    [Serializable]
    public class DialogueDataBaseContainer
    {
        public ContainerInt ID = new();
    }

    [Serializable]
    public class DialogueDataName : DialogueDataBaseContainer
    {
        public TextField TextField { get; set; }
        public ContainerString GuidID = new();
        public List<LanguageGeneric<string>> CharacterName = new();
    }

    [Serializable]
    public class DialogueDataText : DialogueDataBaseContainer
    {
        public TextField TextField { get; set; }
#if UNITY_EDITOR
        public ObjectField ObjectField { get; set; }
#endif
        public ContainerString GuidID = new();
        public List<LanguageGeneric<string>> Text = new();
        public List<LanguageGeneric<AudioClip>> AudioClips = new();
    }

    [Serializable]
    public class DialogueDataImage : DialogueDataBaseContainer
    {
        public ContainerSprite LeftSprite = new();
        public ContainerSprite RightSprite = new();
    }

    [Serializable]
    public class DialogueDataPort : DialogueDataBaseContainer
    {
        public string PortGuid;
        public string InputGuid;
        public string OutputGuid;
    }
}