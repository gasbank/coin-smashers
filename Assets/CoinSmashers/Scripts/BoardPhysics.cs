using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoardPhysics : MonoBehaviour
{
    public GameObject coinGroup;
    public Transform forceApplyPoint;
    public float forceXZScaler = 10.0f;
	public float forceYScaler = 10.0f;
	public float period = 10.0f;
    public Text headTailText;
    private int heads;
    private int tails;
    
    void Update()
    {
        UpdateHeadTail();
    }
    
    void UpdateHeadTail()
    {
        var newHeads = 0;
        var newTails = 0;
        for (int i = 0; i < coinGroup.transform.childCount; i++)
        {
            var c = coinGroup.transform.GetChild(i);
            var rb = c.GetComponent<Rigidbody>();
            if (rb != null)
            {
                var d = Vector3.Dot(-rb.transform.forward, Vector3.up);
                if (d > 0)
                {
                    newHeads++;
                }
				else
                {
                    newTails++;
                }
            }
        }
        
        if (newHeads != heads || newTails != tails)
        {
            heads = newHeads;
            tails = newTails;
            
            headTailText.text = string.Format("HEADS: {0}\nTAILS: {1}", heads, tails);
        }
    }
    
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
				var distanceXZMag = distanceXZ.magnitude * Random.Range(0.9f, 1.1f);
				var force = (forceXZ + forceY) * GetDistanceForceMultiplier(distanceXZMag);
                rb.AddForceAtPosition(force, forceApplyPoint.position);
            }
        }
    }
    
	float GetDistanceForceMultiplier(float distance)
	{
        var v = Mathf.Cos(distance * 2 * Mathf.PI / period);
        return Mathf.Max(v * v, 0.0f);
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
