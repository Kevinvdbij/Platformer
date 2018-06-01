using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityCharFloatEvent : UnityEvent<Character, float> { }

[CreateAssetMenu(fileName = "New CharFloatEvent", menuName = "Events/Character Float Event", order = 3)]
public class CharFloatEvent : GameEvent
{
    public override void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(DummyCharacter.NewDummy, 0f);
    }

    public void Raise(Character character, float value)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(character, value);
    }
}
