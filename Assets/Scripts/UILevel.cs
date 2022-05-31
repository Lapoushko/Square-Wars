using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
    public Slider slider;


    public void SetMaxAm(float exp)
    {
        slider.maxValue = exp;
        slider.value = exp;
    }

    public void SetAm(float exp)
    {
        slider.value = exp;
    }

    public void SetSmoothAm(float exp, float maxExp, float time)
    {
        float velocity = 1f;
        slider.value = Mathf.MoveTowards(exp, maxExp,  time*Time.deltaTime);
    }
}
