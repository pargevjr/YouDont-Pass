using UI.Factory;
using UI.Views;
using UnityEngine;
using Zenject;

namespace UI.Installer {
	[CreateAssetMenu(menuName = "ScriptableObjects/Installers/SceneUIInstaller",fileName = "SceneUIInstaller")]
	public class SceneUIInstaller : ScriptableObjectInstaller<SceneUIInstaller> {
		[SerializeField] private StartUIView _startUIView;
		[SerializeField] private GameUIView _gameUIView;
		[SerializeField] private GameOverUIView _gameOverUIView;

		public override void InstallBindings() {
			SceneUIControllersInstaller.Install(Container);
			Container.Bind<ISceneUIFactory>().To<SceneUIFactory>().AsSingle()
				.WithArguments(_startUIView, _gameUIView, _gameOverUIView);
		}
	}
}