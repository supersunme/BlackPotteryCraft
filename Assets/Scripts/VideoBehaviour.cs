using UnityEngine;

public class VideoBehaviour : MonoBehaviour
{
    public RenderTexture renderTexture;

    private void OnDisable()
    {
        renderTexture.Release();
    }
}