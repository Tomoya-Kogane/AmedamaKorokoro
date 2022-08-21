using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEffect : MonoBehaviour
{
    // �{�[���I�u�W�F�N�g�p�̕ϐ�
    GameObject ball;
    PlayerBall playerBall;

    // �p�[�e�B�N������p�̕ϐ�
    ParticleSystem.MainModule particleMain;


    // ��������
    void Start()
    {
        // �{�[���̃I�u�W�F�N�g���擾
        this.ball = GameObject.Find("PlayerBall");
        this.playerBall = this.ball.GetComponent<PlayerBall>();

        // �p�[�e�B�N���̃��C�����W���[�����擾
        this.particleMain = gameObject.GetComponent<ParticleSystem>().main;
    }

    // �X�V����
    void Update()
    {
        // �ړ��t�F�N�g���{�[�������甭������l�Ɉʒu�X�V
        transform.position = new Vector3(this.ball.transform.position.x, this.ball.transform.position.y - 0.45f, 0.0f);
    }
}
