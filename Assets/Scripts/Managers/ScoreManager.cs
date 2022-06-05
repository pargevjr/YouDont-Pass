using System;
using DI.Interfaces;
using UniRx;
using UnityEngine;
using Utils;
using Zenject;

namespace Managers {
	public class ScoreManager : IScoreManager, IInitializable, IDisposable {
		private const string Key = "score";
		public int HighScore => PlayerPrefs.GetInt(Key);
		public ReactiveProperty<int> CurrentScore { get; } = new(0);

		private void SetScore() {
			PlayerPrefs.SetInt(Key, CurrentScore.Value > HighScore ? CurrentScore.Value : HighScore);
			PlayerPrefs.Save();
		}

		private void OnEnemyDown() {
			CurrentScore.Value++;
		}

		public void Initialize() {
			Events.OnGameOver += GameOver;
			Events.OnEnemyDown += OnEnemyDown;
			Events.OnStartGame += GameStarted;
		}

		private void GameStarted() {
			CurrentScore.Value = 0;
		}

		private void GameOver() {
			SetScore();
		}
		public void Dispose() {
			Events.OnGameOver -= GameOver;
			Events.OnEnemyDown -= OnEnemyDown;
			Events.OnStartGame -= GameStarted;
		}
	}
}