using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveStone : MonoBehaviour
{
    // ���C���J�����̕ϐ�
    GameObject mainCamera;
    CameraControll cameraControll;

    // �X�e�[�W��Ԋm�F�p�̕ϐ�
    GameDirector gameDirector;

    // �����蔻�葀��p�̕ϐ�
    Collider2D mayCollider2D;

    // �X�v���C�g�����_���[����p�̕ϐ�
    SpriteRenderer spriteRenderer;
    Color color;

    // �V�F�[�_�p�̕ϐ�
    public Material material1;
    public Material material2;

    // ��������
    void Start()
    {
        // ���C���J�����̎擾
        this.mainCamera = GameObject.Find("Main Camera");
        this.cameraControll = GameObject.Find("Main Camera").GetComponent<CameraControll>();

        // �X�v���C�g�����_���[�̎擾
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        this.color = this.spriteRenderer.color;

        // �V�F�[�_�̐ݒ�i�f�t�H���g�V�F�[�_�j
        this.spriteRenderer.material = this.material1;

        // Collider�̎擾
        this.mayCollider2D = gameObject.GetComponent<Collider2D>();
    }

    // �X�V����
    void Update()
    {
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
                // �����蔻��𖳌���
                this.mayCollider2D.enabled = false;
                break;
            // ���������O���[�X�P�[���̏ꍇ�A�A�N�Z�T���G�t�F�N�g�ƃA���t�@�l(1)��ݒ�
            case 2:
                // �V�F�[�_�̐ݒ�i�A�N�Z�T���G�t�F�N�g�j
                this.spriteRenderer.material = this.material2;
                // �A���t�@�l�̐ݒ�i1�j
                this.color.a = 1.0f;
                this.spriteRenderer.color = this.color;
                // ��ʉE���̏ꍇ�A�����蔻���L����
                if (transform.position.x >= this.mainCamera.transform.position.x)
                {
                    this.mayCollider2D.enabled = true;
                }
                // ��ʍ����̏ꍇ�A�����蔻��𖳌���
                else
                {
                    this.mayCollider2D.enabled = false;
                }
                break;
            // �S�̂��O���[�X�P�[���̏ꍇ�A�f�t�H���g�V�F�[�_�ƃA���t�@�l(1)��ݒ�
            case 3:
                // �V�F�[�_�̐ݒ�i�f�t�H���g�V�F�[�_�j
                this.spriteRenderer.material = this.material1;
                // �A���t�@�l�̐ݒ�i1�j
                this.color.a = 1.0f;
                this.spriteRenderer.color = this.color;
                // �����蔻���L����
                this.mayCollider2D.enabled = true;
                break;
            default:
                break;
        }
    }
}
