using DI.Interfaces;
using Gun.Views;
using ScriptableObjects;
using UnityEngine;
using Utils;

namespace Gun.Factories {
	public class GunFactory : IGunFactory {
		private readonly IPrefabFactory _prefabFactory;
		private readonly GunRegistry _gunRegistry;

		public GunFactory(GunRegistry gunRegistry, IPrefabFactory prefabFactory) {
			_prefabFactory = prefabFactory;
			_gunRegistry = gunRegistry;
		}
	
		public GunView CreateGun(GunType type, Transform container) {
			var gun = _prefabFactory.Create(_gunRegistry.GetGunByType(type), container);
			var gunTransform = gun.transform;
			gunTransform.localPosition = Vector3.zero;
			gunTransform.localRotation = Quaternion.identity;
			return gun;
		}

		public GunContainer CreateGunContainer() {
			var gunContainer = _prefabFactory.Create(_gunRegistry.GunContainer);
			var transform = gunContainer.transform;
			transform.position = Vector3.zero;
			transform.rotation = Quaternion.identity;
			return gunContainer;
		}
	}
}