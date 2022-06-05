using DI.Factory;
using DI.Interfaces;
using Managers;
using UnityEngine;
using Zenject;

namespace DI.Installers {
	public class GameSceneInstaller : MonoInstaller {
		[SerializeField] private Camera _camera;

		public override void InstallBindings() {
			Container.Bind<IPrefabFactory>().To<PrefabFactory>().AsSingle();
			Container.Bind<ICameraManager>().To<CameraManager>().AsSingle().WithArguments(_camera);
			Container.BindInterfacesTo<ScoreManager>().AsSingle();
		}
	}
}