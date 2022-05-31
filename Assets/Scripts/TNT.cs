using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    SpawnController spawnController;
    GameObject controller;
    public GameObject DeathTNT;
    GameObject player;

    [Header("Camera")]
    MoveCamera camShake;
    float camShakeAmt = 0.3f;

    [Header("Spawn")]
    public Vector2 desiredScale;
    public float scaleSpeed;
    private Vector2 totalScaleVel;

    [Header("explosion")]
    public float force;
    public float fieldofimpact;
    public LayerMask LayerToHit;

    void Start()
    {
        camShake = GameObject.Find("Main Camera").GetComponent<MoveCamera>();
        controller = GameObject.Find("Controller");
        spawnController = controller.GetComponent<SpawnController>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        this.transform.localScale = (Vector3)Vector2.SmoothDamp((Vector2)this.transform.localScale, this.desiredScale, ref this.totalScaleVel, this.scaleSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Bullet":
                {
                    explosion();
                    break;
                }
            case "BulletEnemy":
                {
                    explosion();
                    break;
                }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "lava":
                Boom();
                break;
        }
    }

    public void Boom()
    {
        DeathTNT.transform.position = gameObject.transform.position;
        Instantiate(DeathTNT);
        AudioManager.instance.Play("Explosion");
        spawnController.CountTnt -= 1;
        camShake.Shake(camShakeAmt, 0.15f);
        Destroy(gameObject);
    }

    void explosion()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldofimpact, LayerToHit);
        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
            if (obj.tag == "Player")
            {
                player.GetComponent<MovePlayer>().health -= 20;
            }
        }
        Boom();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldofimpact);
    }
}
