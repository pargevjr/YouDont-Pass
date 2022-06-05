using System;
using DI.Interfaces;
using UI.Factory;
using UI.Views;
using Utils;
using Zenject;

namespace UI.Controllers {
	public class GameOverUIController : IInitializable, IDisposable {
		private readonly ISceneUIFactory _sceneUIFactory;
		private readonly IScoreManager _scoreManager;

		private GameOverUIView _view;

		public GameOverUIController(ISceneUIFactory sceneUIFactory, IScoreManager scoreManager) {
			_sceneUIFactory = sceneUIFactory;
			_scoreManager = scoreManager;
		}

		public void Initialize() {
			Events.OnGameOver += GameOver;
		}

		public void Dispose() {
			Events.OnGameOver -= GameOver;
		}

		private void GameOver() {
			if (_view == null) {
				_view = _sceneUIFactory.CreateGameOverUIView();
			}
			else {
				_view.Show(true);
			}
			_view.Init(GoToHome, RestartGame, _scoreManager.CurrentScore.Value.ToString(),
				_scoreManager.HighScore.ToString());
		}

		private void RestartGame() {
			_view.Show(false);
			Events.StartGame();
		}

		private void GoToHome() {
			_view.Show(false);
			Events.GoHome();
		}
	}
}