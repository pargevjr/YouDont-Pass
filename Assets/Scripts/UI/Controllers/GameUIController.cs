using System;
using DI.Interfaces;
using Gun;
using UI.Factory;
using UI.Views;
using UniRx;
using UnityEngine;
using Utils;
using Zenject;

namespace UI.Controllers {
	public class GameUIController : IInitializable, IDisposable {
		private readonly ISceneUIFactory _sceneUIFactory;
		private readonly IScoreManager _scoreManager;
		private readonly IGunManager _gunManager;
		private GameUIView _view;
		private IDisposable _currentScoreDisposable;

		public GameUIController(ISceneUIFactory sceneUIFactory, IScoreManager scoreManager, IGunManager gunManager) {
			_sceneUIFactory = sceneUIFactory;
			_scoreManager = scoreManager;
			_gunManager = gunManager;
		}

		public void Initialize() {
			Events.OnPlayerHealthChanged += ChangeHealth;
			Events.OnStartGame += GameStared;
			Events.OnGameOver += GameOver;
		}

		public void Dispose() {
			Events.OnPlayerHealthChanged -= ChangeHealth;
			Events.OnStartGame -= GameStared;
			Events.OnGameOver -= GameOver;
		}

		private void GameOver() {
			_view.Show(false);
		}

		private void GameStared() {
			if (_view == null)
				_view = _sceneUIFactory.CreateGameUIView();
			else {
				_view.Show(true);
			}

			_view.Init(ChangeWeapon);
			_gunManager.Health.Subscribe(_ => { _view.SetHealth(_); });

			_currentScoreDisposable ??= _scoreManager.CurrentScore.Subscribe(currentScore => {
				_view.SetCurrentScore(currentScore);
				switch (currentScore) {
					case > 20:
						_view.SetButtonInteractable(GunType.ShotGun);
						break;
					case > 5:
						_view.SetButtonInteractable(GunType.QueueGun);
						break;
					default:
						_view.SetButtonInteractable(GunType.ShootGun);
						break;
				}
			});
			ChangeWeapon(GunType.ShootGun);
		}

		private void ChangeWeapon(GunType type) {
			Events.SetGunType(type);
		}

		private void ChangeHealth(int health) {
		}
	}
}