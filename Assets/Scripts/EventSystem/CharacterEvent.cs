using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityCharacterEvent : UnityEvent<Character> { }

[CreateAssetMenu(fileName = "New CharacterEvent", menuName = "Events/Character Event", order = 2)]
public class CharacterEvent : GameEvent
{
    public override void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(DummyCharacter.NewDummy);
    }

    public void Raise(Character value)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(value);
    }
}