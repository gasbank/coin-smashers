using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class CoinSmashersCamera : MonoBehaviour
{
    public Slider slider;
    
    float lastSliderValue;
    
    public void RotateCamera()
    {
        var d = slider.value - lastSliderValue;
        lastSliderValue = slider.value;
        transform.RotateAround(Vector3.zero, -Vector3.right, d);
    }
}