using System.Collections;
using UnityEngine;

public class GunRotate : MonoBehaviour
{
    public float offset;
    public GameObject bullet;
    public Transform shotPoint;
    SpriteRenderer sr;
    int coefFlipGun = 1;

    string gun;
    MovePlayer playerScript;

    [Header("Property")]
    public float lifetime;
    public float speedBullet;
    public float DamageBullet;
    public float timeBtwShots;
    public float startTimeBtwShots;
    public float sizeBullet;

    [Header("ShootAmmo")]
    public int maxAmmo;
    public int Ammo;
    public float timeReload;
    public bool CanReload;
    public float recoilBehind;
    public float recoil;
    public float endRecoil;

    [Header("Shotgun")]
    public int countShot;
    public float radius;
    public bool shotgun;

    [Header("Joystick")]
    Joystick joystick;
    [Range(-1, 1)]
    float distanceJoystickX = 0.2f;
    [Range(-1, 1)]
    float distanceJoystickY = 0.2f;

    [Header("AmmunUI")]
    AmmunUI ammunUI;
    //public GameObject weapon;

    private void Awake()
    {
        //joystick = GameObject.Find("WeaponJoystick").GetComponent<Joystick>();
        //PlayerPrefs.SetFloat("DamageWeapon", DamageBullet);
    }

    private void Start()
    {
        Ammo = maxAmmo;
        joystick = GameObject.Find("WeaponJoystick").GetComponent<Joystick>();
        PlayerPrefs.SetFloat("DamageWeapon", DamageBullet);
        playerScript = GameObject.Find("Player").GetComponent<MovePlayer>();
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false; 
        if (CanReload)
        {
            ammunUI = GameObject.Find("AmmunationBar").GetComponent<AmmunUI>();
            ammunUI.SetMaxAm(maxAmmo);
        }
        else GameObject.Find("AmmunationBar").SetActive(false);
    }

    void Update()
    {
        float z = this.transform.rotation.eulerAngles.z;
        if (z < 270 && z > 90)
        {
            this.sr.flipY = true;
            coefFlipGun = -1;
            shotPoint.transform.localPosition = new Vector2(shotPoint.transform.localPosition.x, -shotPoint.transform.localPosition.y);
        }
        else
        {
            this.sr.flipY = false;
            coefFlipGun = 1;
            shotPoint.transform.localPosition = new Vector2(shotPoint.transform.localPosition.x, shotPoint.transform.localPosition.y);
        }
        Vector3 difference = new Vector3(joystick.Horizontal, joystick.Vertical, 0f);
        //Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;           
        if (offset > 0.1f)
        {
            offset -= endRecoil;
            
        }
        
        else if (offset < -0.1f)
        {
            offset += endRecoil;
            
        }        
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        if (timeBtwShots <= 0)
        {
            
            //if (Input.GetMouseButton(0))
            if ((difference.x >= distanceJoystickX) || (difference.x <= -distanceJoystickX) || (difference.y >= distanceJoystickY) || (difference.y <= -distanceJoystickY))
            {               
                if (CanReload == true)
                {
                    offset += recoil * coefFlipGun;
                    Instantiate(bullet, shotPoint.transform.position, transform.rotation);                   
                    timeBtwShots = startTimeBtwShots;
                    Ammo -= 1;                   
                    AudioManager.instance.Play("FireGun");
                    ammunUI.SetAm(Ammo);
                    if (Ammo <= 0)
                    {
                        timeBtwShots = timeReload;
                        StartCoroutine("AmmoSet");
                    }
                }
                else if (shotgun == true)
                {
                    Vector3 randomRotation = Random.insideUnitSphere;
                    randomRotation *= radius;
                    for (int i = 1; i <= countShot; i++)
                    {
                        Instantiate(bullet, shotPoint.position, transform.rotation * Quaternion.Euler(0f, 0f, Random.Range(-radius, radius)));
                    }
                    timeBtwShots = startTimeBtwShots;
                    AudioManager.instance.Play("FireGun");
                    this.playerScript.Fire();
                }

                else
                {
                    Instantiate(bullet, shotPoint.position, transform.rotation);
                    timeBtwShots = startTimeBtwShots;
                    AudioManager.instance.Play("FireGun");
                    this.playerScript.Fire();
                }
            }    
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        if ((difference.x >= distanceJoystickX) || (difference.x <= -distanceJoystickX) || (difference.y >= distanceJoystickY) || (difference.y <= -distanceJoystickY)) sr.enabled = true; else sr.enabled = false ; //отображение оружия
    }

     private IEnumerator AmmoSet()
    {
        yield return new WaitForSeconds(timeReload);
        Ammo = maxAmmo;
        ammunUI.SetAm(Ammo);
    }
}
