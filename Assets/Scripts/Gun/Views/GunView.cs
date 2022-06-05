using System;
using System.Collections;
using System.Collections.Generic;
using Bullet.Manager;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Gun.Views {
	public class GunView : MonoBehaviour {
		[SerializeField] private float _shootGroupInterval;
		[SerializeField] private float _shootInterval;
		[SerializeField] private int _shootingBulletsCount;
		[SerializeField] private float _bulletSpeed;
		[SerializeField] private Transform[] _bulletSpawnPoint;


		[Inject] private IBulletManager _bulletManager;
		private IDisposable _everyUpdateDisposable;
		private IDisposable _shootingIntervalDisposable;
		private float _timer;
		private bool _canShoot = true;

		private void OnDisable() {
			_everyUpdateDisposable.Dispose();
			_shootingIntervalDisposable?.Dispose();
			_canShoot = true;
			_timer = 0;
		}

		private void OnEnable() {
			StartTimer();
		}

		public void Show(bool value) {
			gameObject.SetActive(value);
		}


		private void SpawnBullet() {
			foreach (var spawnPoint in _bulletSpawnPoint) {
				_bulletManager.ShootBullet(spawnPoint.position, spawnPoint.forward * _bulletSpeed, "Enemy", 4);
			}
		}

		

		private void StartTimer() {
			_timer = _shootGroupInterval;
			_everyUpdateDisposable = Observable.EveryUpdate().Subscribe(_ => {
				if (_timer > 0) {
					_timer -= Time.deltaTime;
					_canShoot = false;
				}
				else {
					_canShoot = true;
				}
			}).AddTo(this);
		}

		public IEnumerator Timer() {
			while (_timer > 0) {
				_timer -= Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
		}

		public void Shoot() {
			if (_canShoot) {
				_timer = _shootGroupInterval;
				_canShoot = false;
				_shootingIntervalDisposable = Observable.Interval(TimeSpan.FromSeconds(_shootInterval))
					.TakeWhile(_ => _ != _shootingBulletsCount)
					.Subscribe(
						_ => { SpawnBullet(); }).AddTo(this);
			}
		}
	}
}