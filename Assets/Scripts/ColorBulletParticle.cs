using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBulletParticle : MonoBehaviour
{
    private Color col;
    public float size;
    void Start()
    {
        var ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        var mainSize = ps.main;
        //size = Random.Range(0.4f, 0.7f);
        main.startColor = GameObject.Find("Player").GetComponent<SpriteRenderer>().color; 
        //mainSize.startSize = GameObject.FindGameObjectWithTag("Gun").GetComponent<GunRotate>().sizeBullet * size;
    }
}
