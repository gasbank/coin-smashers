using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class BoardPhysics : MonoBehaviour
{
    public GameObject coinGroup;
    public Transform forceApplyPoint;
    public float forceXZScaler = 10.0f;
	public float forceYScaler = 10.0f;
	public float period = 10.0f;
    public void ApplyRandomForce()
    {
        //var force = (forceApplyEndPoint.position - forceApplyBeginPoint.position).normalized;
        //force += Vector3.up * forceYMagnitude;
        //force *= forceScaler;

        //Debug.Log("Apply force: " + force);
        for (int i = 0; i < coinGroup.transform.childCount; i++)
        {
            var c = coinGroup.transform.GetChild(i);
            var rb = c.GetComponent<Rigidbody>();
            if (rb != null)
            {
				var distanceXZ = c.transform.position - forceApplyPoint.position;
				distanceXZ = new Vector3(distanceXZ.x, 0, distanceXZ.z);
				var forceXZ = distanceXZ.normalized * forceXZScaler;
				var forceY = Vector3.up * forceYScaler;
				var distanceXZMag = distanceXZ.magnitude;
				var force = (forceXZ + forceY) * GetDistanceForceMultiplier(distanceXZMag);
                rb.AddForceAtPosition(force, forceApplyPoint.position);
            }
            
        }
    }

	float GetDistanceForceMultiplier(float distance)
	{
		if (distance > period / 4.0f)
		{
			return 0.0f;
		}
		else
		{
			return Mathf.Max(Mathf.Cos(distance * 2 * Mathf.PI / period), 0.0f);
		}
	}

	public void ReloadScene()
	{
		Application.LoadLevel(0);
	}

	public void Test(BaseEventData eventData)
	{
		PointerEventData ped = eventData as PointerEventData;
		Debug.Log(ped);
		Debug.Log("hehe");
		forceApplyPoint.position = ped.pointerPressRaycast.worldPosition;
		ApplyRandomForce();
		//Debug.Log("WP: " + ped.worldPosition);
	}

	public void SetPower(float v)
	{
		forceXZScaler = v;
		forceYScaler = v;
	}
}
