using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class RemoteControlBehaviour : MonoBehaviour
{
    private static RemoteControlBehaviour _singleton;
    public static RemoteControlBehaviour Singleton
    {
        get
        {
            if (_singleton == null)
            {
                _singleton = FindObjectOfType<RemoteControlBehaviour>();
            }
            return _singleton;
        }
    }

    public Sprite[] playButtonSprite;
    public Image playButtonImage;

    public Text currentTimeText;
    public Text totalTimeText;

    //public VideoPlayer videoPlayer;
    public MediaPlayer videoPlayer;

    public GameObject soundSliderObj;
    public AudioSource audioSource;

    public Slider videoProgressSlider;

    public GameObject backButtonObj;

    private Slider soundSlider;

    private Animator remoteControlAnimator;

    private int currentPlayButtonIndex;

    private bool isRemoteControlShow = true;
    // Use this for initialization
    void Start()
    {
        soundSlider = soundSliderObj.GetComponent<Slider>();
        remoteControlAnimator = GetComponent<Animator>();
        //videoPlayer = MainManager.Singleton.page3.GetComponentInChildren<MediaPlayer>();
        
        UpdateTotalTime();
        StartCoroutine("WaitForHideRemoteControl");
    }

    private void UpdateTotalTime()
    {
        int xiaoshi = (int)((videoPlayer.Info.GetDurationMs() * 0.001f) / 60f);
        int fenzhong = (int)(((videoPlayer.Info.GetDurationMs() * 0.001f) / 60f) % 60f);
        totalTimeText.text = xiaoshi.ToString("D2") + ":" + fenzhong.ToString("D2");
        //totalTimeText.text = ((int)((videoPlayer.Info.GetDurationMs()*0.001f) / 60f)).ToString("D2") + ":" + ((int)(((videoPlayer.Info.GetDurationMs() * 0.001f) / 60f) % 60f)).ToString("D2");
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer.Control.IsPlaying())
        {
            UpdateVideoCurrentTime();
            UpdateVideoProgressSlider();
        }
    }

    private void UpdateVideoCurrentTime()
    {
        currentTimeText.text = ((int)((videoPlayer.Control.GetCurrentTimeMs()*0.001f) / 60f)).ToString("D2") + ":" + ((int)((videoPlayer.Control.GetCurrentTimeMs() * 0.001f) % 60f)).ToString("D2");
    }

    private void UpdateVideoProgressSlider()
    {
        videoProgressSlider.value = (float)(videoPlayer.Control.GetCurrentTimeMs() / videoPlayer.Info.GetDurationMs());
    }

    public void OnPlayButtonDown()
    {
        ResetHideCoroutine();

        if (currentPlayButtonIndex == 0)
        {
            currentPlayButtonIndex = 1;
            videoPlayer.Pause();
        }
        else
        {
            currentPlayButtonIndex = 0;
            videoPlayer.Play();
        }

        playButtonImage.sprite = playButtonSprite[currentPlayButtonIndex];
    }

    public void OnSoundButtonDown()
    {
        ResetHideCoroutine();

        soundSliderObj.SetActive(!soundSliderObj.activeInHierarchy);
    }

    public void OnSoundSliderValueChange()
    {
        ResetHideCoroutine();

        audioSource.volume = soundSlider.value;
    }

    public void ToggleRemoteControl()
    {
        if (isRemoteControlShow)
        {
            StopCoroutine("WaitForHideRemoteControl");
            HideRemoteControl();
        }
        else
        {
            StopCoroutine("WaitForHideRemoteControl");
            ShowRemoteControl();
            StartCoroutine("WaitForHideRemoteControl");
        }
    }

    private IEnumerator WaitForHideRemoteControl()
    {
        yield return new WaitForSecondsRealtime(3f);
        HideRemoteControl();
    }

    private void HideRemoteControl()
    {
        remoteControlAnimator.SetTrigger("Hide");
        backButtonObj.SetActive(false);
        soundSliderObj.SetActive(false);
        isRemoteControlShow = false;
    }

    private void ShowRemoteControl()
    {
        remoteControlAnimator.SetTrigger("Show");
        backButtonObj.SetActive(true);
        isRemoteControlShow = true;
    }

    private void ResetHideCoroutine()
    {
        StopCoroutine("WaitForHideRemoteControl");
        StartCoroutine("WaitForHideRemoteControl");
    }

    private void OnDisable()
    {
        currentPlayButtonIndex = 0;
        playButtonImage.sprite = playButtonSprite[currentPlayButtonIndex];
        soundSliderObj.SetActive(false);
        isRemoteControlShow = true;

        //BackgroundMusicBehaviour.Singleton.PlayMusic();
    }
}