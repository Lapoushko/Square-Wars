using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerInGame : MonoBehaviour
{
    [Header("AudioManager")]
    public GameObject audioManager;

    [Header("PanelDeath")]
    public GameObject Joysticks;
    public GameObject PanelDeath;
    public bool PlayerDeath;
    public static float fps;
    public Text FpsTxt;

    [Header ("Score")]
    public Text Highscore;
    public Text score;
    public ScoreController scorecntr;
    public GameObject NewScore;

    GameObject PostProcessing;

    [Header("TextCenterRecordOrNo")]
    public List<string> trashQuotesRecord;
    public List<string> trashQuotesNoRecord;
    public Text trashTalk;

    [Header("Lvl")]
    public int lvl;
    public float expForLvl;
    public float expForLvl_1;
    public float exp;
    public Text lvltxt;
    UILevel uilevel;
    public Text Lasttxt;
    public Text Nexttxt;

    [Header("Money")]
    public float AllMoney;
    public Text Money;
    public float newMoney;
    public Text newMoneyTxt;

    [Header("Settings")]
    public GameObject PanelSettings;
    public GameObject BtnSettings;

    void Start()
    {
        Time.timeScale = 1;
        PlayerDeath = false;
        //
        NewScore.SetActive(false);
        //
        GetComponent<SpawnController>().CanSpawn = true;
        lvl = PlayerPrefs.GetInt("Lvl");
        if (lvl == 1) { expForLvl = expForLvl_1; }
        else
            expForLvl = Mathf.Round(PlayerPrefs.GetFloat("expForLvl"));
        exp = Mathf.Round(PlayerPrefs.GetFloat("exp"));
        Application.targetFrameRate = 60;
        PostProcessing = GameObject.Find("volume");
        scorecntr = GetComponent<ScoreController>();
        if (PlayerPrefs.GetInt("PostProcessActive") == 1) PostProcessing.SetActive(true); else PostProcessing.SetActive(false);
        audioManager = GameObject.Find("AudioManager");      
    }

    private void Update()
    {
        fps = 1.0f / Time.deltaTime;
        FpsTxt.text = ("FPS: " + (int)fps);
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause == true) Time.timeScale = 0; else Time.timeScale = 1;
    }

    public void OnDeathPanel()
    {
        Joysticks.SetActive(false);
        PanelDeath.SetActive(true);
        LvlVoid();
        score.text = scorecntr.Score.ToString();
        newMoney = scorecntr.Score / 10;
        PlayerPrefs.SetFloat("AllMoney", scorecntr.Score / 10 + PlayerPrefs.GetFloat("AllMoney"));
        if (scorecntr.Score >= scorecntr.HighScore)
        {
            this.trashTalk.text = this.trashQuotesRecord[Random.Range(0, this.trashQuotesRecord.Count)];
            PlayerPrefs.SetInt("Highscore", scorecntr.Score);
            NewScore.SetActive(true);
        }
        else
        {
            this.trashTalk.text = this.trashQuotesNoRecord[Random.Range(0, this.trashQuotesNoRecord.Count)];
        }
        Highscore.text = PlayerPrefs.GetInt("Highscore").ToString();
        lvltxt.text = lvl.ToString();
        Lasttxt.text = exp.ToString();
        Nexttxt.text = expForLvl.ToString();
        PlayerPrefs.SetInt("Allscore", PlayerPrefs.GetInt("Allscore") + scorecntr.Score);
        newMoneyTxt.text = newMoney.ToString();
        Money.text = PlayerPrefs.GetFloat("AllMoney").ToString();
        GetComponent<SpawnController>().CanSpawn = false;
        BtnSettings.SetActive(false);
        PlayerPrefs.Save();
    }
    public void OnRestartGame()
    {
        SceneManager.LoadScene(Random.Range(1, SceneManager.sceneCountInBuildSettings-1));
    }

    public void OnRestartGameSp()
    {
        SceneManager.LoadScene("GameScene4-Sp");
    }

    public void OnMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnClickRewardedVideo()
    {
        Ads.ShowAdsVideo("Interstitial_Android");
    }

    public void LvlVoid()
    {
        uilevel = GameObject.Find("LevelBar").GetComponent<UILevel>();
        uilevel.SetMaxAm(expForLvl);
        uilevel.SetAm(exp);
        float exp1 = exp;
        exp += scorecntr.Score;
        uilevel.SetAm(exp);
        while(exp >= expForLvl)
        {
            lvl += 1;
            exp = Mathf.Abs(expForLvl - exp);            
            expForLvl = Mathf.Round(expForLvl * 1.5f);
            uilevel.SetAm(exp);
        }
        PlayerPrefs.SetInt("Lvl",lvl);
        PlayerPrefs.SetFloat("expForLvl",expForLvl);
        PlayerPrefs.SetFloat("exp",exp);
    }

    public void OnSettingsPanel()
    {
        PanelSettings.SetActive(true);
        BtnSettings.SetActive(false);
        Time.timeScale = 0;
    }
    public void BackSettings()
    {
        PanelSettings.SetActive(false);
        BtnSettings.SetActive(true);
        Time.timeScale = 1;
    }
}
