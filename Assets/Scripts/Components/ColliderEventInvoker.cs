using System;
using UniRx;
using UnityEngine;

namespace Components {
	[RequireComponent(typeof(Collider))]
	public class ColliderEventInvoker : MonoBehaviour {
		private readonly Subject<Collider> _onTriggerEnter = new();
		public IObservable<Collider> OnTriggerEnterStream => _onTriggerEnter;

		private void OnTriggerEnter(Collider other) {
			_onTriggerEnter?.OnNext(other);
		}
	}
}