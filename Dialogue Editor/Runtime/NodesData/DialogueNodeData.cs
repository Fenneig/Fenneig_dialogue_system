using System;
using System.Collections.Generic;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.Enums;
using UnityEngine;

namespace Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.NodesData
{
    [Serializable]
    public class DialogueNodeData : BaseNodeData
    {
        public List<DialogueNodePort> DialogueNodePorts;
        public List<LanguageGeneric<string>> Texts;
        public List<LanguageGeneric<AudioClip>> AudioClips;
        public Sprite FaceImage;
        public List<LanguageGeneric<string>> CharacterName;
        public DialogueSideType FaceImageSideType;
    }
}