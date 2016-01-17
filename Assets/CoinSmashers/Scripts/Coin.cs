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
        //Debug.Log(gameObject.name + " Collision Enter - " + collision.contacts.Length + " contact(s)");
    }
    
    void OnCollisionStay(Collision collision)
    {
        contacted = true;
    }
    
    void OnCollisionExit(Collision collision)
    {
        //Debug.Log(gameObject.name + " Collision Exit " + collision.contacts.Length);
        contacted = collision.contacts.Length > 0;
    }
}
