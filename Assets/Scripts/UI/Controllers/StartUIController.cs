using System;
using DI.Interfaces;
using UI.Factory;
using UI.Views;
using UnityEngine;
using Utils;
using Zenject;

namespace UI.Controllers {
	public class StartUIController : IInitializable,IDisposable {
		private readonly ISceneUIFactory _sceneUIFactory;
		private readonly IScoreManager _scoreManager;
		private StartUIView _view;

		public StartUIController(ISceneUIFactory sceneUIFactory, IScoreManager scoreManager) {
			_sceneUIFactory = sceneUIFactory;
			_scoreManager = scoreManager;
		}

		public void Initialize() {
			InitVIew();
			Events.OnGoHome += InitVIew;
		}

		public void Dispose() {
			Events.OnGoHome += InitVIew;
		}

		private void InitVIew() {
			if (_view == null) {
				_view = _sceneUIFactory.CreateStartUIView();
			}
			else {
				_view.Show(true);
			}

			_view.Init(StartGame, _scoreManager.HighScore.ToString());
		}

		private void StartGame() {
			Events.StartGame();
			_view.Show(false);
		}
	}
}