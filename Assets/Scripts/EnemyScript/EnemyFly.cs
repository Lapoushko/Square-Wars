using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{
    [Header("Spawn")]
    public Vector2 desiredScale;
    public float scaleSpeed;
    private Vector2 totalScaleVel;

    [Header("Speed")]
    public float speedx;
    public float forceUp;

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

    [Header("Particles")]
    public GameObject Deathlava;
    public GameObject FallGround;
    public GameObject JumpParticle;
    public GameObject Death;
    public GameObject ParticleScore;
    public GameObject SpawnParticle;

    private void Start()
    {
        // damagePlayer = PlayerPrefs.GetFloat("DamageWeapon");
        col = GetComponent<SpriteRenderer>();
        SpawnParticle.transform.position = transform.position;
        SpawnParticle.GetComponent<ParticleSystem>().startColor = col.color;
        SetDamage = false;
        Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        controller = GameObject.Find("Controller");
        spawnController = controller.GetComponent<SpawnController>();
        scr = controller.GetComponent<ScoreController>();
        controllerInGame = controller.GetComponent<ControllerInGame>();
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
            Destroy(gameObject);
            spawnController.CountFl -= 1;
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
        }
    }

    public void Inlava()
    {
        Deathlava.transform.position = gameObject.transform.position;
        Instantiate(Deathlava);
        AudioManager.instance.Play("InLava");
        spawnController.CountFl -= 1;
        Destroy(gameObject);
    }

    public void Move()
    {
        if (controllerInGame.PlayerDeath == false)
        {
            if (Player.transform.position.x >= transform.position.x + 2f) rb.velocity = new Vector2(speedx, rb.velocity.y);
            else if (Player.transform.position.x <= transform.position.x - 2f) rb.velocity = new Vector2(-speedx, rb.velocity.y);

            if (Player.transform.position.y < transform.position.y) rb.AddForce(Vector2.down * Time.deltaTime * forceUp);
            else if (Player.transform.position.y > transform.position.y ) rb.AddForce(Vector2.up * Time.deltaTime * forceUp);
        }

    }
}
