using UnityEngine;

/// <summary>
/// �W�����v�G�t�F�N�g���Ǘ�����N���X
/// </summary>

public class JumpEffect : MonoBehaviour
{
    // �{�[���I�u�W�F�N�g�p�̕ϐ�
    GameObject ball;

    // ��������
    void Start()
    {
        // �{�[���̃I�u�W�F�N�g���擾
        this.ball = GameObject.Find("PlayerBall");
    }

    // �X�V����
    void Update()
    {
        // �ړ��t�F�N�g���{�[�������甭������l�Ɉʒu�X�V
        transform.position = new Vector3(this.ball.transform.position.x, this.ball.transform.position.y - 0.45f, 0.0f);
    }
}
