using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    Camera mainCam;
    public float shakeAmount;

    private void Start()
    {
        mainCam = Camera.main;
        mainCam.orthographicSize = GameObject.Find("ZoomEffect").GetComponent<ZoomEffect>().minSize;
    }

    public void Shake(float amt, float length)
    {
        shakeAmount = amt;
        InvokeRepeating("DoShake", 0, 01f);
        Invoke("StopShake", length);
    }

    public void DoShake()
    {
        if (shakeAmount > 0)
        {
            Vector3 camPos = mainCam.transform.position;

            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
            camPos.x += offsetX;
            camPos.y += offsetY;

            mainCam.transform.position = camPos;
        }
    }

    public void StopShake()
    {
        CancelInvoke("DoShake");
        mainCam.transform.position = new Vector3(0f,0f,-10f);
    }
}
