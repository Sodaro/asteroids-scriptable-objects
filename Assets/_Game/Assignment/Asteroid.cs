using DefaultNamespace.ScriptableEvents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Asteroid : MonoBehaviour
	{
		[SerializeField] private ScriptableEventInt _onAsteroidHit;

		[Header("Config:")]
		[SerializeField] private float _minForce;
		[SerializeField] private float _maxForce;
		[SerializeField] private float _minSize;
		[SerializeField] private float _maxSize;
		[SerializeField] private float _minTorque;
		[SerializeField] private float _maxTorque;

		[Header("References:")]
		[SerializeField] private Transform _shape;

		private Rigidbody2D _rigidbody;
		private Vector3 _direction;
		private int _instanceId;

		private bool _isInitialized;

		public Asteroid Initialize()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_instanceId = GetInstanceID();

			SetDirection();
			AddForce();
			AddTorque();
			_isInitialized = true;
			return this;
		}

		private void Start()
		{
			if (_isInitialized)
				return;

			Initialize().SetSizeRandom();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (string.Equals(other.tag, "Laser"))
			{
				HitByLaser();
				Destroy(other.gameObject);
			}
		}

		private void HitByLaser()
		{
			_onAsteroidHit.Raise(_instanceId);
		}

		private void SetDirection()
		{
			var size = new Vector2(3f, 3f);
			var target = new Vector3
			(
				Random.Range(-size.x, size.x),
				Random.Range(-size.y, size.y)
			);

			_direction = (target - transform.position).normalized;
		}

		private void AddForce()
		{
			var force = Random.Range(_minForce, _maxForce);
			_rigidbody.AddForce(_direction * force, ForceMode2D.Impulse);
		}

		private void AddTorque()
		{
			var torque = Random.Range(_minTorque, _maxTorque);
			var roll = Random.Range(0, 2);

			if (roll == 0)
				torque = -torque;

			_rigidbody.AddTorque(torque, ForceMode2D.Impulse);
		}

		private void SetSizeRandom()
		{
			var size = Random.Range(_minSize, _maxSize);
			_shape.localScale = new Vector3(size, size, 0f);
		}
		public void SetSize(Vector3 scale)
		{
			_shape.localScale = scale;
		}

		public Vector3 GetSize()
		{
			return _shape.localScale;
		}
	}
}
