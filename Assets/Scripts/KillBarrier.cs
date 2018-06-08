using UnityEngine;
using System.Collections;

public class KillBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Character character = other.GetComponent<Character>();
        if (character)
        {
            character.DecreaseHealth(character.Health);
            Destroy(character.gameObject);
        }
    }
}
