using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeClass : MonoBehaviour
{
    private bool SwitchClass;
    public bool MoveCam;
    public GameObject PanelChange;
    public string ChClass;
    public GameObject Player;
    // Start is called before the first frame update
    public void OnClickChange(string classes)
    {
        ChClass = classes;
        PanelChange.SetActive(false);
        MoveCam = true;
    }

    private void Update()
    {
        if (SwitchClass == true)
        {
            switch (ChClass)
            {
                case "archer":
                    SwitchClass = false;
                    break;
                case "wizzard":
                    SwitchClass = false;
                    Debug.Log("2");
                    break;
                case "warrior":
                    // gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    SwitchClass = false;
                    break;
            }
        }
    }

    public void Spawn()
    {
        switch (ChClass)
        {
            case "archer":
                var PlayerPrefabArcher = Resources.Load("Prefabs/Player_archer");
                var PlayerArcher = GameObject.Instantiate(PlayerPrefabArcher, transform.position, transform.rotation);
                break;
            case "wizzard":
                var PlayerPrefabWizzard = Resources.Load("Prefabs/Player_wizzard");
                var PlayerWizzard = GameObject.Instantiate(PlayerPrefabWizzard, transform.position, transform.rotation);
                Debug.Log("2");
                break;
            case "warrior":
                // gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                var PlayerPrefabWarrior = Resources.Load("Prefabs/Player_warrior");
                var PlayerWarrior = GameObject.Instantiate(PlayerPrefabWarrior, transform.position, transform.rotation);
                break;

        }
    }

}
