﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    public float idleTime;

    private bool isIdling;
    private Coroutine idleCoroutine;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isIdling)
            {
                isIdling = false;
                StopCoroutine(idleCoroutine);
            }

            if (MainManager.Singleton.pagePosition == PagePosition.IdlePage)
            {
                MainManager.Singleton.EnterHomePage();
            }
        }
        else if (!isIdling && MainManager.Singleton.pagePosition != PagePosition.IdlePage)
        {
            idleCoroutine = StartCoroutine(WaittingForIdle());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    /// <summary>
    /// 等待待机
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaittingForIdle()
    {
        isIdling = true;
        yield return new WaitForSeconds(idleTime);
        MainManager.Singleton.BackToIdlePage();

        isIdling = false;
        StopCoroutine(idleCoroutine);
    }
}