using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public bool CanSpawn;
    public float alltime;

    [Header ("DeagleEnemy")]
    public float dTimer;
    public float SpawnD;
    public int CountD;
    public int maxTypeEnemyD;
    public GameObject DesertEnemy;

    [Header ("BigEnemy")]
    public float bTimer;
    public float SpawnB;
    public GameObject BigEnemy;
//CountB = CountD они одинаковы

    [Header("EnemyFly")]
    public float FlTimer;
    public float SpawnFl;
    public GameObject FlEnemy;
    public int CountFl;
    public int maxTypeEnemyFl;


    [Header("EnemyExplosion")]
    public float exTimer;
    public float SpawnEx;
    public GameObject ExEnemy;
    public int CountEx;
    public int maxTypeEnemyEx;

    [Header("TNT")]
    public GameObject TNT;
    public int CountTnt;
    public int maxTypeTNT;
    public float TNTTimer;
    public float SpawnTnt;

    public LayerMask raycast;

    void Update()
    {
        alltime += Time.deltaTime;
        dTimer += Time.deltaTime;
        bTimer += Time.deltaTime;
        exTimer += Time.deltaTime;
        FlTimer += Time.deltaTime;
        TNTTimer += Time.deltaTime;

        if (CanSpawn == true)
        {
            if (CountD < maxTypeEnemyD)
            {
                if (dTimer >= Random.Range(SpawnD - 1.5f, SpawnD + 1.5f)) { this.SpawnDeagleEnemy(); dTimer = 0f; CountD += 1; }
                if (bTimer >= Random.Range(SpawnB - 1.5f, SpawnB + 1.5f)) { this.SpawnBigEnemy(); bTimer = 0f; CountD += 1; }
            }

            if (CountEx < maxTypeEnemyEx)
            {
                if (exTimer >= Random.Range(SpawnEx - 1.5f, SpawnEx + 1.5f)) { this.SpawnBoomEnemy(); exTimer = 0f; CountEx += 1; }
            }

            if (CountFl < maxTypeEnemyFl)
            {
                if (FlTimer >= Random.Range(SpawnFl - 1.5f, SpawnFl + 1.5f)) { this.SpawnFlyEnemy(); FlTimer = 0f; CountFl += 1; }
            }

            if (CountTnt < maxTypeTNT)
            {
                if (TNTTimer >= Random.Range(SpawnTnt - 1.5f, SpawnTnt + 1.5f)) { this.SpawnTNT(); TNTTimer = 0f; CountTnt += 1; }
            }
        }
    }

    public void SpawnFlyEnemy()
    {
        SpawnOneBoi(FlEnemy);
    }

    public void SpawnDeagleEnemy()
    {
        SpawnOneBoi(DesertEnemy);
    }

    public void SpawnBigEnemy()
    {
        SpawnOneBoi(BigEnemy);
    }

    public void SpawnBoomEnemy()
    {
        SpawnOneBoi(ExEnemy);
    }   

    public void SpawnTNT()
    {
        SpawnOneTNT(TNT);
    }

    private void SpawnOneBoi(GameObject boiToSpawn)
    {
        Vector2 origin = new Vector2(Random.Range(-9f, 9f), 0f);
        RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, Vector2.down, 50f, (int)this.raycast);
        if ((Object)raycastHit2D.collider != (Object)null && raycastHit2D.collider.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            this.SpawnOneBoi(boiToSpawn);
            return;
        }
        Object.Instantiate<GameObject>(boiToSpawn, (Vector3)origin, this.transform.rotation);
    }

    private void SpawnOneTNT(GameObject TNT)
    {
        Vector2 origin = new Vector2(Random.Range(-9f, 9f), 15f);
        RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, Vector2.down, 50f, (int)this.raycast);
        if ((Object)raycastHit2D.collider != (Object)null && raycastHit2D.collider.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            this.SpawnOneTNT(TNT);
            return;
        }
        Object.Instantiate<GameObject>(TNT, (Vector3)origin, this.transform.rotation);
    }
}
