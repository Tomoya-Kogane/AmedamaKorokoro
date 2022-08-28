using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �܂΂�������ڋʂ������_����������N���X
/// </summary>
public class BlinkEyeGenerator : MonoBehaviour
{
    // �����_�������Ώۂ̕ϐ�
    public GameObject blinkEyePrefab;

    // ���C���J�����̍��W�擾�p�̕ϐ�
    GameObject mainCamera;
    // �J�����G�t�F�N�g�m�F�p�̕ϐ�
    CameraControll cameraControll;

    // �X�|�[���Ǘ��p�̕ϐ�
    const float SPAWN_TIME = 6.0f;
    float deltaTime = 0.0f;

    // ��������
    void Start()
    {
        // �J�����ƃJ�����R���g���[���[�̎擾
        this.mainCamera = GameObject.Find("Main Camera");
        this.cameraControll = GameObject.Find("Main Camera").GetComponent<CameraControll>();
    }

    // �X�V����
    void Update()
    {
        // �J�����G�t�F�N�g���O���C�X�P�[���̏ꍇ�A�܂΂�������ڋʂ�ǉ�
        if (this.cameraControll.GetEffectStatus() == 2 || this.cameraControll.GetEffectStatus() == 3)
        {
            // �w��b�����ɒǉ�
            this.deltaTime += Time.deltaTime;
            if (this.deltaTime > SPAWN_TIME)
            {
                // �J�����͈͓̔��Ƀ����_���Œǉ�
                this.deltaTime = 0.0f;
                GameObject spawn = Instantiate(blinkEyePrefab);
                float posX = Random.Range(-7.0f, 7.0f);
                float posY = 4.2f;
                spawn.transform.position = new Vector3(this.mainCamera.transform.position.x + posX, this.mainCamera.transform.position.y + posY, 0.0f);
            }
        }
    }
}
