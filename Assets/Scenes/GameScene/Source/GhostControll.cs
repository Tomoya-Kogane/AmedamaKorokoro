using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostControll : MonoBehaviour
{
    // �H�쑀��p�̕ϐ�
    Rigidbody2D rigid2D;
    // ���C���J�����̍��W�擾�p�̕ϐ�
    GameObject mainCamera;

    // �ړ����x
    const float WALK_FORCE = 8.0f;
    // �ړ����x�̐����l
    const float MAX_WALKSPEED = 4.0f;
    // �ړ������i�����l�E�����j
    int movedir = 1;

    // �X�e�[�W��Ԋm�F�p�̕ϐ�
    GameDirector gameDirector;

    // �X�v���C�g�����_���[����p�̕ϐ�
    SpriteRenderer spriteRenderer;
    Color color;

    // �V�F�[�_�p�̕ϐ�
    public Material material1;
    public Material material2;

    // �J�����G�t�F�N�g�m�F�p�̕ϐ�
    CameraControll cameraControll;

    // �I�u�W�F�N�g�������ԊǗ��p�̕ϐ�
    const float LIVE_TIME = 10.0f;
    float deltaTime = 0.0f;

    // ��������
    void Start()
    {
        // �H�쑀��p�R���|�[�l���g�̎擾
        this.rigid2D = GetComponent<Rigidbody2D>();

        // ���C���J�����̃I�u�W�F�N�g�̎擾
        this.mainCamera = GameObject.Find("Main Camera");

        // �X�e�[�W�Ǘ��R���|�[�l���g���擾
        this.gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();

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

        // �ړ������Ɖ摜�̌�����ݒ�
        Vector3 scale = transform.localScale;
        if (transform.position.x >= this.mainCamera.transform.position.x)
        {
            this.movedir = -1;
            scale.x = 1;
        }
        else
        {
            this.movedir = 1;
            scale.x = -1;
        }
        transform.localScale = scale;
    }

    // �X�V����
    void Update()
    {
        // �X�e�[�W��Ԃ��i�s���̏ꍇ�A�������{
        if (this.gameDirector.GetStageStatus() == 0)
        {
            // �ړ�����
            Move();
        }

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
    // �ړ�����
    private void Move()
    {
        // �ړ��x�N�g���p�̕ϐ�
        Vector3 moveForce = new Vector3(0.0f, 0.0f, 0.0f);

        // ���݂̈ړ����x���擾�i�w���Ƃx���j
        float ballSpeedX = Mathf.Abs(this.rigid2D.velocity.x);

        // �ړ����x�̐ݒ�i�w���j
        if (ballSpeedX < MAX_WALKSPEED)
        {
            moveForce = new Vector3(this.movedir * WALK_FORCE, moveForce.y, moveForce.z);
        }

        // �ړ��x�N�g���̓K�p
        this.rigid2D.AddForce(moveForce);
    }
}
