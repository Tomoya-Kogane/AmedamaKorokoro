using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchdownEffect : MonoBehaviour
{
    // �{�[���I�u�W�F�N�g�p�̕ϐ�
    GameObject ball;
    PlayerBall playerBall;

    // ��������
    void Start()
    {
        // �{�[���̃I�u�W�F�N�g���擾
        this.ball = GameObject.Find("PlayerBall");
        this.playerBall = this.ball.GetComponent<PlayerBall>();

        // �{�[���̉��ɃG�t�F�N�g����������l�ɐݒ�
        transform.position = new Vector3(this.ball.transform.position.x, this.ball.transform.position.y - 0.45f, 0.0f);
    }

    // �X�V����
    //void Update()
    //{
    //    // ������
    //}
}
