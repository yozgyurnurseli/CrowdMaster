using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableStack : MonoBehaviour
{
    List<Minion> minions;

    void Start()
    {
        minions = new List<Minion>();
        for (int i = 0; i < transform.childCount; i++)
        {
            minions.Add(transform.GetChild(i).GetComponent<Minion>());
        }
    }

    public List<Minion> CollectedMinions()
    {
        return minions;
    }
}
