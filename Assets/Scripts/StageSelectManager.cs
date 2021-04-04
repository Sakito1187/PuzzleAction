using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using DG.Tweening;
using GoogleMobileAds.Api;
using System;
using Bolt;

public class StageSelectManager : MonoBehaviour
{
    private RewardedAd rewardedAd;
    [SerializeField] InAppPurchasingManager IAPM;
    [SerializeField] CanvasGroup FadePanel;
    [SerializeField] GameObject[] TextsJa;
    [SerializeField] GameObject[] TextsEn;
    [SerializeField] GameObject OptionPanel;
    [SerializeField] GameObject OutsideOptionPanel;
    [SerializeField] GameObject RemoveAdsPanel;
    [SerializeField] GameObject OutsideRemoveAdsPanel;
    [SerializeField] GameObject SuccessPanel;
    [SerializeField] GameObject FailurePanel;
    [SerializeField] GameObject OutsideResultPanel;
    [SerializeField] GameObject LifePanel;
    [SerializeField] GameObject OutsideLifePanel;
    [SerializeField] Button RemoveAdsButton;
    [SerializeField] Image BGMIcon;
    [SerializeField] Image SEIcon;

    void Start() {
        RequestReward();

        int n = TextsJa.Length;
        for (int i = 0; i < n; i++) {
            TextsJa[i].SetActive(Application.systemLanguage ==  SystemLanguage.Japanese);
            TextsEn[i].SetActive(Application.systemLanguage !=  SystemLanguage.Japanese);
        }
        FadeIn();
        SoundManager.instance.PlayBGM(0);
        if (SoundManager.instance.audioSourceBGM.mute) {
            BGMIcon.color = new Color(BGMIcon.color.r, BGMIcon.color.g, BGMIcon.color.b, 0.5f);
        }
        else {
            BGMIcon.color = new Color(BGMIcon.color.r, BGMIcon.color.g, BGMIcon.color.b, 1f);
        }
        if (SoundManager.instance.audioSourceSE.mute) {
            SEIcon.color = new Color(SEIcon.color.r, SEIcon.color.g, SEIcon.color.b, 0.5f);
        }
        else {
            SEIcon.color = new Color(SEIcon.color.r, SEIcon.color.g, SEIcon.color.b, 1f);
        }

    }

    private void Update() {
        if (InAppPurchasingManager.instance.GetSuccessFlag()) {
            InAppPurchasingManager.instance.ResetSuccessFlag();
            PurchaseSuccess();
        }
        if (InAppPurchasingManager.instance.GetFailureFlag()) {
            InAppPurchasingManager.instance.ResetFailureFlag();
            PurchaseFailure();
        }
    }
    public void OnStageButtonClick(int index) {
        SoundManager.instance.PlaySE(1);
        FadePanel.alpha = 0f;
        FadePanel.blocksRaycasts = true;
        FadePanel.DOFade(1f, 1f).OnComplete(() => {
            SceneManager.LoadScene("Stage" + index.ToString());
        });
    }       
    public void OnOptionButtonClick() {
        SoundManager.instance.PlaySE(2);
        OutsideOptionPanel.SetActive(true);
        OptionPanel.SetActive(true);
    }
    public void OnTutorialButtonClick() {
        SoundManager.instance.PlaySE(2);
        FadePanel.alpha = 0f;
        FadePanel.blocksRaycasts = true;
        FadePanel.DOFade(1f, 1f).OnComplete(() => {
            SceneManager.LoadScene("Stage0");
        });
    }
    public void OnRemoveAdsButtonClick() {
        SoundManager.instance.PlaySE(2);
        OutsideRemoveAdsPanel.SetActive(true);
        RemoveAdsPanel.SetActive(true);
    }
    public void CloseRemoveAdsPanel() {
        SoundManager.instance.PlaySE(2);
        OutsideRemoveAdsPanel.SetActive(false);
        RemoveAdsPanel.SetActive(false);
    }
    public void OnPurchaseButtonClick() {
        SoundManager.instance.PlaySE(2);
        InAppPurchasingManager.instance.BuyRemoveAds();
    }
    public void OnRestoreButtonClick() {
        SoundManager.instance.PlaySE(2);
        InAppPurchasingManager.instance.RestorePurchases();
    }

