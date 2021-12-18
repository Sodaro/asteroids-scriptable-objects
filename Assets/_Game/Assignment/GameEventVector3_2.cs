using System;
using UnityEngine;
namespace DefaultNamespace.GameEvents
{
	[CreateAssetMenu(fileName = "new (Vector3, Vector3) event", menuName = "ScriptableObjects/Events/(Vector3,Vector3)Event", order = 0)]
	public class GameEventVector3_2 : ScriptableObject
	{
		private event Action<Vector3, Vector3> _event;

		public void Raise(Vector3 value1, Vector3 value2)
		{
			_event?.Invoke(value1, value2);
		}

		public void Register(Action<Vector3, Vector3> onEvent)
		{
			_event += onEvent;
		}

		public void Unregister(Action<Vector3, Vector3> onEvent)
		{
			_event -= onEvent;
		}
	}

}
