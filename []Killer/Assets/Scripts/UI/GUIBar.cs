using UnityEngine;
using UnityEngine.UI;

public class GUIBar : MonoBehaviour {
    public Slider slider;

    public void SetMaxValue (int value) {
        slider.maxValue = value;
    }

    public void SetValue (int value) {
        slider.value = value;
    }

    public bool IsMaxValue () {
        return slider.value == slider.maxValue;
    }

}
