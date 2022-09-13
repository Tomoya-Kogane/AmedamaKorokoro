using System.Collections;
using UnityEngine;
using Amedamakorokoro.Utilities.SceneDataPack;

/// <summary>
/// GameScene���Ǘ�����N���X
/// </summary>
public class GameDirector : MonoBehaviour
{
    // �g�����W�V��������p�̕ϐ�
    Transition transition;

    // �v���C���[��Ԋm�F�p�̕ϐ�
    PlayerBall player;

    // �J�����G�t�F�N�g�ύX�p�̕ϐ�
    CameraControll mainCamera;

    // UI����p�̕ϐ�
    UIDirector uiDirector;

    // ������������I�u�W�F�N�g�p�̕ϐ�
    GhostGenerator ghost;
    BlinkEyeGenerator blinkEye;

    // �X�e�[�^�X�Ǘ��p�̕ϐ�
    // 0:�Q�[���i�s��
    // 1:�Q�[���N���A
    // 2:�Q�[���I�[�o�[
    private int _status;

    // �|�[�Y�t���O
    private bool _isPause = false;

    // ��������
    void Start()
    {
        // ���t���b�V�����[�g��60�t���[���ɐݒ�
        Application.targetFrameRate = 60;

        // �g�����W�V��������p�R���|�[�l���g���擾
        this.transition = GameObject.Find("TransitionImage").GetComponent<Transition>();
        this.transition.OnTransition.AddListener(Restart);
        this.transition.OnComplete.AddListener(() => this.player.ResetBallPause());

        // �v���C���[��Ԋm�F�p�̕ϐ�
        this.player = GameObject.Find("PlayerBall").GetComponent<PlayerBall>();
        // �_���[�W�C�x���g�Ƀ��X�i�[��ǉ�
        this.player.OnDamage.AddListener(Fade);
        // �N���A�C�x���g�Ƀ��X�i�[��ǉ�
        this.player.OnClear.AddListener(GameClear);

        // �J�����̃I�u�W�F�N�g���擾
        this.mainCamera = GameObject.Find("Main Camera").GetComponent<CameraControll>();

        // UI�Ǘ��̃N���X���擾
        this.uiDirector = GameObject.Find("UI Director").GetComponent<UIDirector>();

        // ������������I�u�W�F�N�g�p�̕ϐ�
        this.ghost = GameObject.Find("GhostGenerator").GetComponent<GhostGenerator>();
        this.blinkEye = GameObject.Find("BlinkEyeGenerator").GetComponent<BlinkEyeGenerator>();

        // �|�[�Y�C�x���g�̓o�^
        SceneMaster.instance.OnScenePause.AddListener(Pause);
        SceneMaster.instance.OnSceneUnpouse.AddListener(Unpause);
    }

    // �X�V����
    private void Update()
    {
        // �|�[�Y���̏ꍇ�A�X�V�������I��
        if (_isPause)
        {
            return;
        }

        // �G�X�P�[�v�L�[����
        if (Input.GetKey(KeyCode.Escape))
        {
            // �|�[�Y���������{
            SceneMaster.instance.Pause();
            // ���j���[�V�[����ǉ�
            SceneMaster.instance.AdditiveScene(SceneList.MenuScene, null);
        }
    }

    // �Ó]����
    private void Fade()
    {
        // �Ó]�����������s�̏ꍇ
        if (!this.transition.IsRunning)
        {
            // �v���C���[���C�t���c���Ă���ꍇ
            if (this.player.Life != 0)
            {
                // �t�F�[�h�A�E�g���C�������s
                this.transition.FadeOutIn(2.0f);
            }
            // �v���C���[���C�t���c���Ă��Ȃ��ꍇ
            else
            {
                // �t�F�[�h�A�E�g�����s
                this.transition.FadeOut(2.0f);
            }
        }
    }

    // �X�e�[�W�N���A����
    private void GameClear()
    {
        StartCoroutine(GameClearCoroutine());
    }
    private IEnumerator GameClearCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        var sprite = GameObject.Find("Main Camera").GetComponent<CameraControll>().PhotoScreen();
        var data = new DefaultSceneDataPack(SceneList.ClearScene, sprite);
        SceneMaster.instance.ChangeNextScene(SceneList.ClearScene, data);
    }

    // �Q�[���I�[�o�[����
    private void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }
    private IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        var data = new DefaultSceneDataPack(SceneList.GameOverScene, null);
        SceneMaster.instance.ChangeNextScene(SceneList.GameOverScene, data);
    }


    // ���X�^�[�g����
    private void Restart()
    {
        // �v���C���[�̃��X�^�[�g����
        this.player.Restart();

        // �v���C���[�̎c���C�t�ɉ����āA�������{
        switch (this.player.Life)
        {
            // �c�@�Q
            case 2:
                // ����ʃO���[�X�P�[����ݒ�
                this.mainCamera.Effect = 2;
                // UI��ύX�i�ڋ�UI�̕Жڕ��j
                this.uiDirector.ChangeUI(2);
                // �S�[�X�g�Əu������ڋʂ̎����������J�n
                this.ghost.CanSpawn = true;
                this.blinkEye.CanSpawn = true;
                break;
            // �c�@�P
            case 1:
                // �S��ʃO���[�X�P�[����ݒ�
                this.mainCamera.Effect = 3;
                // UI��ύX�i�ڋ�UI�̗��ڕ��j
                this.uiDirector.ChangeUI(3);
                break;
            // �Q�[���I�[�o�[
            case 0:
                // �Q�[���I�[�o�[�V�[���֑J��
                GameOver();
                break;
            default:
                break;
        }
    }

    // �|�[�Y����
    private void Pause()
    {
        _isPause = true;
        // �S�[�X�g�Əu������ڋʂ̎����������~
        this.ghost.CanSpawn = false;
        this.blinkEye.CanSpawn = false;
    }

    // �|�[�Y��������
    private void Unpause()
    {
        _isPause = false;
        // �S�[�X�g�Əu������ڋʂ̎����������J�n
        this.ghost.CanSpawn = true;
        this.blinkEye.CanSpawn = true;
    }

    // �v���p�e�B��`
    // �X�e�[�^�X
    public int Status
    {
        get { return _status; }
    }
}
