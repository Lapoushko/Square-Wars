using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomEffect : MonoBehaviour
{
    [Header("ZoomEffect")]
    public float speed;
    public Vector3[] target = new Vector3[1];
    public Camera mainCam;
    public bool ZoomActive;
    public float minSize;
    public float maxSize;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (ZoomActive == false)
        {
            mainCam.orthographicSize = Mathf.Lerp(mainCam.orthographicSize, maxSize, speed);
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, target[0], speed);
        }
        else
        {
            mainCam.orthographicSize = Mathf.Lerp(mainCam.orthographicSize, minSize, speed);
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, target[1], speed);
        }
    }
}
