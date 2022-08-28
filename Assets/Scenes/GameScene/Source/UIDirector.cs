using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDirector : MonoBehaviour
{
    // �v���C���[���C�t�m�F�p�̕ϐ�
    private PlayerBall _player;

    // UI���[�h�Ǘ��p�̕ϐ�
    // 1�F���ʃ��[�h
    // 2�F�ڋʃ��[�h
    int mode;

    // UI�I�u�W�F�N�g����p�̕ϐ�
    // ����UI
    GameObject objCandyUI;

    // �ڋ�UI
    GameObject objEyeUI1;
    GameObject objEyeUI2;

    // ��������
    void Start()
    {
        // �v���C���[�̃R���|�[�l���g���擾
        _player = GameObject.Find("PlayerBall").GetComponent<PlayerBall>();
        // UI���[�h�̏�����
        this.mode = 1;

        // ����UI�̃I�u�W�F�N�g���擾
        this.objCandyUI = GameObject.Find("CandyLife");

        // �ڋ�UI�̃I�u�W�F�N�g���擾
        this.objEyeUI1 = GameObject.Find("EyeLife1");
        this.objEyeUI2 = GameObject.Find("EyeLife2");
        // �ڋ�UI�̖�����
        this.objEyeUI1.SetActive(false);
        this.objEyeUI2.SetActive(false);

    }

    // �X�V����
    void Update()
    {
        switch (this.mode)
        {
            // ����UI
            case 1:
                // ���ʂ̑ϋv�l�ɂ���āA�h��Ԃ��̈��ύX
                this.objCandyUI.GetComponent<Image>().fillAmount = _player.CandyLife * 0.2f;
                break;
            default:
                break;
        }
    }

    // UI���[�h�̐؂�ւ�
    public void ChangeUI(int mode)
    {
        Animator obj;
        this.mode = mode;

        switch(this.mode)
        {
            // ����UI
            case 1:
                // ����UI��L����
                this.objCandyUI.SetActive(true);

                // �ڋ�UI�𖳌���
                this.objEyeUI1.SetActive(false);
                this.objEyeUI2.SetActive(false);

                break;
            // �ڋ�UI1�i�Жڕ��j
            case 2:
                // �ڋ�UI��L����
                this.objEyeUI1.SetActive(true);
                this.objEyeUI2.SetActive(true);

                // �ڋ�UI1�̐ݒ�
                obj = this.objEyeUI1.GetComponent<Animator>();
                obj.SetFloat("AnimeSpeed", 0.0f);
                obj.Play("Base Layer.EyeUI", 0, 1.0f);

                // �ڋ�UI2�̐ݒ�
                obj = this.objEyeUI2.GetComponent<Animator>();
                obj.SetFloat("AnimeSpeed", 0.0f);
                obj.Play("Base Layer.EyeUI", 0, 0.0f);

                // ����UI�𖳌���
                this.objCandyUI.SetActive(false);
                break;
            // �ڋ�UI2�i���ڕ��j
            case 3:
                // �ڋ�UI��L����
                this.objEyeUI1.SetActive(true);
                this.objEyeUI2.SetActive(true);

                // �ڋ�UI1�̐ݒ�
                obj = this.objEyeUI1.GetComponent<Animator>();
                obj.SetFloat("AnimeSpeed", 0.0f);
                obj.Play("Base Layer.EyeUI", 0, 1.0f);

                // �ڋ�UI2�̐ݒ�
                obj = this.objEyeUI2.GetComponent<Animator>();
                obj.SetFloat("AnimeSpeed", 0.0f);
                obj.Play("Base Layer.EyeUI", 0, 1.0f);

                // ����UI�𖳌���
                this.objCandyUI.SetActive(false);
                break;
            default:
                break;
        }
    }
}

