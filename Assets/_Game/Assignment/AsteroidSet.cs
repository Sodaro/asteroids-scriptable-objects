using UnityEngine;
using System.Collections.Generic;

namespace Asteroids
{
	[CreateAssetMenu]
	public class AsteroidSet : ScriptableObject
	{
		private Dictionary<int, Asteroid> _asteroids = new Dictionary<int, Asteroid>();

		private void Awake()
		{
			Clear();
		}

		public void Add(int id, Asteroid asteroid)
		{
			if (_asteroids.ContainsKey(id))
				return;

			_asteroids[id] = asteroid;
		}

		public void Remove(int id)
		{
			if (!_asteroids.ContainsKey(id))
				return;

			_asteroids.Remove(id);
		}

		public Asteroid Get(int id)
		{
			if (!_asteroids.ContainsKey(id))
				return null;

			return _asteroids[id];
		}

		private void Clear()
		{
			if (_asteroids == null)
				_asteroids = new Dictionary<int, Asteroid>();
			else
				_asteroids.Clear();
		}
	}
}
