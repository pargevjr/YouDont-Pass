using DI.Interfaces;
using UnityEngine;
using Zenject;

namespace DI.Factory {
	public class PrefabFactory : IPrefabFactory {
		private readonly DiContainer _container;

		[Inject]
		public PrefabFactory(DiContainer container) {
			_container = container;
		}

		public GameObject Create(GameObject prefab) {
			return _container.InstantiatePrefab(prefab);
		}

		public GameObject Create(GameObject prefab, Transform container) {
			return _container.InstantiatePrefab(prefab, container);
		}

		public T Create<T>(T original) where T : Object {
			return _container.InstantiatePrefab(original).GetComponent<T>();
		}

		public T Create<T>(T original, Transform parent) where T : Object {
			return _container.InstantiatePrefab(original, parent).GetComponent<T>();
		}

		public T Create<T>(GameObject original, Transform parent) where T : Object {
			return _container.InstantiatePrefab(original, parent).GetComponent<T>();
		}
	}
}