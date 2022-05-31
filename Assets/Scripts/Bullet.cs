using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private float damage;
    public float timelife;
    private Color col;
    public LayerMask whatIsLayer;
    private float size;
    public GameObject ParticleDestroy;
    public float moneyDamage;

    [Header("CamShake")]
    MoveCamera camShake;
    public float camShakeAmt = 0.1f;

    void Start()
    {
        timelife = GameObject.FindGameObjectWithTag("Gun").GetComponent<GunRotate>().lifetime;
        speed = GameObject.FindGameObjectWithTag("Gun").GetComponent<GunRotate>().speedBullet;
        damage = GameObject.FindGameObjectWithTag("Gun").GetComponent<GunRotate>().DamageBullet;
        col = GameObject.Find("Player").GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = col;
        size = GameObject.FindGameObjectWithTag("Gun").GetComponent<GunRotate>().sizeBullet;
        transform.localScale = new Vector3(transform.localScale.x * size, transform.localScale.y * size, 1);
        camShake = GameObject.Find("Main Camera").GetComponent<MoveCamera>();
    }

    public void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, timelife, whatIsLayer);
        if (hitInfo.collider == true)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        timelife -= Time.deltaTime;
        if (timelife <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Ground":
                DestroyBullet();
                break;
            case "lava":
                DestroyBullet();
                break;
            case "Enemy":
                DestroyBullet();
                break;
        }
    }

    void DestroyBullet()
    {
        ParticleDestroy.transform.position = gameObject.transform.position;
        Instantiate(ParticleDestroy);
        Destroy(gameObject);
        camShake.Shake(camShakeAmt,0.1f);
        AudioManager.instance.Play("DestroyBullet");
    }

}
