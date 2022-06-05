using System;
using System.Collections.Generic;
using Gun.Views;
using UnityEngine;
using Utils;

namespace ScriptableObjects {
	[CreateAssetMenu(fileName = "GunRegistry", menuName = "ScriptableObjects/GunsRegistry", order = 1)]
	public class GunRegistry : ScriptableObject {
		[SerializeField] private List<GunItem> _guns;
		[SerializeField] private GunContainer _gunContainer;
		
		public GunContainer GunContainer => _gunContainer;
		public GunView GetGunByType(GunType type) {
			return _guns.Find(_ => _.GunType == type).Gun;
		}
	}

	[Serializable]
	public struct GunItem {
		[SerializeField] private GunType _gunType;
		[SerializeField] private GunView _gun;
		
		public GunType GunType => _gunType;
		public GunView Gun => _gun;
	}
}