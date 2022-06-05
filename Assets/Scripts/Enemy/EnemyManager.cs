using System;
using System.Collections.Generic;
using DI.Interfaces;
using Enemy.Factory;
using Enemy.VIew;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;
using Utils;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Enemy {
	public class EnemyManager : IInitializable, IDisposable {
		private readonly ICameraManager _cameraManager;
		private readonly IEnemyFactory _enemyFactory;
		private ObjectPool<EnemyView> _enemyPool;
		private LinkedList<EnemyView> _enemies = new();
		private IDisposable _intervalDisposable;
		EnemyManager(IEnemyFactory enemyFactory, ICameraManager cameraManager) {
			_enemyFactory = enemyFactory;
			_cameraManager = cameraManager;
		}

		public void Initialize() {
			Events.OnStartGame += GameStarted;
			Events.OnGameOver += GameOver;
		}

		public void Dispose() {
			Events.OnStartGame -= GameStarted;
			Events.OnGameOver -= GameOver;
		}

		private void GameOver() {
			_intervalDisposable.Dispose();
			foreach (var enemy in _enemies) {
				if (enemy.gameObject.activeSelf) {
					_enemyPool.Release(enemy);
				}
			}
		}

		private void InitEnemyObjectPool() {
			if (_enemyPool != null) return;
			_enemyPool = new ObjectPool<EnemyView>(() => {
					var enemy = _enemyFactory.CreateEnemy();
					_enemies.AddFirst(enemy);
					return enemy;
				}, enemy => { enemy.gameObject.SetActive(true); },
				enemy => { enemy.gameObject.SetActive(false); },
				enemy => { Object.Destroy(enemy.gameObject); }, false, 10, 20);
		}

		private void GameStarted() {
			InitEnemyObjectPool();

			_intervalDisposable = Observable
				.Interval(TimeSpan.FromSeconds(2))
				.Subscribe(intervalCount => {
					var enemy = _enemyPool.Get();
					SetEnemyTransformByScreenSize(enemy);
					enemy.StartDetectSafeArea(EnemyHitSafeArea);
					enemy.SetEnemyLife(Random.Range(1, 5), EnemyDown);
					enemy.SetVelocity();
				});
		}

		private void EnemyDown(EnemyView enemy) {
			_enemyPool.Release(enemy);
			Events.EnemyDown();
		}

		private void EnemyHitSafeArea(EnemyView enemy) {
			_enemyPool.Release(enemy);
		}

		private void SetEnemyTransformByScreenSize(EnemyView view) {
			var screenSize = Helpers.SetScreenSizeToWorldPoint(_cameraManager.Camera);
			view.transform.position = new Vector3(Random.Range(-screenSize.x, screenSize.x), 0, screenSize.z);
			view.transform.LookAt(new Vector3(Random.Range(-screenSize.x, screenSize.x), 0, -screenSize.z));
		}
	}
}