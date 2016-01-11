using UnityEngine;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class CoinGroup : MonoBehaviour
{
    public List<Coin> coins;
    
    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var c = transform.GetChild(i);
            var coin = c.GetComponent<Coin>();
            if (coin != null)
            {
                coins.Add(coin);
            }
        }
    }
}
