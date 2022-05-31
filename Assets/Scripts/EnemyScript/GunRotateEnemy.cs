using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotateEnemy : MonoBehaviour
{
    public GameObject bullet;
    public Transform shotPoint;
    SpriteRenderer sr;
    int coefFlipGun = 1;

    [Header("Player")]
    Transform player;
    Vector2 target;

    string gun;
    MovePlayer playerScript;

    [Header("Property")]
    public float lifetime;
    public float speedBullet;
    public int DamageBullet;
    public float timeBtwShots;
    public float startTimeBtwShots;
    public float sizeBullet;

    [Header("ShootAmmo")]
    public int maxAmmo;
    private int Ammo;
    public float timeReload;

    [Header("Shotgun")]
    public int countShot;
    public float radius;
    public bool shotgun;

    public bool rifle;
    //public GameObject weapon;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        //sr.enabled = false;
    }

    void Update()
    {
        if (player)
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

            Vector3 difference = player.position - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, transform.position.z + rotZ);
            //recoil
            if (timeBtwShots <= 0)
            {
                //if (Input.GetMouseButton(0))

                if (rifle == true)
                {
                    Instantiate(bullet, shotPoint.position, transform.rotation);
                    timeBtwShots = startTimeBtwShots;
                    AudioManager.instance.Play("FireGun");
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
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }
}
