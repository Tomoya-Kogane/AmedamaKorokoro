using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameScene���Ǘ�����N���X
/// </summary>
public class GameDirector : MonoBehaviour
{
    // BGM�Ǘ��p�̕ϐ�
    AudioSource audioSource;
    // BGM�̃��[�v�ʒu
    float LoopAudioTime = 17.0f;

    // ��������
    void Start()
    {
        // ���t���b�V�����[�g��60�t���[���ɐݒ�
        Application.targetFrameRate = 60;
        // BGM����p�R���|�[�l���g���擾
        this.audioSource = GetComponent<AudioSource>();
    }

    // �X�V����
    void Update()
    {
        // BGM�̍Đ����Ԃ����[�v�ʒu�ɒB���Ă邩���f
        if (this.audioSource.time > this.LoopAudioTime)
        {
            this.audioSource.time = 1.4f;
            this.audioSource.Play();
        }

    }
}
