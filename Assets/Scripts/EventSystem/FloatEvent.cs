using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityFloatEvent : UnityEvent<float> { }

[CreateAssetMenu(fileName = "New FloatEvent", menuName = "Events/Float Event", order = 1)]
public class FloatEvent : GameEvent
{
    public override void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(0f);
    }
   
    public void Raise(float value)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(value);
    }
}