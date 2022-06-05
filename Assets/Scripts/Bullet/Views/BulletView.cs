using System;
using Bullet.Events;
using Components;
using UniRx;
using UnityEngine;

namespace Bullet.Views {
	public class BulletView : MonoBehaviour {
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private ColliderEventInvoker _bulletCollider;
		
		private Action<BulletView> _onBulletTriggerEnter;
		private Action<BulletView> _onBulletLifeTimedOut;

		private IDisposable _deactivationTimerDisposable;
		private IDisposable _triggerDetectionDisposable;
		private string _tag;
		private float _deactivationTime;
		private bool _isInitialized;
		public void Init(string filterTag,float deactivationTime,Action<BulletView> onBulletTriggerEnter, Action<BulletView> onBulletLifeTimedOut) {
			if(_isInitialized) return;
				_tag = filterTag;
			_deactivationTime = deactivationTime;
			_onBulletTriggerEnter = onBulletTriggerEnter;
			_onBulletLifeTimedOut = onBulletLifeTimedOut;
			_isInitialized = true;
		}

		private void OnEnable() {
			StartTriggerDetection();
			SetDeactivationTime();
		}

		private void OnDisable() {
			_triggerDetectionDisposable?.Dispose();
			_deactivationTimerDisposable?.Dispose();
		}

		private void StartTriggerDetection() {
			_triggerDetectionDisposable = _bulletCollider
				.OnTriggerEnterStream
				.Where(_ => _.CompareTag(_tag))
				.Subscribe(_ => {
					_onBulletTriggerEnter?.Invoke(this); 
					_deactivationTimerDisposable?.Dispose();
				})
				.AddTo(this);
		}


		private void SetDeactivationTime() {
			_deactivationTimerDisposable = Observable
				.Timer(TimeSpan.FromSeconds(_deactivationTime))
				.Subscribe(_ => _onBulletLifeTimedOut?.Invoke(this))
				.AddTo(this);
		}

		public void SetVelocity(Vector3 velocityValue) {
			_rigidbody.velocity = Vector3.zero;
			 _rigidbody.velocity = velocityValue;;
		}
	}
}