    public void CloseOptionPanel() {
        SoundManager.instance.PlaySE(2);
        OutsideOptionPanel.SetActive(false);
        OptionPanel.SetActive(false);
    }
    public void OnLifeButtonClick() {
        SoundManager.instance.PlaySE(2);
        OutsideLifePanel.SetActive(true);
        LifePanel.SetActive(true);
    }
    public void CloseLifePanel() {
        SoundManager.instance.PlaySE(2);
        OutsideLifePanel.SetActive(false);
        LifePanel.SetActive(false);
    }
    public void PurchaseSuccess() {
        SoundManager.instance.PlaySE(12);
        OutsideResultPanel.SetActive(true);
        SuccessPanel.SetActive(true);
        OutsideRemoveAdsPanel.SetActive(false);
        RemoveAdsPanel.SetActive(false);
        RemoveAdsButton.interactable = false;
    }
    public void PurchaseFailure() {
        SoundManager.instance.PlaySE(11);
        OutsideResultPanel.SetActive(true);
        FailurePanel.SetActive(true);
    }
    public void CloseResultPanel() {
        SoundManager.instance.PlaySE(2);
        OutsideResultPanel.SetActive(false);
        SuccessPanel.SetActive(false);
        FailurePanel.SetActive(false);
    }
    public void FadeOut() {
        FadePanel.alpha = 0f;
        FadePanel.blocksRaycasts = true;
        FadePanel.DOFade(1f, 1f);
    }
    public void FadeIn() {
        FadePanel.alpha = 1f;
        FadePanel.blocksRaycasts = false;
        FadePanel.DOFade(0f, 1f);
    }
    public void Tweet() {
        SoundManager.instance.PlaySE(2);
        string esctag1 = UnityWebRequest.EscapeURL("ジェムコレ");
        string esctag2 = UnityWebRequest.EscapeURL("GemCol");
        string url = "https://twitter.com/intent/tweet?text=" + "&hashtags=" + esctag1 + "&hashtags=" + esctag2;
        Application.OpenURL(url);
    }
    public void RequestReview() {
        UnityEngine.iOS.Device.RequestStoreReview();
    }
    public void Purchase() {
        IAPM.BuyRemoveAds();
    }
    public void ToggleBGM() {
        SoundManager.instance.audioSourceBGM.mute = !SoundManager.instance.audioSourceBGM.mute;
        SoundManager.instance.PlaySE(2);
        if (SoundManager.instance.audioSourceBGM.mute) {
            BGMIcon.color = new Color(BGMIcon.color.r, BGMIcon.color.g, BGMIcon.color.b, 0.5f);
            PlayerPrefs.SetInt("BGM", 0);
        }
        else {
            BGMIcon.color = new Color(BGMIcon.color.r, BGMIcon.color.g, BGMIcon.color.b, 1f);
            PlayerPrefs.SetInt("BGM", 1);
        }
    }
    public void ToggleSE() {
        SoundManager.instance.audioSourceSE.mute = !SoundManager.instance.audioSourceSE.mute;
        SoundManager.instance.PlaySE(2);
        if (SoundManager.instance.audioSourceSE.mute) {
            SEIcon.color = new Color(SEIcon.color.r, SEIcon.color.g, SEIcon.color.b, 0.5f);
            PlayerPrefs.SetInt("SE", 0);
        }
        else {
            SEIcon.color = new Color(SEIcon.color.r, SEIcon.color.g, SEIcon.color.b, 1f);
            PlayerPrefs.SetInt("SE", 1);
        }
    }
    public void OnAddLifeButtonClick() {
        SoundManager.instance.PlaySE(2);
        UserChoseToWatchAd();
    }

    private void RequestReward() {
        string adUnitId;
        #if UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
        #elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
        #else
            adUnitId = "unexpected_platform";
        #endif

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    private void UserChoseToWatchAd() {
        if (this.rewardedAd.IsLoaded()) {
            this.rewardedAd.Show();
        }
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {

    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        this.RequestReward();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        CustomEvent.Trigger(this.gameObject, "AddLife");
    }
}