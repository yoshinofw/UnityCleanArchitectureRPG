using System.Collections.Generic;
using UnityEngine;

namespace UCARPG.Domain.StatDomain.InterfaceAdapters
{
    public class StatPresenter : MonoBehaviour
    {
        private Dictionary<string, IStatUI> _statUIsById = new Dictionary<string, IStatUI>();
        [SerializeField]
        private Transform _canvas;
        [SerializeField]
        Camera _camera;

        public void OnStatCreated(string id, float maxValue, GameObject prefab, GameObject followTarget)
        {
            if (prefab == null)
            {
                return;
            }
            IStatUI statUI = Instantiate(prefab, _canvas).GetComponent<IStatUI>();
            statUI.MaxValue = maxValue;
            statUI.Value = maxValue;
            statUI.FollowTarget = followTarget.transform;
            statUI.Camera = _camera;
            _statUIsById.Add(id, statUI);
        }

        public void OnStatValueModified(string id, float value)
        {
            if (!_statUIsById.ContainsKey(id))
            {
                return;
            }
            _statUIsById[id].Value = value;
        }

        public void OnStatRemoved(string id)
        {
            if (!_statUIsById.ContainsKey(id))
            {
                return;
            }
            Destroy((_statUIsById[id] as MonoBehaviour).gameObject);
            _statUIsById.Remove(id);
        }
    }
}