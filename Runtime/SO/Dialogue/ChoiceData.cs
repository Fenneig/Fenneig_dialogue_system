using System;
using System.Collections.Generic;
using Fenneig_Dialogue_Editor.Runtime.NodesData;
using Fenneig_Dialogue_Editor.Runtime.SO.Values.Enums;
using Fenneig_Dialogue_Editor.Runtime.SO.Values.Events;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor.UIElements;
#endif
namespace Fenneig_Dialogue_Editor.Runtime.SO.Dialogue
{
    [Serializable]
    public class ChoiceData : BaseData
    {
        public TextField TextField { get; set; }
#if UNITY_EDITOR
        public ObjectField ObjectField { get; set; }
#endif
        public ContainerChoiceStateType ChoiceStateType = new();
        public List<LanguageGeneric<string>> Text = new();
        public List<LanguageGeneric<AudioClip>> AudioClips = new();
        public List<EventDataStringCondition> EventDataStringConditions = new();
    }
}