using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnMenu : MonoBehaviour
{
    public float lerpTime;

    float minDst = 0.01f;
    bool goend = false;
    Vector3 startPos;
    Vector3 endPos;
    public float t = 0f;
    private void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(transform.position.x, transform.position.y - Random.Range(-10f, 10f), transform.position.z);
    }

    void Update()
    {
        if (goend)
        {
            transform.position = Vector3.Lerp(transform.position, endPos, lerpTime * Time.deltaTime);
            if (Vector3.Distance(transform.position,endPos)< minDst)
            {
                lerpTime = Random.Range(4f, 7f);
                goend =! goend;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startPos, lerpTime * Time.deltaTime);
            if (Vector3.Distance(transform.position, startPos) < minDst)
            {
                endPos = new Vector3(transform.position.x, transform.position.y - Random.Range(-10f, 10f), transform.position.z);
                lerpTime = Random.Range(4f, 7f);
                goend = !goend;
            }
        }
    }
}
