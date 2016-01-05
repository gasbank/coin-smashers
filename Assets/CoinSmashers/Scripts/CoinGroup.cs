using System.Collections.Generic;
using UnityEngine;

public class CoinGroup : MonoBehaviour
{
    public List<Rigidbody> coins;
    
    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var c = transform.GetChild(i);
            var rb = c.GetComponent<Rigidbody>();
            if (rb != null)
            {
                coins.Add(rb);
            }
        }
    }
}