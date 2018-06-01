using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;

    public UnityEvent response;
    public UnityFloatEvent floatResponse;
    public UnityCharacterEvent characterResponse;
    public UnityCharFloatEvent charFloatResponse;


    protected virtual void OnEnable()
    {
        if (!gameEvent)
        {
            Debug.LogWarning("No game event. Listener on " + name + "disabled.");
            enabled = false;
            return;
        }
        gameEvent.RegisterListener(this);
    }

    protected virtual void OnDisable()
    {
        if (!gameEvent)
            return;

        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        response.Invoke();
    }

    public void OnEventRaised(float value)
    {
        FloatEvent floatEvent = gameEvent as FloatEvent;
        if (floatEvent)
            floatResponse.Invoke(value);
    }

    public virtual void OnEventRaised(Character value)
    {
        CharacterEvent characterEvent = gameEvent as CharacterEvent;
        if (characterEvent)
            characterResponse.Invoke(value);
    }

    public virtual void OnEventRaised(Character character, float value)
    {
        CharFloatEvent charFloat = gameEvent as CharFloatEvent;
        if (charFloat)
            charFloatResponse.Invoke(character, value);
            
    }
}
