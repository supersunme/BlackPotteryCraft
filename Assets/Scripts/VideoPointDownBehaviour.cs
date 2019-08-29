using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VideoPointDownBehaviour : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        RemoteControlBehaviour.Singleton.ToggleRemoteControl();
    }
}