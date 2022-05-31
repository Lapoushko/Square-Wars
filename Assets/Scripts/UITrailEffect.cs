using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITrailEffect : MonoBehaviour
{
    public float lerpTime;

    float minDst = 1f;
    bool goend = false;
    Vector3 startPos;
    Vector3 endPos;
    private void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(startPos.x+7f, startPos.y, startPos.z);
    }

    void Update()
    {
        if (goend)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, lerpTime * Time.deltaTime);
            if (transform.position == endPos)
            {
                goend = !goend;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, lerpTime * Time.deltaTime);
            if (transform.position == startPos)
            {
                endPos = new Vector3(transform.position.x+7f, transform.position.y, transform.position.z);
                goend = !goend;
            }
        }
    }
}
