using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [Header("Color")]
    SpriteRenderer col;

    [Header("Health")]
    public int health;
    public int armor;
    public int DamageLava;
    public int healthAfterTakeDamage;
    public int TakeDamage;
    public int DamageExplosion;

    [Header("Move")]
    public float speedx;
    float speedUpgrades;
    //public bool faceRight;

    [Header("TransformPos")]
    public GameObject[] Position;

    [Header("Jump")]
    public float Jumpforce;
    public LayerMask WhatIsGround;
    public Transform GroundCheck;
    public float groundRadius;
    public bool jump;
    public float timeInAir;
    public float timeForInstatiateParticleInAir;

    [Header("Joystick")]
    Joystick joystick;
    [Range(-1, 1)]
    public float distanceJoystickX;
    [Range(-1, 1)]
    public float distanceJoystickY;
    float move;
    float verticalMove;

    [Header("PlatformDown")]
    public float waitTime;
    public float TimeForWait;
    bool OneKeyUp = true;

    [Header("Particles")]
    public GameObject Deathlava;
    public GameObject FallGround;
    public GameObject JumpParticle;

    [Header("CamShake")]
    MoveCamera camShake;
    float camShakeAmt = 0.1f;

    [Header("Weapon")]
    int WeaponMassive;
    GameObject child;
    string gun;
    public GameObject[] weapon;
    GunRotate weaponScript;

    [Header("Trails")]
    int TrailMassive;
    GameObject childTrail;
    string trail;
    public GameObject[] trails;
    public GameObject SetTrail;

    //[Header ("PropertyWeapon")]

    GameObject player;
    ControllerInGame controller;
    private int countDeath;
    //private PlatformEffector2D effector; 
    Rigidbody2D rb;
    void Start()
    {
        camShake = GameObject.Find("Main Camera").GetComponent<MoveCamera>();
        joystick = GameObject.Find("Floating Joystick").GetComponent<Joystick>();     
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        Position[0] = GameObject.Find("StartPosition");
        transform.position = Position[0].transform.position;
        col = GetComponent<SpriteRenderer>();
        ChangeWeapon(gun);
        ChangeTrail(trail);
        TakeDamage = GameObject.FindWithTag("GunEnemy").GetComponent<GunRotateEnemy>().DamageBullet;
        controller = GameObject.Find("Controller").GetComponent<ControllerInGame>();
        countDeath = PlayerPrefs.GetInt("CountDeath");
        armor = PlayerPrefs.GetInt("countUpgradesArmor");
        speedUpgrades = PlayerPrefs.GetInt("countUpgradesSpeed")/2;
        speedx = speedx + (int)speedUpgrades*0.85f;
        //effector = GameObject.Find("PlatformOneWay").GetComponent<PlatformEffector2D>();
    }
    
    void FixedUpdate()
    {
        //float move = Input.GetAxis("Horizontal");        
        Move();
        if((timeInAir >= timeForInstatiateParticleInAir) && (jump == true))
        {
            FallGround.transform.position = new Vector3(transform.position.x,transform.position.y - 0.5f,transform.position.z);
            Instantiate(FallGround);
            AudioManager.instance.Play("FallDown");
            camShake.Shake(camShakeAmt, 0.1f);
        }
        //if ((faceRight == false) && (move < 0)) Flip(); //поворот персонажа 
        //else if ((faceRight == true) && (move > 0)) Flip(); //поворот персонажа

        healthAfterTakeDamage = TakeDamage / health;
        jump = Physics2D.OverlapCircle(GroundCheck.position, groundRadius, WhatIsGround);
        if (Input.GetKeyDown(KeyCode.UpArrow) && jump)  Jump();
        if(jump == true) timeInAir = 0; else timeInAir += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "lava":
                Inlava();
                break;
            case "EnemyBoom":
                TakeDamageVoid(DamageExplosion);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "BulletEnemy":
                TakeDamageVoid(TakeDamage);
                break;
        }        
    }

    public void Jump()
    {
        if (jump == true)
        {
            rb.AddForce(Vector2.up * Jumpforce, ForceMode2D.Impulse);
            JumpParticle.transform.position = transform.position;
            Instantiate(JumpParticle);
            AudioManager.instance.Play("Jump");
        }
    }

    public void Inlava()
    {
        Destroy(childTrail);
        Deathlava.transform.position = gameObject.transform.position;
        Instantiate(Deathlava);
        AudioManager.instance.Play("InLava");
        TakeDamage = DamageLava;
        health -= DamageLava - armor;
        if (health <= 0)
        {
            controller.OnDeathPanel();
            controller.PlayerDeath = true;
            //controller.OnApplicationPause(true);
            PlayerPrefs.SetInt("CountDeath", countDeath + 1);
            Destroy(gameObject);
        }
        col.color = new Color(0f, 0.55f * 0.01f * health, 1 * 0.01f * health);
        if (col.color.b <= 0.4f)
        {
            col.color = new Color(0f, 0.55f * 0.01f * health, 0.4f);
        }
        //gameObject.transform.position = Position[Random.Range(0, 3)].transform.position;
        gameObject.transform.position = Position[0].transform.position;
        SpawnTrail();
    }

    public void Move()
    {
        move = joystick.Horizontal;
        if (move >= distanceJoystickX)
        {
            rb.velocity = new Vector2(move * speedx, rb.velocity.y);
        }
        else if (move <= -distanceJoystickX)
        {
            rb.velocity = new Vector2(move * speedx, rb.velocity.y);
        }

        verticalMove = joystick.Vertical;

        jump = Physics2D.OverlapCircle(GroundCheck.position, groundRadius, WhatIsGround);
        if (verticalMove >= distanceJoystickY && jump)
        {
            Jump();
            //effector.rotationalOffset = 0f;
        }
        if (verticalMove <= -distanceJoystickY && jump)
        {
            if (OneKeyUp == true)
            {
                waitTime = TimeForWait;
                OneKeyUp = false;
            }
            if (waitTime <= 0)
            {
                //effector.rotationalOffset = 180f;
                waitTime = TimeForWait;
                OneKeyUp = true;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }

        }
    }

    public void TakeDamageVoid(int damage)
    {
        health = health - (damage - armor);
        if (health <= 0)
        {
            controller.OnDeathPanel();
            controller.PlayerDeath = true;
            //controller.OnApplicationPause(true);
            PlayerPrefs.SetInt("CountDeath", countDeath + 1);
            Destroy(gameObject);
        }
        col.color = new Color(0f,0.55f*0.01f*health, 1 * 0.01f * health);
        if (col.color.b <= 0.4f)
        {
            col.color = new Color(0f, 0.55f * 0.01f * health, 0.4f);
        }
        //Debug.Log(col.color.g * 255);
    }

    public void ChangeWeapon(string changeWeapon)
    {
        changeWeapon = PlayerPrefs.GetString("weapon");
        switch (changeWeapon)
        {
            case "ak-47":
                WeaponMassive = 0;
                break;
            case "AR-15":
                WeaponMassive = 1;
                break;
            case "fn-scar-h":
                WeaponMassive = 2;
                break;
            case "Norinco-shotgun":
                WeaponMassive = 3;
                break;
            case "Sawed-off":
                WeaponMassive = 4;
                break;
            case "UTS-15":
                WeaponMassive = 5;
                break;
            case "L115A3":
                WeaponMassive = 6;
                break;
            case "Sniper-rifle":
                WeaponMassive = 7;
                break;
            case "M2010":
                WeaponMassive = 8;
                break;

        }
        child = Instantiate(weapon[WeaponMassive]);
        child.transform.parent = player.transform;
        child.transform.localScale = weapon[WeaponMassive].transform.localScale;
        child.transform.localPosition = weapon[WeaponMassive].transform.localPosition;
        gun = changeWeapon;
        this.weaponScript = child.GetComponent<GunRotate>();
    }

    public void Fire()
    {
        this.rb.AddForce((Vector2)(-this.child.transform.right * this.weaponScript.recoilBehind*10f));
    }

    public void ChangeTrail(string changeTrail)
    {
        changeTrail = PlayerPrefs.GetString("Trail");
        switch (changeTrail)
        {
            case "TrailBase":
                TrailMassive = 0;
                break;
            case "TrailRainbow":
                TrailMassive = 1;
                break;
            case "TrailWind":
                TrailMassive = 2;
                break;

        }
        SpawnTrail();
        trail = changeTrail;
        SetTrail = childTrail;
    }

    void SpawnTrail()
    {
        childTrail = Instantiate(trails[TrailMassive]);
        childTrail.transform.parent = player.transform;
        childTrail.transform.localScale = trails[TrailMassive].transform.localScale;
        childTrail.transform.localPosition = trails[TrailMassive].transform.localPosition;
    }
}
