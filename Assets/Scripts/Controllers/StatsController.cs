using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsController : MonoBehaviour
{
    [Header("Score")]
    public Text allscore;
    public Text highscore;
    public Text countDeath;
    public Text lvl;

    public void UpdateStats()
    {
        allscore.text = PlayerPrefs.GetInt("Allscore").ToString();
        highscore.text = PlayerPrefs.GetInt("Highscore").ToString();
        countDeath.text = PlayerPrefs.GetInt("CountDeath").ToString();
        lvl.text = PlayerPrefs.GetInt("Lvl").ToString();
    }
}
