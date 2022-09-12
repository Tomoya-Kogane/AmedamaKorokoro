using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Amedamakorokoro.Utilities.SceneDataPack;

/// <summary>
/// �V�[���ؑւƃ|�[�Y���Ǘ�����N���X�i�V���O���g���j
/// </summary>
public class SceneMaster : MonoBehaviour
{
    // �C���X�^���X
    public static SceneMaster instance;

    // �V�[���ԂŎ󂯓n���f�[�^
    public static SceneDataPack SceneData;

    // �C�x���g��`
    public UnityEvent OnSceneChangeComplete;
    public UnityEvent OnSceneUnloadComplete;
    public UnityEvent OnScenePause;
    public UnityEvent OnSceneUnpouse;

    // ���s���t���O
    private bool _isRunning = false;

    // ���݂̃V�[����
    private SceneList _currentScene;
    // �ߋ��̃V�[����
    private SceneList _previousScene;

    // ��������
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        instance = this;
        _currentScene = (SceneList)0;
        _previousScene = (SceneList)0;
        SceneData = null;
    }

    // �V�[���ؑ֏���
    public void ChangeNextScene(SceneList nextScene, SceneDataPack data = null)
    {
        // ���s���A���͓����V�[�������w�肵���ꍇ�A�ؑ֏����͎��{���Ȃ�
        if (_isRunning || _currentScene == nextScene) return;

        // �󂯓n���f�[�^�����w��̏ꍇ�A�V�[�����̂ݐݒ�
        if (data == null)
        {
            SceneData = new DefaultSceneDataPack(nextScene, null);
        }
        else
        {
            SceneData = data;
        }

        // �V�[���ؑ֏����i�{�́j
        StartCoroutine(ChangeSceneCoroutine(nextScene));
    }

    // �V�[���ؑ֏����i�{�́j
    private IEnumerator ChangeSceneCoroutine(SceneList nextScene)
    {
        // �V�[���ؑ֎��s
        _isRunning = true;

        // �ؑ֐�V�[���̓ǂݍ���
        yield return SceneManager.LoadSceneAsync(nextScene.ToString(), LoadSceneMode.Single);

        // �ߋ��̃V�[������ݒ�
        _previousScene = _currentScene;

        // ���݂̃V�[������ݒ�
        _currentScene = nextScene;

        // �V�[���ؑ֊����̃C�x���g�𔭍s
        OnSceneChangeComplete.Invoke();

        // �V�[���ؑ֊���
        _isRunning = false;
    }

    // �V�[���ǉ�����
    public void AdditiveScene(SceneList addScene, SceneDataPack data = null)
    {
        // ���s���̏ꍇ�A�V�[���ǉ������͎��{���Ȃ�
        if (_isRunning) return;

        // �󂯓n���f�[�^�����w��̏ꍇ�A�V�[�����̂ݐݒ�
        if (data == null)
        {
            SceneData = new DefaultSceneDataPack(addScene, null);
        }
        else
        {
            SceneData = data;
        }

        // �V�[���ؑ֏����i�{�́j
        StartCoroutine(AdditiveSceneCoroutine(addScene));
    }

    // �V�[���ǉ������i�{�́j
    private IEnumerator AdditiveSceneCoroutine(SceneList nextScene)
    {
        // �V�[���ؑ֎��s
        _isRunning = true;

        // �ؑ֐�V�[���̓ǂݍ���
        yield return SceneManager.LoadSceneAsync(nextScene.ToString(), LoadSceneMode.Additive);

        // �ߋ��̃V�[������ݒ�
        _previousScene = _currentScene;

        // ���݂̃V�[������ݒ�
        _currentScene = nextScene;

        // �V�[���ؑ֊����̃C�x���g�𔭍s
        OnSceneChangeComplete.Invoke();

        // �V�[���ؑ֊���
        _isRunning = false;
    }

    // �V�[���A�����[�h����
    public void UnloadScene(SceneList unloadScene, bool isPause)
    {
        // ���s���̏ꍇ�A�V�[���A�����[�h�����͎��{���Ȃ�
        if (_isRunning) return;

        // �V�[���A�����[�h�����i�{�́j
        StartCoroutine(UnloadSceneCoroutine(unloadScene, isPause));
    }

    // �V�[���A�����[�h�����i�{�́j
    private IEnumerator UnloadSceneCoroutine(SceneList unloadScene, bool isPause)
    {
        // �V�[���A�����[�h���s
        _isRunning = true;

        // �V�[���̃A�����[�h
        yield return SceneManager.UnloadSceneAsync(unloadScene.ToString());

        // �|�[�Y���̏ꍇ�A�|�[�Y�������������{����
        if (isPause)
        {
            Unpause();
        }

        // ���݂̃V�[������ݒ�
        _currentScene = _previousScene;

        // �V�[���A�����[�h�����̃C�x���g�𔭍s
        OnSceneUnloadComplete.Invoke();

        // �V�[���A�����[�h����
        _isRunning = false;
    }

    // �|�[�Y����
    public void Pause()
    {
        Time.timeScale = 0.0f;
        OnScenePause.Invoke();
    }

    // �|�[�Y��������
    public void Unpause()
    {
        OnSceneUnpouse.Invoke();
        Time.timeScale = 1.0f;
    }
}
