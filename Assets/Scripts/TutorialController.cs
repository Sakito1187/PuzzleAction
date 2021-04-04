using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
public class TutorialController : MonoBehaviour
{
    [SerializeField] CanvasGroup FadePanel;
    [SerializeField] CanvasGroup DeadPanel;
    [SerializeField] CanvasGroup TutorialPanel;
    [SerializeField] GameObject ClearText;
    [SerializeField] GameObject DefeatText;
    // [SerializeField] GameObject Joystick;
    // [SerializeField] GameObject Firebutton;
    [SerializeField] GameObject ItemCounterText;
    [SerializeField] GameObject TitleJa;
    [SerializeField] GameObject TitleEn;
    [SerializeField] GameObject TextJa;
    [SerializeField] GameObject TextEn;

    TextMeshProUGUI Title;
    TextMeshProUGUI Text;

    void Awake(){
        if (Application.systemLanguage == SystemLanguage.Japanese) {
            Title = TitleJa.GetComponent<TextMeshProUGUI>();
            Text = TextJa.GetComponent<TextMeshProUGUI>();
        }
        else {
            Title = TitleEn.GetComponent<TextMeshProUGUI>();
            Text = TextEn.GetComponent<TextMeshProUGUI>();
        }

        // Title = TitleEn.GetComponent<TextMeshProUGUI>();
        // Text = TextEn.GetComponent<TextMeshProUGUI>();

        FadeIn();
        SoundManager.instance.PlayBGM(1);
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
                SceneManager.LoadScene("StageSelect");
        });
    }

    public void GameOver() {

    }

    public void UpdateTitle(string title) {
        Title.text = title;
        Text.text = "";
    }

    public void UpdateText(string text) {
        Text.text = text;
        Title.text = "";
    }

    public void TutorialPanelOn() {
        TutorialPanel.alpha = 0f;
        TutorialPanel.blocksRaycasts = true;
        TutorialPanel.DOFade(1f, 0.5f);
    }

    public void TutorialPanelOff() {
        TutorialPanel.alpha = 1f;
        TutorialPanel.blocksRaycasts = false;
        TutorialPanel.DOFade(0f, 0.5f);
    }
    public void ToggleItemCounterText(bool isActive) {
        ItemCounterText.SetActive(isActive);
    }

}
