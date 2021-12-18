using DefaultNamespace.GameEvents;
using DefaultNamespace.ScriptableEvents;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
	public class AsteroidSpawner : MonoBehaviour
	{
		[SerializeField] private GameEventVector3_2 _onAsteroidSplit;
		[SerializeField] private AsteroidSet _asteroidSet;
		[SerializeField] private Asteroid _asteroidPrefab;
		[SerializeField] private float _minSpawnTime;
		[SerializeField] private float _maxSpawnTime;
		[SerializeField] private int _minAmount;
		[SerializeField] private int _maxAmount;

		private float _timer;
		private float _nextSpawnTime;
		private Camera _camera;

		private enum SpawnLocation
		{
			Top,
			Bottom,
			Left,
			Right
		}

		private void OnEnable()
		{
			_onAsteroidSplit.Register(SpawnMinisInPosition);

		}
		private void OnDisable()
		{
			_onAsteroidSplit.Unregister(SpawnMinisInPosition);
		}

		private void Start()
		{
			_camera = Camera.main;
			Spawn();
			UpdateNextSpawnTime();
		}

		private void Update()
		{
			UpdateTimer();

			if (!ShouldSpawn())
				return;

			Spawn();
			UpdateNextSpawnTime();
			_timer = 0f;
		}

		private void UpdateNextSpawnTime()
		{
			_nextSpawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
		}

		private void UpdateTimer()
		{
			_timer += Time.deltaTime;
		}

		private bool ShouldSpawn()
		{
			return _timer >= _nextSpawnTime;
		}

		private void SpawnMinisInPosition(Vector3 position, Vector3 scale)
		{
			Vector3 randomPos = Random.onUnitSphere;
			randomPos.z = 0;
			Vector3 pos1 = position + randomPos;
			Vector3 pos2 = position - randomPos;
			var instance1 = Instantiate(_asteroidPrefab, pos1, Quaternion.identity);
			var instance2 = Instantiate(_asteroidPrefab, pos2, Quaternion.identity);
			instance1.Initialize().SetSize(scale / 2);
			instance2.Initialize().SetSize(scale / 2);
			_asteroidSet.Add(instance1.GetInstanceID(), instance1);
			_asteroidSet.Add(instance2.GetInstanceID(), instance2);
		}

		private void Spawn()
		{
			var amount = Random.Range(_minAmount, _maxAmount + 1);

			for (var i = 0; i < amount; i++)
			{
				var location = GetSpawnLocation();
				var position = GetStartPosition(location);
				var instance = Instantiate(_asteroidPrefab, position, Quaternion.identity);

				_asteroidSet.Add(instance.GetInstanceID(), instance);
			}
		}

		private static SpawnLocation GetSpawnLocation()
		{
			var roll = Random.Range(0, 4);

			return roll switch
			{
				1 => SpawnLocation.Bottom,
				2 => SpawnLocation.Left,
				3 => SpawnLocation.Right,
				_ => SpawnLocation.Top
			};
		}

		private Vector3 GetStartPosition(SpawnLocation spawnLocation)
		{
			var pos = new Vector3 { z = Mathf.Abs(_camera.transform.position.z) };

			const float padding = 5f;
			switch (spawnLocation)
			{
				case SpawnLocation.Top:
					pos.x = Random.Range(0f, Screen.width);
					pos.y = Screen.height + padding;
					break;
				case SpawnLocation.Bottom:
					pos.x = Random.Range(0f, Screen.width);
					pos.y = 0f - padding;
					break;
				case SpawnLocation.Left:
					pos.x = 0f - padding;
					pos.y = Random.Range(0f, Screen.height);
					break;
				case SpawnLocation.Right:
					pos.x = Screen.width - padding;
					pos.y = Random.Range(0f, Screen.height);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(spawnLocation), spawnLocation, null);
			}

			return _camera.ScreenToWorldPoint(pos);
		}
	}
}
