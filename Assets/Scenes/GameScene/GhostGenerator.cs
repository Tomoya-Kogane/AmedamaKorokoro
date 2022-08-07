using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostGenerator : MonoBehaviour
{
    // �����_�������Ώۂ̕ϐ�
    public GameObject ghostPrefab;

    // ���C���J�����̍��W�擾�p�̕ϐ�
    GameObject mainCamera;
    // �J�����G�t�F�N�g�m�F�p�̕ϐ�
    CameraControll cameraControll;

    // �X�|�[���Ǘ��p�̕ϐ�
    const float SPAWN_TIME = 3.0f;
    float deltaTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // �J�����ƃJ�����R���g���[���[�̎擾
        this.mainCamera = GameObject.Find("Main Camera");
        this.cameraControll = GameObject.Find("Main Camera").GetComponent<CameraControll>();
    }

    // Update is called once per frame
    void Update()
    {
        // �J�����G�t�F�N�g���O���C�X�P�[���̏ꍇ�A�H���ǉ�
        if (this.cameraControll.GetEffectStatus() == 2 || this.cameraControll.GetEffectStatus() == 3)
        {
            // 2�b���ɒǉ�����
            this.deltaTime += Time.deltaTime;
            if (this.deltaTime > SPAWN_TIME)
            {
                // �J�����͈͓̔��Ƀ����_���Œǉ�
                this.deltaTime = 0.0f;
                GameObject spawn = Instantiate(ghostPrefab);
                float posX = Random.Range(-1.0f, 1.0f);
                float posY = Random.Range(0.0f, 3.0f);
                spawn.transform.position = new Vector3(this.mainCamera.transform.position.x + (posX * 10.0f), this.mainCamera.transform.position.y + posY, 0.0f);
            }
        }
    }
}
