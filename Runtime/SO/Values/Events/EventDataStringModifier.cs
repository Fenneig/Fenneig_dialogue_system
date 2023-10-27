using System;
using Fenneig_Dialogue_Editor.Runtime.SO.Values.Enums;

namespace Fenneig_Dialogue_Editor.Runtime.SO.Values.Events
{
    [Serializable]
    public class EventDataStringModifier
    {
        public ContainerString StringEventText = new();
        public ContainerFloat Number = new();
        public ContainerStringEventModifierType StringEventModifierType = new();
    }
}