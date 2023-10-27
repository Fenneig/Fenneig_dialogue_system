using System;
using System.Collections.Generic;
using Fenneig_Dialogue_Editor.Runtime.SO.Dialogue;
using UnityEngine;

namespace Fenneig_Dialogue_Editor.Runtime.SO
{
    [CreateAssetMenu(menuName = "Dialogue/Dialogue container")]
    [Serializable]
    public class DialogueContainerSO : ScriptableObject
    {
        public List<LinkData> NodeLinkData = new();
        
        public List<StartData> StartData = new();
        public List<DialogueData> DialogueData = new();
        public List<EventData> EventData = new();
        public List<EndData> EndData = new();
        public List<BranchData> BranchData = new();
        public List<ChoiceData> ChoiceData = new();

        public List<BaseData> AllData
        {
            get
            {
                List<BaseData> allNodes = new();
                allNodes.AddRange(StartData);
                allNodes.AddRange(DialogueData);
                allNodes.AddRange(EventData);
                allNodes.AddRange(EndData);
                allNodes.AddRange(BranchData);
                allNodes.AddRange(ChoiceData);
                return allNodes;
            }
        }

        public void ClearData()
        {
            StartData.Clear();
            DialogueData.Clear();
            EventData.Clear();
            EndData.Clear();
            BranchData.Clear();
            ChoiceData.Clear();
        }
    }
}