using UnityEngine;

/// <summary>
/// ���n�G�t�F�N�g���Ǘ�����N���X
/// </summary>
public class TouchdownEffect : MonoBehaviour
{
    // �{�[���I�u�W�F�N�g�p�̕ϐ�
    GameObject ball;

    // ��������
    void Start()
    {
        // �{�[���̃I�u�W�F�N�g���擾
        this.ball = GameObject.Find("PlayerBall");

        // �{�[���̉��ɃG�t�F�N�g����������l�ɐݒ�
        transform.position = new Vector3(this.ball.transform.position.x, this.ball.transform.position.y - 0.45f, 0.0f);
    }
}
