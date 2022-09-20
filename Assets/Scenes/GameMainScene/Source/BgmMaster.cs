using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �T�E���h���Ǘ�����N���X�i�V���O���g���j
/// </summary>
public class BgmMaster : MonoBehaviour
{
    // �C���X�^���X
    public static BgmMaster instance;

    // BGM�Ǘ��p�̕ϐ�
    AudioSource audioSource;
    // BGM�̃��[�v�ʒu
    const float GAME_BGM_LOOPTIME = 17.0f;
    // BGM�̃��[�v�J�n�ʒu
    const float GAME_BGM_RESTARTTIME = 1.4f;

    // ��������
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        instance = this;

        // BGM����p�R���|�[�l���g���擾
        this.audioSource = GetComponent<AudioSource>();
    }

    // �X�V����
    void Update()
    {
        // BGM�̍Đ����Ԃ����[�v�ʒu�ɒB���Ă邩���f
        if (this.audioSource.time > GAME_BGM_LOOPTIME)
        {
            this.audioSource.time = GAME_BGM_RESTARTTIME;
            this.audioSource.Play();
        }
    }
}
