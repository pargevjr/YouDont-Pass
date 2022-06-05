using Enemy.Factory;
using Enemy.VIew;
using UnityEngine;
using Zenject;

namespace Enemy.Installer {
	[CreateAssetMenu(menuName = "ScriptableObjects/Installers/EnemyInstaller", fileName = "EnemyInstaller")]
	public class EnemyInstaller : ScriptableObjectInstaller<EnemyInstaller> {
		[SerializeField] private EnemyView _enemyView;
		public override void InstallBindings() {
			Container.BindInterfacesTo<EnemyManager>().AsSingle().NonLazy();
			Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle().WithArguments(_enemyView);
		}
	}
}