using UnityEngine;
using UnityEngine.UI;

public class SpeedManger : MonoBehaviour
{
    private Slider timestepSlider;

    private void Start()
    {
        timestepSlider = GetComponent<Slider>();
        timestepSlider.onValueChanged.AddListener(OnSliderValueChanged);
        OnSliderValueChanged(timestepSlider.value); // apply initial value
    }

    private void OnSliderValueChanged(float value)
    {
        Time.fixedDeltaTime = value;

    }
}
