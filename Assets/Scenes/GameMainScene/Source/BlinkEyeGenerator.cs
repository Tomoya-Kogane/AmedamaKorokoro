using UnityEngine;

/// <summary>
/// �܂΂�������ڋʂ������_����������N���X
/// </summary>
public class BlinkEyeGenerator : MonoBehaviour
{
    // �����_�������Ώۂ̕ϐ�
    public GameObject blinkEyePrefab;

    // ���C���J�����̃I�u�W�F�N�g�p�̕ϐ�
    GameObject mainCamera;

    // �X�|�[���Ǘ��p�̕ϐ�
    const float SPAWN_TIME = 6.5f;
    float deltaTime = 0.0f;
    bool canSpawn;

    // ��������
    void Start()
    {
        // �J�����ƃJ�����R���g���[���[�̎擾
        this.mainCamera = GameObject.Find("Main Camera");
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

    // �v���p�e�B��`
    // ���������t���O
    public bool CanSpawn
    {
        get { return this.canSpawn; }
        set { this.canSpawn = value; }
    }
}
