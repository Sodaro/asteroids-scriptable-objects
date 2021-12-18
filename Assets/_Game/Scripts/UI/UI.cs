using System;
using DefaultNamespace.ScriptableEvents;
using TMPro;
using UnityEngine;
using Variables;

namespace UI
{
	public class UI : MonoBehaviour
	{
		[Header("Health:")]
		[SerializeField] private IntVariable _healthVar;
		[SerializeField] private TextMeshProUGUI _healthText;
		[SerializeField] private ScriptableEventIntReference _onHealthChangedEvent;

		[Header("Score:")]
		[SerializeField] private TextMeshProUGUI _scoreText;
		[SerializeField] private ScriptableEventInt _onAsteroidHit;

		[Header("Timer:")]
		[SerializeField] private TextMeshProUGUI _timerText;

		[Header("Laser:")]
		[SerializeField] private TextMeshProUGUI _laserText;

		private const string _scoreDescription = "Asteroids destroyed: ";
		private uint _score = 0;

		private void OnEnable()
		{
			_onAsteroidHit.Register(UpdateScore);
		}
		private void OnDisable()
		{
			_onAsteroidHit.Unregister(UpdateScore);
		}
		private void Start()
		{
			SetHealthText($"Health: {_healthVar.Value}");
		}

		public void OnHealthChanged(IntReference newValue)
		{
			SetHealthText($"Health: {newValue.GetValue()}");
		}

		private void SetHealthText(string text)
		{
			_healthText.text = text;
		}

		private void UpdateScore(int _)
		{
			_score++;
			SetScoreText(_scoreDescription + _score.ToString());

		}
		private void SetScoreText(string text)
		{
			_scoreText.text = text;
		}

		private void SetTimerText(string text)
		{
			_timerText.text = text;
		}

		private void SetLaserText(string text)
		{
			_laserText.text = text;
		}
	}
}
