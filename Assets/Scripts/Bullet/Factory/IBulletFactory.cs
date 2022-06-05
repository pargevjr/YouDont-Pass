using Bullet.Views;
using UnityEngine;

namespace Bullet.Factory {
	public interface IBulletFactory {
		public BulletView CreateBullet();
	}
}