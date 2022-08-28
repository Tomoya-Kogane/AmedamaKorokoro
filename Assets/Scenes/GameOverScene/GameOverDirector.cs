using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �Q�[���I�[�o�[�V�[�����Ǘ�����N���X
/// </summary>
public class GameOverDirector : MonoBehaviour
{
    // BGM�Ǘ��p�̕ϐ�
    AudioSource audioSource;
    // BGM�̊J�n�ʒu�i�O�V�[������̈����p���j
    private static float _audioStartTime = 0.0f;
    // BGM�̃��[�v�ʒu
    const float AUDIO_LOOPTIME = 17.0f;

    // ��������
    void Start()
    {
        // ���t���b�V�����[�g��60�t���[���ɐݒ�
        Application.targetFrameRate = 60;
        // BGM����p�R���|�[�l���g���擾
        this.audioSource = GetComponent<AudioSource>();
        this.audioSource.time = _audioStartTime;
        this.audioSource.Play();
    }

    // �X�V����
    void Update()
    {
        // BGM�̍Đ����Ԃ����[�v�ʒu�ɒB���Ă邩���f
        if (this.audioSource.time > AUDIO_LOOPTIME)
        {
            this.audioSource.time = 1.4f;
            this.audioSource.Play();
        }

        // �X�y�[�X or ��ʃ^�b�v��GameScene�֖߂�
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            SceneManager.LoadScene("GameScene");
        }
    }
    // BGM�J�n�ʒu�̐ݒ�
    public void SetAudioTime(float time)
    {
        _audioStartTime = time;
    }
}