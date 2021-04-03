using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StageSelectManager : MonoBehaviour
{

    [SerializeField] CanvasGroup FadePanel;
    [SerializeField] GameObject OptionPanel;
    [SerializeField] GameObject OutsideOptionPanel;
    [SerializeField] GameObject RemoveAdsPanel;
    [SerializeField] GameObject OutsideRemoveAdsPanel;

    void Start() {
        FadeIn();
    }
    public void OnStageButtonClick(int index) {
        FadePanel.alpha = 0f;
        FadePanel.blocksRaycasts = true;
        FadePanel.DOFade(1f, 1f).OnComplete(() => {
            SceneManager.LoadScene("Stage" + index.ToString());
        });
    }       

    public void OnOptionButtonClick() {
        OutsideOptionPanel.SetActive(true);
        OptionPanel.SetActive(true);
    }

    public void OnRemoveAdsButtonClick() {
        OutsideRemoveAdsPanel.SetActive(true);
        RemoveAdsPanel.SetActive(true);
    }

    public void CloseRemoveAdsPanel() {
        OutsideRemoveAdsPanel.SetActive(false);
        RemoveAdsPanel.SetActive(false);
    }

    public void CloseOptionPanel() {
        OutsideOptionPanel.SetActive(false);
        OptionPanel.SetActive(false);
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
        string esctag1 = UnityWebRequest.EscapeURL("ジェムコレ");
        string esctag2 = UnityWebRequest.EscapeURL("GemCollect");
        string url = "https://twitter.com/intent/tweet?text=" + "&hashtags=" + esctag1 + "&hashtags=" + esctag2  ;
        Application.OpenURL(url);
    }

    public void RequestReview() {
        UnityEngine.iOS.Device.RequestStoreReview();
    }

    public void Purchase() {
        InAppPurchasingManager.instance.BuyRemoveAds();
    }


}