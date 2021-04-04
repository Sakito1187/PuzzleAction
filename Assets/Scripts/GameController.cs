using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using GoogleMobileAds.Api;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField] CanvasGroup FadePanel;
    [SerializeField] CanvasGroup DeadPanel;
    [SerializeField] GameObject ClearText;
    [SerializeField] GameObject DefeatText;
    private InterstitialAd interstitial;
    private bool clearFlag;
    void Start(){

        RequestInterstitial();

        FadeIn();
        if (1 <= GetCurrentStageNum() &&  GetCurrentStageNum() <= 9) {
            // easy stage
            SoundManager.instance.PlayBGM(1);
        }
        else if (GetCurrentStageNum() == 10) {
            // easy boss
            SoundManager.instance.PlayBGM(2);
        } 
        else if (11 <= GetCurrentStageNum() &&  GetCurrentStageNum() <= 19) {
            // normal stage
            SoundManager.instance.PlayBGM(3);
        }
        else if (GetCurrentStageNum() == 20) {
            // normal boss
            SoundManager.instance.PlayBGM(4);
        }
        else {

        }
    }
    public void OnGoToStageSelectButtonClick(){
        SoundManager.instance.PlaySE(2);
        FadePanel.alpha = 0f;
        FadePanel.blocksRaycasts = true;
        FadePanel.DOFade(1f, 1f).OnComplete(() => {
            SceneManager.LoadScene("StageSelect");
        });
    }

    public void OnRestartButtonClick(){
        SoundManager.instance.PlaySE(2);
        FadePanel.alpha = 0f;
        FadePanel.blocksRaycasts = true;
        FadePanel.DOFade(1f, 1f).OnComplete(() => {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        });
        
    }

    public void FadeOut(){
        FadePanel.alpha = 0f;
        FadePanel.blocksRaycasts = true;
        FadePanel.DOFade(1f, 1f);
    }

    public void FadeIn(){
        FadePanel.alpha = 1f;
        FadePanel.blocksRaycasts = false;
        FadePanel.DOFade(0f, 1f);
    }
    public void GameClear(){
        ClearText.SetActive(true);
        FadePanel.alpha = 0f;
        FadePanel.blocksRaycasts = true;
        FadePanel.DOFade(1f, 3f).OnComplete(() => {
            LoadNextScene();
        });
    }
    public void GameClearAds(){
        clearFlag = true;
        ClearText.SetActive(true);
        FadePanel.alpha = 0f;
        FadePanel.blocksRaycasts = true;
        FadePanel.DOFade(1f, 3f).OnComplete(() => {
            DisplayInterstitial();
        });
    }

    public void GameOver() {
        DefeatText.SetActive(true);
        DeadPanel.alpha = 0f;
        DeadPanel.blocksRaycasts = true;
        DeadPanel.DOFade(1f, 3f).OnComplete(() => {
            ReloadScene();
        });

    }

    public void GameOverAds() {
        clearFlag = false;
        DefeatText.SetActive(true);
        DeadPanel.alpha = 0f;
        DeadPanel.blocksRaycasts = true;
        DeadPanel.DOFade(1f, 3f).OnComplete(() => {
            DisplayInterstitial();
        });

    }

    public int GetCurrentStageNum() {
        return int.Parse(SceneManager.GetActiveScene().name.Replace("Stage", ""));
    }

    private void LoadNextScene() {
        int currentStageNum = GetCurrentStageNum();
        if (currentStageNum < 20) {
            SceneManager.LoadScene("Stage" + (currentStageNum + 1).ToString());  
        }
        else {
            SceneManager.LoadScene("StageSelect");
        }
    }

    private void ReloadScene() {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);  
    }

    private void DisplayInterstitial()
    {
    if (this.interstitial.IsLoaded()) {
        this.interstitial.Show();
    }
    }

    private void RequestInterstitial() {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    private void OnDestroy()
    {
        interstitial.Destroy();
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        if (clearFlag) {
            LoadNextScene();
        }
        else {
            ReloadScene();
        }
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        if (clearFlag) {
            LoadNextScene();
        }
        else {
            ReloadScene();
        }
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
    }
}
