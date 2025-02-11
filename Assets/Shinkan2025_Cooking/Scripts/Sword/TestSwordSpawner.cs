using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSwordSpawner : MonoBehaviour,ISwordSpawnable
{
    [SerializeField]private GameObject sword;

    public ISwordTracker InstiniateSword()
    {
        var newSword = Instantiate(sword);
        newSword.transform.parent = this.transform;
        newSword.TryGetComponent(out ISwordTracker s);
        if(s == null)
        {
            Debug.Log("s is null");
        }
        return s;
    }
}
