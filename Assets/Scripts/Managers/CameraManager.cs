using DI.Interfaces;
using UnityEngine;

namespace Managers {
	public class CameraManager: ICameraManager {
		public Camera Camera { get; }

		public CameraManager(Camera camera) {
			Camera = camera;
		}

	}
}