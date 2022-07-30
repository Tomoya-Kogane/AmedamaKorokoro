using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[����̃{�[�����Ǘ�����N���X
/// </summary>
public class PlayerBall : MonoBehaviour
{
    // �{�[������p�̕ϐ�
    Rigidbody2D rigid2D;
    // �{�[���̏����ʒu
    Vector3 startPos;
    // �W�����v��
    float jumpForce = 680.0f;
    // �W�����v��Ԃ̔���l
    bool flgJump = false;
    // �ړ����x
    float walkForce = 20.0f;
    // �ړ����x�̐����l
    float MaxWalkSpeed = 4.5f;
    // ��������̊�l
    float CheckFallOut = -4.5f;
    // ���C�t�Ǘ��p�̕ϐ�
    int life;
    // �X���̊�l
    float CheckThreshold = 0.2f;
    
    // �摜�ؑ֗p�̕ϐ�
    public SpriteRenderer spriteRenderer;
    // ���ߋʂ̉摜
    public Sprite sprite1;
    // �ڋʂ̉摜
    public Sprite sprite2;

    // �|�X�g�G�t�F�N�g�ύX�p�̕ϐ�
    GameObject mainCamera;

    // ��������
    void Start()
    {
        // �������Z�p�̃{�[���̃R���|�[�l���g���擾
        this.rigid2D = GetComponent<Rigidbody2D>();
        // �{�[���̏����ʒu���擾
        this.startPos = transform.position;
        // �J�����̃I�u�W�F�N�g���擾
        this.mainCamera = GameObject.Find("Main Camera");
        // �|�X�g�G�t�F�N�g�̏�����
        this.mainCamera.GetComponent<CameraControll>().setEffectStatus(1);
        // ���C�t�̏����l��ݒ�
        this.life = 2;
    }

    // �X�V����
    void Update()
    {
        // �ړ�����
        Move();

        // �ړ��̉e���m�F
        CheckMovement();

    }

    // �ړ�����
    private void Move()
    {
        // �ړ��x�N�g���p�̕ϐ�
        Vector3 moveForce = new Vector3(0.0f, 0.0f, 0.0f);

        // �����̐ݒ�i���L�[�j
        int walkDir = 0;
        if (Input.GetKey(KeyCode.RightArrow)) walkDir = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) walkDir = -1;

        // �����̐ݒ�i�X���j
        if (Input.acceleration.x > this.CheckThreshold) walkDir = 1;
        if (Input.acceleration.x < -this.CheckThreshold) walkDir = -1;

        // ���݂̈ړ����x���擾�i�w���Ƃx���j
        float ballSpeedX = Mathf.Abs(this.rigid2D.velocity.x);

        // �ړ����x�̐ݒ�i�w���j
        if (ballSpeedX < this.MaxWalkSpeed)
        {
            moveForce = new Vector3(walkDir * this.walkForce, moveForce.y, moveForce.z);
        }

        // �W�����v�̐ݒ�i�x���j
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !flgJump)
        {
            moveForce = new Vector3(moveForce.x, this.jumpForce, moveForce.z);
            flgJump = true;
        }

        // �ړ��x�N�g���̓K�p
        this.rigid2D.AddForce(moveForce);
    }

    // �ړ��̉e���m�F
    private void CheckMovement()
    {
        // ��������i��ʊO�j
        if (transform.position.y < CheckFallOut)
        {
            // ����������A�����ʒu�ɖ߂�
            transform.position = this.startPos;

            // ���C�t�ɉ����āA�|�X�g�G�t�F�N�g��ݒ�
            switch (this.life)
            {
                // ����ʃO���[�X�P�[���̃G�t�F�N�g��ݒ�
                case 2:
                    this.mainCamera.GetComponent<CameraControll>().setEffectStatus(2);
                    break;
                // �S��ʃO���[�X�P�[���̃G�t�F�N�g��ݒ�
                case 1:
                    this.mainCamera.GetComponent<CameraControll>().setEffectStatus(3);
                    break;
                // �Q�[���I�[�o�[
                case 0:
                    // ����������
                    break;
                default:
                    break;
            }

            // ���C�t���P���炷
            this.life--;
            // �ڋʃe�N�X�`����ݒ�
            this.spriteRenderer.sprite = this.sprite2;
        }
    }

    // �Փ˔���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �n�ʂƏՓ˂����ꍇ�A�W�����v�t���O��OFF
        if (collision.gameObject.tag == "Ground")
        {
            this.flgJump = false;
        }
    }
}
