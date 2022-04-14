namespace UCARPG.View.Item
{
    public class SelectedSceneItemChanged
    {
        public SceneItem SceneItem { get; private set; }

        public SelectedSceneItemChanged(SceneItem sceneItem)
        {
            SceneItem = sceneItem;
        }
    }
}