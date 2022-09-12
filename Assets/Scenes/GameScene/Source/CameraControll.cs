using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ���C���J�������Ǘ�����N���X
/// </summary>
public class CameraControll : MonoBehaviour
{
    // �J�����ړ��p�̕ϐ�
    GameObject player;
    // �J�����ړ������p�̕ϐ�
    Vector2 MinCameraPos;
    Vector2 MaxCameraPos;

    // �����_�[�e�N�X�`������p�̕ϐ�
    Camera subCamera;
    SubCameraControll subCameraControll;

    // �|�X�g�G�t�F�N�g�p�̕ϐ�
    public Material material1;
    public Material material2;
    public Material material3;

    // �G�t�F�N�g��ԊǗ��p�̕ϐ�
    // 1:�ʏ�
    // 2:�O���C�X�P�[���i�E��ʁj
    // 3:�O���C�X�P�[���i�S��ʁj
    int effect;

    // �C�x���g��`
    public class myEvent : UnityEvent<int> { }
    // �G�t�F�N�g�ύX
    public myEvent OnChangeEffect = new myEvent();

    // ��������
    void Start()
    {
        // �v���C���[�{�[���̃I�u�W�F�N�g���擾
        this.player = GameObject.Find("PlayerBall");

        // �J�����I�u�W�F�N�g���擾
        this.subCamera = GameObject.Find("Sub Camera").GetComponent<Camera>();
        this.subCameraControll = GameObject.Find("Sub Camera").GetComponent<SubCameraControll>();

        // �J�����̈ړ��͈͂�ݒ�
        this.MinCameraPos = new Vector2(0.0f, 5.0f) ;
        this.MaxCameraPos = new Vector2(30.0f, -5.0f);

        // �G�t�F�N�g��ԂɃm�[�}����ݒ�
        this.effect = 1;
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
        cameraPos.x = this.player.transform.position.x;

        // �ړ������̔���
        if (cameraPos.x < this.MinCameraPos.x) cameraPos.x = this.MinCameraPos.x;
        if (cameraPos.x > this.MaxCameraPos.x) cameraPos.x = this.MaxCameraPos.x;

        // �ړ��̓K�p
        transform.position = cameraPos;
        this.subCamera.transform.position = cameraPos;
    }

    // �|�X�g�G�t�F�N�g����
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        switch(this.effect)
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
    // ��ʂ̃e�N�X�`�����쐬
    public Sprite PhotoScreen()
    {
        // Texture2D���쐬
        Texture2D tex = new Texture2D(this.subCamera.targetTexture.width, this.subCamera.targetTexture.height);

        // �����_�[�e�N�X�`����L����
        RenderTexture.active = this.subCamera.targetTexture;

        // �J�����̃����_�����O�����{
        this.subCamera.Render();

        // Texture2D���쐬
        tex.ReadPixels(new Rect(0, 0, this.subCamera.targetTexture.width, this.subCamera.targetTexture.height), 0, 0);
        tex.Apply();

        // �����_�[�e�N�X�`���𖳌���
        RenderTexture.active = null;

        // ���݂̉�ʂ�Sprite�����ĕԂ�
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f, 108.0f);
    }

    // �v���p�e�B��`
    // �G�t�F�N�g
    public int Effect
    {
        get { return this.effect; }
        set
        { 
            // ���g�̃G�t�F�N�g��ύX
            this.effect = value;
            // �T�u�J�����̃G�t�F�N�g�����킹�ĕύX
            this.subCameraControll.Effect = value;
            // �G�t�F�N�g�ύX�̃C�x���g�𔭍s
            OnChangeEffect.Invoke(value);
        }
    }
}
