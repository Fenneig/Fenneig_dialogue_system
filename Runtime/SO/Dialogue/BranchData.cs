using System;
using System.Collections.Generic;
using Fenneig_Dialogue_Editor.Runtime.SO.Values.Events;

namespace Fenneig_Dialogue_Editor.Runtime.SO.Dialogue
{
    [Serializable]
    public class BranchData : BaseData
    {
        public string TrueGuidNode;
        public string FalseGuidNode;
        public List<EventDataStringCondition> EventDataStringConditions = new();
    }
}