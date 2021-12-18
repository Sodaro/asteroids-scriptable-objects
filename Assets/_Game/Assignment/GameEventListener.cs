using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.GameEvents
{
    public class GameEventListenerVector3_2 : MonoBehaviour
    {
        [SerializeField] private GameEventVector3_2 _gameEvent;
        [SerializeField] private UnityEvent<Vector3, Vector3> _response;

        private void OnEnable()
        {
            _gameEvent.Register(OnEventRaised);
        }

        private void OnDisable()
        {
            _gameEvent.Unregister(OnEventRaised);
        }

        private void OnEventRaised(Vector3 v1, Vector3 v2)
        {
            _response?.Invoke(v1,v2);
        }
    }
}
