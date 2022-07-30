using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���C���J�������Ǘ�����N���X
/// </summary>
public class CameraControll : MonoBehaviour
{
    // �J�����ړ��p�̕ϐ�
    GameObject ball;
    // �J�����ړ������p�̕ϐ�
    Vector2 MinCameraPos;
    Vector2 MaxCameraPos;

    // �|�X�g�G�t�F�N�g�p�̕ϐ�
    public Material material1;
    public Material material2;
    public Material material3;

    // �G�t�F�N�g��ԊǗ��p�̕ϐ�
    // 1:�ʏ�A2:ʰ̸�ڲ���فA3:��ٸ�ڲ����
    int effectStatus;

    // ��������
    void Start()
    {
        // �v���C���[�{�[���̃I�u�W�F�N�g���擾
        this.ball = GameObject.Find("PlayerBall");
        // �J�����̈ړ��͈͂�ݒ�
        this.MinCameraPos = new Vector2(0.0f, 5.0f) ;
        this.MaxCameraPos = new Vector2(30.0f, -5.0f);
        // �G�t�F�N�g��ԂɃm�[�}����ݒ�
        this.effectStatus = 1;
    }

    // �X�V����
    void Update()
    {
        // �ړ�����
        Move();
    }

    // �ړ�����
    private void Move()
    {
        // �J�����ʒu�p�̕ϐ�
        Vector3 cameraPos = transform.position;

        // �J�����ړ��i�{�[����X���W�ɍ��킹�Ĉړ��j
        cameraPos.x = this.ball.transform.position.x;

        // �ړ������̔���
        if (cameraPos.x < this.MinCameraPos.x) cameraPos.x = this.MinCameraPos.x;
        if (cameraPos.x > this.MaxCameraPos.x) cameraPos.x = this.MaxCameraPos.x;

        // �ړ��̓K�p
        transform.position = cameraPos;
    }

    // �|�X�g�G�t�F�N�g����
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        switch(this.effectStatus)
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

    // �G�t�F�N�g��Ԃ̎擾
    public int getEffectStatus()
    {
        return this.effectStatus;
    }

    // �G�t�F�N�g��Ԃ̐ݒ�
    public void setEffectStatus(int status)
    {
        this.effectStatus = status;
    }
}
