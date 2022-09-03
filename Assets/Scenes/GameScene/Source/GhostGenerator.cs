using UnityEngine;

/// <summary>
/// �S�[�X�g�𐶐�����N���X
/// </summary>
public class GhostGenerator : MonoBehaviour
{
    // �����_�������Ώۂ̕ϐ�
    public GameObject ghostPrefab;

    // ���C���J�����̃I�u�W�F�N�g�ƃN���X�p�̕ϐ�
    GameObject mainCamera;
    CameraControll cameraControll;

    // �X�|�[���Ǘ��p�̕ϐ�
    const float SPAWN_TIME = 6.0f;
    float deltaTime = 0.0f;
    bool canSpawn = false;

    // ��������
    void Start()
    {
        // �J�����ƃJ�����R���g���[���[�̎擾
        this.mainCamera = GameObject.Find("Main Camera");
        this.cameraControll = GameObject.Find("Main Camera").GetComponent<CameraControll>();

        // ���������t���O���I�t
        this.canSpawn = false;
    }

    // �X�V����
    void Update()
    {
        // ���������t���O���I�t�̏ꍇ�A�����I��
        if (!this.canSpawn)
        {
            return;
        }

        // ���������t���O���I���̏ꍇ�A�w��b�����ɒǉ�
        GameObject spawn;
        float posX;
        float posY;

        this.deltaTime += Time.deltaTime;
        if (this.deltaTime > SPAWN_TIME)
        {
            this.deltaTime = 0.0f;
            switch (this.cameraControll.Effect)
            {
                case 2:
                    // �J�����̉E���ɒǉ�
                    spawn = Instantiate(ghostPrefab);
                    posY = Random.Range(0.0f, 3.0f);
                    spawn.transform.position = new Vector3(this.mainCamera.transform.position.x + 15.0f, this.mainCamera.transform.position.y + posY, 0.0f);
                    break;
                case 3:
                    // �J�����̍��E�ɒǉ�
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

    // �v���p�e�B��`
    // ���������t���O
    public bool CanSpawn
    {
        get { return this.canSpawn; }
        set { this.canSpawn = value; }
    }
}
