using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PagePosition
{
    IdlePage,
    HomePage,
    Page1,
    Page2,
    Page3
}

public class MainManager : MonoBehaviour
{
    private static MainManager _singleton;
    public static MainManager Singleton
    {
        get
        {
            if (_singleton == null)
            {
                _singleton = FindObjectOfType<MainManager>();
            }
            return _singleton;
        }
    }
   void Awake()
    {
        Cursor.visible = false;
    }
    public GameObject idlePage;
    public GameObject homePage;
    public GameObject page1;
    public GameObject page2;
    public GameObject page3;

    public AudioSource audioSource;

    public PagePosition pagePosition;

    public void BackToIdlePage()
    {
        idlePage.SetActive(true);
        homePage.SetActive(false);
        idlePage.GetComponentInChildren<MediaPlayer>().Play();
        pagePosition = PagePosition.IdlePage;
    }
    public void BackToHomePage()
    {
        homePage.SetActive(true);
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(false);

        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        pagePosition = PagePosition.HomePage;
    }

    public void EnterHomePage()
    {
        homePage.SetActive(true);
        idlePage.SetActive(false);

        pagePosition = PagePosition.HomePage;
    }

    public void EnterPage1()
    {
        page1.SetActive(true);
        homePage.SetActive(false);

        pagePosition = PagePosition.Page1;
    }
    public void EnterPage2()
    {
        page2.SetActive(true);
        homePage.SetActive(false);

        pagePosition = PagePosition.Page2;
    }
    public void EnterPage3()
    {
        if(audioSource.isPlaying)
        {
            audioSource.Pause();
        }

        page3.SetActive(true);
        homePage.SetActive(false);

        pagePosition = PagePosition.Page3;
    }
}