using Bullet.Events;
using Bullet.Factory;
using Bullet.Manager;
using Bullet.Views;
using UnityEngine;
using Zenject;

namespace Bullet.Installers {
    [CreateAssetMenu(menuName = "ScriptableObjects/Installers/BulletInstaller",fileName = "BulletInstaller")]

    public class BulletInstaller : ScriptableObjectInstaller<BulletInstaller> {
        [SerializeField] private BulletView _bulletView;
        public override void InstallBindings() {
            Container.Bind<IBulletFactory>().To<BulletFactory>().AsSingle().WithArguments(_bulletView);
            Container.Bind<IBulletManager>().To<BulletManager>().AsSingle();
            //Container.Bind<IBulletEvents>().To<BulletEvents>().AsSingle();
        }
    }
}
