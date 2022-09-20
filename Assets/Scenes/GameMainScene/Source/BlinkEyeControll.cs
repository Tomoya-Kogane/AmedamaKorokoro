using UnityEngine;

/// <summary>
/// �܂΂�������ڋʂ��Ǘ�����N���X
/// </summary>
public class BlinkEyeControll : MonoBehaviour
{
    // �X�v���C�g�����_���[����p�̕ϐ�
    SpriteRenderer spriteRenderer;

    // �V�F�[�_�p�̕ϐ�
    public Material material1;
    public Material material2;

    // �J�����G�t�F�N�g�m�F�p�̕ϐ�
    CameraControll cameraControll;

    // �I�u�W�F�N�g�������ԊǗ��p�̕ϐ�
    const float LIVE_TIME = 6.0f;
    float deltaTime = 0.0f;

    // �|�[�Y�t���O
    private bool _isPause = false;

    // ��������
    void Start()
    {
        // �X�v���C�g�����_���[�̎擾
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // �J�����R���g���[���[�R���|�[�l���g���擾
        this.cameraControll = GameObject.Find("Main Camera").GetComponent<CameraControll>();
        this.cameraControll.OnChangeEffect.AddListener((value) => { SetShader((int)value); });

        // �J�����̃G�t�F�N�g�ɉ������V�F�[�_��ݒ�
        switch (this.cameraControll.Effect)
        {
            case 2:
                this.spriteRenderer.material = this.material2;
                break;
            case 3:
                this.spriteRenderer.material = this.material1;
                break;
            default:
                this.spriteRenderer.material = this.material1;
                break;
        }

        // �|�[�Y�C�x���g�̓o�^
        SceneMaster.instance.OnScenePause.AddListener(Pause);
        SceneMaster.instance.OnSceneUnpouse.AddListener(Unpause);
    }

    // �X�V����
    void Update()
    {
        // �|�[�Y���̏ꍇ�A�X�V�������I��
        if (_isPause)
        {
            return;
        }

        // ��莞�Ԍo�߂�����A���g��j��
        this.deltaTime += Time.deltaTime;
        if (this.deltaTime >= LIVE_TIME)
        {
            Destroy(gameObject);
        }
    }

    // �V�F�[�_�ݒ�
    private void SetShader(int value)
    {
        // �J�����̃G�t�F�N�g�ɉ������V�F�[�_��ݒ�
        switch (value)
        {
            case 2:
                this.spriteRenderer.material = this.material2;
                break;
            case 3:
                this.spriteRenderer.material = this.material1;
                break;
            default:
                this.spriteRenderer.material = this.material1;
                break;
        }
    }

    // �|�[�Y����
    private void Pause()
    {
        _isPause = true;
    }

    // �|�[�Y��������
    private void Unpause()
    {
        _isPause = false;
    }
}
