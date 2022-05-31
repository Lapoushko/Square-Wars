using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LerpColor : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] float LerpTime;
    [SerializeField] Color[] myColors;

    Color txtColor;

    int colorIndex;
    float t = 0f;
    int len;

    private void Start()
    {
        txtColor = GetComponent<Text>().color;
        len = myColors.Length;
    }

    private void Update()
    {
        txtColor = Color.Lerp(txtColor, myColors[colorIndex], LerpTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, LerpTime * Time.deltaTime);
        if (t > .9f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= len) ? 0 : colorIndex;
        }
    }
}
