using System;
using Bullet.Views;
using UniRx;

namespace Bullet.Events {
	public interface IBulletReadOnlyEvents {
		public IObservable<BulletView> OnBulletLifeTimeOut { get; }
	}
}