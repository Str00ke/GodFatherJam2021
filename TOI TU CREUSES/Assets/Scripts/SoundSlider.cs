using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public AudioSource music;
    public Slider slider;

    public void volumeChange()
    {
        music.volume = slider.value;
    }
}
