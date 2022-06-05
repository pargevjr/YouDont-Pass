using System;
using System.Collections;
using UniRx;
using UnityEngine;

namespace Utils {
	public static class Helpers {
		public static Vector3 SetScreenSizeToWorldPoint(Camera camera) {
			return camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,10));
		}

		public static IEnumerator WaitForSeconds(float secs,Action onCompleted) {
			yield return new WaitForSeconds(secs);
			onCompleted?.Invoke();
		}
	}
}