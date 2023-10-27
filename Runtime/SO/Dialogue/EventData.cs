using System;
using System.Collections.Generic;
using Fenneig_Dialogue_Editor.Runtime.NodesData;
using Fenneig_Dialogue_Editor.Runtime.SO.Values.Events;

namespace Fenneig_Dialogue_Editor.Runtime.SO.Dialogue
{
    [Serializable]
    public class EventData : BaseData
    {
        public List<EventDataStringModifier> EventDataStringModifiers = new();
        public List<ContainerDialogueEventSO> ContainerDialogueEventSOs = new();
    }
}