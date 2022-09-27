using UnityEngine;

namespace Amedamakorokoro.Utilities.SceneDataPack
{
    /// <summary>
    /// �V�[���ԂŃf�[�^�󂯓n�����e���`
    /// </summary>
    public abstract class SceneDataPack
    {
        // �󂯓n�����̃V�[����
        public abstract SceneList PreviousScene { get; }

        // �V�[���֑ؑO�̃Q�[�����
        public abstract Sprite GameDisplay { get; }
    }

    /// <summary>
    /// �f�t�H���g����
    /// </summary>
    public class DefaultSceneDataPack : SceneDataPack
    {
        private SceneList _prevScene;
        private Sprite _gameDisplay;

        public override SceneList PreviousScene
        {
            get { return _prevScene; }
        }
        public override Sprite GameDisplay
        {
            get { return _gameDisplay; }
        }

        public DefaultSceneDataPack(SceneList scene, Sprite sprite)
        {
            _prevScene = scene;
            _gameDisplay = sprite;
        }
    }
}