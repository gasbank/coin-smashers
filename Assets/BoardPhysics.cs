using UnityEngine;
using System.Collections;

public class BoardPhysics : MonoBehaviour
{
    public GameObject coinGroup;
    public Transform forceApplyBeginPoint;
    public Transform forceApplyEndPoint;
    public float forceYMagnitude = 1.0f;
    public float forceScaler = 10.0f;

    public void ApplyRandomForce()
    {
        var force = (forceApplyEndPoint.position - forceApplyBeginPoint.position).normalized;
        force += Vector3.up * forceYMagnitude;
        force *= forceScaler;

        Debug.Log("Apply force: " + force);
        for (int i = 0; i < coinGroup.transform.childCount; i++)
        {
            var c = coinGroup.transform.GetChild(i);
            var rb = c.GetComponent<Rigidbody>();
            if (rb != null)
            {
                var dist = (c.transform.position - forceApplyBeginPoint.position).magnitude;
                
                rb.AddForceAtPosition(force / dist, forceApplyBeginPoint.position);
            }
            
        }
    }
}
