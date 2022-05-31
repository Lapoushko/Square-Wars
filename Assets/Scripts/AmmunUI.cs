using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmunUI : MonoBehaviour
{
    public Slider slider;

    public void SetMaxAm(int ammo)
    {
        slider.maxValue = ammo;
        slider.value = ammo;
    }

    public void SetAm(int ammo)
    {
        slider.value = ammo;
    }

    public void SetSmoothAm(float ammo ,float maxAmmo, float time)
    {
        //float velocity = 1f;
        slider.value = Mathf.MoveTowards(ammo, maxAmmo,  time);
    }
}
