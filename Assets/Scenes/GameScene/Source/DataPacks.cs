/// <summary>
/// シーンを跨いで引き継ぐデータ
/// </summary>
namespace AmedamaKorokoro.Scripts.Utilities.DataPacks
{
    //
    public abstract class DataPacks
    {
        // 前シーン
        public abstract ScenesList PreviousScene { get; }
    }

    //
    public class DefaultSceneDataPack : DataPacks
    {
        private readonly ScenesList _prevScene;

        //
        public override ScenesList PreviousScene
        {
            get { return _prevScene; }
        }

        //
        public DefaultSceneDataPack(ScenesList prev)
        {
            _prevScene = prev;
        }
    }
}