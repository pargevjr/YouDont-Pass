using UnityEngine;
using UnityEngine.Serialization;

namespace Gun.Views {
	public abstract class GunBaseView: MonoBehaviour {
		[SerializeField] private float _shootGroupInterval;
		[SerializeField] private float _shootInterval;
		[SerializeField] private int _shootingBulletsCount;
		[SerializeField] private float _bulletSpeed;
		[FormerlySerializedAs("_bullet")] [SerializeField] private Bullet.Views.BulletView _bulletView;
		[SerializeField] private Transform[] _bulletSpawnPoint;
	}
}