using System;
using System.Collections.Generic;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.SO;

namespace Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.NodesData
{
    [Serializable]
    public class EventNodeData : BaseNodeData
    {
        public DialogueEventSO DialogueEvent;
        public List<EventScriptableObjectData> EventScriptableObjectDatas;
        public List<EventStringIdData> EventStringIdDatas;
    }
}