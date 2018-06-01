using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameEvent", menuName = "Events/Game Event", order = 0)]
public class GameEvent : ScriptableObject
{
    protected List<GameEventListener> listeners = 
        new List<GameEventListener>();

    public virtual void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised();
    }

    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }

    public bool ContainsListener(GameEventListener listener)
    {
        return listeners.Contains(listener) ? true : false;
    }
}
