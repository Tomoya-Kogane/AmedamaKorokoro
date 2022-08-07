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
                // ������
                break;
            // �Q�[���N���A
            case 1:
                // �N���A�V�[���֑J�ځi�x������j
                StartCoroutine(DelayChangeScene(SCENE_CHANGETIME));
                break;
            // ��L�ȊO
            default:
                // ������
                break;
        }

    }

    // �V�[���؂�ւ������i�x������j
    private IEnumerator DelayChangeScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject.Find("Main Camera").GetComponent<CameraControll>().PhotoScreen();
        SceneManager.LoadScene("ClearScene");
    }
    

    // �V�[���؂�ւ����̏���
    public void ChangeSceneDirector(Scene next, LoadSceneMode mode)
    {
        // ���̃V�[����BGM�̍Đ��ʒu�������n��
        GameObject.Find("ClearDirector").GetComponent<ClearDirector>().SetAudioTime(this.audioSource.time);

        // �I�u�W�F�N�g�̔j��
        Destroy(gameObject);

        // �C�x���g�����i�V�[���؂�ւ��j
        SceneManager.sceneLoaded -= ChangeSceneDirector;
    }

    // �X�e�[�W�X�e�[�^�X�̐ݒ�
    public void SetStageStatus(int status)
    {
        this.stageStatus = status;
    }
    // �X�e�[�W�X�e�[�^�X�̎擾
    public int GetStageStatus()
    {
        return this.stageStatus;
    }
}
