using System;
using Fenneig_Dialogue_Editor.Runtime.SO;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Dialogue/barrel roll event")]
[Serializable]
public class Event_DoABarrelRoll : DialogueEventSO
{
    [SerializeField] private UnityEvent _event;
    public override void RunEvent()
    {
        _event?.Invoke();
    }
}