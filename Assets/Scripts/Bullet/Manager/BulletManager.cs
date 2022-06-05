using Bullet.Events;
using Bullet.Factory;
using Bullet.Views;
using UnityEngine;
using UnityEngine.Pool;

namespace Bullet.Manager {
	public class BulletManager : IBulletManager {
		private readonly IBulletFactory _bulletFactory;

		private ObjectPool<BulletView> _bulletsPool;

		public BulletManager(IBulletFactory bulletFactory) {
			_bulletFactory = bulletFactory;

			_bulletsPool =
				new ObjectPool<BulletView>(() => {
						var bullet = _bulletFactory.CreateBullet();
						return bullet;
					}, bullet => { bullet.gameObject.SetActive(true);},
					bullet => { bullet.gameObject.SetActive(false);},
					bullet => { Object.Destroy(bullet.gameObject); }, false, 10, 20);
		}

		public void ShootBullet( Vector3 spawnPosition,Vector3 velocity, string filterTag, float destroyTime) {
			var bullet = _bulletsPool.Get();
			bullet.Init(filterTag,4,OnBulletEnteredTrigger,OnBulletLifeTimedOut);
			bullet.transform.position = spawnPosition;
			bullet.SetVelocity(velocity);
		}

		private void OnBulletEnteredTrigger(BulletView bullet) {
			_bulletsPool.Release(bullet);
		}

		private void OnBulletLifeTimedOut(BulletView bullet) {
			_bulletsPool.Release(bullet);
		}
	}
}