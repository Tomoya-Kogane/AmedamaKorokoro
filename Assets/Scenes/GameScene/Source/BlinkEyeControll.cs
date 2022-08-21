using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �܂΂�������ڋʂ��Ǘ�����N���X
/// </summary>
public class BlinkEyeControll : MonoBehaviour
{
    // �X�v���C�g�����_���[����p�̕ϐ�
    SpriteRenderer spriteRenderer;
    Color color;

    // �A�j���[�V��������p�̕ϐ�
    Animator animator;
    
    // �V�F�[�_�p�̕ϐ�
    public Material material1;
    public Material material2;

    // �J�����G�t�F�N�g�m�F�p�̕ϐ�
    CameraControll cameraControll;

    // �I�u�W�F�N�g�������ԊǗ��p�̕ϐ�
    const float LIVE_TIME = 3.0f;
    float deltaTime = 0.0f;

    // ��������
    void Start()
    {
        // �X�v���C�g�����_���[�̎擾
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        this.color = this.spriteRenderer.color;

        // �V�F�[�_�̐ݒ�i�f�t�H���g�V�F�[�_�j
        this.spriteRenderer.material = this.material1;

        // �J�����R���g���[���[�R���|�[�l���g���擾
        this.cameraControll = GameObject.Find("Main Camera").GetComponent<CameraControll>();
        // �J�����̃G�t�F�N�g�ɉ������V�F�[�_��ݒ�
        switch (this.cameraControll.GetEffectStatus())
        {
            // �����G�t�F�N�g�̏ꍇ�A�f�t�H���g�V�F�[�_�ƃA���t�@�l(ZERO)��ݒ�
            case 1:
                // �V�F�[�_�̐ݒ�i�f�t�H���g�V�F�[�_�j
                this.spriteRenderer.material = this.material1;
                // �A���t�@�l�̐ݒ�iZERO�j
                this.color.a = 0.0f;
                this.spriteRenderer.color = this.color;
                break;
            // ���������O���[�X�P�[���̏ꍇ�A�A�N�Z�T���G�t�F�N�g�ƃA���t�@�l(1)��ݒ�
            case 2:
                // �V�F�[�_�̐ݒ�i�A�N�Z�T���G�t�F�N�g�j
                this.spriteRenderer.material = this.material2;
                // �A���t�@�l�̐ݒ�i1�j
                this.color.a = 1.0f;
                this.spriteRenderer.color = this.color;
                break;
            // �S�̂��O���[�X�P�[���̏ꍇ�A�f�t�H���g�V�F�[�_�ƃA���t�@�l(1)��ݒ�
            case 3:
                // �V�F�[�_�̐ݒ�i�f�t�H���g�V�F�[�_�j
                this.spriteRenderer.material = this.material1;
                // �A���t�@�l�̐ݒ�i1�j
                this.color.a = 1.0f;
                this.spriteRenderer.color = this.color;
                break;
            default:
                break;
        }

        // �A�j���[�V�����̃R���|�[�l���g���擾
        this.animator = GetComponent<Animator>();
    }

    // �X�V����
    void Update()
    {
        // ��莞�Ԍo�߂�����A�I�u�W�F�N�g���폜
        if (this.cameraControll.GetEffectStatus() == 2 || this.cameraControll.GetEffectStatus() == 3)
        {
            this.deltaTime += Time.deltaTime;
            // �قړ��߂��ꂽ��A�I�u�W�F�N�g��j��
            if (this.deltaTime >= LIVE_TIME)
            {
                Destroy(gameObject);
            }
        }
    }

    // �A�j���[�V�������x�̐ݒ�
    public void SetAnimationSpeed(float speed)
    {
        this.animator.speed = speed;
    }
    // �A�j���[�V�������x�̎擾
    public float GetAnimationSpeed()
    {
        return this.animator.speed;
    }

}
