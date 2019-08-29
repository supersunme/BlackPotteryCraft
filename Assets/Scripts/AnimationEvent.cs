using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    /// <summary>
    /// 启动动画单一步骤完成时
    /// </summary>
    public void BootAnimationStepFinsh()
    {
        State2Behaviour.Singleton.StartNextBootAnimationStep();
    }
    /// <summary>
    /// 启动动画完成时
    /// </summary>
    public void BootAnimationFinsh()
    {
        State2Behaviour.Singleton.BootAnimationFinsh();
    }
}