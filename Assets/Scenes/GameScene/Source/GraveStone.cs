using UnityEngine;

/// <summary>
/// ��΂��Ǘ�����N���X
/// </summary>
public class GraveStone : MonoBehaviour
{
    // �X�v���C�g�����_���[����p�̕ϐ�
    SpriteRenderer spriteRenderer;
    Color color;

    // �V�F�[�_�p�̕ϐ�Listener

    public Material material1;
    public Material material2;

    // �����蔻�葀��p�̕ϐ�
    Collider2D mayCollider2D;

    // ���C���J�����̕ϐ�
    GameObject mainCamera;
    CameraControll cameraControll;

    // ��������
    void Start()
    {
        // �X�v���C�g�����_���[�̎擾
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        this.color = this.spriteRenderer.color;

        // �V�F�[�_�̐ݒ�i�f�t�H���g�V�F�[�_�j
        this.spriteRenderer.material = this.material1;

        // Collider�̎擾
        this.mayCollider2D = gameObject.GetComponent<Collider2D>();

        // ���C���J�����̎擾
        this.mainCamera = GameObject.Find("Main Camera");
        this.cameraControll = GameObject.Find("Main Camera").GetComponent<CameraControll>();
        this.cameraControll.OnChangeEffect.AddListener((value) => { SetShader((int)value); });

        // �V�F�[�_�ƃA���t�@�l�̐ݒ�i���߁j
        this.spriteRenderer.material = this.material1;
        this.color.a = 0.0f;
        this.spriteRenderer.color = this.color;
    }

    // �X�V����
    void Update()
    {
        // �J�����G�t�F�N�g�ɉ����ē����蔻���ݒ�
        switch (this.cameraControll.Effect)
        {
            // �J�����G�t�F�N�g�Ȃ��̏ꍇ�A�����蔻��𖳌���
            case 1:
                this.mayCollider2D.enabled = false;
                break;
            // �E��ʂ��O���C�X�P�[���̏ꍇ�A�`��ʒu�ɉ����ē����蔻���ݒ�
            case 2:
                // ��ʂ̉E���ɑ��݂���ꍇ�A�����蔻���L����
                if (transform.position.x >= this.mainCamera.transform.position.x)
                {
                    this.mayCollider2D.enabled = true;
                }
                // ��ʂ̍����ɑ��݂���ꍇ�A�����蔻��𖳌���
                else
                {
                    this.mayCollider2D.enabled = false;
                }
                break;
            // ��ʑS�̂��O���C�X�P�[���̏ꍇ�A�����蔻���L����
            case 3:
                this.mayCollider2D.enabled = true;
                break;
            default:
                break;
        }
    }

    private void SetShader(int value)
    {
        // �J�����G�t�F�N�g�ɉ����ăV�F�[�_��ݒ�
        switch (value)
        {
            // �����G�t�F�N�g�̏ꍇ�A�f�t�H���g�V�F�[�_�ƃA���t�@�l(ZERO)��ݒ�
            case 1:
                // �V�F�[�_�̐ݒ�i�f�t�H���g�V�F�[�_�j
                this.spriteRenderer.material = this.material1;
                // �A���t�@�l�̐ݒ�iZERO�j
                this.color.a = 0.0f;
                this.spriteRenderer.color = this.color;
                break;
            // ��ʉE�����O���[�X�P�[���̏ꍇ�A�A�N�Z�T���G�t�F�N�g�ƃA���t�@�l(1)��ݒ�
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
    }
}
