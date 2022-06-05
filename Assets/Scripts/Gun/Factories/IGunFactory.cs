using Gun.Views;
using UnityEngine;
using Utils;

namespace Gun.Factories {
    public interface IGunFactory {
        public GunView CreateGun(GunType type, Transform container);
        public GunContainer CreateGunContainer();
    }
}
