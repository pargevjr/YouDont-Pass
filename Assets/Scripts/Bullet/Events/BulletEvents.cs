using System;
using Bullet.Views;
using UniRx;

namespace Bullet.Events {
	public class BulletEvents : IBulletEvents {
		private readonly Subject<BulletView> _onBulletLifeTimeOutStream = new();
		public IObservable<BulletView> OnBulletLifeTimeOut => _onBulletLifeTimeOutStream;
		public void BulletLifeTimeOut(BulletView bullet) {
			_onBulletLifeTimeOutStream?.OnNext(bullet);
		}
	}
}