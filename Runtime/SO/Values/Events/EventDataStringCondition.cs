using System;
using Fenneig_Dialogue_Editor.Runtime.SO.Values.Enums;

namespace Fenneig_Dialogue_Editor.Runtime.SO.Values.Events
{
    [Serializable]
    public class EventDataStringCondition
    { 
        public ContainerEventString StringEventText = new();
        public ContainerFloat Number = new();
        public ContainerStringEventConditionType StringEventConditionType = new();
    }
}