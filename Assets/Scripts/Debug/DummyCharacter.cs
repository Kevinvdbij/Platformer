using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCharacter : Character
{
    private void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    public static DummyCharacter NewDummy
    {
        get
        {
            GameObject obj = new GameObject();
            obj.name = "Dummy Character";
            return (obj.AddComponent<DummyCharacter>());
        }
    }
}
