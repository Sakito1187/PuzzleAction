using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using GoogleMobileAds.Api;
public class TitleController : MonoBehaviour
{
    [SerializeField] GameObject hasItemsParent;
    [SerializeField] GameObject TitleJa;
    [SerializeField] GameObject TitleEn;
    [SerializeField] CanvasGroup FadePanel;

    void Start() {
    MobileAds.Initialize(initStatus => { });
    FadeIn();
    SoundManager.instance.PlayBGM(0);
    TitleJa.SetActive(Application.systemLanguage ==  SystemLanguage.Japanese);
    TitleEn.SetActive(Application.systemLanguage !=  SystemLanguage.Japanese);
    }

    void FixedUpdate(){
        hasItemsParent.transform.Rotate(0f, 3f, 0f);
    }


    public void FadeIn(){
        FadePanel.alpha = 1f;
        FadePanel.blocksRaycasts = false;
        FadePanel.DOFade(0f, 1f);
    }

    public void GoToTutorial(){
        SoundManager.instance.PlaySE(0);
        FadePanel.alpha = 0f;
        FadePanel.blocksRaycasts = true;
        FadePanel.DOFade(1f, 1f).OnComplete(() => {
            SceneManager.LoadScene("stage0");
        });
    }

    public void GoToStageSelect(){
        SoundManager.instance.PlaySE(0);
        FadePanel.alpha = 0f;
        FadePanel.blocksRaycasts = true;
        FadePanel.DOFade(1f, 1f).OnComplete(() => {
            SceneManager.LoadScene("StageSelect");
        });
    }

}
