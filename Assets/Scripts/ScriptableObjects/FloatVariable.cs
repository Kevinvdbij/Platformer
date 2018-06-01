using UnityEngine;

[CreateAssetMenu(fileName = "new FloatVariable", menuName = "Data/Float Variable", order = 0)]
public class FloatVariable : ScriptableObject
{
    [SerializeField] private float value;

    public void SetValue(float amount)
    {
        value = amount;
    }

    public void SetValue(FloatVariable variable)
    {
        value = variable.value;
    }

    public void ApplyChange(float amount)
    {
        value += amount;
    }

    public void ApplyChange(FloatVariable variable)
    {
        value += variable.value;
    }
}
