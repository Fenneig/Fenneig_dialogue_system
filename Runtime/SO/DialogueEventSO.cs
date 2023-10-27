using UnityEngine;

namespace Fenneig_Dialogue_Editor.Runtime.SO
{
    public class DialogueEventSO : ScriptableObject
    {
        public virtual void RunEvent()
        {
            Debug.Log("Event fired");
        }
    }
}