using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CustomScroll : MonoBehaviour {

    private Image mainRender;

    private float startLeft;
    private float startBottom;

    public float obscureLeft;
    public float obscureBottom;

    private float lastLeft;
    private float lastBottom;



    void Awake()
    {
        mainRender = this.transform.GetComponent<Image>();
    }



	void Update ()
    {
        if (lastLeft != obscureLeft || lastBottom != obscureBottom)
        {
            SetObscure(obscureLeft, obscureBottom);
        }
    }

    void OnEnable()
    {
        lastLeft = startLeft = obscureLeft;
        lastBottom = obscureBottom = startBottom;
        SetObscure(obscureLeft, obscureBottom);
    }



    void SetObscure(float obscureTop,float obscureBottom)
    {
        mainRender.material.SetFloat("_AlphaLX", obscureTop);
        mainRender.material.SetFloat("_AlphaBY", obscureBottom);
    }



    public void RevsetObscure()
    {
        SetObscure(1.5f,1.5f);
    }


    void OnDisable()
    {
        obscureLeft = startLeft;
        lastBottom = startBottom;
    }

}
