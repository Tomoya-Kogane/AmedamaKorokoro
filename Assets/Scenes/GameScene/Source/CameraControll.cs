using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // �����_�[�e�N�X�`���p�̕ϐ��i�N���A�V�[���Ŏg�p�j
    //public RenderTexture renderTexture;
    public static Texture2D texture2D;
    // �����_�[�e�N�X�`������p�̕ϐ�
    Camera subCamera;

    // �|�X�g�G�t�F�N�g�p�̕ϐ�
    public Material material1;
    public Material material2;
    public Material material3;

    // �G�t�F�N�g��ԊǗ��p�̕ϐ�
    // 1:�ʏ�A2:ʰ̸�ڲ���فA3:��ٸ�ڲ����
    int effectStatus = 1;

    // ��������
    void Start()
    {
        // �v���C���[�{�[���̃I�u�W�F�N�g���擾
        this.ball = GameObject.Find("PlayerBall");

        // �J�����I�u�W�F�N�g���擾
        this.subCamera = GameObject.Find("Sub Camera").GetComponent<Camera>();

        // �J�����̈ړ��͈͂�ݒ�
        this.MinCameraPos = new Vector2(0.0f, 5.0f) ;
        this.MaxCameraPos = new Vector2(30.0f, -5.0f);

        // �G�t�F�N�g��ԂɃm�[�}����ݒ�
        this.effectStatus = 1;

        // Texture2D�̏�����
        texture2D = null;

        // �V�[���U��ւ����̔j���𖳌���
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("Sub Camera"));

        // �C�x���g�o�^�i�V�[���؂�ւ��j
        SceneManager.sceneLoaded += ChangeSceneCamera;
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
        this.subCamera.transform.position = cameraPos;
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
    public int GetEffectStatus()
    {
        return this.effectStatus;
    }

    // �G�t�F�N�g��Ԃ̐ݒ�
    public void SetEffectStatus(int status)
    {
        this.effectStatus = status;
    }

    // ��ʂ̃e�N�X�`�����쐬
    public void PhotoScreen()
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
        texture2D = tex;

        // �����_�[�e�N�X�`���𖳌���
        RenderTexture.active = null;

        // Debug 
        byte[] bytes = texture2D.EncodeToPNG();
        File.WriteAllBytes("../../sample.png",bytes);
    }

    // �ʃV�[���ւ̒l�n��
    public void ChangeSceneCamera(Scene next, LoadSceneMode mode)
    {
         // ���̃V�[����Sprite��Texture2D�������n��
        SpriteRenderer clearSprite = GameObject.Find("GameSceneImage").GetComponent<SpriteRenderer>();
        clearSprite.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.one * 0.5f, 108.0f);

         // �I�u�W�F�N�g�̔j��
        Destroy(gameObject);
        Destroy(GameObject.Find("Sub Camera"));

        // �C�x���g�����i�V�[���؂�ւ��j
        SceneManager.sceneLoaded -= ChangeSceneCamera;
    }
}
