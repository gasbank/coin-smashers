using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class BoardPhysics : MonoBehaviour
{
    public CoinGroup coinGroup;
    public Transform forceApplyPoint;
    public float forceXZScaler = 10.0f;
    public float forceYScaler = 10.0f;
    public float torqueScaler = 10.0f;
    public float period = 10.0f;
    public float distanceScaleRandomMin = 0.9f;
    public float distanceScaleRandomMax = 1.1f;
    public Text headTailText;
    public Slider powerSlider;
    public Slider lastPowerSlider;

    private int heads;
    private int tails;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        UpdateHeadTail();
    }

    void UpdateHeadTail()
    {
        var newHeads = 0;
        var newTails = 0;
        for (int i = 0; i < coinGroup.coins.Count; i++)
        {
            var coin = coinGroup.coins[i];
            if (coin.isActiveAndEnabled)
            {
                var d = Vector3.Dot(-coin.transform.forward, Vector3.up);
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
        for (int i = 0; i < coinGroup.coins.Count; i++)
        {
            var coin = coinGroup.coins[i];
            if (coin.contacted)
            {
                var distanceXZ = coin.transform.position - forceApplyPoint.position;
                distanceXZ = new Vector3(distanceXZ.x, 0, distanceXZ.z);
                var distanceXZMag = distanceXZ.magnitude * Random.Range(distanceScaleRandomMin, distanceScaleRandomMax);
                var distanceCoeff = GetDistanceForceMultiplier(distanceXZMag);
                var powerCoeff = powerSlider.value;

                // 위로 튀어오르는 힘
                coin.rb.AddForce(Vector3.up * forceYScaler * distanceCoeff * powerCoeff, ForceMode.Force);

                // 회전 속도 제한 해제
                coin.rb.maxAngularVelocity = Mathf.Infinity;

                // 회전하는 힘 (힘 원점에서 동전 위치까지의 벡터와 평면상에서 수직이 되는 방향의 토크)
                var coinToGz = forceApplyPoint.position - coin.transform.position;
                coin.rb.AddTorque(Vector3.Cross(coinToGz, Vector3.up).normalized * torqueScaler * distanceCoeff * powerCoeff, ForceMode.Force);
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

    public void OnHitBoard(BaseEventData eventData)
    {
        PointerEventData ped = eventData as PointerEventData;
        Debug.Log(ped);
        forceApplyPoint.position = ped.pointerPressRaycast.worldPosition;
        ApplyRandomForce();
        
        lastPowerSlider.value = powerSlider.value;
    }

    public void SetPower(float v)
    {
        forceXZScaler = v;
        forceYScaler = v;
    }
}
