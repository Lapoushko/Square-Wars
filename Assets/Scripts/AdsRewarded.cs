using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AdsRewarded : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private bool _testMode = false;
    [SerializeField] private Button _adsButton;
    [SerializeField] public GameObject btnOpenAds;

    private string _gameid = "4226603";

    private string _rewardedVideo = "Rewarded_Android";
    [SerializeField] public ControllerInGame contr;
    ScoreController scorecontr;

    public bool AdsExperience;

    private void Start()
    {
        contr = GameObject.Find("Controller").GetComponent<ControllerInGame>();
        scorecontr = GameObject.Find("Controller").GetComponent<ScoreController>();
        _adsButton = GetComponent<Button>();
        _adsButton.interactable = Advertisement.IsReady(_rewardedVideo);
        if (_adsButton)
            _adsButton.onClick.AddListener(ShowRewardedVideo);

        Advertisement.AddListener(this);
        Advertisement.Initialize(_gameid, _testMode);
    }

    public void ShowRewardedVideo()
    {
        Advertisement.Show(_rewardedVideo);
    }

    public static void ShowAdsVideo(string placementId)
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show(placementId);
        }
        else
        {
            Debug.Log("Advertisiment not ready");
        }
    }   

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == _rewardedVideo)
        {
            _adsButton.interactable = true;
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (contr)
        {
            if (showResult == ShowResult.Finished)
            {
                contr.newMoney *= 2;
                contr.newMoneyTxt.text = contr.newMoney.ToString();
                PlayerPrefs.SetFloat("AllMoney", contr.scorecntr.Score / 10 + PlayerPrefs.GetFloat("AllMoney"));
                contr.Money.text = PlayerPrefs.GetFloat("AllMoney").ToString();
                gameObject.SetActive(false);
                PlayerPrefs.Save();
                //scorecontr.Score *= 2;
                //contr.Lasttxt.text = contr.exp.ToString();
                //if (scorecontr.Score >= scorecontr.HighScore)
                //{
                //  this.contr.trashTalk.text = this.contr.trashQuotesRecord[Random.Range(0, this.contr.trashQuotesRecord.Count)];
                // PlayerPrefs.SetInt("Highscore", scorecontr.Score);
                //contr.NewScore.SetActive(true);
                //}
                //PlayerPrefs.SetInt("Allscore", PlayerPrefs.GetInt("Allscore") + scorecontr.Score);
                //contr.Money.text = PlayerPrefs.GetFloat("AllMoney").ToString();
                //btnOpenAds.SetActive(false);
                //PlayerPrefs.Save();

            }
            else if (showResult == ShowResult.Skipped)
            {
                Debug.Log("WHEREMYMONEY");
            }
            else if (showResult == ShowResult.Failed)
            {
                Debug.Log("///");
            }
        }
    }
}
