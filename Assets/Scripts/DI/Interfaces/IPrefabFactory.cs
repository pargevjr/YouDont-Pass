using UnityEngine;

namespace DI.Interfaces {
	public interface IPrefabFactory {
		GameObject Create(GameObject prefab);
		GameObject Create(GameObject prefab, Transform container);
		T Create<T>(T original) where T : Object;
		T Create<T>(T original, Transform parent) where T : Object;
		T Create<T>(GameObject original, Transform parent) where T : Object;
	}
}