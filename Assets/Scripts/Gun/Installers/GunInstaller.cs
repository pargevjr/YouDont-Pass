using Gun.Factories;
using ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Gun.Installers {
    [CreateAssetMenu(menuName = "ScriptableObjects/Installers/GunInstaller",fileName = "GunInstaller")]
    public class GunInstaller : ScriptableObjectInstaller<GunInstaller> {
        [SerializeField] private GunRegistry _gunRegistry;
        public override void InstallBindings() {
            Container.BindInterfacesTo<GunManager>().AsSingle();
            Container.Bind<IGunFactory>().To<GunFactory>().AsSingle().WithArguments(_gunRegistry);
        }
    }
}
