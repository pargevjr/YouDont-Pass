using UnityEngine;

namespace Bullet.Manager {
	public interface IBulletManager {
		public void ShootBullet(Vector3 spawnPosition, Vector3 velocity, string filterTag, float destroyTime);
	}
}