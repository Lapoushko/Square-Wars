using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerInMenu : MonoBehaviour
{
    [Header ("LVLBUY")]
    public int LvlWeapon;
    public int LVLplayer;
    public float AllMoney;
    public string PanelActive;

    [Header("TextElements")]
    public Text countPixels;
    public Text priceArmorText;
    public Text priceSpeedText;
    public Text LVLShop;
    public Text lvlArmor;
    public Text lvlSpeed;

    public GameObject PostProcessing;
    public bool SpecialBoolForDelete;
    public StatsController statscont;

    [Header("PanelMenu")]
    public GameObject MainMenu;
    public GameObject ShopWeapons;
    public GameObject settings;
    public GameObject Upgrades;
    public GameObject Stats;
    public GameObject Trails;

    [Header("UpgradesMenu")]
    public float coefPrice;
    //armor
    public int countUpgradesArmor = 1;
    public int OneUpArmor;
    public int allArmor;
    public float priceArmor;
    public float startPriceAr = 100f;
    //speed
    public int countUpgradesSpeed = 1;
    public float priceSpeed;
    public int oneUpSpeed;
    public int allSpeed;
    public float startPriceSp;

    [Header("ShopButtons")]
    public string SelectedMenu;
    public GameObject ContentAmmo;
    public GameObject ContentShotgun;
    public GameObject ContentSniper;
    public GameObject[] PlusUnderGun;
    public string SelectedWeaponShop;

    [Header("Trails")]
    public int highscoreForTrail;
    public float priceTrail;
    public Text HighscoreTrailText;
    public Text priceTrailText;
    public GameObject[] PlusUnderTrail;
    public GameObject[] crossTrail;

    [Header("AudioSource")]
    GameObject audio_;

    [Header("SpecialLevel")]
    [SerializeField] GameObject crossSpecialLevel;
    public int scoreForOpen;
    public Text scoreForOpenTxt;

    private void Start()
    {
        int intFirst = 0;
        PanelActive = "Menu";
        Application.targetFrameRate = 60;
        intFirst = PlayerPrefs.GetInt("FirstPlay");
        audio_ = GameObject.Find("AudioManager");
        Debug.Log(PlayerPrefs.GetInt("AudioManager"));
        
        if (intFirst == 0)
        {
            PlayerPrefs.SetInt("FirstPlay", 1);
            intFirst = 1;
            PlayerPrefs.SetInt("Lvl", 1);
            PlayerPrefs.SetInt("PostProcessActive", 1);
        }
        else if (intFirst == 1)
        {
            LVLplayer = PlayerPrefs.GetInt("Lvl");           
        }
        if (PlayerPrefs.GetInt("PostProcessActive") == 1) PostProcessing.SetActive(true); else PostProcessing.SetActive(false);
        AllMoney = PlayerPrefs.GetFloat("AllMoney");
        AudioManager.instance.Play("GameSound");
        SelectedWeaponShop = PlayerPrefs.GetString("weapon");
        CheckUpgrades();
        if (PlayerPrefs.GetInt("Highscore") >= highscoreForTrail) crossTrail[0].SetActive(false);
        if (PlayerPrefs.GetInt("TrailWind") == 1) crossTrail[1].SetActive(false);
        if (PlayerPrefs.GetInt("Highscore") >= scoreForOpen) crossSpecialLevel.SetActive(false);
    }

    private void Update()
    {
        UpdateText();
    }

    public void OnClickPlayMenu()
    {
        //string scene = "GameScene" + Random.Range(1,2).ToString();
        AudioManager.instance.Play("ButtonMenu");
        SceneManager.LoadScene(Random.Range(1,SceneManager.sceneCountInBuildSettings-1)) ;
    }

    public void OnClickShopWeapons()
    {
        MainMenu.SetActive(false);
        ShopWeapons.SetActive(true);
        PanelActive = "Shop";
        OnPlusUnderGun();
        AudioManager.instance.Play("ButtonMenu");        
    }
    public void OnClickBuyWeapon(string nameWeapon)
    {
        
        if (LvlWeapon <= LVLplayer)
        {
            PlayerPrefs.SetString("weapon", nameWeapon);
            SelectedWeaponShop = nameWeapon;
            OnPlusUnderGun();
        }
        else Debug.Log("LOH");
        AudioManager.instance.Play("ButtonMenu");
    }
    public void OnClickBuyWeaponLVL(int lvl)
    {
        LvlWeapon = lvl;
    }

    public void OnPlusUnderGun()
    {       
        switch (SelectedWeaponShop)
        {
            case "ak-47":
                PlusUnderGun[0].SetActive(false); PlusUnderGun[1].SetActive(false); PlusUnderGun[2].SetActive(false); PlusUnderGun[3].SetActive(false);
                PlusUnderGun[4].SetActive(false); PlusUnderGun[5].SetActive(false); PlusUnderGun[6].SetActive(false); PlusUnderGun[7].SetActive(false);
                PlusUnderGun[8].SetActive(false);
                PlusUnderGun[0].SetActive(true);
                break;
            case "fn-scar-h":
                PlusUnderGun[0].SetActive(false); PlusUnderGun[1].SetActive(false); PlusUnderGun[2].SetActive(false); PlusUnderGun[3].SetActive(false);
                PlusUnderGun[4].SetActive(false); PlusUnderGun[5].SetActive(false); PlusUnderGun[6].SetActive(false); PlusUnderGun[7].SetActive(false);
                PlusUnderGun[8].SetActive(false);
                PlusUnderGun[1].SetActive(true);
                break;
            case "AR-15":
                PlusUnderGun[0].SetActive(false); PlusUnderGun[1].SetActive(false); PlusUnderGun[2].SetActive(false); PlusUnderGun[3].SetActive(false);
                PlusUnderGun[4].SetActive(false); PlusUnderGun[5].SetActive(false); PlusUnderGun[6].SetActive(false); PlusUnderGun[7].SetActive(false);
                PlusUnderGun[8].SetActive(false);
                PlusUnderGun[2].SetActive(true);
                break;
            case "Norinco-shotgun":
                PlusUnderGun[0].SetActive(false); PlusUnderGun[1].SetActive(false); PlusUnderGun[2].SetActive(false); PlusUnderGun[3].SetActive(false);
                PlusUnderGun[4].SetActive(false); PlusUnderGun[5].SetActive(false); PlusUnderGun[6].SetActive(false); PlusUnderGun[7].SetActive(false);
                PlusUnderGun[8].SetActive(false);
                PlusUnderGun[3].SetActive(true);
                break;
            case "Sawed-off":
                PlusUnderGun[0].SetActive(false); PlusUnderGun[1].SetActive(false); PlusUnderGun[2].SetActive(false); PlusUnderGun[3].SetActive(false);
                PlusUnderGun[4].SetActive(false); PlusUnderGun[5].SetActive(false); PlusUnderGun[6].SetActive(false); PlusUnderGun[7].SetActive(false);
                PlusUnderGun[8].SetActive(false);
                PlusUnderGun[4].SetActive(true);
                break;
            case "UTS-15":
                PlusUnderGun[0].SetActive(false); PlusUnderGun[1].SetActive(false); PlusUnderGun[2].SetActive(false); PlusUnderGun[3].SetActive(false);
                PlusUnderGun[4].SetActive(false); PlusUnderGun[5].SetActive(false); PlusUnderGun[6].SetActive(false); PlusUnderGun[7].SetActive(false);
                PlusUnderGun[8].SetActive(false);
                PlusUnderGun[5].SetActive(true);
                break;
            case "L115A3":
                PlusUnderGun[0].SetActive(false); PlusUnderGun[1].SetActive(false); PlusUnderGun[2].SetActive(false); PlusUnderGun[3].SetActive(false);
                PlusUnderGun[4].SetActive(false); PlusUnderGun[5].SetActive(false); PlusUnderGun[6].SetActive(false); PlusUnderGun[7].SetActive(false);
                PlusUnderGun[8].SetActive(false);
                PlusUnderGun[7].SetActive(true);
                break;
            case "Sniper-rifle":
                PlusUnderGun[0].SetActive(false); PlusUnderGun[1].SetActive(false); PlusUnderGun[2].SetActive(false); PlusUnderGun[3].SetActive(false);
                PlusUnderGun[4].SetActive(false); PlusUnderGun[5].SetActive(false); PlusUnderGun[6].SetActive(false); PlusUnderGun[7].SetActive(false);
                PlusUnderGun[8].SetActive(false);
                PlusUnderGun[6].SetActive(true);
                break;
            case "M2010":
                PlusUnderGun[0].SetActive(false); PlusUnderGun[1].SetActive(false); PlusUnderGun[2].SetActive(false); PlusUnderGun[3].SetActive(false);
                PlusUnderGun[4].SetActive(false); PlusUnderGun[5].SetActive(false); PlusUnderGun[6].SetActive(false); PlusUnderGun[7].SetActive(false);
                PlusUnderGun[8].SetActive(false);
                PlusUnderGun[8].SetActive(true);
                break;
        }
    }

    public void OnClickBuyTrails(string trail)
    {        
        switch (trail)
            {
            case "TrailRainbow":
                if (PlayerPrefs.GetInt("Highscore") >= highscoreForTrail)
                {
                    PlayerPrefs.SetString("Trail", trail);
                    crossTrail[0].SetActive(false);
                }
                        
                PlusUnderTrail[1].SetActive(true); PlusUnderTrail[0].SetActive(false); PlusUnderTrail[2].SetActive(false);
                AudioManager.instance.Play("Upgrade");
                break;
            case "TrailBase":
                PlayerPrefs.SetString("Trail", trail);
                PlusUnderTrail[0].SetActive(true); PlusUnderTrail[1].SetActive(false); PlusUnderTrail[2].SetActive(false);
                AudioManager.instance.Play("Upgrade");
                break;
            case "TrailWind":
                if ((AllMoney >= priceTrail) || (PlayerPrefs.GetInt("TrailWind") == 1))
                {
                    if (PlayerPrefs.GetInt("TrailWind") == 0)
                    {
                        AllMoney -= priceTrail;
                        PlayerPrefs.SetInt("TrailWind", 1);
                    }
                    PlayerPrefs.SetFloat("AllMoney", AllMoney);
                    PlayerPrefs.SetString("Trail", trail);
                    crossTrail[1].SetActive(false);
                    PlusUnderTrail[2].SetActive(true); PlusUnderTrail[0].SetActive(false); PlusUnderTrail[1].SetActive(false);
                    AudioManager.instance.Play("Upgrade");
                    UpdateText();
                }
                break;
        }
        AudioManager.instance.Play("ButtonMenu");
    }

    public void OnClickSettings()
    {
        MainMenu.SetActive(false);
        settings.SetActive(true);
        PanelActive = "Settings";
        AudioManager.instance.Play("ButtonMenu");
    }

    public void OnClickSpLevel()
    {
        if (PlayerPrefs.GetInt("Highscore") >= scoreForOpen) SceneManager.LoadScene("GameScene4-Sp");
    }

    public void OnClickPrivacyPolicy()
    {
        Application.OpenURL("https://github.com/Lapoushko/privacy-policy/blob/master/README");
    }

    public void OnClickStats()
    {
        MainMenu.SetActive(false);
        Stats.SetActive(true);
        statscont.UpdateStats();
        PanelActive = "Stats";
        AudioManager.instance.Play("ButtonMenu");
    }

    public void OnClickTrails()
    {
        MainMenu.SetActive(false);
        Trails.SetActive(true);
        PanelActive = "Trails";
        AudioManager.instance.Play("ButtonMenu");
    }

    public void OnBack(string BackPanel)
    {
        if (BackPanel == "MenuShop")
        {
            MainMenu.SetActive(true);
            ShopWeapons.SetActive(false);
            PanelActive = "Menu";
        }

        if (BackPanel == "MenuSettings")
        {
            MainMenu.SetActive(true);
            settings.SetActive(false);
            PanelActive = "Menu";
        }

        if (BackPanel == "Upgrades")
        {
            MainMenu.SetActive(true);
            Upgrades.SetActive(false);
            PanelActive = "Menu";
        }

        if (BackPanel == "Stats")
        {
            MainMenu.SetActive(true);
            Stats.SetActive(false);
            PanelActive = "Menu";
        }

        if (BackPanel == "Trails")
        {
            MainMenu.SetActive(true);
            Trails.SetActive(false);
            PanelActive = "Menu";
        }
        AudioManager.instance.Play("ButtonMenu");
    }

    public void OnNextChangeShop(string ButtonNext)
    {
        switch (ButtonNext)
        {
            case "ContentAmmo":
                if (SelectedMenu == "ContentShotgun") { ContentShotgun.SetActive(false); } else if(SelectedMenu == "ContentSniper") {ContentSniper.SetActive(false); }
                ContentAmmo.SetActive(true);
                break;
            case "ContentShotgun":
                if (SelectedMenu == "ContentAmmo") { ContentAmmo.SetActive(false); } else if (SelectedMenu == "ContentSniper") { ContentSniper.SetActive(false); }
                ContentShotgun.SetActive(true);
                break;
            case "ContentSniper":
                if (SelectedMenu == "ContentShotgun") { ContentShotgun.SetActive(false); } else if (SelectedMenu == "ContentAmmo") { ContentAmmo.SetActive(false); }
                ContentSniper.SetActive(true);
                break;
        }
        SelectedMenu = ButtonNext;
        AudioManager.instance.Play("ButtonMenu");
    }

    void UpdateText()
    {
        countPixels.text = AllMoney.ToString();
        priceArmorText.text = priceArmor.ToString();
        priceSpeedText.text = priceSpeed.ToString();
        LVLShop.text = LVLplayer.ToString();
        lvlArmor.text = countUpgradesArmor.ToString();
        lvlSpeed.text = countUpgradesSpeed.ToString();
        priceTrailText.text = priceTrail.ToString();
        HighscoreTrailText.text = highscoreForTrail.ToString();
        scoreForOpenTxt.text = scoreForOpen.ToString();
    }

    public void OnClickSocial(string network)
    {
        switch (network)
        {
            case "inst":
                Application.OpenURL("https://www.instagram.com/lapoushkko/");
                break;
            case "vk":
                Application.OpenURL("https://vk.com/etoyadyadyavadya");
                break;
            case "instnico":
                Application.OpenURL("https://www.instagram.com/nico_staf/");
                break;
        }
    }

    public void DeleteSaveMoney()
    {
        AudioManager.instance.Play("ButtonMenu");
        PlayerPrefs.DeleteKey("AllMoney");
        PlayerPrefs.DeleteKey("Highscore");
        PlayerPrefs.DeleteKey("Allscore");
        PlayerPrefs.SetInt("Lvl",1);
        PlayerPrefs.DeleteKey("exp");
        PlayerPrefs.DeleteKey("expForLvl");
        PlayerPrefs.DeleteKey("CountDeath");
        PlayerPrefs.SetString("weapon", "ak47");
        PlayerPrefs.DeleteKey("Trail");
        PlayerPrefs.DeleteKey("TrailWind");
        PlayerPrefs.DeleteKey("countUpgradesArmor");
        PlayerPrefs.DeleteKey("countUpgradesSpeed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //time void
    public void GiveAllMoney()
    {
        AllMoney += 1000;
    }
    public void OnClickUpgradesMenu()
    {
        MainMenu.SetActive(false);
        Upgrades.SetActive(true);
        AudioManager.instance.Play("ButtonMenu");
    }

    public void OnClickQuit()
    {
        Application.Quit();
        AudioManager.instance.Play("ButtonMenu");
    }
    public void OnClickUpgrade(string button)
    {
        if (button == "Armor")
        {
            if (AllMoney >= priceArmor)
            {
                if (countUpgradesArmor <= 6)
                {
                    countUpgradesArmor += 1;
                    allArmor = OneUpArmor * countUpgradesArmor;
                    AllMoney -= priceArmor;
                    priceArmor = priceArmor * coefPrice;
                    priceArmor = Mathf.Round(priceArmor);
                    AudioManager.instance.Play("Upgrade");
                    PlayerPrefs.SetFloat("priceArmor", priceArmor);
                    PlayerPrefs.SetFloat("AllMoney", AllMoney);
                    PlayerPrefs.SetInt("AllArmor", allArmor);
                    PlayerPrefs.SetInt("countUpgradesArmor", countUpgradesArmor);
                }
            }
        }
        if (button == "Speed")
        {
            if (AllMoney >= priceSpeed)
            {
                if (countUpgradesSpeed <= 8)
                {
                    countUpgradesSpeed += 1;
                    allSpeed = oneUpSpeed * countUpgradesSpeed;
                    AllMoney -= priceSpeed;
                    priceSpeed = priceSpeed * coefPrice;
                    priceSpeed = Mathf.Round(priceSpeed);
                    AudioManager.instance.Play("Upgrade");
                    PlayerPrefs.SetFloat("priceSpeed", priceSpeed);
                    PlayerPrefs.SetFloat("AllMoney", AllMoney);
                    PlayerPrefs.SetInt("AllSpeed", allSpeed);
                    PlayerPrefs.SetInt("countUpgradesSpeed", countUpgradesSpeed);
                }
            }
        }
    }

    public void CheckUpgrades()
    {
        countUpgradesArmor = PlayerPrefs.GetInt("countUpgradesArmor");
        if (countUpgradesArmor == 0) priceArmor = startPriceAr; else priceArmor = PlayerPrefs.GetFloat("priceArmor");
        countUpgradesSpeed = PlayerPrefs.GetInt("countUpgradesSpeed");
        if (countUpgradesSpeed == 0) priceSpeed = startPriceSp; else priceSpeed = PlayerPrefs.GetFloat("priceSpeed");
    }

    //control buttons

    public void InvisFunction(string function)
    {
        
        switch (function)
        {
            case "Delete":
                PlayerPrefs.DeleteAll();
                break;
            case "Money":
                AllMoney += 1000000;
                PlayerPrefs.SetFloat("AllMoney", AllMoney);
                break;
            case "level":
                LVLplayer = 40;
                PlayerPrefs.SetInt("Lvl", LVLplayer);
                break;
        }
        SceneManager.LoadScene("Menu");
    }

    
}
