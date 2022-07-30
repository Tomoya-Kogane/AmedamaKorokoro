using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メインカメラにシェーダを適用するためのクラス
/// </summary>
public class PostEffect : MonoBehaviour
{
    public Material material;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
