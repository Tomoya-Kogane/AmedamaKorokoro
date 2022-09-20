using UnityEngine;

namespace Amedamakorokoro.Utilities.SceneDataPack
{
    /// <summary>
    /// シーン間でデータ受け渡す内容を定義
    /// </summary>
    public abstract class SceneDataPack
    {
        // 受け渡し元のシーン名
        public abstract SceneList PreviousScene { get; }

        // シーン切替前のゲーム画面
        public abstract Sprite GameDisplay { get; }
    }

    /// <summary>
    /// デフォルト実装
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