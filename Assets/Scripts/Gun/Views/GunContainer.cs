using System;
using Components;
using UniRx;
using UnityEngine;

namespace Gun.Views {
	public class GunContainer : MonoBehaviour {
		[SerializeField] private Transform _weaponContainer;
		[SerializeField] private Transform _bulletSpawnPoint;
		[SerializeField] private ColliderEventInvoker _safeArea;
		[SerializeField] private int _health;
		private GunView _currentGun;

		public Transform WeaponContainer => _weaponContainer;
		private IDisposable _everyUpdateDisposable;
		private IDisposable _enemyEnteredSafeAreaDisposable;

		public void Init(out int health, Action<Collider> onEnemyEnteredSafeArea) {
			health = _health;
			_enemyEnteredSafeAreaDisposable = _safeArea.OnTriggerEnterStream
				.Where(_ => _.CompareTag("Enemy"))
				.Subscribe(onEnemyEnteredSafeArea);
			_everyUpdateDisposable = Observable
				.EveryUpdate()
				.Where(_ => Physics.Raycast(_bulletSpawnPoint.position, _bulletSpawnPoint.forward,
					10000, LayerMask.GetMask("Enemy"))
				)
				.Subscribe(_ => { _currentGun.Shoot(); });
		}

		private void OnDisable() {
			_everyUpdateDisposable?.Dispose();
			_enemyEnteredSafeAreaDisposable?.Dispose();
		}

		public void Show(bool value) {
			gameObject.SetActive(value);
		}

		public void SwitchGun(GunView gun) {
			if (_currentGun != null)
				_currentGun.Show(false);
			_currentGun = gun;
			_currentGun.Show(true);
		}

		public void RotateWeapon(Vector3 value) {
			WeaponContainer.forward = value - WeaponContainer.position;
		}
	}
}