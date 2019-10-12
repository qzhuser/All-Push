using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTrigger : MonoBehaviour
{
    //淡入淡出持续时间(timer/fadeDuration得出alpha值)
    public float fadeDuration = 1f;
    public AudioSource exitAudio,caughtAudio;
    [Header("持续一定时间再退出游戏")]
    public float displayDuration = 1f;
    public CanvasGroup exitFadeCanvasGroup,BeiZhuaFadeCanvasGroup;
    bool isExit = false;
    bool isBeiZhua = false;
    bool m_isAudioPlayed;
    //计时器，每帧加上Time.deltaTime
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ZhuaDaoPlayer() {
        isBeiZhua = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isBeiZhua) {
            EndLevel(BeiZhuaFadeCanvasGroup,true,caughtAudio);
        }
        else if (isExit)
        {
            EndLevel(exitFadeCanvasGroup,false,exitAudio);
        }
    }
    /// <summary>
    /// 被抓结束游戏
    /// </summary>
    public void EndBeiZhua() {

    }
    /// <summary>
    /// 逃生成功，结束游戏
    /// </summary>
    void EndLevel(CanvasGroup canvasgroup,bool doReset,AudioSource audioSource) {
        if (!m_isAudioPlayed)
        {
            m_isAudioPlayed = true;
            audioSource.Play();
        }
        timer += Time.deltaTime;
        canvasgroup.alpha = timer / fadeDuration;
        if (timer > fadeDuration + displayDuration)
        {
            if (doReset)
                SceneManager.LoadScene(0);
            else
                Application.Quit();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            isExit = true;
        }
    }
}
