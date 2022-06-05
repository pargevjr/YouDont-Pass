using UI.Controllers;
using UnityEngine;
using Zenject;

namespace UI.Installer {
	public class SceneUIControllersInstaller : Installer<SceneUIControllersInstaller> {
		public override void InstallBindings() {
			Container.BindInterfacesTo<StartUIController>().AsSingle().NonLazy();
			Container.BindInterfacesTo<GameUIController>().AsSingle().NonLazy();
			Container.BindInterfacesTo<GameOverUIController>().AsSingle().NonLazy();
		}
	}
}