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
    const float SPAWN_TIME = 6.0f;
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
        GameObject spawn;
        float posX;
        float posY;

        this.deltaTime += Time.deltaTime;
        if (this.deltaTime > SPAWN_TIME)
        {
            this.deltaTime = 0.0f;
            switch (this.cameraControll.GetEffectStatus())
            {
                case 2:
                    // �J�����͈͓̔��Ƀ����_���Œǉ�
                    spawn = Instantiate(ghostPrefab);
                    posY = Random.Range(0.0f, 3.0f);
                    spawn.transform.position = new Vector3(this.mainCamera.transform.position.x + 15.0f, this.mainCamera.transform.position.y + posY, 0.0f);
                    break;
                case 3:
                    // �J�����͈͓̔��Ƀ����_���Œǉ�
                    spawn = Instantiate(ghostPrefab);
                    posX = (float)Random.Range(0, 2);
                    posY = Random.Range(0.0f, 3.0f);
                    spawn.transform.position = new Vector3(this.mainCamera.transform.position.x + (posX * 30.0f) - 15.0f, this.mainCamera.transform.position.y + posY, 0.0f);
                    break;
                default:
                    break;
            }
        }
    }
}
