using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScriptSettings : MonoBehaviour
{
    [Header("Controller")]
    GameObject contr;

    [Header ("Graphic")]
    public GameObject PostProcessing;
    bool HighGraphic;
    int intProcessActive;
    GameObject ButtonInGraphic;

    [Header("Music")]
    public GameObject ActiveMusic;
    public GameObject NonActiveMusic;
    int MusicActive;
    AudioManager audio_;

    private void Start()
    {
        audio_ = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (PlayerPrefs.GetInt("FirstPlay")== 0)
        {
            ActiveMusic.SetActive(true);
            NonActiveMusic.SetActive(false);
            PlayerPrefs.SetInt("MusicActive", 1);
        }
        ButtonInGraphic = GameObject.Find("ButtonInGraphic");
        intProcessActive = PlayerPrefs.GetInt("PostProcessActive");
        if (intProcessActive == 1)
        {
            HighGraphic = true;
            PostProcessing.SetActive(true);
        }
        else
        {
            HighGraphic = false;
            PostProcessing.SetActive(false);
        }
        if (PlayerPrefs.GetInt("MusicActive") == 1)
        {
            ActiveMusic.SetActive(true);
            NonActiveMusic.SetActive(false);
            //audio_.SetActive(true);
        } else
        {
            ActiveMusic.SetActive(false);
            NonActiveMusic.SetActive(true);
           // audio_.SetActive(false);
        }
        MusicActive = PlayerPrefs.GetInt("MusicActive");
        
    }

    private void Update()
    {
        if (intProcessActive == 1)
        {
            ButtonInGraphic.SetActive(true);
        }
        else if (intProcessActive == 0)
        {
            ButtonInGraphic.SetActive(false);
        }
    }

    public void ButtonHighGraphic()
    {
        HighGraphic =! HighGraphic;
        PostProcessing.SetActive(HighGraphic);
        if (HighGraphic == true) intProcessActive = 1; else if (HighGraphic == false) intProcessActive = 0;
        PlayerPrefs.SetInt("PostProcessActive", intProcessActive);
    }

    public void OnButtonMusic()
    {
            MusicActive = 0;
            ActiveMusic.SetActive(false);
            NonActiveMusic.SetActive(true);
            PlayerPrefs.SetInt("MusicActive", 0);
        //audio_.sounds[].volume = 0f;
       // SceneManager.LoadScene("Menu");
    }

    public void OffButtonMusic()
    {
            MusicActive = 1;
            ActiveMusic.SetActive(true);
            NonActiveMusic.SetActive(false);
            PlayerPrefs.SetInt("MusicActive", 1);
        AudioManager.instance.Play("ButtonMenu");
       // SceneManager.LoadScene("Menu");
    }

    
}
