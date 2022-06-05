using Bullet.Views;
using DI.Interfaces;

namespace Bullet.Factory {
	public class BulletFactory : IBulletFactory {
		private readonly BulletView _bulletView;
		private readonly IPrefabFactory _prefabFactory;

		public BulletFactory(BulletView bulletView, IPrefabFactory prefabFactory) {
			_bulletView = bulletView;
			_prefabFactory = prefabFactory;
		}

		public BulletView CreateBullet() {
			var bullet = _prefabFactory.Create(_bulletView);
			return bullet;
		}
	}
}