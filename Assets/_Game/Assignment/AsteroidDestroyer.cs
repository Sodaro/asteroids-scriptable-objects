using DefaultNamespace.GameEvents;
using DefaultNamespace.ScriptableEvents;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	public class AsteroidDestroyer : MonoBehaviour
	{
		[SerializeField] private AsteroidSet _asteroidSet;
		[SerializeField] private ScriptableEventInt _onAsteroidHit;
		[SerializeField] private GameEventVector3_2 _onAsteroidSplit;

		private void OnEnable()
		{
			_onAsteroidHit.Register(OnAsteroidHitByLaser);
		}
		private void OnDisable()
		{
			_onAsteroidHit.Unregister(OnAsteroidHitByLaser);
		}

		public void OnAsteroidHitByLaser(int asteroidId)
		{
			Asteroid asteroid = _asteroidSet.Get(asteroidId);

			if (asteroid == null)
				return;

			_asteroidSet.Remove(asteroidId);

			Vector3 size = asteroid.GetSize();
			if (size.x >= 0.4f)
			{
				_onAsteroidSplit.Raise(asteroid.transform.position, size);
			}
			Destroy(asteroid.gameObject);
		}
	}
}
