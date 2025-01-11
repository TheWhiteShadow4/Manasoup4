using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string mixerGroup;
    public Slider slider;

    private float floatValue;

    void Start()
    {
        audioMixer.GetFloat(mixerGroup, out floatValue);
        slider.value = floatValue + 80;
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(float newValue)
    {
        float shiftedValue = newValue - 80;
        audioMixer.SetFloat(mixerGroup, shiftedValue);
        PlayerPrefs.SetFloat(mixerGroup, shiftedValue);
    }

    public void ApplyPlayerPrefs()
    {
        audioMixer.SetFloat(mixerGroup, PlayerPrefs.GetFloat(mixerGroup, 0));
    }
}
