using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���C���J�����ɃV�F�[�_��K�p���邽�߂̃N���X
/// </summary>
public class PostEffect : MonoBehaviour
{
    public Material material;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
