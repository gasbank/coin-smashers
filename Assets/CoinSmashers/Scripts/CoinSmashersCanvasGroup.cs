using UnityEngine;

[DisallowMultipleComponent]
public class CoinSmashersCanvasGroup : MonoBehaviour
{
    void Awake()
    {
        transform.localPosition = Vector3.zero;
    }
}