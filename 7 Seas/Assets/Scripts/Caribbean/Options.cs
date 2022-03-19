using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    void Start()
    {
        Slider slider = GameObject.Find("DifficultySlider").GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("DifficultySlider");
    }
}
