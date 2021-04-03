using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class GameController : MonoBehaviour
{
    [SerializeField] CanvasGroup FadePanel;
    [SerializeField] CanvasGroup DeadPanel;
    [SerializeField] GameObject ClearText;
    [SerializeField] GameObject DefeatText;

    void Start(){
        FadeIn();
    }
    public void OnGoToStageSelectButtonClick(){
        FadePanel.alpha = 0f;
        FadePanel.blocksRaycasts = true;
        FadePanel.DOFade(1f, 1f).OnComplete(() => {
            SceneManager.LoadScene("StageSelect");
        });
    }

    public void OnRestartButtonClick(){
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
            int currentStageNum = GetCurrentStageNum();
            if (currentStageNum < 20) {
                SceneManager.LoadScene("Stage" + (currentStageNum + 1).ToString());  
            }
            else {
                SceneManager.LoadScene("StageSelect");
            }
        });
    }

    public void GameOver() {
        DefeatText.SetActive(true);
        DeadPanel.alpha = 0f;
        DeadPanel.blocksRaycasts = true;
        DeadPanel.DOFade(1f, 3f).OnComplete(() => {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);  
        });

    }

    public int GetCurrentStageNum() {
        return int.Parse(SceneManager.GetActiveScene().name.Replace("Stage", ""));
    }




}
