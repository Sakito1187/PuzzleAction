using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }

    public AudioSource audioSourceBGM;
    public AudioClip[] audioClipsBGM;
    public AudioSource audioSourceSE;
    public AudioClip[] audioClipsSE;

    private void Start(){
        audioSourceBGM.mute = PlayerPrefs.GetInt("BGM", 1) == 0;
        audioSourceSE.mute = PlayerPrefs.GetInt("SE", 1) == 0;
    }

    public void PlayBGM(int id) {
        if (audioSourceBGM.clip != audioClipsBGM[id]) {
            audioSourceBGM.Stop();
            audioSourceBGM.clip = audioClipsBGM[id];
            audioSourceBGM.Play();
        }
    }

    public void PlaySE(int id) {
        audioSourceSE.PlayOneShot(audioClipsSE[id]);
    }
}
