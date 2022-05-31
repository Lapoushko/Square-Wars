using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoom : MonoBehaviour
{
    [Header("Spawn")]
    public Vector2 desiredScale;
    public float scaleSpeed;
    private Vector2 totalScaleVel;

    [Header("explosion")]
    public float force;
    public float fieldofimpact;
    public LayerMask LayerToHit;

    [Header("Speed")]
    public float speedx;
    
    [Header("Jump")]
    public float Jumpforce;
    public LayerMask WhatIsGround;
    public Transform GroundCheck;
    public float groundRadius;
    public bool jump;
    public float timeInAir;
    public float timeForInstatiateParticleInAir;

    public int health;
    public float damagePlayer;
    public GameObject Player;
    Rigidbody2D rb;
    public int pointKill;
    public SpriteRenderer col;

    [Header("Controllers")]
    SpawnController spawnController;
    GameObject controller;
    ScoreController scr;
    ControllerInGame controllerInGame;

    bool fireReady;
    bool SetDamage;


    [Header("CamShake")]
    MoveCamera camShake;
    public float camShakeAmt = 0.3f;

    [Header("Particles")]
    public GameObject Deathlava;
    public GameObject FallGround;
    public GameObject JumpParticle;
    public GameObject Death;
    public GameObject ParticleScore;
    public GameObject SpawnParticle;

    private void Start()
    {
        col = GetComponent<SpriteRenderer>();
        SpawnParticle.transform.position = transform.position;
        SpawnParticle.GetComponent<ParticleSystem>().startColor = col.color;
        // damagePlayer = PlayerPrefs.GetFloat("DamageWeapon");
        SetDamage = false;
        Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        controller = GameObject.Find("Controller");
        spawnController = controller.GetComponent<SpawnController>();
        scr = controller.GetComponent<ScoreController>();
        controllerInGame = controller.GetComponent<ControllerInGame>();
        camShake = GameObject.Find("Main Camera").GetComponent<MoveCamera>();
    }
    private void Update()
    {
        Move();
        this.transform.localScale = (Vector3)Vector2.SmoothDamp((Vector2)this.transform.localScale, this.desiredScale, ref this.totalScaleVel, this.scaleSpeed);
    }

    public void DamageWeapon()
    {
        damagePlayer = PlayerPrefs.GetFloat("DamageWeapon");
        SetDamage = true;
    }

    public void TakeDamage()
    {
        if (SetDamage == false) DamageWeapon();
        health -= (int)damagePlayer;
        if (health <= 0)
        {
            Death.transform.position = transform.position;
            Instantiate(Death);
            GameObject PointsPopup = Instantiate(ParticleScore, transform.position, Quaternion.identity) as GameObject;
            PointsPopup.transform.GetChild(0).GetComponent<TextMesh>().text = pointKill.ToString();
            spawnController.CountEx -= 1;
            DestroyEnemy();
            scr.AddScore(this.pointKill);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Bullet":
                {
                    TakeDamage();
                    break;
                }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "lava":
                Inlava();
                break;
            case "Player":
                DestroyEnemy();
                break;
        }
    }

    public void Inlava()
    {
        Deathlava.transform.position = gameObject.transform.position;
        Instantiate(Deathlava);
        AudioManager.instance.Play("InLava");
        spawnController.CountEx -= 1;
        Destroy(gameObject);
    }

    public void Move()
    {
        if (Player)
        {
            if (controllerInGame.PlayerDeath == false)
            {
                if (Player.transform.position.x >= transform.position.x) rb.velocity = new Vector2(speedx, rb.velocity.y);
                else if (Player.transform.position.x <= transform.position.x) rb.velocity = new Vector2(-speedx, rb.velocity.y);
            }
            if ((Player.transform.position.y >= transform.position.y + 1.5f) && (jump == true))
            {
                Jump();
                jump = false;
                this.Invoke("JumpCoolDown", 0.2f);
            }
        }
    }

    void DestroyEnemy()
    {
        camShake.Shake(camShakeAmt, 0.1f);
        explosion();
        //spawnController.CountD -= 1;
    }

    void explosion()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldofimpact, LayerToHit);
        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
        }
        Death.transform.position = transform.position;
        Instantiate(Death);
        AudioManager.instance.Play("Explosion");
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldofimpact);
    }
    public void Jump()
    {
        if (jump == true)
        {
            rb.AddForce(Vector2.up * Jumpforce, ForceMode2D.Impulse);
            JumpParticle.transform.position = transform.position;
            Instantiate(JumpParticle);
        }
    }

    void JumpCoolDown()
    {
        jump = true;
    }
}
