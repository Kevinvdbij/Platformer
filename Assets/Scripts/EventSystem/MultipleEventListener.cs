using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultipleEventListener : GameEventListener
{
    public List<GameEvent> events =
        new List<GameEvent>();

    protected override void OnEnable()
    {
        foreach (GameEvent e in events)
        {
            if (e == null || e.ContainsListener(this)) continue;
            e.RegisterListener(this);
        }
    }

    protected override void OnDisable()
    {
        foreach (GameEvent e in events)
        {
            if (e == null || !e.ContainsListener(this)) continue;
            e.UnregisterListener(this);
        }
    }

    public override void OnEventRaised(Character value)
    {
        OnEventRaised();
    }

    public override void OnEventRaised(Character character, float value)
    {
        OnEventRaised();
    }
}
