using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextDelete : MonoBehaviour
{
    public float DestroyTime;
    void Start()
    {
        Destroy(gameObject,DestroyTime);
        transform.localPosition += new Vector3(0f, 0.5f, 0f);
    }

}
