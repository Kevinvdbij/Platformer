using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBarrier : MonoBehaviour
{
    public void KillPlayer(Character character)
    {
        character.OnKillBarrier();
    }
}
