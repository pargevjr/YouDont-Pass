using Bullet.Views;

namespace Bullet.Events {
	public interface IBulletEvents : IBulletReadOnlyEvents {
		public void BulletLifeTimeOut(BulletView bullet);
	}
}