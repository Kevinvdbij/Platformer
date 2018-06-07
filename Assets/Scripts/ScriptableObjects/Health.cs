using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new HealthData", menuName = "Data/Health Data", order = 0)]
public class Health : ScriptableObject
{
    public float current;
    public float minimum;
    public float maximum;
    public float starting;

    public Health Init(Health data, bool setMaxHealth)
    {
        current = data.current;
        minimum = data.minimum;
        maximum = data.maximum;
        starting = data.starting;

        if (setMaxHealth)
            current = maximum;
        return this;
    }

    public Health Init (bool setMaxHealth)
    {
        current = setMaxHealth ? maximum : starting;
        return this;
    }

    public Health GetInstance(bool setMaxHealth)
    {
        Health instance = CreateInstance(typeof(Health)) as Health;
        instance.Init(this, setMaxHealth);
        return instance;
    }

    public void Increase(float amount)
    {
        current = Mathf.Clamp(current + amount, minimum, maximum);
    }

    public void Decrease(float amount)
    {
        current = Mathf.Clamp(current - amount, minimum, maximum);
    }

    public void InstaKill()
    {
        current = minimum;
    }

    public bool IsDepleted
    {
        get
        {
            return current == minimum ? true : false;
        }
    }
}
