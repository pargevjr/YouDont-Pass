using System;
using System.Collections.Generic;
using DI.Interfaces;
using Gun.Factories;
using Gun.Views;
using UniRx;
using UnityEngine;
using Utils;
using Zenject;

namespace Gun {
	public class GunManager : IGunManager, IInitializable, IDisposable {
		private readonly ICameraManager _cameraManager;
		private readonly CompositeDisposable _compositeDisposable = new();
		private readonly Dictionary<GunType, GunView> _guns = new();
		private readonly IGunFactory _gunFactory;
		private GunContainer _gunContainer;
		private IDisposable _everyUpdate;
		private float _mouseDeltaX;
		public ReactiveProperty<int> Health { get; } = new ReactiveProperty<int>();
		private int _currentScore;

		public GunManager(
			ICameraManager cameraManager,
			IGunFactory gunFactory) {
			_cameraManager = cameraManager;
			_gunFactory = gunFactory;
		}

		public void Initialize() {
			Events.OnSetGunType += SwitchGun;
			Events.OnStartGame += GameStarted;
		}

		private void GameStarted() {
			if(_gunContainer == null)
				_gunContainer = _gunFactory.CreateGunContainer();
			else {
				_gunContainer.Show(true);
			}
			
			_gunContainer.Init(out var health, OnChangedPlayerHealth);
			Health.Value = health;
			RotatePlayer();
		}

		public void Dispose() {
			Events.OnSetGunType -= SwitchGun;
			Events.OnStartGame -= GameStarted;
			_everyUpdate?.Dispose();
		}

		private void RotatePlayer() {
			var screenSize = Helpers.SetScreenSizeToWorldPoint(_cameraManager.Camera);
			_gunContainer.RotateWeapon(Vector3.zero);
			_everyUpdate = Observable
				.EveryUpdate()
				.Where(_ => Input.GetMouseButton(0))
				.Subscribe(_ => { RotateWeapon(-screenSize.x, screenSize.x);})
				.AddTo(_compositeDisposable);
		}

		private void RotateWeapon(float min, float max) {
			_mouseDeltaX = Mathf.Clamp(_mouseDeltaX + Input.GetAxis("Mouse X"), min, max);
			var mouseDelta = Vector3.right * _mouseDeltaX;
			_gunContainer.RotateWeapon(mouseDelta);
		}

		private void OnChangedPlayerHealth(Collider enemy) {
			if (Health.Value > 1) {
				Health.Value--;
			}
			else {
				_everyUpdate?.Dispose();
				_gunContainer.Show(false);
				_mouseDeltaX = 0;
				Events.GameOver();
			}
		}

		private void SwitchGun(GunType type) {
			if (!_guns.ContainsKey(type)) {
				var gun = _gunFactory.CreateGun(type, _gunContainer.WeaponContainer);
				_guns.Add(type, gun);
			}

			_gunContainer.SwitchGun(_guns[type]);
		}

	}
}