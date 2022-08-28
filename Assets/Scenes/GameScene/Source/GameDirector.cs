using System.Collections;
using System.Collections.Generic;
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
    private Transition _transition;

    // �v���C���[��Ԋm�F�p�̕ϐ�
    private PlayerBall _player;

    // �J�����G�t�F�N�g�ύX�p�̕ϐ�
    CameraControll mainCamera;

    // UI����p�̕ϐ�
    UIDirector uiDirector;

    // �X�e�[�W�X�e�[�^�X�Ǘ��p�̕ϐ�
    int stageStatus;

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
        _transition = GameObject.Find("TransitionImage").GetComponent<Transition>();
        _transition.OnTransition.AddListener(Restart);
        _transition.OnComplete.AddListener(() => _player.ResetBallPause()) ;

        // �J�����̃I�u�W�F�N�g���擾
        this.mainCamera = GameObject.Find("Main Camera").GetComponent<CameraControll>();
        this.mainCamera.SetEffectStatus(1);

        // UI�Ǘ��̃N���X���擾
        this.uiDirector = GameObject.Find("UI Director").GetComponent<UIDirector>();

        // �v���C���[��Ԋm�F�p�̕ϐ�
        _player = GameObject.Find("PlayerBall").GetComponent<PlayerBall>();

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

        // �X�e�[�W�X�e�[�^�X�ɉ�������������
        switch(this.stageStatus)
        {
            // �Q�[���i�s��
            case 0:
                // �v���C���[���_���[�W��Ԃ̏ꍇ
                if (_player.Status == 3 && _transition.IsRunning() == false)
                {
                    // �v���C���[���C�t���c���Ă���ꍇ�A
                    if (_player.Life != 0)
                    {
                        // �t�F�[�h�A�E�g���C�������s
                        _transition.FadeOutIn(2.0f);
                    } 
                    else
                    {
                        // �t�F�[�h�A�E�g�����s
                        _transition.FadeOut(2.0f);
                    }
                }
                break;
            // �Q�[���N���A
            case 1:
                // �N���A�V�[���֑J�ځi�x������j
                StartCoroutine(DelayChangeScene(1, SCENE_CHANGETIME));
                break;
            // ��L�ȊO
            default:
                // ������
                break;
        }

    }

    // �V�[���؂�ւ������i�x������j
    private IEnumerator DelayChangeScene(int selectScene, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject.Find("Main Camera").GetComponent<CameraControll>().PhotoScreen();
        switch (selectScene)
        {
            case 1:
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

    // �X�e�[�W�X�e�[�^�X�̐ݒ�
    public void SetStageStatus(int status) { this.stageStatus = status; }
    // �X�e�[�W�X�e�[�^�X�̎擾
    public int GetStageStatus() { return this.stageStatus; }

    // �Q�[���V�[���̃��X�^�[�g����
    private void Restart()
    {
        // �v���C���[�̂�X�^�[�g����
        _player.Restart();

        // �v���C���[�̎c�胉�C�t�����ăJ�����G�t�F�N�g��ݒ�
        switch (_player.Life)
        {
            // �c�@�Q
            case 2:
                // ����ʃO���[�X�P�[����ݒ�
                this.mainCamera.SetEffectStatus(2);
                // UI��ύX�i�ڋ�UI�̕Жڕ��j
                this.uiDirector.ChangeUI(2);
                break;
            // �c�@�P
            case 1:
                // �S��ʃO���[�X�P�[����ݒ�
                this.mainCamera.SetEffectStatus(3);
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
}
