using System;
using Components;
using UniRx;
using UnityEngine;

namespace Enemy.VIew {
	public class EnemyView : MonoBehaviour {
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private ColliderEventInvoker _colliderEvent;
		private int _life;

		private CompositeDisposable _enemyLifeDisposable = new();
		private IDisposable _safeAreaDisposable;
		
		public void StartDetectSafeArea(Action<EnemyView> onEnemyHitSafeArea) {
			_enemyLifeDisposable?.Dispose();
			_safeAreaDisposable?.Dispose();    
			_safeAreaDisposable = _colliderEvent
				.OnTriggerEnterStream
				.Where(_ => _.CompareTag("SafeArea"))
				.Subscribe(_ => { onEnemyHitSafeArea?.Invoke(this);})
				.AddTo(this);
			
		}

		public void SetEnemyLife(int life, Action<EnemyView> onEnemyDown) {
			_enemyLifeDisposable = new();
			_life = life;
			_colliderEvent
				.OnTriggerEnterStream
				.Where(_ => _.CompareTag("Bullet") &&  _life > 0)
				.Subscribe(_ => { _life--;})
				.AddTo(_enemyLifeDisposable);
			_colliderEvent
				.OnTriggerEnterStream
				.Where(_ => _.CompareTag("Bullet") &&  _life <= 0)
				.Subscribe(_ => { onEnemyDown?.Invoke(this);  })
				.AddTo(_enemyLifeDisposable);
		}

		public void SetVelocity() {
			_rigidbody.velocity = Vector3.zero;
			_rigidbody.velocity = transform.forward * 3;
		}
	}
}