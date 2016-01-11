using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Slider))]
public class PowerSlider : MonoBehaviour
{
    public float speed = 1.0f;
    public float minValue = 0.1f;
    Slider slider;
    
    void Awake()
    {
        slider = GetComponent<Slider>();
    }
    
    void Update()
    {
        slider.value += speed * Time.deltaTime;
        
        if (slider.value >= slider.maxValue)
        {
            slider.value = minValue;
        }
    }
}