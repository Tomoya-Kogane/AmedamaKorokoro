/// <summary>
/// �V�[�����ׂ��ň����p���f�[�^
/// </summary>
namespace AmedamaKorokoro.Scripts.Utilities.DataPacks
{
    //
    public abstract class DataPacks
    {
        // �O�V�[��
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