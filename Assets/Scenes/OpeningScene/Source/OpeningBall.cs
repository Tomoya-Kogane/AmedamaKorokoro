using UnityEngine;

/// <summary>
/// �I�[�v�j���O�̃{�[���̋������Ǘ�����N���X
/// </summary>
public class OpeningBall : MonoBehaviour
{
    // �{�[������p�̕ϐ�
    Rigidbody2D rigid2D;
    // �ړ�����W
    public Vector2 destinationPosition;
    // �ړ�����
    public enum MoveDir
    {
        Left = 1,
        Right = -1
    }
    public MoveDir moveDir;

    // �ړ����x
    const float WALK_FORCE = 20.0f;
    // �ړ����x�̐���
    const float MAX_WALKSPEED = 4.5f;

    // ��������
    void Start()
    {
        // �������Z�p�̃{�[���̃R���|�[�l���g���擾
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    // �X�V����
    void Update()
    {
        // �ړI�n�ɋ߂Â��܂ňړ�
        var distance = Vector3.Distance(transform.position, new Vector3(destinationPosition.x, destinationPosition.y, 0.0f));
        if (distance > 0.1f)
        {
            Move();
        }
        // �ړI�n�ɓ����������~
        else
        {
            this.rigid2D.velocity = Vector3.zero;
        }
    }

    // �ړ�����
    private void Move()
    {
        // �ړ��x�N�g���p�̕ϐ�
        Vector3 moveForce = new Vector3(0.0f, 0.0f, 0.0f);

        // ���݂̈ړ����x���擾�i�w���j
        float ballSpeedX = Mathf.Abs(this.rigid2D.velocity.x);
        if (ballSpeedX < MAX_WALKSPEED)
        {
            moveForce = new Vector3((float)moveDir * WALK_FORCE, moveForce.y, moveForce.z);
        }

        // �ړ��x�N�g���̓K�p
        this.rigid2D.AddForce(moveForce);
    }
}
