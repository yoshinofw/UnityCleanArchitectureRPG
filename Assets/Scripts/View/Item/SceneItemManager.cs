using System;
using System.Collections.Generic;
using UnityEngine;
using UCARPG.Utilities;

namespace UCARPG.View.Item
{
    public class SceneItemManager : MonoBehaviour
    {
        public SceneItem SelectedSceneItem { get; private set; }
        [SerializeField]
        private EventBusProvider _eventBusProvider;
        [SerializeField]
        private SceneItem _sceneItemPrefab;
        [SerializeField]
        private Transform _sceneItemParent;
        [SerializeField]
        private GameObject _starPrefab;
        private HashSet<SceneItem> _triggeredSceneItems = new HashSet<SceneItem>();
        [SerializeField]
        private InitialDeploySceneItem[] _initialDeploySceneItems;
        private ObjectPool<SceneItem> _sceneItemPool;

        public void Create(Vector3 position, string configId, int count)
        {
            if (configId == "Star")
            {
                Instantiate(_starPrefab, position, Quaternion.Euler(0, -45, 0));
                return;
            }
            SceneItem sceneItem = _sceneItemPool.GetObject();
            sceneItem.transform.position = position;
            sceneItem.TriggerEntered += Add;
            sceneItem.TriggerExited += Remove;
            sceneItem.ConfigId = configId;
            sceneItem.Count = count;
            sceneItem.gameObject.SetActive(true);
        }

        public void Add(SceneItem sceneItem)
        {
            _triggeredSceneItems.Add(sceneItem);
            if (SelectedSceneItem == null)
            {
                SelectedSceneItem = sceneItem;
                _eventBusProvider.Instance.Post(new SelectedSceneItemChanged(SelectedSceneItem));
            }
        }

        public void Remove(SceneItem sceneItem)
        {
            _triggeredSceneItems.Remove(sceneItem);
            if (sceneItem == SelectedSceneItem)
            {
                SelectedSceneItem = null;
                foreach (var triggeredSceneItem in _triggeredSceneItems)
                {
                    SelectedSceneItem = triggeredSceneItem;
                    break;
                }
                _eventBusProvider.Instance.Post(new SelectedSceneItemChanged(SelectedSceneItem));
            }
        }

        private void Awake()
        {
            _sceneItemPool = new ObjectPool<SceneItem>(_sceneItemPrefab, _sceneItemParent, 5);
            foreach (var initialDeploySceneItem in _initialDeploySceneItems)
            {
                Create(initialDeploySceneItem.Transform.position, initialDeploySceneItem.ConfigId, initialDeploySceneItem.Count);
            }
        }
    }

    [Serializable]
    public class InitialDeploySceneItem
    {
        public Transform Transform;
        public string ConfigId;
        public int Count;
    }
}