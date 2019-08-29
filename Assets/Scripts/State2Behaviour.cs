using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AnimatorArray
{
    public Animator[] Animators;
}

public class State2Behaviour : MonoBehaviour
{
    private const int detailsMaxIndex = 5;

    private static State2Behaviour _singleton;
    public static State2Behaviour Singleton
    {
        get
        {
            if (_singleton == null)
            {
                _singleton = FindObjectOfType<State2Behaviour>();
            }
            return _singleton;
        }
    }

    public ScrollRect scrollRect;
    public Image leftButtonImage;
    public Image rightButtonImage;

    public GameObject arrows;

    public GameObject[] bootAnimations;
    public GameObject[] tipObjs;

    public int pageSpacing;
    public float AutoSlideTime;

    public int currentPage = -1;

    private int currentBootAnimationIndex = -1;

    private bool isSliding;
    private Coroutine slideCoroutine;

    private void OnEnable()
    {
        StartNextBootAnimationStep();
    }
    /// <summary>
    /// 左切换按钮按下时
    /// </summary>
    public void OnLeftButtonDown()
    {
        StartSlideScroll(-1);
    }
    /// <summary>
    /// 右切换按钮按下时
    /// </summary>
    public void OnRightButtonDown()
    {
        StartSlideScroll(1);
    }
    /// <summary>
    /// 启动下一个启动动画
    /// </summary>
    public void StartNextBootAnimationStep()
    {
        if (currentBootAnimationIndex != -1)
        {
            bootAnimations[currentBootAnimationIndex].SetActive(false);
        }
        currentBootAnimationIndex++;
        bootAnimations[currentBootAnimationIndex].SetActive(true);
    }
    /// <summary>
    /// 启动动画完成时
    /// </summary>
    public void BootAnimationFinsh()
    {
        bootAnimations[currentBootAnimationIndex].SetActive(false);
        StartSlideScroll(1);
        arrows.SetActive(true);
    }
    /// <summary>
    /// 开始滑动卷轴
    /// </summary>
    /// <param name="flag"></param>
    public void StartSlideScroll(int flag)
    {
        if (isSliding || currentPage + flag < 0 || currentPage + flag > detailsMaxIndex)
            return;

        if (currentPage != -1)
            tipObjs[currentPage].SetActive(false);

        currentPage += flag;

        if (currentPage <= 0)
            leftButtonImage.color = Color.gray;
        else if (currentPage >= detailsMaxIndex)
            rightButtonImage.color = Color.gray;
        else
        {
            leftButtonImage.color = Color.white;
            rightButtonImage.color = Color.white;
        }

        slideCoroutine = StartCoroutine(AutoSlideScroll(flag));
    }
    /// <summary>
    /// 自动滑动卷轴协程
    /// </summary>
    /// <param name="flag">-1Left 1Right</param>
    /// <returns></returns>
    private IEnumerator AutoSlideScroll(int flag)
    {
        isSliding = true;
        float t = AutoSlideTime * 100;
        Vector2 offset = new Vector2(-pageSpacing / t * flag, 0);
        while (t > 0)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            scrollRect.content.anchoredPosition += offset;

            t--;
        }

        scrollRect.content.anchoredPosition = new Vector2(-pageSpacing * (currentPage + 1), 0);

        isSliding = false;

        tipObjs[currentPage].SetActive(true);

        StopCoroutine(slideCoroutine);
    }

    /// <summary>
    /// 设定默认值
    /// </summary>
    private void SetDefault()
    {
        scrollRect.content.anchoredPosition = Vector2.zero;

        if (currentPage != -1)
        {
            tipObjs[currentPage].SetActive(false);
            currentPage = -1;
        }

        arrows.SetActive(false);
        isSliding = false;

        currentBootAnimationIndex = -1;
        for (int i = 0; i < bootAnimations.Length; i++)
        {
            bootAnimations[i].SetActive(false);
        }

        leftButtonImage.color = Color.gray;
        rightButtonImage.color = Color.white;
    }

    private void OnDisable()
    {
        SetDefault();
    }
}