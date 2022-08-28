using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �T�u�J�������Ǘ�����N���X
/// </summary>
public class SubCameraControll : MonoBehaviour
{
    // �|�X�g�G�t�F�N�g�p�̕ϐ�
    public Material material1;
    public Material material2;
    public Material material3;

    // �G�t�F�N�g��ԊǗ��p�̕ϐ�
    // 1:�ʏ�
    // 2:�O���C�X�P�[���i�����j
    // 3:�O���C�X�P�[���i�S�́j
    int effectStatus = 1;

    // Start is called before the first frame update
    void Start()
    {
        // �G�t�F�N�g��ԂɃm�[�}����ݒ�
        this.effectStatus = 1;
    }

    // �|�X�g�G�t�F�N�g����
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        switch (this.effectStatus)
        {
            // �����G�t�F�N�g�i�G�t�F�N�g�Ȃ��Ɠ��l�j
            case 1:
                Graphics.Blit(source, destination, material1);
                break;
            // ���������O���[�X�P�[��
            case 2:
                Graphics.Blit(source, destination, material2);
                break;
            // �S�̂��O���[�X�P�[��
            case 3:
                Graphics.Blit(source, destination, material3);
                break;
            // �G�t�F�N�g�Ȃ�
            default:
                Graphics.Blit(source, destination);
                break;
        }
    }

    // �v���p�e�B��`
    // �G�t�F�N�g���
    public int EffectStatus
    {
        get { return this.effectStatus; }
        set { this.effectStatus = value; }
    }
}