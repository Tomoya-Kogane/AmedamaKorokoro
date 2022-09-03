using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// GameScene���Ǘ�����N���X
/// </summary>
public class GameDirector : MonoBehaviour
{
    // BGM�Ǘ��p�̕ϐ�
    AudioSource audioSource;
    // BGM�̃��[�v�ʒu
    const float AUDIO_LOOPTIME = 17.0f;
    // BGM�̃��[�v�J�n�ʒu
    const float AUDIO_STARTTIME = 1.4f;

    // �g�����W�V��������p�̕ϐ�
    Transition transition;

    // �v���C���[��Ԋm�F�p�̕ϐ�
    PlayerBall player;

    // �J�����G�t�F�N�g�ύX�p�̕ϐ�
    CameraControll mainCamera;

    // UI����p�̕ϐ�
    UIDirector uiDirector;

    // ������������I�u�W�F�N�g�p�̕ϐ�
    GhostGenerator ghost;
    BlinkEyeGenerator blinkEye;

    // �X�e�[�^�X�Ǘ��p�̕ϐ�
    // 0:�Q�[���i�s��
    // 1:�Q�[���N���A
    // 2:�Q�[���I�[�o�[
    private int _status;

    // �V�[���؂�ւ����̃X���[�v���ԁi�t���[�����w��j
    const float SCENE_CHANGETIME = 2.0f;

    // ��������
    void Start()
    {
        // ���t���b�V�����[�g��60�t���[���ɐݒ�
        Application.targetFrameRate = 60;

        // BGM����p�R���|�[�l���g���擾
        this.audioSource = GetComponent<AudioSource>();

        // �g�����W�V��������p�R���|�[�l���g���擾
        this.transition = GameObject.Find("TransitionImage").GetComponent<Transition>();
        this.transition.OnTransition.AddListener(Restart);
        this.transition.OnComplete.AddListener(() => this.player.ResetBallPause());

        // �v���C���[��Ԋm�F�p�̕ϐ�
        this.player = GameObject.Find("PlayerBall").GetComponent<PlayerBall>();
        // �_���[�W�C�x���g�Ƀ��X�i�[��ǉ�
        this.player.OnDamage.AddListener(Fade);
        // �N���A�C�x���g�Ƀ��X�i�[��ǉ�
        this.player.OnClear.AddListener(() => StartCoroutine(DelayChangeScene(1, SCENE_CHANGETIME)));

        // �J�����̃I�u�W�F�N�g���擾
        this.mainCamera = GameObject.Find("Main Camera").GetComponent<CameraControll>();

        // UI�Ǘ��̃N���X���擾
        this.uiDirector = GameObject.Find("UI Director").GetComponent<UIDirector>();

        // ������������I�u�W�F�N�g�p�̕ϐ�
        this.ghost = GameObject.Find("GhostGenerator").GetComponent<GhostGenerator>();
        this.blinkEye = GameObject.Find("BlinkEyeGenerator").GetComponent<BlinkEyeGenerator>();

        // �V�[���U��ւ����̔j���𖳌���
        DontDestroyOnLoad(gameObject);

        // �C�x���g�o�^�i�V�[���؂�ւ��j
        SceneManager.sceneLoaded += ChangeSceneDirector;
    }

    // �X�V����
    void Update()
    {
        // BGM�̍Đ����Ԃ����[�v�ʒu�ɒB���Ă邩���f
        if (this.audioSource.time > AUDIO_LOOPTIME)
        {
            this.audioSource.time = AUDIO_STARTTIME;
            this.audioSource.Play();
        }
    }

    // �V�[���؂�ւ������i�x������j
    private IEnumerator DelayChangeScene(int selectScene, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        switch (selectScene)
        {
            case 1:
                GameObject.Find("Main Camera").GetComponent<CameraControll>().PhotoScreen();
                SceneManager.LoadScene("ClearScene");
                break;
            case 2:
                SceneManager.LoadScene("GameOverScene");
                break;
            default:
                break;
        }
    }
    
    // �V�[���؂�ւ����̏���
    public void ChangeSceneDirector(Scene next, LoadSceneMode mode)
    {
        switch (next.name)
        {
            case "ClearScene":
                // ���̃V�[����BGM�̍Đ��ʒu�������n��
                GameObject.Find("ClearDirector").GetComponent<ClearDirector>().SetAudioTime(this.audioSource.time);
                break;
            case "GameOverScene":
                // ���̃V�[����BGM�̍Đ��ʒu�������n��
                GameObject.Find("GameOverDirector").GetComponent<GameOverDirector>().SetAudioTime(this.audioSource.time);
                break;
            default:
                break;
        }

        // �C�x���g�����i�V�[���؂�ւ��j
        SceneManager.sceneLoaded -= ChangeSceneDirector;

        // �I�u�W�F�N�g�̔j��
        Destroy(gameObject);
    }
    
    // �Ó]����
    private void Fade()
    {
        // �Ó]�����������s�̏ꍇ
        if (!this.transition.IsRunning)
        {
            // �v���C���[���C�t���c���Ă���ꍇ
            if (this.player.Life != 0)
            {
                // �t�F�[�h�A�E�g���C�������s
                this.transition.FadeOutIn(2.0f);
            }
            // �v���C���[���C�t���c���Ă��Ȃ��ꍇ
            else
            {
                // �t�F�[�h�A�E�g�����s
                this.transition.FadeOut(2.0f);
            }
        }
    }

    // ���X�^�[�g����
    private void Restart()
    {
        // �v���C���[�̂�X�^�[�g����
        this.player.Restart();

        // �v���C���[�̎c���C�t�ɉ����āA�������{
        switch (this.player.Life)
        {
            // �c�@�Q
            case 2:
                // ����ʃO���[�X�P�[����ݒ�
                this.mainCamera.Effect = 2;
                // UI��ύX�i�ڋ�UI�̕Жڕ��j
                this.uiDirector.ChangeUI(2);
                // �S�[�X�g�Əu������ڋʂ̎����������J�n
                this.ghost.CanSpawn = true;
                this.blinkEye.CanSpawn = true;
                break;
            // �c�@�P
            case 1:
                // �S��ʃO���[�X�P�[����ݒ�
                this.mainCamera.Effect = 3;
                // UI��ύX�i�ڋ�UI�̗��ڕ��j
                this.uiDirector.ChangeUI(3);
                break;
            // �Q�[���I�[�o�[
            case 0:
                // �Q�[���I�[�o�[�V�[���֑J�ځi�x������j
                StartCoroutine(DelayChangeScene(2, SCENE_CHANGETIME));
                break;
            default:
                break;
        }
    }

    // �v���p�e�B��`
    // �X�e�[�^�X
    public int Status
    {
        get { return _status; }
    }
}
