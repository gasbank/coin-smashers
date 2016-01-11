using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class Coin : MonoBehaviour
{
    public bool contacted { get; private set; }
    public Rigidbody rb { get; private set; }
    
    static int COIN_INDEX;
    int coinIndex;
    
    void Awake()
    {
        coinIndex = ++COIN_INDEX;
        
        rb = GetComponent<Rigidbody>();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Coin #" + coinIndex + " Collision Enter - " + collision.contacts.Length + " contact(s)");
        contacted = true;
    }
    
    void OnCollisionStay(Collision collision)
    {
    }
    
    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Coin #" + coinIndex + " Collision Exit");
        contacted = false;
    }
}
